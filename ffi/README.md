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

To build and run the C program which exercises FFI:

```sh
cd /workspaces/delta-kernel-rs/ffi
table=../kernel/tests/data/table-without-dv-small make run
```

This will place libraries in the root `target` dir (`../target/[debug,release]` from the directory containing this README), and headers in `../target/ffi-headers`. In that directory there will be a `delta_kernel_ffi.h` file, which is the C header, and a `delta_kernel_ffi.hpp` which is the C++ header.

### C/C++ Extension (VSCode)

By default the VSCode C/C++ Extension does not use any defines flags. You can open `settings.json` and set the following line:

```json
    "C_Cpp.default.defines": [
        "DEFINE_DEFAULT_ENGINE",
        "DEFINE_SYNC_ENGINE"
    ]
```

### Debugging

To debug, we need `gdb`.

```
sudo apt-get update
sudo apt-get install gdb -y
```

#### ffi/cffi-test.c

Localize `.vscode/launch.json`, then hit a breakpoint.

#### ffi/examples/read-table/read_table.c

Ensure Arrow is installed (should be done already via `install-packages.sh`).

To debug the `read_table`, the Azure Feature must be turned on.
Ensure to build the entire project with `--all-features`.

Localize `.vscode/launch.json`, with a bearer token and your ADLS endpoint, then hit a breakpoint.