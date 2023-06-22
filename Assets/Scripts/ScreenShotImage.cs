using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenShotImage : MonoBehaviour {
	
	[SerializeField]
	GameObject canvas;
	string[] file = null;
	int whichScreenShotIsShown;
	public Text filename;
    public static int photoNum;
	int flag =0;
	// Use this for initialization
    void Start()
    {
    }
	public void ShowImage()
	{
        photoNum = CaptureScreenshotmgr.photo_num;//此時photo_num因為按下快門鍵後為0
        whichScreenShotIsShown = photoNum;
		file = Directory.GetFiles(Application.persistentDataPath + "/", "*.png"); //file為找出此路徑且為png的路徑(沒找到則為空白)
		if(file.Length>0)
		{
			GetPictureAndShowIt(); 
		}   
	}
	void GetPictureAndShowIt()
	{
		  
		string pathToFile = file [whichScreenShotIsShown];
		Texture2D texture = GetshotImage (pathToFile);
		Sprite sp = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height),
		new Vector2 (0.5f, 0.5f));
		canvas.GetComponent<Image> ().sprite = sp;
        whichScreenShotIsShown++;
	}

	Texture2D GetshotImage(string filePath)
	{
		Texture2D texture = null;
		byte[] fileBytes;
		if (File.Exists (filePath)) {
			fileBytes = File.ReadAllBytes (filePath);
			texture = new Texture2D (2, 2, TextureFormat.RGB24, false);
			texture.LoadImage (fileBytes);
		}
		return texture;
	}

}
