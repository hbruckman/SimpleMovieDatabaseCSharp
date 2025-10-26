@echo off

echo 1
REM ========= CONFIG (edit these) =========
set "ABCS_PATH=..\AbcSolutionsCSharp\Abcs"
set "SMDB_PATH=."
set "ABCS_REMOTE_URL=https://github.com/hbruckman/AbcSolutionsCSharp.git"
set "ABCS_BRANCH=main"
set "SMDB_BRANCH=main"
set "REMOTE_NAME=abcs"
set "PREFIX=Abcs"
REM =======================================
echo 2
if "%~1"=="" (
  echo Usage: %~nx0 "Commit message"
  exit /b 1
)
echo 3
set "MESSAGE=%~1"
echo 4
pushd "%SMDB_PATH%" || (echo ERROR: Smdb path not found & exit /b 1)

REM === Check if the subtree remote already exists ===
echo 5
git remote get-url %REMOTE_NAME% >nul 2>&1
if errorlevel neq 0 (
  echo 7a
  echo === [1/1] First run: add Abcs as subtree \(squashed\) and hide locally ===
  git remote add %REMOTE_NAME% "%ABCS_REMOTE_URL%"
  git fetch %REMOTE_NAME%
  git checkout %SMDB_BRANCH%
	git add -A
	git commit -m "%MESSAGE%"
  git subtree add --prefix "%PREFIX%" %REMOTE_NAME% %ABCS_BRANCH% --squash || (echo ERROR: Subtree add failed & popd & exit /b 1)
  git push origin %SMDB_BRANCH% || (echo ERROR: Push Smdb failed & popd & exit /b 1)

  REM Hide the subtree folder from the working tree (won’t show in VS Code)
  git sparse-checkout init --cone
  git sparse-checkout set --no-cone "/*" "!/%PREFIX%/"
) else (
  echo 7b
  echo === [1/1] Sync (pull) Abcs into Smdb subtree \(squashed\) ===
  git fetch %REMOTE_NAME%
  git checkout %SMDB_BRANCH%
	git add -A
	git commit -m "%MESSAGE%"
  git subtree pull --prefix "%PREFIX%" %REMOTE_NAME% %ABCS_BRANCH% --squash || (echo ERROR: Subtree pull failed & popd & exit /b 1)
  git push origin %SMDB_BRANCH% || (echo ERROR: Push Smdb failed & popd & exit /b 1)

  REM Keep the subtree hidden locally
  git sparse-checkout set --no-cone "/*" "!/%PREFIX%/"
)
echo 8
popd
echo.
echo Done.
