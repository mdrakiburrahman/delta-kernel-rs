#!/bin/bash -e

export GIT_ROOT=$(git rev-parse --show-toplevel)

pushd ${GIT_ROOT}
echo
echo ">>> Building all rust projects:"
echo
cargo build --all-features
echo
echo ">>> Rust projects built successfully."
echo
popd

pushd ${GIT_ROOT}/ffi
echo
echo ">>> Building Clang FFI interface:"
echo
make all
echo
echo ">>> Clang FFIs built successfully."
echo
popd

echo
echo ">>> Post-Attach Commands Complete."
echo