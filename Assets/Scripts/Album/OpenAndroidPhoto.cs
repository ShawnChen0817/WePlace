using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class OpenAndroidPhoto : MonoBehaviour
{  
    public  RawImage  photo;  
    public void OpenPhoto(){
        AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call ("TakePhoto", Application.persistentDataPath);

    }

    public void GetPhoto(string path){
        Debug.Log("File Exists :"+File.Exists(path));
        StartCoroutine (LoadImage (path));
    }


    IEnumerator LoadImage(string path){

        WWW www = new WWW ("file://"+path);
        yield return www;

        if (www.isDone && www.error == null) {
            Texture2D tex = www.texture;
            photo.texture = tex;
        } else {


            Debug.Log ("Load Failuer："+www.error);
            try
            {
                byte[] bytes = File.ReadAllBytes(path);
                Texture2D tex2d = new Texture2D(128, 128, TextureFormat.RGBA32, false);
                if (tex2d.LoadImage(bytes, false))
                {
                    photo.texture = tex2d;
                    Debug.Log(" File Read OK!");
                }
            }
            catch (System.Exception e) {
                Debug.Log(e);
            }
        }

    }
}
