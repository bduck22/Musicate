using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public GameObject Panel;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Panel.SetActive(!Panel.activeSelf);
        }
    }
}
