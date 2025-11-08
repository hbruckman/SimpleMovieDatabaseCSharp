@echo off
setlocal EnableDelayedExpansion

set "A_PATH=..\AbcSolutionsCSharp"
set "A_BRANCH=main"
set "A_FOLDERS=Config;Http"
set "B_PATH=."

echo =====================================================
echo  Importing selected folders from A into B via subtree
echo =====================================================

rem === STEP 1: In A, (re)create split branches for each folder ===
pushd "%A_PATH%"
git checkout %A_BRANCH% >nul 2>&1

for %%F in (%A_FOLDERS%) do (
    echo [A] Creating subtree branch for src/%%F ...
    git branch -D src-%%F 2>nul
    git subtree split --prefix=src/%%F -b src-%%F >nul
)
popd

rem === STEP 2: In B, add subtree for each folder ===
pushd "%B_PATH%"
git switch -c vendoring/a-folders 2>nul || git switch vendoring/a-folders >nul 2>&1

git remote remove A 2>nul
git remote add A "%A_PATH%"
git fetch A >nul

for %%F in (%A_FOLDERS%) do (
    echo [B] Importing A/src/%%F into B/src/%%F ...
    git subtree add --prefix=src/%%F A src-%%F --squash -m "import A/src/%%F"
)

rem === STEP 3: Sparse-checkout hide A’s folders ===
echo [B] Configuring sparse-checkout to hide A's folders...
git sparse-checkout init --no-cone >nul 2>&1
> .git\info\sparse-checkout echo /*

for %%F in (%A_FOLDERS%) do (
    echo !/src/%%F/**>> .git\info\sparse-checkout
)
git read-tree -mu HEAD >nul

rem === STEP 4: Physically delete the hidden folders from disk (not from Git) ===
echo [B] Removing hidden folders from working tree...
for %%F in (%A_FOLDERS%) do (
    if exist "src\%%F" (
        echo     Deleting src\%%F ...
        rmdir /s /q "src\%%F"
    )
)

popd

echo.
echo =====================================================
echo Done.
echo Imported folders: %A_FOLDERS%
echo A's folders are hidden and deleted locally, but still tracked in Git.
echo To show them again: git sparse-checkout disable
echo =====================================================
