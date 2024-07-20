#!/bin/bash -e

export GIT_ROOT=$(git rev-parse --show-toplevel)

echo
echo ">>> Building all rust projects:"
echo

cargo build --all-features

echo
echo ">>> Rust projects built successfully."
echo

pushd ${GIT_ROOT}/ffi

echo
echo ">>> Building Clang FFIs:"
echo

make all

echo
echo ">>> Clang FFIs built successfully."
echo
echo ">>> Building Windows and Linux Targets:"
echo

for target in x86_64-pc-windows-gnu x86_64-unknown-linux-gnu; do
  rustup target add $target
  cargo build --all-features --lib --target "$target"
done

echo
echo ">>> Windows: $(find "${GIT_ROOT}/target/x86_64-pc-windows-gnu/debug" -type f \( -name "*.dll" \) ! -path "${GIT_ROOT}/target/x86_64-pc-windows-gnu/debug/deps/*")"
echo ">>> Linux: $(find "${GIT_ROOT}/target/x86_64-unknown-linux-gnu/debug" -type f \( -name "*.so" \) ! -path "${GIT_ROOT}/target/x86_64-unknown-linux-gnu/debug/deps/*")"
echo
echo ">>> Windows and Linux targets built successfully."
echo

popd

echo
echo ">>> Post-Attach Commands Complete."
echo
