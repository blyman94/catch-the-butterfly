using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// A module represents a chunk of the level that corresponds with a particular
/// voiceover segment.
/// </summary>
[CreateAssetMenu]
public class Module : ScriptableObject
{
    public string ModuleName;
    public AudioClip VoiceoverClip;
    public GameObject[] RiverSectionPrefabs;

    public GameObject GetRandomRiverSectionPrefab()
    {
        return RiverSectionPrefabs[Random.Range(0, RiverSectionPrefabs.Length)];
    }
}
