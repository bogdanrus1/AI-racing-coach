
using System.Runtime.InteropServices;

namespace RacingCoach.API.Telemetry.SharedMemory.Structures;

/// <summary>
/// Represents Assetto Corsa's native SPageFileStatic structure.
/// </summary>
[StructLayout(
    LayoutKind.Sequential,
    Pack = 4,
    CharSet = CharSet.Unicode
)]
public struct StaticPage
{
    // Shared memory and simulator versions
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string SmVersion;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string AcVersion;

    // Session information
    public int NumberOfSessions;
    public int NumCars;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string CarModel;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string Track;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string PlayerName;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string PlayerSurname;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string PlayerNick;

    public int SectorCount;

    // Car information
    public float MaxTorque;
    public float MaxPower;
    public int MaxRpm;
    public float MaxFuel;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] SuspensionMaxTravel;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] TyreRadius;

    public float MaxTurboBoost;

    // Environmental information
    public float AirTemp;
    public float RoadTemp;

    // Driving aids
    public int PenaltiesEnabled;

    public float AidFuelRate;
    public float AidTireRate;
    public float AidMechanicalDamage;

    public int AidAllowTyreBlankets;

    public float AidStability;

    public int AidAutoClutch;
    public int AidAutoBlip;

    // Car systems
    public int HasDRS;
    public int HasERS;
    public int HasKERS;

    public float KersMaxJ;

    public int EngineBrakeSettingsCount;
    public int ErsPowerControllerCount;

    // Track information
    public float TrackSPlineLength;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string TrackConfiguration;

    public float ErsMaxJ;

    public int IsTimedRace;
    public int HasExtraLap;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string CarSkin;

    public int ReversedGridPositions;

    public int PitWindowStart;
    public int PitWindowEnd;
}
