using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SwipePages : MonoBehaviour , IDragHandler , IEndDragHandler
{
    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    private Vector3 newLocation;
    public Text Page1;

    public Text Page2;

    public Text Page3;

    public Text Page4;
   
    // Start is called before the first frame update
    void Start()
    {
        panelLocation = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0 ,0);
    }
    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x)/ Screen.width;
        if(Mathf.Abs(percentage) >= percentThreshold)
        {
            newLocation = panelLocation;
            
            //percentage大於0時，swipe follow的位置則向左移動
                
            if(percentage > 0)
            {
                newLocation += new Vector3(-Screen.width, 0,0);
                if(newLocation.x >= 234 & newLocation.x >-218)
                {
                    Page1.color = Color.white;
                    Page2.color = Color.gray;
                    Page3.color = Color.gray; 
                    Page4.color = Color.gray; 
                }
                else if(newLocation.x <= -218 & newLocation.x >-670)
                {
                    Page1.color = Color.gray;
                    Page2.color = Color.white;
                    Page3.color = Color.gray; 
                    Page4.color = Color.gray; 
                }
                else if(newLocation.x <= -670 & newLocation.x>-1595.505)
                {
                    Page1.color = Color.gray;
                    Page2.color = Color.gray;
                    Page3.color = Color.white;
                    Page4.color = Color.gray;  
                }
                else if(newLocation.x <=-1595.505)
                {
                    Page1.color = Color.gray;
                    Page2.color = Color.gray;
                    Page3.color = Color.gray;
                    Page4.color = Color.white;  
                }
            }

            //percentage小於0時，swipe follow的位置則向右移動
            else if(percentage < 0)
            {
                newLocation += new Vector3(Screen.width,0 ,0);
                if(newLocation.x >= 234 & newLocation.x >-213)
                {
                    Page1.color = Color.white;
                    Page2.color = Color.gray;
                    Page3.color = Color.gray; 
                    Page4.color = Color.gray;  
                }
                else if(newLocation.x <= -213 & newLocation.x >-661)
                {
                    Page1.color = Color.gray;
                    Page2.color = Color.white;
                    Page3.color = Color.gray; 
                    Page4.color = Color.gray;  
                }
                else if(newLocation.x <= -661 & newLocation.x>-1595.505)
                {
                    Page1.color = Color.gray;
                    Page2.color = Color.gray;
                    Page3.color = Color.white;
                    Page4.color = Color.gray;  
                }
                else if(newLocation.x <=-1595.505)
                {
                    Page1.color = Color.gray;
                    Page2.color = Color.gray;
                    Page3.color = Color.gray;
                    Page4.color = Color.white;  
                }
            }
            
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
                
            
        }
        
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
        Debug.Log(newLocation.x);
    }
  
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t =0f;
        while(t<=1.0)
        {
            t+= Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
