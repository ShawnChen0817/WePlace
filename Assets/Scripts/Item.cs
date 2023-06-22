using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Item1", menuName="AddItem/Item")]
public class Item : ScriptableObject //會將裡面的程式碼通通繼承為ScriptableObject
{
   //給我們item類別屬性 
   public float price;
   public GameObject itemPefab;  
   public Sprite itemImage;
}
