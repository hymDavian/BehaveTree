using FairyGUI;
using FGUICode.BehaviorTreeEditUI;
using SkillEditor.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

namespace SkillEditor
{
    internal class MainCanvasUICtor : UICtor<UI_MainCanvas>
    {
        private Dictionary<int, Func<GComponent>> createNodeFuncs = new Dictionary<int, Func<GComponent>>();
        private Dictionary<int, Func<GComponent, INodeTree>> getNodeCtorType = new Dictionary<int, Func<GComponent, INodeTree>>();
        private Vector2? lastRightClickPos = null;
        public MainCanvasUICtor(UI_MainCanvas ui) : base(ui) { }

        protected override void OnInit()
        {
            this.createNodeFuncs[0] = UI_ActionNode.CreateInstance;
            this.getNodeCtorType[0] = (comp) => { return new ActionNodeCtor(comp as UI_ActionNode, uiObj.m_nodesViewPanel); };
            this.createNodeFuncs[1] = UI_ConditionNode.CreateInstance;
            this.getNodeCtorType[1] = (comp) => { return new ConditionNodeCtor(comp as UI_ConditionNode, uiObj.m_nodesViewPanel); };
            this.createNodeFuncs[2] = UI_SelectorNode.CreateInstance;
            this.getNodeCtorType[2] = (comp) => { return new SelectorNodeCtor(comp as UI_SelectorNode, uiObj.m_nodesViewPanel); };
            this.createNodeFuncs[3] = UI_SequenceNode.CreateInstance;
            this.getNodeCtorType[3] = (comp) => { return new SequenceNodeCtor(comp as UI_SequenceNode, uiObj.m_nodesViewPanel); };

            uiObj.m_Tips.visible = false;
            uiObj.m_Tips.m_okBtn.asButton.onClick.Add(() =>
            {
                uiObj.m_Tips.visible = false;
            });

            this.uiObj.m_menuList.visible = false;//默认隐藏右键菜单
            for (int i = 0; i < this.uiObj.m_menuList.m_list.numItems; i++)
            {
                int index = i;
                var menuBtn = this.uiObj.m_menuList.m_list.GetChildAt(i).asButton;
                menuBtn.onClick.Add(() =>
                {
                    this.uiObj.m_menuList.visible = false;
                    this.createNode(index);
                });
            }

            this.uiObj.m_inputTreeBtn.onClick.Add(inputTreeClick);
            this.uiObj.m_outputTreeBtn.onClick.Add(utputTreeClick);
            this.uiObj.m_inputClassBtn.onClick.Add(inputClassClick);

            this.uiObj.onRightClick.Add(rightClick);

            setDragCamera();
            uiObj.m_TSClassComboBox.onChanged.Add(() =>
            {
                if (uiObj.m_TSClassComboBox.items.Count() > 0)
                {
                    SkillEditData.currentClass = uiObj.m_TSClassComboBox.items[uiObj.m_TSClassComboBox.selectedIndex];

                }
            });
            SkillEditData.onClassListRefresh += dic =>
            {
                if (dic.Values.Count > 0)
                {
                    uiObj.m_TSClassComboBox.items = dic.Keys.ToArray();
                }
            };

            new BlackboardCtor(uiObj.m_blackboard);
        }


        private float nodesSize = 1f;
        private void setDragCamera()
        {
            uiObj.m_nodesViewPanel.onClick.Add(() =>
            {
                uiObj.m_menuList.visible = false;
            });

            uiObj.m_nodesViewPanel.draggable = true;
            uiObj.m_nodesViewPanel.onDragStart.Add((EventContext context) =>
            {
                uiObj.m_menuList.visible = false;
                if (this.inNode)
                {
                    context.PreventDefault();
                }
            });

            Vector2 oriSize = uiObj.m_nodesViewPanel.size;
            Vector2 tempsize = Vector2.zero;
            uiObj.m_sizeScroll.onChanged.Add((EventContext context) =>
            {
                nodesSize = (float)(uiObj.m_sizeScroll.value * 0.01f);

                //nodesSize = Mathf.Clamp(nodesSize, 0.1f, 5f);
                tempsize.x = oriSize.x * nodesSize; tempsize.y = oriSize.y * nodesSize;
                uiObj.m_nodesViewPanel.SetSize(tempsize.x, tempsize.y);
            });
        }

        private void inputTreeClick(EventContext context)
        {
            string jasonPath = JsonHelper.openFileDialog(out string clsname);
            if (!String.IsNullOrEmpty(jasonPath))
            {
                var treeinfo = JsonHelper.ReadTreeInfo(jasonPath);
                if (treeinfo!=null)
                {
                    SkillEditData.currentClass = treeinfo.useCls;
                    Dictionary<string, KeyValuePair<INodeTree, EditTreeNodeInfo>> dic = new Dictionary<string, KeyValuePair<INodeTree, EditTreeNodeInfo>>();
                    for(int i = 0; i < treeinfo.nodes.Length; i++)
                    {
                        var info = treeinfo.nodes[i];
                        int type = (int)info.nodetype;
                        var pos = new Vector3() {  x=info.pos.x,y=info.pos.y,z=info.pos.z };

                        var ctor = this.createNode(type, pos);
                        ctor.id = info.id;
                        dic[info.id] = new KeyValuePair<INodeTree, EditTreeNodeInfo>(ctor,info);
                    }
                    //设置父子关系
                    foreach(var kv in dic)
                    {
                        var childrenids = kv.Value.Value.children;
                        for(int i = 0;i < childrenids.Length;i++)
                        {
                            var cid = childrenids[i];
                            dic[cid].Key.parent = kv.Value.Key;
                        }
                    }
                    SkillEditData.SetBlackboardValues(treeinfo.bkKeys,treeinfo.bkValues);
                }
            }
        }
        private void utputTreeClick(EventContext context)
        {
            string filename = String.IsNullOrEmpty(uiObj.m_fileNameInput.m_title.text) ? "newTree" : uiObj.m_fileNameInput.m_title.text;
            uiObj.m_fileNameInput.m_title.text = filename;
            JsonHelper.WriteTreeInfoToFile(filename);
            uiObj.m_Tips.visible = true;
            uiObj.m_Tips.m_title.text = "写入到:\n" + Application.streamingAssetsPath + "/" + filename + ".json";
        }

        private void inputClassClick(EventContext context)
        {
            string jasonPath = JsonHelper.openFileDialog(out string clsname);
            if (!String.IsNullOrEmpty(jasonPath))
            {
                JsonHelper.GetTSClsFunctions(clsname, jasonPath);
            }
        }

        private void rightClick(EventContext context)
        {
            FairyGUI.InputEvent d = (InputEvent)context.data;
            this.uiObj.m_menuList.position = d.position;
            this.uiObj.m_menuList.visible = true;
            Debug.Log("右键点击");
            this.lastRightClickPos = d.position;
        }

        private bool inNode = false;
        private INodeTree createNode(int index,Vector2? pos=null)
        {

            var f = this.createNodeFuncs[index];
            if (f != null)
            {
                GComponent comp = f();
                this.uiObj.AddChild(comp);

                if (pos != null)
                {
                    uiObj.m_nodesViewPanel.AddChild(comp);
                    comp.position = pos.Value;
                }
                else
                {
                    comp.position = this.lastRightClickPos.Value;
                    var offset = uiObj.m_nodesViewPanel.position;
                    uiObj.m_nodesViewPanel.AddChild(comp);
                    comp.position -= offset;
                }
      

                //comp.AddRelation(this.uiObj.m_nodesViewPanel, RelationType.Size);
                GGraph dragArea = comp.GetChild("base").asCom.GetChild("dragArea").asGraph;
                dragArea.draggable = true;
                dragArea.onDragStart.Add((EventContext context) =>
                {
                    context.PreventDefault();
                    comp.StartDrag((int)context.data);
                });

                //comp.displayObject.gameObject.transform.parent = null;

           

                comp.onFocusIn.Add(() =>
                {
                    this.inNode = true;
                });
                comp.onFocusOut.Add(() =>
                {
                    this.inNode = false;
                });
                //var tempsize = comp.size;
                //comp.SetSize(tempsize.x * nodesSize, tempsize.y * nodesSize);
                return getNodeCtorType[index].Invoke(comp);

            }
            return null;

        }
    }
}
