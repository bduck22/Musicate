using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Unity.VisualScripting;
//using NUnit.Framework;
using UnityEngine.SceneManagement;

public class fead : MonoBehaviour
{
    public Image fade;
    public GameObject faded;
    public float FadeInTime;
    public float FadeOutTime;
    public float FadeColor;
    float FadeCount;
    public bool FadeOut;
    public bool scenemove;
    public int scene;

    private void Start()
    {
        FadeCount = 1;
        StartCoroutine(FadeStart());
    }

    IEnumerator FadeStart()
    {
        yield return new WaitForSeconds(FadeInTime);
        if (!FadeOut) FadeCount = 0;
        while ((FadeCount >= 0.0f&&FadeOut)||(FadeCount<=1&&!FadeOut))
        {
            if (FadeOut) FadeCount -= (FadeColor / 100);
            else FadeCount += (FadeColor / 100);
            yield return new WaitForSeconds(0.01f);
            Color color = fade.color;
            color.a = FadeCount;
            fade.color = color;
        }
        yield return new WaitForSeconds(FadeOutTime);
        if (scenemove) SceneManager.LoadScene(scene);
    }
}



