using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class diffcu_look : MonoBehaviour
{
    public GameObject esc_panel;
    public RectTransform look;
    public RectTransform[] diff;
    public int diff_int ;
    Outline line;
    float[] Scale_L = { 7, 6, 5, 4.5f };
    float[] scale_f = { 1.1f, 1.4f, 1.6f, 1.5f };
    public GameObject song_ob;
    select song_sc;
    data_script data;
    string[] diff_name = { "Basic\n", "Medium\n", "Expert\n", "Chaos\n" };
    string[] song_difficult;
    void Start()
    {
        bool fir = true;
        diff_int = 0;
        song_sc = song_ob.GetComponent<select>();
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        line = GetComponent<Outline>();
        int[] diffic = new int[4];
        song_difficult = data.song_list[(song_sc.nowsong) % (data.song_list.Count)].diffi;
        for (int i = 0; i < song_difficult.Length; i++)
        {
            bool che = int.TryParse(song_difficult[i], out diffic[i]);
            if (che && diffic[i] != 0 && fir)
            {
                fir = false;
                diff_int = i;
            }
        }
    }

    public void init()
    {
        diff_int = data.nowdiffi;
        data.nowdiffi = 0;
        song_difficult = data.song_list[(song_sc.nowsong) % (data.song_list.Count)].diffi;
        for (int i = 0; i < 4; i++)
        {
            int num;
            int.TryParse(song_difficult[i], out num);
            if (num != 0)
            {
                transform.parent.GetChild(3).GetChild(i).GetComponent<TMP_Text>().text = diff_name[i] + song_difficult[i];
            }
            else transform.parent.GetChild(3).GetChild(i).GetComponent<TMP_Text>().text = "";
        }
    }
    bool voidcheck(bool left_right)
    {
        if (!int.TryParse(song_difficult[diff_int], out int a) || int.Parse(song_difficult[diff_int]) == 0)
        {
            if (left_right)
            {
                if (++diff_int > 3)
                {
                    diff_int = 0;
                }
            }
            else
            {
                if (--diff_int < 0)
                {
                    diff_int = 3;
                }
            }
            return voidcheck(left_right);
        }
        else return true;
    }
    float time;
    float stime;
    void Update()
    {
        if (!esc_panel.activeSelf && data.state == State.Select)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (++diff_int > 3)
                {
                    diff_int = 0;
                }
                if (voidcheck(true)) song_sc.redata();
            }
            if (Input.GetKey(KeyCode.RightArrow)&&!Input.GetKey(KeyCode.LeftArrow))
            {
                if (time > 0.3f)
                {
                    stime += Time.deltaTime;
                    if (stime > 0.04f)
                    {
                        stime = 0;
                        if (++diff_int > 3)
                        {
                            diff_int = 0;
                        }
                        if (voidcheck(true)) song_sc.redata();
                    }
                }
                else time += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                time = 0;
                stime = 0;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (--diff_int < 0)
                {
                    diff_int = 3;
                }
                if (voidcheck(false)) song_sc.redata();
            }
            if (Input.GetKey(KeyCode.LeftArrow)&&!Input.GetKey(KeyCode.RightArrow))
            {
                if (time > 0.3f)
                {
                    stime += Time.deltaTime;
                    if (stime > 0.04f)
                    {
                        stime = 0;
                        if (--diff_int < 0)
                        {
                            diff_int = 3;
                        }
                        if (voidcheck(false)) song_sc.redata();
                    }
                }
                else time += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                time = 0;
                stime = 0;
            }
            if (voidcheck(true)) scale();
        }
    }
    void scale()
    {
        look.localScale = new Vector2(scale_f[diff_int], 1.8f);
        transform.position = Vector3.MoveTowards(transform.position, diff[diff_int].position, 3800f*Time.deltaTime);
    }
}
