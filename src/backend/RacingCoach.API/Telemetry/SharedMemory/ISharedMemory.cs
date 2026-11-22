using RacingCoach.API.Telemetry.SharedMemory.Structures;

namespace RacingCoach.API.Telemetry.SharedMemory;

public interface ISharedMemoryReader : IDisposable
{
    bool IsConnected { get; }

    bool TryReadPhysics(out PhysicsPage physics);

    bool TryReadGraphics(out GraphicsPage graphics);

    bool TryReadStatic(out StaticPage staticPage);
}
