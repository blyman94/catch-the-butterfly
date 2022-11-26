using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatWaterSegment : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WaterEnd"))
        {
            Vector3 parentPos = other.transform.parent.position;
            other.transform.parent.position =
                new Vector3(parentPos.x, parentPos.y, 31.8f);
        }
    }
}
