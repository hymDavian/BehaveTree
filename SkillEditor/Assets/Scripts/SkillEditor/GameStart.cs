
using FairyGUI;
using FGUICode.BehaviorTreeEditUI;
using SkillEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


        UIPackage.AddPackage("BehaviorTreeEditUI");


        BehaviorTreeEditUIBinder.BindAll();
        var mainview = UI_MainCanvas.CreateInstance();
        GRoot.inst.AddChild(mainview);
        mainview.MakeFullScreen();
        new MainCanvasUICtor(mainview);

        //comp.AddRelation(this.uiObj.m_nodesViewPanel, RelationType.Size);
        mainview.AddRelation(GRoot.inst, RelationType.Size);
    }

    // Update is called once per frame
    void Update()
    {
        if (OnUpdate!=null)
        {
            OnUpdate.Invoke();

        }
    }

    public static event Action OnUpdate;
}
