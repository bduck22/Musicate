using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class what : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0f;
        StartCoroutine(test());
    }
    void Update()
    {
        
    }
    IEnumerator test()
    {
        Debug.Log("bbb");
        yield return new WaitForSecondsRealtime(1);
        Debug.Log("aaa");
    }
}
