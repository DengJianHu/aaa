using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour 
{
    public static UIControl Instance;
    public GameObject UI;
    UIAtlas Floor;
    UIAtlas Wall;
    UIAtlas Chair;
    UIAtlas Door;
    Material[] materials;
    Dictionary<string, UIAtlas> AllUIAtlas;
    Dictionary<string, Material> AllMaterials;
    Material currentMaterial;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        AllUIAtlas = new Dictionary<string, UIAtlas>();
        AllMaterials = new Dictionary<string, Material>();

        materials = Resources.LoadAll<Material>("Materials");
        foreach (Material m in materials)
        {
            AllMaterials.Add(m.name, m);
        }
        Floor = Resources.Load<UIAtlas>("FloorAtlas");
        AllUIAtlas.Add("Floor", Floor);
        Wall = Resources.Load<UIAtlas>("WallAtlas");
        AllUIAtlas.Add("Wall", Wall);
        Chair = Resources.Load<UIAtlas>("ChairAtlas");
        AllUIAtlas.Add("Chair", Chair);
        Door = Resources.Load<UIAtlas>("DoorAtlas");
        AllUIAtlas.Add("Door", Door);
    }
    UISlider Slider;
    string Tag;
    GameObject Obj;
    public void ShowUI(string tag,GameObject obj)
    {
        Tag = tag;
        Obj = obj;
        UI.SetActive(true);
        GameObject.Find("UICamera").transform.tag = "MainCamera";
        GameObject.Find("MainCamera").transform.tag = "Untagged";
        Slider = GameObject.Find("Slider").GetComponent<UISlider>();
        Transform Content = GameObject.Find("Content").transform;
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            Content.GetChild(i).GetComponent<UISprite>().atlas = AllUIAtlas[tag];
            Content.GetChild(i).GetComponent<UISprite>().spriteName = tag + (i + 1);
        }
        currentMaterial = AllMaterials[tag + 1];
    }
    int index = 0;
    public void OnUp()
    {
        index++;
        if (index > 4) index = 1;
        currentMaterial = AllMaterials[Tag + index];
        switch(index)
        {
            case 1: Slider.value = 0.0816f; break;
            case 2: Slider.value = 0.3869f; break;
            case 3: Slider.value = 0.6922f; break;
            case 4: Slider.value = 0.9975f; break;
        }
    }
    public void OnNext()
    {
        index--;
        if (index <= 0) index = 4;
        currentMaterial = AllMaterials[Tag + index];
        switch (index)
        {
            case 1: Slider.value = 0.0816f; break;
            case 2: Slider.value = 0.3869f; break;
            case 3: Slider.value = 0.6922f; break;
            case 4: Slider.value = 0.9975f; break;
        }
    }
    public void OnClose()
    {        
        GameObject.Find("UICamera").transform.tag = "Untagged";
        GameObject.Find("MainCamera").transform.tag = "MainCamera";
        UI.SetActive(false);
    }
    public void OnOK()
    {
        Obj.GetComponent<MeshRenderer>().material = currentMaterial;
    }
}

//克隆 第一个参数：克隆到的位置 第二个参数：需要克隆的对象
//Transform t = NGUITools.AddChild(Content.gameObject, Content.GetChild(0).gameObject).transform;
