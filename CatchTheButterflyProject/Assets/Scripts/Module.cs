using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Module : ScriptableObject
{
    public string ModuleName;
    public AudioClip ModuleVoiceoverAudio;
    public RiverSectionData RiverSectionData;
    public float StartDelayMeters = 5.0f;

    [Header("Pickups")]
    public GameObject[] Pickups;
    public float[] PickupSpawnTimes;

    [Header("Random Spawn Data")]
    public float MinXPos = -2.52f;
    public float MaxXPos = 1.18f;
    public float MinZDist = 3.0f;
    public float MaxZDist = 7.0f;
}
