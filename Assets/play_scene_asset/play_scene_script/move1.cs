using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class move1 : MonoBehaviour
{
    data_script data;
    [SerializeField]float speed;
    TMP_Text text;
    judgement judge;
    float timeer;
    void Start()
    {
        speed = FindAnyObjectByType<bms_test>().speed;
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        judge = GameObject.FindWithTag("gameobject").GetComponent<judgement>();
    }
    void Update()
    {
        timeer += Time.deltaTime;
        if (text == null) text = GameObject.FindWithTag("canvas").transform.GetChild(0).GetComponent<TMP_Text>();
        transform.Translate(0, -speed * Time.deltaTime, 0);
        if (transform.position.y < 0 - speed * 0.1f&&!(gameObject.tag == "last"))
        {
            if (gameObject.GetComponent<Long>() && gameObject.GetComponent<Long>().type)
            {
                //��ù��Ʈ����Ȯ��
            }
            else
            {
                if (gameObject.GetComponent<Long>() && !gameObject.GetComponent<Long>().cantscan)
                {
                    if (transform.position.y < 0 - speed * 0.13f)
                    {
                        judge.booooom[int.Parse(gameObject.tag.Substring(4, 1)) - 1].GetComponent<boooomoff>().stop = false;
                        data.score++;
                        data.combo++;
                        data.judge[4]++;
                        text.text = "HIT";//�ճ�Ʈ�ȶ�
                        text.color = new Color32(102, 102, 102, 255);
                        GameObject.FindWithTag("gameobject").GetComponent<judgement>().long_wait[int.Parse(gameObject.tag.Substring(4, 1)) - 1] = false;
                        Destroy(gameObject);
                    }
                }
                else if (!gameObject.GetComponent<empty>() && !gameObject.GetComponent<Long>())
                {
                    data.combo = 0;
                    data.judge[5]++;
                    text.text = "MISS";//�Ϲݳ�Ʈ��ħ
                    text.color = new Color(255, 0, 0);
                    //Debug.Log(timeer);
                    timeer = 0;
                    Destroy(gameObject);
                }
                else
                {
                    //Debug.Log(timeer);
                    timeer = 0;
                    Destroy(gameObject);
                } 
            }
        }
    }
}
