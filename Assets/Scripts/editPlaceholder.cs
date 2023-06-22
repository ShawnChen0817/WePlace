using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.XR.ARFoundation;
public class editPlaceholder : MonoBehaviour
{
    private Text placeholder;
    public void Select_furniture()
    {
        placeholder = GameObject.Find("Placeholder").GetComponent<Text>();
        placeholder.text = "現有家具商品";
    }
    public void Select_furniture_type()
    {
        placeholder = GameObject.Find("Placeholder").GetComponent<Text>();
        placeholder.text = "請選擇家具類型";
    }
}
