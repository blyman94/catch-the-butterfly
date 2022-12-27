using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafPickup : MonoBehaviour
{
    [SerializeField] private Sprite _pickupSprite;
    [SerializeField] private SpriteVariable _imageToDisplay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _imageToDisplay.Value = _pickupSprite;
            Destroy(gameObject);
        }
    }
}
