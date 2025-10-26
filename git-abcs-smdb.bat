@echo off
setlocal

set "ABCS_PATH=..\AbcSolutionsCSharp\Abcs"
set "SMDB_PATH=."
set "ABCS_REMOTE_URL=https://github.com/hbruckman/AbcSolutionsCSharp.git"
set "ABCS_BRANCH=main"
set "SMDB_BRANCH=main"
set "REMOTE_NAME=abcs"
set "PREFIX=Abcs"

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
git rev-parse --verify --quiet HEAD:%PREFIX% >nul 2>&1

if errorlevel 1 (
  echo First time: subtree add "%PREFIX%" [squashed]...
  git subtree add --prefix "%PREFIX%" %REMOTE_NAME% %ABCS_BRANCH% --squash || (echo ERROR: subtree add failed & popd & exit /b 1)
) else (
  echo Updating: subtree pull into "%PREFIX%" [squashed]...
  git subtree pull --prefix "%PREFIX%" %REMOTE_NAME% %ABCS_BRANCH% --squash || (echo ERROR: subtree pull failed & popd & exit /b 1)
)

git push origin %SMDB_BRANCH% || (echo ERROR: push failed & popd & exit /b 1)

REM Keep subtree hidden locally (safe to run every time)
git sparse-checkout init --cone >nul 2>&1
git sparse-checkout set --no-cone "/*" "!/%PREFIX%/"

popd
echo.
echo Done.
endlocal
