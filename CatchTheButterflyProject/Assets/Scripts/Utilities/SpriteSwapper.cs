using UnityEngine;

public class SpriteSwapper : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _swappableSprites;

    public void SwapToSprite(int spriteIndex)
    {
        _spriteRenderer.sprite = _swappableSprites[spriteIndex];
    }
}
