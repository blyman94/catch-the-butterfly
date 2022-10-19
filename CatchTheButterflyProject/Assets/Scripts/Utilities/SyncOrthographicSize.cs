using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncOrthographicSize : MonoBehaviour
{
    public Camera OverlayCamera;
    public Camera MainCamera;

    public void Update()
    {
        if (OverlayCamera.orthographicSize != MainCamera.orthographicSize)
        {
            OverlayCamera.orthographicSize = MainCamera.orthographicSize;
        }
    }
}
