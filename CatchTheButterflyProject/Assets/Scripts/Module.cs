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
    public GameObject[] FixedSectionSequence;
    public GameObject[] RandomSections;

    public GameObject GetRandomRiverSectionPrefab()
    {
        return RandomSections[Random.Range(0, RandomSections.Length)];
    }

    public GameObject GetFixedSection(int sectionIndex)
    {
        return FixedSectionSequence[sectionIndex];
    }
}
