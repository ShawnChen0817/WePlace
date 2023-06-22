using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadSceneToStart : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator black_back;
    public Animator wait_exit;
    public Animator number_exit;
    public Animator WePlace_bottom_line_exist;
    private int curProgressValue = 0;
    public Image progressBar;
    public Text loadingText;
    void Start()
    {
        
    }
    

    void FixedUpdate()
    {
        int progressValue = 200;

        if(curProgressValue < progressValue)
        {
            curProgressValue++;
            loadingText.text = (curProgressValue/2).ToString()+" %";
        }
        progressBar.fillAmount = curProgressValue /100f;
        if(curProgressValue == 200)
        {
            black_back.SetTrigger("black back exist");
            wait_exit.SetTrigger("wait exit");
            number_exit.SetTrigger("number exit");
            WePlace_bottom_line_exist.SetTrigger("WePlace bottom line exist");
            loadingText.text = "100 %";
            Invoke("changeScene",1.3f);
        }
        
    }
    void changeScene()
    {
        SceneManager.LoadScene("SampleScene");
    } 
}

