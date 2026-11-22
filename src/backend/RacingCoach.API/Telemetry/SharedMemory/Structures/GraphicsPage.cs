using System.Runtime.InteropServices;

namespace RacingCoach.API.Telemetry.SharedMemory.Structures;

public enum AcStatus : int
{
    Off = 0,
    Replay = 1,
    Live = 2,
    Pause = 3
}

public enum AcSessionType : int
{
    Unknown = -1,
    Practice = 0,
    Qualify = 1,
    Race = 2,
    Hotlap = 3,
    TimeAttack = 4,
    Drift = 5,
    Drag = 6
}

public enum AcFlagType : int
{
    NoFlag = 0,
    BlueFlag = 1,
    YellowFlag = 2,
    BlackFlag = 3,
    WhiteFlag = 4,
    CheckeredFlag = 5,
    PenaltyFlag = 6
}

/// <summary>
/// Represents the native Assetto Corsa SPageFileGraphic structure.
/// </summary>
[StructLayout(
    LayoutKind.Sequential,
    Pack = 4,
    CharSet = CharSet.Unicode
)]
public struct GraphicsPage
{
    public int PacketId;

    public AcStatus Status;
    public AcSessionType Session;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string CurrentTime;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string LastTime;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string BestTime;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Split;

    public int CompletedLaps;
    public int Position;

    public int CurrentTimeMs;
    public int LastTimeMs;
    public int BestTimeMs;

    public float SessionTimeLeft;
    public float DistanceTraveled;

    public int IsInPit;
    public int CurrentSectorIndex;
    public int LastSectorTime;
    public int NumberOfLaps;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string TyreCompound;

    public float ReplayTimeMultiplier;
    public float NormalizedCarPosition;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public float[] CarCoordinates;

    public float PenaltyTime;
    public AcFlagType Flag;

    public int IdealLineOn;
    public int IsInPitLine;

    public float SurfaceGrip;
    public int MandatoryPitDone;

    public float WindSpeed;
    public float WindDirection;
}
