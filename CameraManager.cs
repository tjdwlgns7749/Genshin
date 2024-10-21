using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static CameraManager instance = null;

    public CinemachineFreeLook cinemachineFreeLook;

    public static CameraManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<CameraManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (this != Instance)
            Destroy(gameObject);
    }

    public void TargetChange(Transform transform)
    {
        //Ä«¸Þ¶ó
        cinemachineFreeLook.Follow = transform;
        cinemachineFreeLook.LookAt = transform;
    }

    public void ControllerOnOff(bool Active)
    {
        cinemachineFreeLook.enabled = Active;
    }
}
