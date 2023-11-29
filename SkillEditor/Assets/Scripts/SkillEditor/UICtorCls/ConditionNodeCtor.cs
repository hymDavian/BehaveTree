using FairyGUI;
using FGUICode.BehaviorTreeEditUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillEditor
{
    internal class ConditionNodeCtor : NodeCtor<UI_ConditionNode>
    {
        public override ENodeType nodetype { get { return ENodeType.ConditionNodeCtor; } }
        public ConditionNodeCtor(UI_ConditionNode ui, GComponent canvas) : base(ui, ui.m_base, canvas) { }

        protected override void OnNodeInit()
        {
            SkillEditData.onBlackboardKeysChange += OnBkKeyChanged;
            OnBkKeyChanged(SkillEditData.bkDatas.Key);
        }

        private void OnBkKeyChanged(string[] keys)
        {
            if (selfUI.m_keyCombBox.selectedIndex >=0) //有旧值
            {
                string curkey = selfUI.m_keyCombBox.items[selfUI.m_keyCombBox.selectedIndex];
                selfUI.m_keyCombBox.items = keys;
                if (!keys.Contains(curkey))//新的key列表不存在这个值了
                {
                    selfUI.m_keyCombBox.selectedIndex = -1;
                }
                else //还存在
                {
                    for(int i=0;i<keys.Length; i++)
                    {
                        if (keys[i] == curkey)
                        {
                            selfUI.m_keyCombBox.selectedIndex = i;
                            break;
                        }
                    }
                }
            }
            else
            {
                selfUI.m_keyCombBox.items = keys;
            }
        }

        protected override void OnDelete()
        {
            SkillEditData.onBlackboardKeysChange -= OnBkKeyChanged;
        }



        protected override void onClickChildBtn()
        {
            if (SetParentClickInfo.ins.oldNode != null && SetParentClickInfo.ins.oldNode == this)
            {
                cancelLine();
                return;
            }
            if (this.children.Count > 0)//如果之前有子节点
            {
                this.children[0].parent = null;
            }
            if (SetParentClickInfo.ins.oldNode == null)  //自己想链接其他节点
            {
                SetParentClickInfo.ins.oldNode = this;
                this.SetCtorIndex(1);
                SetParentClickInfo.ins.wantParent = false;
                return;
            }
            if (SetParentClickInfo.ins.wantParent)//其他节点想设置父级到自己
            {
                SetParentClickInfo.ins.oldNode.parent = this;
                cancelLine();
            }
        }
    }
}
