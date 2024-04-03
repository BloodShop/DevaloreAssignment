#!/bin/bash

# Execute git log -1 and extract the commit hash
commit_hash=$(git log -1 | grep commit | awk '{ print $2 }')

# Check if the commit hash is empty
if [ -z "$commit_hash" ]; then
    echo "Failed to retrieve commit hash."
    exit 1
fi

# Print the commit hash
echo "Commit hash: $commit_hash"

# Checkout master branch
git checkout master

# Reset master branch to the commit hash
git reset --hard $commit_hash

# Force push changes to master branch
git push origin master --force

echo "Git commands executed successfully."