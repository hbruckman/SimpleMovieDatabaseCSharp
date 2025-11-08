@echo off

if "%~1"=="" (
  echo Usage: %~nx0 "Commit message"
  exit /b 1
)

set "MESSAGE=%~1"

echo === [1/1] Commit and push Abcs ===
pushd "..\AbcSolutionsCSharp" || (echo ERROR: Abcs path not found & exit /b 1)
git add -A
git commit -m "%MESSAGE%" || echo (No changes to commit in Abcs)
git push origin main || (echo ERROR: Push Abcs failed & popd & exit /b 1)
popd
echo.
echo Done.
