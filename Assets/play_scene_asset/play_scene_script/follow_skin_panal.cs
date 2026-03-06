using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class follow_skin_panal : MonoBehaviour
{
    public GameObject skin_panel;
    void Start()
    {

    }
    void Update()
    {
        if ((skin_panel.transform.position + new Vector3(0, 200)).y >= 115.2324f + 30)
        {
            if (skin_panel.transform.position.y == Camera.main.WorldToScreenPoint(new Vector2(0, -3.72f)).y || skin_panel.GetComponent<gear_noteskinchange>().down && skin_panel.activeSelf) GetComponent<TMP_Text>().text = "¡å";
            transform.position = skin_panel.transform.position + new Vector3(0, 170);
        }
        else
        {
            transform.position = new Vector3(960, 115.2324f + 35);
            GetComponent<TMP_Text>().text = "¡ã";
        }
    }
}
