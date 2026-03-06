using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class judgeani : MonoBehaviour
{
    float size;
    void Start()
    {
        size = gameObject.GetComponent<TMP_Text>().fontSize;
    }
    public IEnumerator ani()
    {
        gameObject.GetComponent<TMP_Text>().fontSize =size+5;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<TMP_Text>().fontSize =size + 10;
        yield return new WaitForSeconds(0.01f);
        gameObject.GetComponent<TMP_Text>().fontSize =size + 15;
        yield return new WaitForSeconds(0.01f);
        gameObject.GetComponent<TMP_Text>().fontSize = size;
        yield return new WaitForSeconds(0.01f);
    }
}
