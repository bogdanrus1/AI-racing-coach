using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using RacingCoach.API.Telemetry.SharedMemory.Structures;

namespace RacingCoach.API.Telemetry.SharedMemory;

public sealed class AssettoCorsaMemoryReader : ISharedMemoryReader
{
    private const string PhysicsMapName = @"Local\acpmf_physics";
    private const string GraphicsMapName = @"Local\acpmf_graphics";
    private const string StaticMapName = @"Local\acpmf_static";

    private MemoryMappedFile? _physicsMemory;
    private MemoryMappedFile? _graphicsMemory;
    private MemoryMappedFile? _staticMemory;

    private MemoryMappedViewAccessor? _physicsAccessor;
    private MemoryMappedViewAccessor? _graphicsAccessor;
    private MemoryMappedViewAccessor? _staticAccessor;

    public bool IsConnected =>
        _physicsAccessor is not null &&
        _graphicsAccessor is not null &&
        _staticAccessor is not null;

    public AssettoCorsaMemoryReader()
    {
        TryConnect();
    }

    private bool TryConnect()
    {
        if (!OperatingSystem.IsWindows())
        {
            return false;
        }
        
        Disconnect();

        MemoryMappedFile? physicsMemory = null;
        MemoryMappedFile? graphicsMemory = null;
        MemoryMappedFile? staticMemory = null;

        MemoryMappedViewAccessor? physicsAccessor = null;
        MemoryMappedViewAccessor? graphicsAccessor = null;
        MemoryMappedViewAccessor? staticAccessor = null;

        try
        {
            physicsMemory = MemoryMappedFile.OpenExisting(
                PhysicsMapName,
                MemoryMappedFileRights.Read
            );

            graphicsMemory = MemoryMappedFile.OpenExisting(
                GraphicsMapName,
                MemoryMappedFileRights.Read
            );

            staticMemory = MemoryMappedFile.OpenExisting(
                StaticMapName,
                MemoryMappedFileRights.Read
            );

            physicsAccessor = physicsMemory.CreateViewAccessor(
                0,
                Marshal.SizeOf<PhysicsPage>(),
                MemoryMappedFileAccess.Read
            );

            graphicsAccessor = graphicsMemory.CreateViewAccessor(
                0,
                Marshal.SizeOf<GraphicsPage>(),
                MemoryMappedFileAccess.Read
            );

            staticAccessor = staticMemory.CreateViewAccessor(
                0,
                Marshal.SizeOf<StaticPage>(),
                MemoryMappedFileAccess.Read
            );

            // All mappings opened successfully.
            _physicsMemory = physicsMemory;
            _graphicsMemory = graphicsMemory;
            _staticMemory = staticMemory;

            _physicsAccessor = physicsAccessor;
            _graphicsAccessor = graphicsAccessor;
            _staticAccessor = staticAccessor;

            return true;
        }
        catch (FileNotFoundException)
        {
            // Assetto Corsa is probably not running.
        }
        catch (UnauthorizedAccessException)
        {
            // The process does not have permission to access the mappings.
        }
        catch (IOException)
        {
            // A mapping could not be opened or accessed.
        }

        // Clean up partially opened mappings.
        physicsAccessor?.Dispose();
        graphicsAccessor?.Dispose();
        staticAccessor?.Dispose();

        physicsMemory?.Dispose();
        graphicsMemory?.Dispose();
        staticMemory?.Dispose();

        return false;
    }

    private bool EnsureConnected()
    {
        return IsConnected || TryConnect();
    }

    public bool TryReadPhysics(out PhysicsPage physics)
    {
        physics = default;

        if (!EnsureConnected())
            return false;

        try
        {
            return TryReadStructure(_physicsAccessor!, out physics);
        }
        catch (IOException)
        {
            Disconnect();
            physics = default;
            return false;
        }
        catch (ObjectDisposedException)
        {
            Disconnect();
            physics = default;
            return false;
        }
    }

    public bool TryReadGraphics(out GraphicsPage graphics)
    {
        graphics = default;

        if (!EnsureConnected())
            return false;

        try
        {
            return TryReadStructure(_graphicsAccessor!, out graphics);
        }
        catch (IOException)
        {
            Disconnect();
            graphics = default;
            return false;
        }
        catch (ObjectDisposedException)
        {
            Disconnect();
            graphics = default;
            return false;
        }
    }

    public bool TryReadStatic(out StaticPage staticPage)
    {
        staticPage = default;

        if (!EnsureConnected())
            return false;

        try
        {
            return TryReadStructure(_staticAccessor!, out staticPage);
        }
        catch (IOException)
        {
            Disconnect();
            staticPage = default;
            return false;
        }
        catch (ObjectDisposedException)
        {
            Disconnect();
            staticPage = default;
            return false;
        }
    }

    private static bool TryReadStructure<T>(
        MemoryMappedViewAccessor accessor,
        out T result
    ) where T : struct
    {
        result = default;

        int size = Marshal.SizeOf<T>();

        byte[] buffer = new byte[size];

        int bytesRead = accessor.ReadArray(
            0,
            buffer,
            0,
            size
        );

        if (bytesRead != size)
            return false;

        GCHandle handle = GCHandle.Alloc(
            buffer,
            GCHandleType.Pinned
        );

        try
        {
            result = Marshal.PtrToStructure<T>(
                handle.AddrOfPinnedObject()
            );

            return true;
        }
        finally
        {
            handle.Free();
        }
    }

    private void Disconnect()
    {
        _physicsAccessor?.Dispose();
        _graphicsAccessor?.Dispose();
        _staticAccessor?.Dispose();

        _physicsMemory?.Dispose();
        _graphicsMemory?.Dispose();
        _staticMemory?.Dispose();

        _physicsAccessor = null;
        _graphicsAccessor = null;
        _staticAccessor = null;

        _physicsMemory = null;
        _graphicsMemory = null;
        _staticMemory = null;
    }

    public void Dispose()
    {
        Disconnect();

        GC.SuppressFinalize(this);
    }
}
