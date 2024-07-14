namespace DeltaLake.Kernel.Rust.Ffi;

public partial struct Stats
{
    [NativeTypeName("uint64_t")]
    public ulong num_records;
}
