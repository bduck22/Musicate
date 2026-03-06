using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class stop : MonoBehaviour
{
    float time;
    public GameObject pause;
    public GameObject info;
    data_script data;
    bms_test bms;
    public downdown Pause;
    int pausetime;
    Vector3 po;
    void Start()
    {
        pausetime = 2;
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        bms = GameObject.FindWithTag("gameobject").GetComponent<bms_test>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& Time.timeScale == 1)
        {
            time = transform.GetComponent<AudioSource>().time;
            transform.GetComponent<AudioSource>().Stop();
            pause.SetActive(true);
            po = info.transform.position;
            info.transform.position = Camera.main.WorldToScreenPoint(new Vector2(12.7639f, 7.0943f));
            Time.timeScale = 0;
            Pause.upup = false;
        }
    }
    public void resume()
    {
        pause.SetActive(false);
        float xxxx = 0;
        switch (data.po)
        {
            case gear_po.left: xxxx = 2f; break;
            case gear_po.mid: xxxx = 12.7639f; break;
            case gear_po.right: xxxx = -2f; break;
        }
        info.transform.position = po;
        StartCoroutine(resume2());
    }
    public IEnumerator resume2()
    {
        yield return new WaitForSecondsRealtime(3);
        Pause.upup = true;
        yield return new WaitForSecondsRealtime(bms.playsong.measure);
        Time.timeScale = 1;
        transform.GetComponent<AudioSource>().Play();
        transform.GetComponent<AudioSource>().time = time;
    }
}
