using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class mainlobby: MonoBehaviour
{
    public GameObject[] Setext;
    public Transform po;
    int Count;
    bool MoVe;
    bool one;
    bool left_right;
    Vector3 target;
    Vector3[] move = { new Vector3(0,0,0), new Vector3(0, 0, 0)}; 
    void Start()
    {
        one = false;
        MoVe = false;
        Count = 1;
        move[0] = po.position - new Vector3(-1300, 0);
        move[1] = po.position - new Vector3(1300, 0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            left_right = false;
            one = true;
            MoVe = true;
            target = po.position - new Vector3(1300, 0);
            if (++Count > 2)
            {
                Count = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            left_right = true;
            one = true;
            MoVe = true;
            target = po.position + new Vector3(1300, 0);
            if (--Count < 0)
            {
                Count = 2;
            }
        }
        Move();
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(Count);
        }
    }
    void Move()
    {
        if (one)
        {
            Setext[2 - Count].transform.position = move[left_right?1:0];
            one = false;
        }
        if (MoVe) po.position = Vector2.MoveTowards(po.position, target, 7900f * Time.deltaTime);
        if (po.position == target) MoVe = false;
    }
}
