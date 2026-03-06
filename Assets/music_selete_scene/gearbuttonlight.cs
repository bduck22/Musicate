using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gearbuttonlight : MonoBehaviour
{
    gear_po po;
    data_script data;
    TMP_Text[] GearPo;
    public GameObject gear_ob;
    void Start()
    {
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        po = data.po;
        GearPo = new TMP_Text[3];
        for(int i= 0; i < 3; i++)
        {
            GearPo[i] = gear_ob.transform.GetChild(i+1).GetChild(0).GetComponent<TMP_Text>();
        }
        switch (po)
        {
            case gear_po.left: colorchange(2); break;
            case gear_po.right: colorchange(1); break;
            case gear_po.mid: colorchange(0); break;
        }
    }
    void Update()
    {
        if (po != data.po)
        {
            po = data.po;
            switch(po){
                case gear_po.left:  colorchange(2); break;
                case gear_po.right: colorchange(1); break;
                case gear_po.mid: colorchange(0); break;
            }
        }
    }
    void colorchange(int color)
    {
        for(int i = 0; i < 3; i++)
        {
            GearPo[i].color = new Color32(150, 150, 150, 255);
        }
        GearPo[color].color = new Color32(255, 255, 255, 255);
    }
}
