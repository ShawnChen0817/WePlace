using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.XR.ARFoundation;
public class DataHandler : MonoBehaviour
{
    public GameObject CabinetScroll;
    public GameObject ChairScroll;
    public GameObject SofaScroll;
    public GameObject TableScroll;
    private GameObject furniture;
    public Sprite Selected_ItemImage;
    private GameObject ItemListButtonClone;
    //Clear函式用的Furniture
    private GameObject Furniture;
    //得到所有furniture的資訊 //宣告buttonPrefab為ButtonManager取出來的
    private Button back;
    [SerializeField] private ButtonManager buttonPrefab;
    //放置所有buttons的地方
    [SerializeField] private GameObject buttonContainer;
    //放置已經放過的家具的清單
    [SerializeField] private GameObject Selected_ItemList;
    //宣告一個item的列表
    [SerializeField] private List<Item> items;
    [SerializeField] private List<Item> CabinetItems;
    [SerializeField] private List<Item> ChairItems;
    [SerializeField] private List<Item> SofaItems;
    [SerializeField] private List<Item> TableItems;
    [SerializeField] private List<Item> testItems;
    //只要label為Furniture(家具類型)皆視為我們要顯示的button
    [SerializeField] private String label1;//label1 = cabinet
    [SerializeField] private String label2;//label2 = chair
    [SerializeField] private String label3;//label3 = sofa
    [SerializeField] private String label4;//label4 = table
    //各類商品的id
    private int id;
    private int pid;
    //在list中clone的順序
    private int clone_id = 0;
    private int cabinet1_cloneid;
    private int chair1_cloneid;
    private int chair2_cloneid;
    private int chair3_cloneid;
    private int chair4_cloneid;
    private int sofa1_cloneid;
    private int sofa2_cloneid;
    private int sofa3_cloneid;
    private int sofa4_cloneid;
    private int table1_cloneid;
    private int table2_cloneid;
    //list中clone的數量
    private int ItemList_ButtonClone=0;    
    //將clone_id集中的一個陣列
    private int[] Item_CloneSet = new int[50];
    //ButtonClone的數量
    private int ButtonCloneCount;
    //每個家具的id
    private int chair_id = 0;
    private int sofa_id = 0;
    private int cabinet_id = 0;
    private int table_id = 0;

    private int clone3;
    private int swap;

    //實例化button
    private ButtonManager img;
    
    //判斷各自物件的數量(在列表中顯示)
    //櫃子
    public GameObject CabinetCount1_Text;
    private Text CabinetCount1_text;
    private float CabinetCount1 = 0;
    
    //椅子
    public GameObject ChairCount1_Text;
    private Text ChairCount1_text;
    private float ChairCount1=0;

    public GameObject ChairCount2_Text;
    private Text ChairCount2_text;
    private float ChairCount2=0;

    public GameObject ChairCount3_Text;
    private Text ChairCount3_text;
    private float ChairCount3=0;

    public GameObject ChairCount4_Text;
    private Text ChairCount4_text;
    private float ChairCount4=0;
    
    //沙發
    public GameObject SofaCount1_Text;
    private Text SofaCount1_text;
    private float SofaCount1=0;

    public GameObject SofaCount2_Text;
    private Text SofaCount2_text;
    private float SofaCount2=0;

    public GameObject SofaCount3_Text;
    private Text SofaCount3_text;
    private float SofaCount3=0;

    public GameObject SofaCount4_Text;
    private Text SofaCount4_text;
    private float SofaCount4=0;
    
    //桌子
    public GameObject TableCount1_Text;
    private Text TableCount1_text;
    private float TableCount1=0;

    public GameObject TableCount2_Text;
    private Text TableCount2_text;
    private float TableCount2=0;
    
    //判別tag
    private string Tag;
    
    //進入DestroyItemList所需要依照name來判斷進入哪種判斷式中
    private string name;

    private static DataHandler instance;
    
    //判別是否消除
    private int destroy=0;

    public Animator follow_circle_anim;
    public Animator follow_arrow_anim;
    public Animator question_circle_anim;
    public Animator question_arrow_anim;
    public Animator album_circle_anim;
    public Animator album_arrow_anim;
    public Animator start_scan_anim;
    public Animator blur_back_anim;
    public Animator Scan_btn;
    //public Animator glass_anim;
    
    private static int count=0;
    private int flag=0;
    
    //控制ARSession中的ARPlaneManager
    public ARSessionOrigin ARSession;
   
    private int planedetectstate=0;
    
    //判斷家具清單是否完全清除
    private int ClearItemList=0;
    public static DataHandler Instance 
    { 
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<DataHandler>();//搜索DataHandler中的Component //在DataHandler中尋找匹配對象，如果沒有則return null
            }
            return instance;
        } 

    }

    /*動畫控制*/
    private void Start()
    {
        //找到tag為furniture的object
        //Furniture = GameObject.FindGameObjectWithTag("furniture");

        //呼叫函式
        //LoadItems();
        //等待我們拿取所有label為Furniture的asset
        //await Get(label);
        //並為這些label為Furniture的asset創建button
        //CreateButton();
        StartAnimate();
        
    } 

    public void StartScan_exist()
    {
        start_scan_anim.SetTrigger("start text exist");
    }

    public void StartAnimate()
    {
        GameObject follow_circle = GameObject.Find("follow circle exist");
        GameObject follow_arrow = GameObject.Find("follow arrow exist");
        GameObject question_circle = GameObject.Find("question circle exist");
        GameObject question_arrow = GameObject.Find("question arrow exist");
        GameObject album_circle = GameObject.Find("album circle exist");
        GameObject album_arrow = GameObject.Find("album arrow exist");
        follow_circle.SetActive(false);
        follow_arrow.SetActive(false);
        question_circle.SetActive(false);
        question_arrow.SetActive(false);
        album_circle.SetActive(false);
        album_arrow.SetActive(false);
        if(count == 0)
		{
            follow_circle.SetActive(true);
            follow_arrow.SetActive(true);
            question_circle.SetActive(true);
            question_arrow.SetActive(true);
            album_circle.SetActive(true);
            album_arrow.SetActive(true);
			follow_circle_anim.SetTrigger("follow circle exist");
			follow_arrow_anim.SetTrigger("follow arrow exist");
            question_circle_anim.SetTrigger("question circle exist");
            question_arrow_anim.SetTrigger("question arrow exist");
            album_circle_anim.SetTrigger("album circle exist");
            album_arrow_anim.SetTrigger("album arrow exist");
            blur_back_anim.SetTrigger("blur back exist");
            Invoke("StartScan_exist",1.5f);
			count++;
		}
    }

    /*設置家具類別*/
    public async void cabinet()
    {
        Tag = "Cabinet"; 
        CabinetItems = new List<Item>();
        await Get(label1);
        CreateButton();  
    }
    public async void chair()
    {
        Tag = "Chair"; 
        ChairItems = new List<Item>();
        await Get(label2);
        CreateButton();   
    }
    public async void sofa()
    {
        Tag = "Sofa";
        SofaItems = new List<Item>();
        await Get(label3);
        CreateButton();

    }
    public async void table()
    {
        Tag= "Table";
        TableItems = new List<Item>();
        await Get(label4);
        CreateButton();
    }

    //載入Items
    /*void LoadItems()
    {
        var items_obj = Resources.LoadAll("Items", typeof(Item));
        foreach (var item in items_obj)
        {
            items.Add(item as Item);
        }
    }*/

    /*建立按鈕讓其實例化*/
    void CreateButton()
    {
        //運用迴圈去抓取在list item中的所有items(即是list中的button)
        //當增加button時，會進行下列code
        if(Tag=="Cabinet")
        {
            ButtonCloneCount=0;
            foreach (Item i in CabinetItems )
            {
            //讓button的prefab和圖片顯示出來    
            //b為實例化buttonContainer中的buttonPrefab
            ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
            //給予新增的button id
            b.ItemId = cabinet_id;
            //給予新增的button texture //給予這些新增的button各自的raw image
            b.ButtonTexture = i.itemImage;
            //每次新增一個新的button則id+1
            cabinet_id++;
            //依照cabinet的數量重新宣告cabinet_id，因為這樣我們才可以防止商品id持續累加
            if(cabinet_id>=1)
            {
                cabinet_id=0;
            } 
            ButtonCloneCount++; 
            }
            id = cabinet_id;
        }
        else if(Tag=="Chair")
        {
            ButtonCloneCount=0;
            foreach (Item j in ChairItems )
            {
            //b為實例化buttonContainer中的buttonPrefab
            ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
            //給予新增的button id
            b.ItemId = chair_id;
            //給予新增的button texture //給予這些新增的button各自的raw image
            b.ButtonTexture = j.itemImage;
            //每次新增一個新的button則id+1
            chair_id++; 
            if(chair_id>3)
            {
                chair_id=0;
            }
            ButtonCloneCount++;   
            }
            id = chair_id;
        }
        else if(Tag=="Sofa")
        {
            ButtonCloneCount=0;
            foreach (Item k in SofaItems )
            {
            //b為實例化buttonContainer中的buttonPrefab
            ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
            //給予新增的button id
            b.ItemId = sofa_id;
            //給予新增的button texture //給予這些新增的button各自的raw image
            b.ButtonTexture = k.itemImage;
            //每次新增一個新的button則id+1
            sofa_id++; 
            if(sofa_id>3)
            {
                sofa_id=0;
            }
            ButtonCloneCount++;  
            }
            id = sofa_id;
        }
        else if(Tag=="Table")
        {
            ButtonCloneCount=0;
            foreach (Item l in TableItems )
            {
            //b為實例化buttonContainer中的buttonPrefab
            ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
            //給予新增的button id
            b.ItemId = table_id;
            //給予新增的button texture //給予這些新增的button各自的raw image
            b.ButtonTexture = l.itemImage;
            //每次新增一個新的button則id+1
            table_id++; 
            if(table_id>1)
            {
                table_id=0;
            }
            ButtonCloneCount++;  
            }
            id = table_id;
        }
    }
    
    //設置家具在點擊時會出現的Prefab
    //這依然是在點擊button時會觸發的函式
    public void SetFurniture(int id)
    {
        //根據id顯示各自的itemPrefab
        if(Tag == "Cabinet")
        {   
            furniture = CabinetItems[id].itemPefab;
        }
        else if(Tag== "Chair")
        {
            furniture = ChairItems[id].itemPefab;      
        }
        else if(Tag == "Sofa")
        {
            furniture = SofaItems[id].itemPefab;
        } 
        else if(Tag== "Table")
        {
            furniture = TableItems[id].itemPefab;     
        }
        pid = id;
    }
    //給予權限去讀取furniture
    //在觸控時會抓到當時應該顯示的furniture
    public GameObject GetFurniture()
    {
        return furniture;
    }
    public void GetItemList()
    {
        if(Tag == "Cabinet")
        {
            if(pid==0)
            {
                if(CabinetCount1==0)
                {
                //furnutire為當時id的itemPrefab
                img = Instantiate(buttonPrefab, Selected_ItemList.transform);
                img.ButtonTexture = CabinetItems[pid].itemImage;
                Instantiate(CabinetCount1_Text, img.transform);
                //取得clne_button時出現的位置
                cabinet1_cloneid = clone_id;
                Item_CloneSet[cabinet1_cloneid]= clone_id;
                clone_id++;
                //計算ItemList中點擊的次數
                ItemList_ButtonClone++; 
                ClearItemList++;   
                }
                CabinetCount1_text = GameObject.Find("SelectedCabinet1_Count(Clone)").GetComponent<Text>();
                CabinetCount1+=1;
                CabinetCount1_text.text = "Cabinet(black)    " + CabinetCount1.ToString(); 
            }
            
               
        }
        else if(Tag== "Chair")
        {
                if(pid==0)
                {
                    if(ChairCount1==0)
                    {
                    img = Instantiate(buttonPrefab, Selected_ItemList.transform);
                    img.ButtonTexture = ChairItems[pid].itemImage;
                    Instantiate(ChairCount1_Text, img.transform);
                    chair1_cloneid = clone_id;
                    Item_CloneSet[chair1_cloneid]= chair1_cloneid;
                    clone_id++;
                    //計算ItemList中點擊的次數
                    ItemList_ButtonClone++;
                    ClearItemList++;
                    }
                    ChairCount1_text = GameObject.Find("SelectedChair1_Count(Clone)").GetComponent<Text>();
                    ChairCount1+=1;
                    ChairCount1_text.text = "Bamboo stool       " + ChairCount1.ToString();
                    
                }
                else if(pid==1)
                {
                    if(ChairCount2==0)
                    {
                    img = Instantiate(buttonPrefab, Selected_ItemList.transform);
                    img.ButtonTexture = ChairItems[pid].itemImage;
                    Instantiate(ChairCount2_Text, img.transform);
                    chair2_cloneid = clone_id;
                    Item_CloneSet[chair2_cloneid]= chair2_cloneid;
                    clone_id++;
                    //計算ItemList中點擊的次數
                    ItemList_ButtonClone++;
                    ClearItemList++;
                    }
                    ChairCount2_text = GameObject.Find("SelectedChair2_Count(Clone)").GetComponent<Text>();
                    ChairCount2+=1;
                    ChairCount2_text.text = "Knitting stool       " + ChairCount2.ToString();
                }
                else if(pid==2)
                {
                    if(ChairCount3==0)
                    {
                    img = Instantiate(buttonPrefab, Selected_ItemList.transform);
                    img.ButtonTexture = ChairItems[pid].itemImage;
                    Instantiate(ChairCount3_Text, img.transform);
                    chair3_cloneid = clone_id;
                    Item_CloneSet[chair3_cloneid]= chair3_cloneid;
                    clone_id++;
                    //計算ItemList中點擊的次數
                    ItemList_ButtonClone++;
                    ClearItemList++;
                    }
                    ChairCount3_text = GameObject.Find("SelectedChair3_Count(Clone)").GetComponent<Text>();
                    ChairCount3+=1;
                    ChairCount3_text.text = "Traditional chair       " + ChairCount3.ToString();
                }
                else if(pid==3)
                {
                    if(ChairCount4==0)
                    {
                    img = Instantiate(buttonPrefab, Selected_ItemList.transform);
                    img.ButtonTexture = ChairItems[pid].itemImage;
                    Instantiate(ChairCount4_Text, img.transform);
                    chair4_cloneid = clone_id;
                    Item_CloneSet[chair4_cloneid]= chair4_cloneid;
                    clone_id++;
                    //計算ItemList中點擊的次數
                    ItemList_ButtonClone++;
                    ClearItemList++;
                    }
                    ChairCount4_text = GameObject.Find("SelectedChair4_Count(Clone)").GetComponent<Text>();
                    ChairCount4+=1;
                    ChairCount4_text.text = "Leather chair       " + ChairCount4.ToString();
                }
    
            
        }
        else if(Tag== "Sofa")
        {
                if(pid==0)
                {
                    if(SofaCount1==0)
                    {  
                        img = Instantiate(buttonPrefab, Selected_ItemList.transform);
                        img.ButtonTexture = SofaItems[pid].itemImage; 
                        Instantiate(SofaCount1_Text, img.transform);
                        sofa1_cloneid = clone_id;
                        Item_CloneSet[sofa1_cloneid]= clone_id;
                        clone_id++;
                        //計算ItemList中點擊的次數
                        ItemList_ButtonClone++;
                        ClearItemList++;
                    }
                    SofaCount1_text = GameObject.Find("SelectedSofa1_Count(Clone)").GetComponent<Text>();
                    SofaCount1+=1;
                    SofaCount1_text.text = "Green sofa     "+SofaCount1.ToString();
                
                }
                else if(pid==1)
                {
                    if(SofaCount2==0)
                    {
                        img = Instantiate(buttonPrefab, Selected_ItemList.transform);
                        img.ButtonTexture = SofaItems[pid].itemImage; 
                        Instantiate(SofaCount2_Text, img.transform);
                        sofa2_cloneid = clone_id;
                        Item_CloneSet[sofa2_cloneid]= clone_id;
                        clone_id++;
                        //計算ItemList中點擊的次數
                        ItemList_ButtonClone++;
                        ClearItemList++;
                    }
                    SofaCount2_text = GameObject.Find("SelectedSofa2_Count(Clone)").GetComponent<Text>();
                    SofaCount2+=1;
                    SofaCount2_text.text = "Yellow sofa    "+ SofaCount2.ToString();
                } 
                else if(pid==2)
                {
                    if(SofaCount3==0)
                    {
                        img = Instantiate(buttonPrefab, Selected_ItemList.transform);
                        img.ButtonTexture = SofaItems[pid].itemImage; 
                        Instantiate(SofaCount3_Text, img.transform);
                        sofa3_cloneid = clone_id;
                        Item_CloneSet[sofa3_cloneid]= clone_id;
                        clone_id++;
                        //計算ItemList中點擊的次數
                        ItemList_ButtonClone++;
                        ClearItemList++;
                    }
                    SofaCount3_text = GameObject.Find("SelectedSofa3_Count(Clone)").GetComponent<Text>();
                    SofaCount3+=1;
                    SofaCount3_text.text = "Modern sofa    "+ SofaCount3.ToString();
                }
                else if(pid==3)
                {
                    if(SofaCount4==0)
                    {
                        img = Instantiate(buttonPrefab, Selected_ItemList.transform);
                        img.ButtonTexture = SofaItems[pid].itemImage; 
                        Instantiate(SofaCount4_Text, img.transform);
                        sofa4_cloneid = clone_id;
                        Item_CloneSet[sofa4_cloneid]= clone_id;
                        clone_id++;
                        //計算ItemList中點擊的次數
                        ItemList_ButtonClone++;
                        ClearItemList++;
                    }
                    SofaCount4_text = GameObject.Find("SelectedSofa4_Count(Clone)").GetComponent<Text>();
                    SofaCount4+=1;
                    SofaCount4_text.text = "Traditional sofa    "+ SofaCount4.ToString();
                }
            
        } 
        else if(Tag== "Table")
        {
                if(pid==0)
                {
                    
                    if(TableCount1==0)
                    {
                    img = Instantiate(buttonPrefab, Selected_ItemList.transform);
                    img.ButtonTexture = TableItems[pid].itemImage; 
                    Instantiate(TableCount1_Text, img.transform);
                    table1_cloneid = clone_id;
                    Item_CloneSet[table1_cloneid]= clone_id;
                    clone_id++;
                    //計算ItemList中點擊的次數
                    ItemList_ButtonClone++;
                    ClearItemList++;
                    }
                    TableCount1_text = GameObject.Find("SelectedTable1_Count(Clone)").GetComponent<Text>();
                    TableCount1+=1;
                    TableCount1_text.text = "Traditional table     "+TableCount1.ToString();
                }
            

                else if(pid==1)
                {
    
                    if(TableCount2==0)
                    {
                    img = Instantiate(buttonPrefab, Selected_ItemList.transform);
                    img.ButtonTexture = TableItems[pid].itemImage; 
                    Instantiate(TableCount2_Text, img.transform);
                    table2_cloneid = clone_id;
                    Item_CloneSet[table2_cloneid]= clone_id;
                    clone_id++;
                    //計算ItemList中點擊的次數
                    ItemList_ButtonClone++;
                    ClearItemList++;
                    }
                    TableCount2_text = GameObject.Find("SelectedTable2_Count(Clone)").GetComponent<Text>();
                    TableCount2+=1;
                    TableCount2_text.text = "Simple table     "+TableCount2.ToString();
                }
        }     
    }
    public void DestroyItemList(string name)
    {   
        if(name == "cabinet1")
        {
            CabinetCount1-=1;
            if(CabinetCount1==0)
            {
                Destroy(GameObject.Find("itemList Content").transform.GetChild(Item_CloneSet[cabinet1_cloneid]).gameObject);
                ClearItemList--;//此變數為判斷清單是否為空
                //將ItemList中的clone id順序交換
                for(int i=Item_CloneSet[cabinet1_cloneid] ; i<ItemList_ButtonClone ; i++)
                {
                     if(i+1<ItemList_ButtonClone)
                    {
                        Item_CloneSet[i+1]--;
                        continue;
                    }
                }
            }
            CabinetCount1_text.text = "Cabinet(black)     " + CabinetCount1.ToString();
        }
        else if(name == "chair1")
        {
            ChairCount1-=1;
            if(ChairCount1==0)
            {
                Destroy(GameObject.Find("itemList Content").transform.GetChild(Item_CloneSet[chair1_cloneid]).gameObject);
                ClearItemList--;
                for(int i=chair1_cloneid ; i<ItemList_ButtonClone-1 ; i++)
                {
                    if(i+1<ItemList_ButtonClone)
                    {
                        Item_CloneSet[i+1]=Item_CloneSet[i+1]-1;
                    }
                    
                } /*chair1_cloneid = clone_id;
                    Item_CloneSet[chair1_cloneid]= clone_id;*/
                
                //ChairCount3_text.text = Item_CloneSet[chair3_cloneid].ToString();
            }
            else
            {
                ChairCount1_text.text = "Bamboo stool        " + ChairCount1.ToString();
            }
            
        }
        else if(name == "chair2")
        {
            ChairCount2-=1;
            if(ChairCount2==0)
            {
                Destroy(GameObject.Find("itemList Content").transform.GetChild(Item_CloneSet[chair2_cloneid]).gameObject);
                ClearItemList--;
                for(int i=chair2_cloneid ; i<ItemList_ButtonClone-1 ; i++)
                {
                  
                    if(i+1<ItemList_ButtonClone)
                    {
                       Item_CloneSet[i+1]=Item_CloneSet[i+1]-1;
                    }
                    
                    
                }/*chair2_cloneid = clone_id;
                    Item_CloneSet[chair2_cloneid]= clone_id;*/
            }
            else
            {
                ChairCount2_text.text = "Knitting stool        " + ChairCount2.ToString();
            }
            
        }
        else if(name == "chair3")
        {
            
            ChairCount3-=1;
            if(ChairCount3==0)
            {
                Destroy(GameObject.Find("itemList Content").transform.GetChild(Item_CloneSet[chair3_cloneid]).gameObject); 
                ClearItemList--; 
                for(int i=chair3_cloneid ; i<ItemList_ButtonClone-1 ; i++)
                {
                    if(i+1<ItemList_ButtonClone)
                    {
                       Item_CloneSet[i+1]=Item_CloneSet[i+1]-1;
                    }
                   
                } /*chair2_cloneid = clone_id;
                    Item_CloneSet[chair2_cloneid]= clone_id;*/
            }
            else
            {
                ChairCount3_text.text = "Traditional chair        " + ChairCount3.ToString();
            }
            
        }
        else if(name == "chair4")
        {
            ChairCount4-=1;
            if(ChairCount4==0)
            {
                
                Destroy(GameObject.Find("itemList Content").transform.GetChild(Item_CloneSet[chair4_cloneid]).gameObject);
                ClearItemList--;
                for(int i=chair4_cloneid ; i<ItemList_ButtonClone-1 ; i++)
                {
                    if(i+1<ItemList_ButtonClone)
                    {
                        Item_CloneSet[i+1]=Item_CloneSet[i+1]-1;;
                    }
                    
                }
            }
            else
            {
                ChairCount4_text.text = "Leahter chair        " + ChairCount4.ToString();
            }
            
        }
        else if(name == "sofa1")
        {
            SofaCount1-=1;
            if(SofaCount1==0)
            {
                Destroy(GameObject.Find("itemList Content").transform.GetChild(Item_CloneSet[sofa1_cloneid]).gameObject);
                ClearItemList--;
                for(int i=sofa1_cloneid ; i<ItemList_ButtonClone-1 ; i++)
                {
                    if(i+1<ItemList_ButtonClone)
                    {
                        Item_CloneSet[i+1]=Item_CloneSet[i+1]-1;
                    }
                    
                }
            }
            SofaCount1_text.text = "Green sofa      "+SofaCount1.ToString();
        }
        else if(name == "sofa2")
        {
            SofaCount2-=1;
            if(SofaCount2==0)
            {
                Destroy(GameObject.Find("itemList Content").transform.GetChild(Item_CloneSet[sofa2_cloneid]).gameObject);
                ClearItemList--;
                for(int i=sofa2_cloneid ; i<ItemList_ButtonClone-1 ; i++)
                {
                    if(i+1<ItemList_ButtonClone)
                    {
                        Item_CloneSet[i+1]=Item_CloneSet[i+1]-1;
                    }
                    
                }
            }
            SofaCount2_text.text = "Yellow sofa    "+ SofaCount2.ToString();
        }
        else if(name == "sofa3")
        {
            SofaCount3-=1;
            if(SofaCount2==0)
            {
                Destroy(GameObject.Find("itemList Content").transform.GetChild(Item_CloneSet[sofa3_cloneid]).gameObject);
                ClearItemList--;
                for(int i=sofa3_cloneid ; i<ItemList_ButtonClone-1 ; i++)
                {
                    if(i+1<ItemList_ButtonClone)
                    {
                        Item_CloneSet[i+1]=Item_CloneSet[i+1]-1;
                    }
                    
                }
            }
            SofaCount3_text.text = "Modern sofa    "+ SofaCount3.ToString();
        }
        else if(name == "sofa4")
        {
            SofaCount4-=1;
            if(SofaCount4==0)
            {
                Destroy(GameObject.Find("itemList Content").transform.GetChild(Item_CloneSet[sofa4_cloneid]).gameObject);
                ClearItemList--;
                for(int i=sofa4_cloneid ; i<ItemList_ButtonClone-1 ; i++)
                {
                    if(i+1<ItemList_ButtonClone)
                    {
                        Item_CloneSet[i+1]=Item_CloneSet[i+1]-1;
                    }
                    
                }
            }
            SofaCount4_text.text = "Traditional sofa    "+ SofaCount4.ToString();
        }
        else if(name == "table1")
        {
            TableCount1-=1;
            if(TableCount1==0)
            {
                Destroy(GameObject.Find("itemList Content").transform.GetChild(Item_CloneSet[table1_cloneid]).gameObject);
                ClearItemList--;
                for(int i=table1_cloneid ; i<ItemList_ButtonClone-1 ; i++)
                {
                    if(i+1<ItemList_ButtonClone)
                    {
                        Item_CloneSet[i+1]=Item_CloneSet[i+1]-1;
                    }
                    
                }
            }
            TableCount1_text.text = "Traditional table      "+TableCount1.ToString();
        }
        else if(name == "table2")
        {
            TableCount2-=1;
            if(TableCount2==0)
            {
                Destroy(GameObject.Find("itemList Content").transform.GetChild(Item_CloneSet[table2_cloneid]).gameObject);
                ClearItemList--;
                for(int i=table2_cloneid ; i<ItemList_ButtonClone-1 ; i++)
                {
                    if(i+1<ItemList_ButtonClone)
                    {
                        Item_CloneSet[i+1]=Item_CloneSet[i+1]-1;
                    }
                    
                }
            }
            TableCount2_text.text = "Simple table      "+TableCount2.ToString();
        }
        if(ClearItemList==0)
        {
            clone_id=0;
            cabinet1_cloneid=0;
            chair1_cloneid=0;
            chair2_cloneid=0;
            chair3_cloneid=0;
            chair4_cloneid=0;
            sofa1_cloneid=0;
            sofa2_cloneid=0;
            sofa3_cloneid=0;
            sofa4_cloneid=0;
            table1_cloneid=0;
            table2_cloneid=0;
            ItemList_ButtonClone=0;
        } 
    }

    /*根據unity設置的label，從中抓取其在google cloud platform的資料*/
    public async Task Get(String label)
    {
        //找出我們要load Asset的位置(在這之前都不會執行，除非拿到load asset的位置)
        //這就是為何將這些items放到google cloud中再執行會需要等待一段時間的原因
        var locations1 =  await Addressables.LoadResourceLocationsAsync(label1).Task;
        var locations2 =  await Addressables.LoadResourceLocationsAsync(label2).Task;
        var locations3 =  await Addressables.LoadResourceLocationsAsync(label3).Task;
        var locations4 =  await Addressables.LoadResourceLocationsAsync(label4).Task;
        
        //利用上述設置的路徑來源，抓取從來源的資料至各自家具類別的List中
        if(Tag == "Cabinet")
        {
            foreach(var location1 in locations1)
            {
            var obj = await Addressables.LoadAssetAsync<Item>(location1).Task;
            //抓取每個items的obj檔(item Prefab)
            CabinetItems.Add(obj);
            }
        }
        else if(Tag == "Chair")
        {
            foreach(var location2 in locations2)
            {
            var obj = await Addressables.LoadAssetAsync<Item>(location2).Task;
            //抓取每個items的obj檔(item Prefab)
            ChairItems.Add(obj);
            }
        }
        else if(Tag == "Sofa")
        {
            foreach(var location3 in locations3)
            {
            var obj = await Addressables.LoadAssetAsync<Item>(location3).Task;
            //抓取每個items的obj檔(item Prefab)
            SofaItems.Add(obj);
            }
        }
        else if(Tag == "Table")
        {
            foreach(var location4 in locations4)
            {
            var obj = await Addressables.LoadAssetAsync<Item>(location4).Task;
            //抓取每個items的obj檔(item Prefab)
            TableItems.Add(obj);
            }
        }
        
    }

    /*設置返回鍵偵測Tag，返回需顯示的畫面*/
    public void back_btn_manager()
    {
        if(Tag == "Cabinet")
        {
            CabinetScroll.SetActive(true);
        }
        else if(Tag == "Chair")
        {
           ChairScroll.SetActive(true);
        }
        else if(Tag == "Sofa")
        {
           SofaScroll.SetActive(true);
        }
        else if(Tag == "Table")
        {
            TableScroll.SetActive(true);   
        }
        //讓Tag=null，使itemlist不會隨意去增加空的itemImage
        Tag = null;
        //讓furniture = null，使退出Scroll view時不允許再實例化家具
        //也能使在轉換家具類型時可以重新賦予furniture的itemPrefab，不受上一個家具影響
        furniture = null;
    }

    /*返回家具頁面後，需直接刪除Scroll View中Content的內容*/
    public void DeleteButton()
    {
        for(int i=0;i<ButtonCloneCount;i++)
        {
            GameObject ButtonClone = GameObject.Find("Content").transform.GetChild(i).gameObject;
            Destroy(ButtonClone);
        }
    }

    /*垃圾桶(UI)呼叫其函式刪除所有物件與清空清單列表*/
    public void DestroyPlacementObject()
    {
        //刪除ItemList中所有的家具圖表
        for(int i=0;i<ClearItemList;i++)
        {
            ItemListButtonClone = GameObject.Find("itemList Content").transform.GetChild(i).gameObject;
            GameObject.Destroy(ItemListButtonClone);   
        }
        ClearItemList=0;//將清單中的變數歸0
        //刪除tag = cabinet1的物件
        GameObject[] cabinet1_gameObjects = GameObject.FindGameObjectsWithTag("cabinet1");
        foreach(GameObject cabinet1 in cabinet1_gameObjects)
        {
            GameObject.Destroy(cabinet1);
        }
        GameObject[] chair1_gameObjects = GameObject.FindGameObjectsWithTag("chair1");
        foreach(GameObject chair1 in chair1_gameObjects)
        {
            GameObject.Destroy(chair1);
        }
        //刪除tag = chair2的物件
        GameObject[] chair2_gameObjects = GameObject.FindGameObjectsWithTag("chair2");
        foreach(GameObject chair2 in chair2_gameObjects)
        {
            GameObject.Destroy(chair2);
        }
        //刪除tag = chair3的物件
        GameObject[] chair3_gameObjects = GameObject.FindGameObjectsWithTag("chair3");
        foreach(GameObject chair3 in chair3_gameObjects)
        {
            GameObject.Destroy(chair3);
        }
        GameObject[] chair4_gameObjects = GameObject.FindGameObjectsWithTag("chair4");
        foreach(GameObject chair4 in chair4_gameObjects)
        {
            GameObject.Destroy(chair4);
        }
        //刪除tag = sofa1的物件
        GameObject[] sofa1_gameObjects = GameObject.FindGameObjectsWithTag("sofa1");
        foreach(GameObject sofa1 in sofa1_gameObjects)
        {
            GameObject.Destroy(sofa1);
        }
        //刪除tag = sofa2的物件
        GameObject[] sofa2_gameObjects = GameObject.FindGameObjectsWithTag("sofa2");
        foreach(GameObject sofa2 in sofa2_gameObjects)
        {
            GameObject.Destroy(sofa2);
        }
        //刪除tag = sofa2的物件
        GameObject[] sofa3_gameObjects = GameObject.FindGameObjectsWithTag("sofa3");
        foreach(GameObject sofa3 in sofa3_gameObjects)
        {
            GameObject.Destroy(sofa3);
        }
        //刪除tag = sofa2的物件
        GameObject[] sofa4_gameObjects = GameObject.FindGameObjectsWithTag("sofa4");
        foreach(GameObject sofa4 in sofa4_gameObjects)
        {
            GameObject.Destroy(sofa4);
        }
        //刪除tag = table1的物件
        GameObject[] table1_gameObjects = GameObject.FindGameObjectsWithTag("table1");
        foreach(GameObject table1 in table1_gameObjects)
        {
            GameObject.Destroy(table1);
        }
        //刪除tag = table2的物件
        GameObject[] table2_gameObjects = GameObject.FindGameObjectsWithTag("table2");
        foreach(GameObject table2 in table2_gameObjects)
        {
            GameObject.Destroy(table2);
        }

        CabinetCount1=0;
        ChairCount1=0;
        ChairCount2=0;
        ChairCount3=0;
        ChairCount4=0;
        SofaCount1=0;
        SofaCount2=0;
        SofaCount3=0;
        SofaCount4=0;
        TableCount1=0;
        TableCount2=0;
        clone_id=0;
        ItemList_ButtonClone=0;

        Array.Clear(Item_CloneSet,0, Item_CloneSet.Length);
        Array.Clear(cabinet1_gameObjects,0, cabinet1_gameObjects.Length);
        Array.Clear(chair1_gameObjects,0, chair1_gameObjects.Length);
        Array.Clear(chair2_gameObjects,0, chair2_gameObjects.Length);
        Array.Clear(chair3_gameObjects,0, chair3_gameObjects.Length);
        Array.Clear(chair4_gameObjects,0, chair4_gameObjects.Length);
        Array.Clear(sofa1_gameObjects,0, sofa1_gameObjects.Length);
        Array.Clear(sofa2_gameObjects,0, sofa2_gameObjects.Length);
        Array.Clear(sofa3_gameObjects,0, sofa3_gameObjects.Length);
        Array.Clear(sofa4_gameObjects,0, sofa4_gameObjects.Length);
        Array.Clear(table1_gameObjects,0, table1_gameObjects.Length);
        Array.Clear(table2_gameObjects,0, table2_gameObjects.Length);

            
    }

    /*透過Scan Ui與參數進行掃描控制*/
    public void planedetectmode()
    {
        if(planedetectstate==0)
        {
            ARSession.GetComponent<ARPlaneManager>().enabled = true;
            Scan_btn.SetTrigger("start scan");
            planedetectstate=1;
            
        }
        else
        {
            ARSession.GetComponent<ARPlaneManager>().enabled = false;
            Scan_btn.SetTrigger("stop scan");
            planedetectstate = 0;

        }
    }

 
}
