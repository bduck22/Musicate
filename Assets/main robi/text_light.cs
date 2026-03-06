using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class text_light : MonoBehaviour
{
    TMP_Text text;
    void Start()
    {
        text= GetComponent<TMP_Text>();
        StartCoroutine(light());
    }
    IEnumerator light()
    {
        yield return new WaitForSeconds(0.35f);
        text.text = "";
        yield return new WaitForSeconds(0.35f);
        text.text = "- PRESS ENTER TO START -";
        yield return StartCoroutine(light());
    }
}
