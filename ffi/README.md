# delta-kernel-rs ffi

This crate provides a c foreign function internface (ffi) for delta-kernel-rs.

```bash
git clean -ffdX
```

## Building
You can build static and shared-libraries, as well as the include headers by simply running:

```sh
cargo build [--release] [--features default-engine]
```

to build and run the C program which exercises FFI:

```sh
# First, build the kernel and all dependencies, including the FFI
#
cd /workspaces/delta-kernel-rs
cargo build --all-features

# Then, run the test FFI program
#
cd /workspaces/delta-kernel-rs/ffi
table=../kernel/tests/data/table-without-dv-small make run
```



This will place libraries in the root `target` dir (`../target/[debug,release]` from the directory containing this README), and headers in `../target/ffi-headers`. In that directory there will be a `delta_kernel_ffi.h` file, which is the C header, and a `delta_kernel_ffi.hpp` which is the C++ header.

### C/C++ Extension (VSCode)

By default the VSCode C/C++ Extension does not use any defines flags. You can open `settings.json` and set the following line:
```
    "C_Cpp.default.defines": [
        "DEFINE_DEFAULT_ENGINE",
        "DEFINE_SYNC_ENGINE"
    ]
```

### Building DLL

To build DLL (for windows) and .so (for linux):

```
cd /workspaces/delta-kernel-rs/ffi
rustup target add x86_64-pc-windows-gnu
sudo apt-get update
sudo apt-get install mingw-w64  -y

cargo build --all-features --lib --target x86_64-pc-windows-gnu
```

DLL is available here: `target/x86_64-pc-windows-gnu/debug/delta_kernel_ffi.dll`

### Debugging

To debug, we need `gdb`.

```
sudo apt-get update
sudo apt-get install gdb -y
```

#### ffi/cffi-test.c

Localize `.vscode/launch.json`, then hit a breakpoint.

#### ffi/examples/read-table/read_table.c

To debug the "read_table", the Azure Feature must be turned on.
Ensure to build the entire project with all features.

Localize `.vscode/launch.json`, with a bearer token and your ADLS endpoint, then hit a breakpoint.