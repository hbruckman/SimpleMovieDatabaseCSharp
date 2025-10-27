@echo off
setlocal

set "ABCS_PATH=..\AbcSolutionsCSharp\Abcs"
set "SMDB_PATH=."
set "ABCS_REMOTE_URL=https://github.com/hbruckman/AbcSolutionsCSharp.git"
set "ABCS_BRANCH=main"
set "SMDB_BRANCH=main"
set "REMOTE_NAME=abcs"
set "PREFIX=Abcs"
set "SUBTREE_MSG=Sync %PREFIX% from %REMOTE_NAME%/%ABCS_BRANCH% [squashed]"

if "%~1"=="" (
  echo Usage: %~nx0 "Commit message for Abcs"
  exit /b 1
)

set "MESSAGE=%~1"
echo === Sync Abcs into Smdb (subtree, squashed) ===
pushd "%SMDB_PATH%" || (echo ERROR: Smdb path not found & exit /b 1)

git remote get-url %REMOTE_NAME% >nul 2>&1

if errorlevel 1 git remote add %REMOTE_NAME% "%ABCS_REMOTE_URL%"

git fetch %REMOTE_NAME% || (echo ERROR: fetch failed & popd & exit /b 1)
git checkout %SMDB_BRANCH% || (echo ERROR: checkout failed & popd & exit /b 1)
git add -A
git commit -m "%MESSAGE%"
git sparse-checkout init --cone >nul 2>&1
git sparse-checkout set --no-cone "/*" "/%PREFIX%/**" >nul 2>&1
git rev-parse --verify --quiet HEAD:%PREFIX% >nul 2>&1
if errorlevel 1 goto FIRST_RUN
goto SYNC

:FIRST_RUN
echo First run.
git subtree add --prefix "%PREFIX%" %REMOTE_NAME% %ABCS_BRANCH% --squash -m "%SUBTREE_MSG%" || (echo ERROR: subtree add failed & popd & exit /b 1)
goto END

:SYNC
echo Sync run.
git subtree pull --prefix "%PREFIX%" %REMOTE_NAME% %ABCS_BRANCH% --squash -m "%SUBTREE_MSG%" || (echo ERROR: subtree pull failed & popd & exit /b 1)
git push origin %SMDB_BRANCH% || (echo ERROR: push failed & popd & exit /b 1)
git sparse-checkout init --cone >nul 2>&1
git sparse-checkout set --no-cone "/*" "!/%PREFIX%/"
goto END

:END
popd
echo.
echo Done.
endlocal
