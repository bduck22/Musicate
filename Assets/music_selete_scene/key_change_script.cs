using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key_change_script : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
        GameObject.FindFirstObjectByType<GameMaster>().enabled = !gameObject.activeSelf;
    }
    public void open_key_change()
    {
        gameObject.SetActive(true);
    }
}
