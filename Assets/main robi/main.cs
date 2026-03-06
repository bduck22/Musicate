using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main : MonoBehaviour
{
    public GameObject light;
    void Update()
    {
        if (transform.parent.GetChild(3).GetComponent<Image>().color.a <= 0 && transform.parent.GetChild(4).gameObject.activeSelf) transform.parent.GetChild(4).gameObject.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StartCoroutine(shake());
            light.SetActive(true);
        }
    }
    IEnumerator shake()
    {
        transform.localScale = new Vector3(1.01f, 1.01f, 1);
        yield return new WaitForSeconds(0.035f);
        transform.localScale = new Vector3(1, 1, 1);
    }
}
