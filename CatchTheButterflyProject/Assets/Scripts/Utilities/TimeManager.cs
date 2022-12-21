using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private bool _beginPaused = true;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_beginPaused)
        {
            Time.timeScale = 0.0f;
        }
    }

    public void StartTime()
    {
        Time.timeScale = 1.0f;
    }
    
    public void StopTime()
    {
        Time.timeScale = 0.0f;
    }
}
