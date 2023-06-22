using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    public Text loadingText;
    public Image progressBar;
    private int curProgressValue = 0;
    public Animator successfully_sent;
    void FixedUpdate()
    {
        int progressValue = 100;

        if(curProgressValue < progressValue)
        {
            curProgressValue++;
        }
        progressBar.fillAmount = curProgressValue /100f;
        if(curProgressValue == 100)
        {
            GameObject wait = GameObject.FindGameObjectWithTag("wait");
            wait.SetActive(false);
            successfully_sent.SetTrigger("successfully sent exist");
            Invoke("changeScene",0.5f);
        }
        
    }
    void changeScene()
    {
        SceneManager.LoadScene("Question");
    }    
}
