using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameend : MonoBehaviour
{
    AudioSource audio;
    bool start;
    data_script data;
    GameObject end;
    void Start()
    {
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        start = true;
        audio = gameObject.GetComponent<AudioSource>();
        end = GameObject.FindWithTag("last");
    }

    void Update()
    {
        if (end.transform.position.y<0&&start&&!audio.isPlaying)
        {
            start = false;
            StartCoroutine(waitandsel());
        }
    }
    IEnumerator waitandsel()
    {
        yield return new WaitForSeconds(1.5f);
        data.state = State.end;
        SceneManager.LoadScene(4);
    }
}
