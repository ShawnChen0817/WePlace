using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_switch : MonoBehaviour
{
    public GameObject switch_btn;
    private RectTransform rectTransform;
    public GameObject chartmode;
    public GameObject listmode;
    public GameObject list_mode;
    public GameObject chart_mode;
    private int flag = 0;
    // Start is called before the first frame update
    public void Switch()
    {
        rectTransform = switch_btn.GetComponent<RectTransform>();
        if(flag == 0)
        {
            //chart_mode.SetActive(false);
            rectTransform.localPosition = new Vector3(57,1,0);
            flag = 1;
            chartmode.GetComponent<Image>().color = Color.gray;
            listmode.GetComponent<Image>().color = Color.white;
            list_mode.SetActive(true);

        }
        else
        {
            list_mode.SetActive(false);
            rectTransform.localPosition = new Vector3(-52,1,0);
            flag = 0;
            chartmode.GetComponent<Image>().color = Color.white;
            listmode.GetComponent<Image>().color = Color.gray;
            chart_mode.SetActive(true);
        }
        
    }
}
