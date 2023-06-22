using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragButton : MonoBehaviour
{
    //偏移值
    Vector3 m_Offset;
    //當前物體對應的螢幕座標
    Vector3 m_TargetScreenVec;

    private IEnumerator OnMouseDown()
    {
        //當前物體對應的螢幕座標
        m_TargetScreenVec = Camera.main.WorldToScreenPoint(transform.position);
        //偏移值=物體的世界座標，減去轉化之後的滑鼠世界座標（z軸的值為物體螢幕座標的z值）
        m_Offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3
        (Input.mousePosition.x, Input.mousePosition.y, m_TargetScreenVec.z));
        //當滑鼠左鍵點選
        while (Input.GetMouseButton(0))
        {
            //當前座標等於轉化滑鼠為世界座標（z軸的值為物體螢幕座標的z值）+ 偏移量
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
             Input.mousePosition.y, m_TargetScreenVec.z)) + m_Offset;
            //等待固定更新
            yield return new WaitForFixedUpdate();
        }
    }
}