using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using static UnityEditor.Progress;
#endif

public class DropItem : MonoBehaviour
{
    BoxCollider boxcol;
    (int key, string name) ItemData;

    private void Start()
    {
        boxcol = GetComponent<BoxCollider>();
    }

    public void DropSetting(int key)
    {
        ItemData = ItemManager.Instance.DropItemGetKeyIcon(key);
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemManager.Instance.CreatItem(ItemData.key);
        Destroy(gameObject);
    }
}
