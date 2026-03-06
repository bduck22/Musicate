using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class downdown : MonoBehaviour
{
    data_script data;
    bms_test bms;
    public bool upup;
    void Start()
    {
        upup = false;
        bms = GameObject.FindWithTag("gameobject").GetComponent<bms_test>();
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
    }
    void Update()
    {
        if(Time.timeScale == 0&&!upup)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 4.31f-bms.playsong.measure*data.speed,30), 50 * Time.unscaledDeltaTime);
        }
        else if(Time.timeScale == 0 && upup)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,4.31f,30), data.speed * Time.unscaledDeltaTime);
        }
    }
}
