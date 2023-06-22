using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
//處理輸入事件的組件 //可用於管理事件 such as raycast
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.XR.ARSubsystems;

//自定義 ARBaseGestureInteractable接口
//運用此class可繼承  ARRotationInteractable ARScaleInteractable ARSelectionInteractable ARTranslationInteractable (可控制家具的script)
public class InputManager : ARBaseGestureInteractable 
{
    [SerializeField] private Camera arCam;//ar camera 
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject crosshair;//宣告十字準線
    private GameObject placeObj;
    
    //點擊(touch)與操作姿勢(pose)
    private Touch touch;
    private Pose pose;
    
    //List<> 表示可以依照索引存取的強類型物件清單。提供搜尋、排序和管理清單的方法
    //ARRaycastHit(XRRaycastHit hit, float distance, Transform transform)
    //hit為擊中的資訊  distance為擊中的距離  transform為unity空間到現實空間的轉換
    List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    
    /*開始即執行一次*/
    void Start()
    {
        
    }

    /*每根據設定時間呼叫函式(此函式會因為十字準線的方位來使物品放置有不同位置)*/
    void FixedUpdate()
    {
        CrosshairCalculation();
        
        /*//呼叫十字準線
        CrosshairCalculation();
        //觸控操作
        touch = Input.GetTouch(0);

        if(Input.touchCount<0 || touch.phase!=TouchPhase.Began)
            return;

        if(IsPointerOverUI(touch)) return;//如果touch後為true則return
        //根據GetFurniture所選擇到的家具來實例化
        Instantiate(DataHandler.Instance.GetFurniture(),pose.position, pose.rotation);//家具顯示出來並放置的位置*/
    }

    /*十字準線讓擺放的位置更加精確*/
    void CrosshairCalculation()
    {
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f,0.5f,0));
        
        //Raycast(Vector2, List<ARRaycastHit>, TrackableType) 
        //TrackableType.PlaneWithinPolygon代表為當射線與bounded plane的多邊形相交則是為該射線命中
        //Raycast(Vector2, List<ARRaycastHit>, TrackableType),若是投射TrackableType成功則回傳true，反之則回傳false 
        if(GestureTransformationUtility.Raycast(origin,_hits,TrackableType.PlaneWithinPolygon))
        {
            //點擊放置
            //pose紀錄我們在範圍內所有操作的姿勢
            pose = _hits[0].pose;

            //crosshair的位置在pose點擊屏幕的位置
            crosshair.transform.position = pose.position;
            
            //永遠跟著這個參數在做轉動
            //unity中我們設置的crosshair Marker原本為直立，這裡設置他永遠都會向x軸選轉90度，以此Marker才會永遠保持水平
            crosshair.transform.eulerAngles = new Vector3(90,0,0);
        }
    }

    /*確定是否可以給定的手式啟動操作(return boolean value)*/
    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        //當我們點擊的位置沒有衝突到 則可以啟動操作
        if(gesture.targetObject == null)
        {
            return true;
        }
        return false;
    }

    /*有3種判斷是否能夠進行放置*/
    protected override void OnEndManipulation(TapGesture gesture)
    {
        //讀取手勢操作是否取消的boolean值(read only)
        if(gesture.isCanceled)
        {
             return;
        }

        //gesture.targetObject為讀取是否擁有手勢操作的對象
        //IsPointerOverUI回傳是否操作資訊大於0的boolean值
        if(gesture.targetObject!=null || IsPointerOverUI(gesture))
        {
            return;
        }

        //Raycast(Vector2, List<ARRaycastHit>, TrackableType),若是投射TrackableType成功則回傳true，反之則回傳false 
        //TrackableType.PlaneWithinPolygon代表為當射線與bounded plane的多邊形相交則是為該射線命中
        if(GestureTransformationUtility.Raycast(gesture.startPosition,_hits,TrackableType.PlaneWithinPolygon))
        {
            /*實例化家具模板*/
            //點選crosshair處會顯示當時button中的家具(根據點擊的位置實例化家具模板)
            placeObj = Instantiate(DataHandler.Instance.GetFurniture(),pose.position, pose.rotation);
            
            /*放置家具清單*/
            //呼叫DataHandler的GetItemList取得放置家具的itemImage在ItemList中
            DataHandler.Instance.GetItemList();
            
            //anchor為找出我們object中在現實環境中的位置
            var anchorObj = new GameObject("PlacementAnchor");
            anchorObj.transform.position = pose.position;
            anchorObj.transform.rotation = pose.rotation;
            placeObj.transform.parent = anchorObj.transform;
        }
        
    }
    
    /*回傳在投射範圍內的資訊是否大於0(是否點到UI)*/
    bool IsPointerOverUI(TapGesture touch)//透過touch來控制UI(TapGesture會偵測我們的動作來active功能)
    {
        //現在發生的事情回傳到eventdata(鼠標點擊事件)
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        
        //返回當前我們touch到的位置(設置鼠標位置)
        eventData.position = new Vector2(touch.startPosition.x,touch.startPosition.y);

        //創建List收集raycast的結果
        List<RaycastResult> results = new List<RaycastResult>();
        
        //運用我們之前的到的資訊來進行raycastall，代表可以在此範圍內使用所有的baseraycasters
        //raycastall為找出現在的所有射線的資訊
        //檢測是否有點擊到UI
        EventSystem.current.RaycastAll(eventData,results);

        //若是result回傳的資訊大於0時為true，反之則為false(>0代表點到UI)
        return results.Count > 0;
    }
}
