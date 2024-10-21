using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.Instance.PlayBGM("LoginOST");
    }

    private void OnDisable()
    {
        AudioManager.Instance.MusicStop();
    }
}
