using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Date_Time : MonoBehaviour
{
    public Text DateTimeText;
    public int MinuteNum;
    // Start is called before the first frame update
    void Start()
    {
        MinuteNum = DateTime.Now.Minute;
    }

    // Update is called once per frame
    void Update()
    {
        DateTimeText.text = DateTime.Now.ToString();
    }
}
