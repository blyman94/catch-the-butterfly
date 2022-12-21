using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatInfinite : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _textureUnitSizeZ;

    private void Start()
    {
        Sprite sprite = _spriteRenderer.sprite;
        Texture2D texture = sprite.texture;
        _textureUnitSizeZ = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        if (_cameraTransform.position.z - transform.position.z >=
            _textureUnitSizeZ)
        {
            float offsetPositionZ =
                (_cameraTransform.position.z - transform.position.z) % _textureUnitSizeZ;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, _cameraTransform.position.z + offsetPositionZ);
        }
    }
}
