using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class note_produ : MonoBehaviour
{
    public GameObject notePrefab;
    public GameObject speed_text;
    public GameObject space_note_pre;
    public GameObject long_note_pre;
    public GameObject long_middle_pre;
    public GameObject one_one_bit;
    float timer = 0;
    public float ti = 1f;
    float speed_time = 0;
    public float speed = 4.0f;
    Vector2 line_one = new Vector2(1, 10f);
    Vector2 line_two = new Vector2(2.5f, 10f);
    Vector2 line_three = new Vector2(4, 10f);
    Vector2 line_four = new Vector2(5.5f, 10f);
    Vector2 line_space = new Vector2(3.3f, 10f);
    void Update()
    {
        speed_text.GetComponent<TMP_Text>().text = (speed/3).ToString();
        if (Input.GetKey(KeyCode.Alpha1))
        {
            speed_time += Time.deltaTime;
            if (speed_time > 0.5f&&speed>3)
            {
                speed_time = 0;
                speed-=3;
            }
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            speed_time += Time.deltaTime;
            if (speed_time > 0.5f&&speed<30)
            {
                speed_time = 0;
                speed+=3;
            }
        }
        else speed_time = 0;
        timer += Time.deltaTime;
        if(timer > ti) 
        {
            int rn = Random.Range(1, 6);
            int dn = Random.Range(1, 5);
            summon_note(rn, dn);
            timer = 0;
        }/*
        one_bit_time += Time.deltaTime;
        if(one_bit_time >= 1f)
        {
            one_bit_time = 0;
            Instantiate(one_one_bit, line_space, transform.rotation);
        }*/
    }
    int line;
    void summon_note(int line, int double_note)
    {
        int rn = Random.Range(1, 6);
        if (double_note == 1&&rn!=line) summon_note(rn, 0);
        GameObject note = Instantiate(line == 5 ? space_note_pre : notePrefab, transform.position, transform.rotation);
        check(note, line);
    }
    void check(GameObject note, int line)
    {
        switch (line)
        {
            case 1:
                note.transform.position = line_one;
                note.tag = "line1";
                break;
            case 2:
                note.transform.position = line_two;
                note.tag = "line2";
                break;
            case 3:
                note.transform.position = line_three;
                note.tag = "line3";
                break;
            case 4:
                note.transform.position = line_four;
                note.tag = "line4";
                break;
            case 5:
                note.transform.position = line_space;
                note.tag = "line5";
                break;
        }
    }
}