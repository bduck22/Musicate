using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class boooomoff : MonoBehaviour
{
    public bool stop;
    judgeani Judge;
    void Start()
    {
        Judge = GameObject.FindWithTag("judge").GetComponent<judgeani>();
    }
    void Update()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(off());
        }
    }
    IEnumerator off()
    {
        yield return new WaitForSeconds(0.16f);
        gameObject.SetActive(false);
    }
    public IEnumerator repeat()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        if (stop) StartCoroutine(repeat());
        else gameObject.SetActive(false);
    }
}
