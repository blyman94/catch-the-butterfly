using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RiverSectionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _riverSectionObject;
    
    public void SpawnRiverSection()
    {
        Instantiate(_riverSectionObject, new Vector3(0, 0, 24), Quaternion.identity);
    }
}
