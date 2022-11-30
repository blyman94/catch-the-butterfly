using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A module represents a chunk of the level that corresponds with a particular
/// voiceover segment.
/// </summary>
[CreateAssetMenu]
public class Module : ScriptableObject
{
    public string Name;
    public AudioClip VoiceoverClip;
    public GameObject[] RiverSections;
}
