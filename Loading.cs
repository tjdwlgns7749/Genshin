using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Image loading_bar;
    public Sprite[] Images;
    public Image center_Image;

    private void OnEnable()
    {
        int a = Random.Range(0, Images.Length);

        center_Image.sprite = Images[a];

        StartCoroutine(LoadingBarStart());
    }

    IEnumerator LoadingBarStart()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            loading_bar.fillAmount += 0.02f;

            if (loading_bar.fillAmount >= 1)
            {
                gameObject.SetActive(false);
                AudioManager.Instance.PlayBGM("TownOST");
                break;
            }
        }
    }

}
