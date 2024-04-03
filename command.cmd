@echo off
setlocal EnableDelayedExpansion

for /F "tokens=2" %%G in ('git log -1 ^| findstr /C:"commit"') do (
    set "commit_hash=%%G"
)

if not defined commit_hash (
    echo Failed to retrieve commit hash.
    exit /b 1
)

git checkout master
git reset --hard !commit_hash!
git push origin master --force

echo Git commands executed successfully.