#!/bin/bash

# Set error flags
set -o nounset
set -o errexit
set -o pipefail

: ${LOCAL:=""}
: ${GITHUB_USER_NAME?}
: ${GITHUB_USER_EMAIL?}
: ${GITHUB_GPG_ID?}
: ${GITHUB_GPG_SEC_B64?}

# Load GPG keys
GITHUB_GPG_SEC=$(base64 -di - <<< "$GITHUB_GPG_SEC_B64")
gpg --import - <<< "$GITHUB_GPG_SEC"

# Git settings
git config --global user.name "$GITHUB_USER_NAME"
echo "Set git username to: '$GITHUB_USER_NAME'"
git config --global user.email "$GITHUB_USER_EMAIL"
echo "Set git user email to: '$GITHUB_USER_EMAIL'"
git config --global user.signingKey "$GITHUB_GPG_ID"
echo "Set git gpg key to: '$GITHUB_GPG_ID'"
git config --global commit.gpgSign true
echo "Set git commit signing to: true"
git config --global tag.forceSignAnnotated true
echo "Set git tag signing to: true"

mkdir -p ~/.gnupg
echo 'no-tty' >> ~/.gnupg/gpg.conf

# Setup if "circleci local execute"
if [ -n "${LOCAL:-}" ]; then
    mkdir -p ~/.ssh

    # Add ssh key
    eval `ssh-agent` # create the process
    GITHUB_SSH_SEC=$(base64 -di - <<< "${GITHUB_SSH_SEC_B64?}")
    echo "${GITHUB_SSH_SEC?}" > ~/.ssh/id_rsa
    chmod 600 ~/.ssh/id_rsa
    ssh-add ~/.ssh/id_rsa

    # Add github.com RSA fingerprints
    echo 'github.com ssh-rsa AAAAB3NzaC1yc2EAAAABIwAAAQEAq2A7hRGmdnm9tUDbO9IDSwBK6TbQa+PXYPCPy6rbTrTtw7PHkccKrpp0yVhp5HdEIcKr6pLlVDBfOLX9QUsyCOV0wzfjIJNlGEYsdlLJizHhbn2mUjvSAHQqZETYP81eFzLQNnPHt4EVVUh7VfDESU84KezmD5QlWpXLmvU31/yMf+Se8xhHTvKSCZIFImWwoG6mbUoWf9nzpIoaSjB+weqqUUmpaaasXVal72J+UX2B+2RPW3RcT0eOzQgqlJL3RKrTJvdsjE3JEAvGq3lGHSZXy28G3skua2SmVi/w4yCE6gbODqnTWlg7+wC604ydGXA8VJiS5ap43JXiUFFAaQ==' >> ~/.ssh/known_hosts
    echo "Added github RSA fingerprint"
fi
