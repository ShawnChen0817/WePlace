using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//讓所有的furniture都被選到
public class UIContentfitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //每個button之中應該如何編排的函式
    public void Fit()
    {
        //hg為取得所有HorizontalLayoutGroup的Component
        HorizontalLayoutGroup hg = GetComponent<HorizontalLayoutGroup>();
        //計算子物件中有幾個(這裡-1是因為每個furniture中間地格子會比child還要少一個)
        int childCount = transform.childCount-1;
        //找出child中的寬度 //從第一個小孩計算button的寬度
        float childWidth = transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        //width為所有子物件中的寬度加總每個物件的中間都要空120
        float width = hg.spacing * childCount + childCount * childWidth + hg.padding.left;
        //獲取content的長寬 
        GetComponent<RectTransform>().sizeDelta = new Vector2(width,265);
    }
}
