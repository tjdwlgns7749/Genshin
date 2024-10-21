using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider stamina;
    public Transform Target;
    public PlayerManager user;
    public int offset = 500;


    void Update()
    {
        Vector3 worldPosition = Target.position;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        screenPosition.x = Mathf.RoundToInt(screenPosition.x);
        stamina.transform.parent.position = (screenPosition + Vector3.right * offset);
        stamina.value = user.Stamina;
    }
}
