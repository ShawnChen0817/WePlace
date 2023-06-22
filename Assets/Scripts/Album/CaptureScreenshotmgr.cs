using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class CaptureScreenshotmgr: MonoBehaviour
{

    [Header("檔案名稱")]
    public string FileName;
    int FileNumber=0;
    [Header("APP上的UI")]
    public GameObject[] UIs;
    [SerializeField]
	GameObject blink;
    public  RectTransform img_panel;
    private Sprite sp;
    public Image img;
    private float timer=0;
    private bool isstart = false;

    int flag;
    public static int photo_num=-1;
    public void CapturePhoto() {
        //先將UI全部關閉
        for (int i = 0; i < UIs.Length; i++) {
            UIs[i].SetActive(false);
        }
        //每按一次按鈕FileNumber會+1
        FileNumber += 1;
        //圖片名稱為IThomeAR + FileNumber.png
        FileName =  GetCurTime() + " AR-Screen Shot " + FileNumber + ".png";
        //截圖的程式碼
        ScreenCapture.CaptureScreenshot(FileName);
        Invoke("CameraLight", 0.35f);
        //1秒後執行打開UI的函式
        Invoke("openUI", 1);
        photo_num++;
        Debug.Log(FileName);
        Debug.Log(Screenshot_preview.photo_number);
    }
    void openUI() {
        
        //將UI全部開啟
        for (int i = 0; i < UIs.Length; i++)
        {
            UIs[i].SetActive(true);
        }
        blink.SetActive(false);
        flag = 0;
    }
    void CameraLight()
    {
        blink.SetActive(true);
        AudioSource audio =  GameObject.FindGameObjectWithTag ("screen shot audio").GetComponent<AudioSource> ();
        flag=1;
        timer = 0;
        audio.Play();
    }   
    string GetCurTime()
    {
        if(DateTime.Now.Hour>12)
        {
            return DateTime.Now.Year.ToString() +"年"+ DateTime.Now.Month.ToString() +"月"+ DateTime.Now.Day.ToString()+"日 "+"下午"
            + (DateTime.Now.Hour-12).ToString() +"時"+ DateTime.Now.Minute.ToString()+"分"+DateTime.Now.Second.ToString();
        }
        else if(DateTime.Now.Hour==12)
        {
            return DateTime.Now.Year.ToString() +"年"+ DateTime.Now.Month.ToString() +"月"+ DateTime.Now.Day.ToString()+"日 "+"中午"
            + DateTime.Now.Hour.ToString() +"時"+ DateTime.Now.Minute.ToString()+"分"+DateTime.Now.Second.ToString();
        }
        else
        {
            return DateTime.Now.Year.ToString() +"年"+ DateTime.Now.Month.ToString() +"月"+ DateTime.Now.Day.ToString()+"日 "+"上午"
            + DateTime.Now.Hour.ToString() +"時"+ DateTime.Now.Minute.ToString()+"分"+DateTime.Now.Second.ToString();
        }
        
    }
    void Update()
    {
        if(isstart)
        {
            timer += Time.deltaTime * 2.5f;
            img_panel.localScale = Vector3.Lerp(new Vector3(1, 1, 0), new Vector3(0.5f, 0.5f, 0), timer);
            img_panel.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(-430, -950f, 0), timer);
            img_panel.localRotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, new Vector3(0, 0, 0), timer));
            if(flag == 0)
            {
                
                img_panel.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(850.921f,-382.249f, 0), timer);
                img_panel.localRotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, new Vector3(0, 0, 0), timer));
                img_panel.localScale = Vector3.Lerp(new Vector3(1, 1, 0), new Vector3(1.2f,1.2f, 0), timer);
                
            }
            
        }
        
        if(flag == 1)
        {
            isstart = true;
            Camera camera = GameObject.Find("AR Camera").gameObject.GetComponent<Camera>();
            int ratio = 2;
            Rect rect = new Rect(0, 0, (int)Screen.width / ratio, (int)Screen.height / ratio);//图片大小取决于ratio的大小*/
        }    
    }

}