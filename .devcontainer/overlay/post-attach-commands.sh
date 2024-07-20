#!/bin/bash -e

echo
echo ">>> Building all rust projects:"
echo
cargo build --all-features
echo
echo ">>> Rust projects built successfully."
echo

pushd ./ffi
echo
echo ">>> Building Clang FFIs:"
echo
make all
echo
echo ">>> Clang FFIs built successfully."
echo
popd

echo
echo ">>> Post-Attach Commands Complete."
echo
