using UnityEngine;

public class SyncFOV : MonoBehaviour
{
    public Camera OverlayCamera;
    public Camera MainCamera;

    public void Update()
    {
        if (OverlayCamera.fieldOfView != MainCamera.fieldOfView)
        {
            OverlayCamera.fieldOfView = MainCamera.fieldOfView;
        }
    }
}
