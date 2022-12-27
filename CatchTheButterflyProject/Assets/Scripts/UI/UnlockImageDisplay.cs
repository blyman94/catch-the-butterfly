using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockImageDisplay : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Image _displayImage;
    [SerializeField] private Animator _imageAnimator;

    [Header("Display Parameters")]
    [SerializeField] private float _imageDisplayTime;

    [Header("Data")]
    [SerializeField] private SpriteVariable _imageToDisplay;

    [Header("Random Positioning")]
    [SerializeField] private bool _useRandomPosition;
    [SerializeField] private float _minXPos = -200.0f;
    [SerializeField] private float _maxXPos = 200.0f;
    [SerializeField] private float _minYPos = -35;
    [SerializeField] private float _maxYPos = 0.0f;

    private float _fadeOutTimer;

    private int _fadeInHash;
    private int _fadeOutHash;

    #region MonoBehaviour Methods
    private void Awake()
    {
        _fadeInHash = Animator.StringToHash("FadeIn");
        _fadeOutHash = Animator.StringToHash("FadeOut");
    }
    private void OnEnable()
    {
        _imageToDisplay.VariableUpdated += DisplayImage;
    }
    private void Update()
    {
        if (_fadeOutTimer > 0.0f)
        {
            _fadeOutTimer -= Time.deltaTime;
            if (_fadeOutTimer <= 0.0f)
            {
                _fadeOutTimer = 0.0f;
                _imageAnimator.Play(_fadeOutHash);
            }
        }
    }
    private void OnDisable()
    {
        _imageToDisplay.VariableUpdated -= DisplayImage;
    }
    #endregion

    private void DisplayImage()
    {
        SetImagePosition();
        _displayImage.sprite = _imageToDisplay.Value;
        _imageAnimator.Play(_fadeInHash);
        _fadeOutTimer = _imageDisplayTime;
    }

    private void SetImagePosition()
    {
        if (_useRandomPosition)
        {
            _displayImage.rectTransform.anchoredPosition =
                new Vector2(Random.Range(_minXPos, _maxXPos),
                Random.Range(_minYPos, _maxYPos));
        }
    }
}
