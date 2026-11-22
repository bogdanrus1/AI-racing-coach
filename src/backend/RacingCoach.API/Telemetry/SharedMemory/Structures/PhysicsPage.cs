using System.Runtime.InteropServices;

namespace RacingCoach.API.Telemetry.SharedMemory.Structures;

/// <summary>
/// Represents the native Assetto Corsa SPageFilePhysics structure.
/// Memory layout must match the simulator's Shared Memory definition.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct PhysicsPage
{
    // Packet information
    public int packetId;

    // Driver inputs and engine
    public float gas;
    public float brake;
    public float fuel;
    public int gear;
    public int rpms;

    // Vehicle movement
    public float steerAngle;
    public float speedKmh;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public float[] velocity;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public float[] accG;

    // Wheel information: FL, FR, RL, RR
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] wheelSlip;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] wheelLoad;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] wheelsPressure;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] wheelAngularSpeed;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] tyreWear;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] tyreDirtyLevel;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] tyreCoreTemperature;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] camberRAD;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] suspensionTravel;

    // Vehicle dynamics
    public float drs;
    public float tc;
    public float heading;
    public float pitch;
    public float roll;
    public float cgHeight;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public float[] carDamage;

    public int numberOfTyresOut;
    public int pitLimiterOn;

    // Driving aids and energy recovery
    public float abs;
    public float kersCharge;
    public float kersInput;
    public int autoShifterOn;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public float[] rideHeight;

    public float turboBoost;
    public float ballast;
    public float airDensity;
    public float airTemp;
    public float roadTemp;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public float[] localAngularVel;

    public float finalFF;
    public float performanceMeter;

    public int engineBrake;
    public int ersRecoveryLevel;
    public int ersPowerLevel;
    public int ersHeatCharging;
    public int ersIsCharging;

    public float kersCurrentKJ;

    public int drsAvailable;
    public int drsEnabled;

    // Brake and tire temperatures
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] brakeTemp;

    public float clutch;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] tyreTempI;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] tyreTempM;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] tyreTempO;

    public int isAIControlled;

    // Tire contact information: 3 axes x 4 wheels
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
    public float[] tyreContactPoint;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
    public float[] tyreContactNormal;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
    public float[] tyreContactHeading;

    // Additional vehicle information
    public float brakeBias;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public float[] localVelocity;
}
