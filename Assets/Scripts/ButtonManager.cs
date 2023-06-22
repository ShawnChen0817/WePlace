using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ButtonManager : MonoBehaviour
{
    private Button btn;
    //button中的圖片
    //這樣設置可以讀取每次從不同item中找出他們的圖片
    [SerializeField] private RawImage buttonImage;
    //設置每個物件的id和材質
    private int itemId;
    private Sprite buttonTexture;
    //設置每個button都是以圖片的形式展示
    public Sprite ButtonTexture
    {
        //set為設定屬性
        //設定buttonImage中rawImage的圖片
        set
        {
            buttonTexture=value;
            //對應到DataHandler的i.itemImage(value.itemImage);
            buttonImage.texture = buttonTexture.texture;
            
        }
    }
    //設置itemId是一個值
    public int ItemId
    {
        set {itemId = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SelectObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(UIManager.Instance.OnEntered(gameObject))
        {
            transform.DOScale(Vector3.one*2, 0.3f);//滑動到selectionpoint時經過0.3秒後放大
            //transform.localScale = Vector3.one*2;
        }
        else
        {
            transform.DOScale(Vector3.one, 0.3f);//可以控制我們每次滑動後所移動的範圍
            //transform.localScale = Vector3.one;
        }
    }
    //SelectObject函式為在按下button時抓取其id
    void SelectObject()
    {
        Debug.Log(itemId);
        DataHandler.Instance.SetFurniture(itemId);//根據ID來選擇家具    
    }
}
