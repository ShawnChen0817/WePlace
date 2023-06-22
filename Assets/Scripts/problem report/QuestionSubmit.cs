using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionSubmit : MonoBehaviour {

	[SerializeField]
	string sceneName;
	public void Change_main_Scene()
	{
		Invoke("back_MainScene",0.65f);
	}
	public void back_MainScene()
	{
		SceneManager.LoadScene (sceneName);
	}
}
