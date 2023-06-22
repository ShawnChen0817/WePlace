using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
public class UIManager : MonoBehaviour
{
    private GraphicRaycaster raycaster;

    private PointerEventData pData;
    private EventSystem eventSystem;

    public Transform selectionPoint; //每個格子的選擇 選擇的家具會在這個selectionpoint放大
    public static UIManager instance;
    public static UIManager Instance
    {
         get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UIManager>();//此為在UIManager中尋找匹配的對象，若無的話則return null
            }
            return instance;
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        //偵測我們正在選擇的button //對我們正在選擇的物件進行投射
        raycaster = GetComponent<GraphicRaycaster>(); 
        //eventsystem 為取得EventSystem中所有的Component
        eventSystem = GetComponent<EventSystem>();
        //利用eventsystem的資訊來做控制的參數
        pData = new PointerEventData(eventSystem);
        //取得selectionPoint的位置判斷現在是哪一個ui在上面
        pData.position = selectionPoint.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //傳遞UI的資料
    public bool OnEntered(GameObject button)
    {
        //touch screen後我們得到投射的結果
        List<RaycastResult> results = new List<RaycastResult>();
        //投射結果和pdata可以直接存在raycaster上
        raycaster.Raycast(pData,results);
        //利用迴圈檢查results中的物件是否為button，若為button則return true
        foreach( var result in results)
        {
            if(result.gameObject == button)
            {
                return true;
            }
            
        }
        
        return false;
    }
}
