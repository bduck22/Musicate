using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class very_big_light : MonoBehaviour
{
    bool light;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            light = true;
        }
        /*if (light)
        {
            if (!transform.parent.GetChild(3).gameObject.activeSelf) transform.parent.GetChild(3).gameObject.SetActive(true);
            if (transform.parent.GetChild(3).localScale.y < 50) transform.parent.GetChild(3).localScale += new Vector3(55 * Time.deltaTime, 55 * Time.deltaTime, 0);
            else
            {
                transform.parent.GetChild(3).GetComponent<fead>().enabled = true;
                transform.parent.GetChild(1).gameObject.SetActive(true);
                transform.parent.GetChild(4).gameObject.SetActive(false);
                transform.parent.GetChild(2).gameObject.SetActive(false);
            }
        }*/
    }
}
