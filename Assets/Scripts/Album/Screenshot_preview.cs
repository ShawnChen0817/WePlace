using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Screenshot_preview : MonoBehaviour {
	
	[SerializeField]
	GameObject canvas;
	string[] files = null;
	int whichScreenShotIsShown;
	public Text filename;
	string tpath;
	int i;
	public static int photo_number;
	// Use this for initialization
	void Start () {
		
		photo_number = CaptureScreenshotmgr.photo_num;
		whichScreenShotIsShown = photo_number;
		i = photo_number;
		files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
		if (files.Length > 0) {
			GetPictureAndShowIt ();
			filenamechange();
		}
	}
	void filenamechange(){
		tpath =files[i];
		filename.text = Path.GetFileName(tpath);
		
	}
	void GetPictureAndShowIt()
	{
		string pathToFile = files [whichScreenShotIsShown];
		Texture2D texture = GetScreenshotImage (pathToFile);
		Sprite sp = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height),
			new Vector2 (0.5f, 0.5f));
		canvas.GetComponent<Image> ().sprite = sp;
	}

	Texture2D GetScreenshotImage(string filePath)
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

	public void NextPicture()
	{
		if (files.Length > 0) {
			tpath =files[i];
			whichScreenShotIsShown += 1;
			i+=1;
			if (whichScreenShotIsShown > files.Length - 1)
				whichScreenShotIsShown = 0;
			if(i >files.Length - 1)
				i=0;	
			GetPictureAndShowIt ();
			filenamechange();
		}
	}

	public void PreviousPicture()
	{
		
		if (files.Length > 0) {
			whichScreenShotIsShown -= 1;
			i-=1;
			if (whichScreenShotIsShown < 0)
				whichScreenShotIsShown = files.Length - 1;
			if(i<0)
				i=files.Length - 1;	
			GetPictureAndShowIt ();
			filenamechange();
		}
	}
}
