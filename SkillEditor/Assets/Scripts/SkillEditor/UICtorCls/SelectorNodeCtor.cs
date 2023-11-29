using FairyGUI;
using FGUICode.BehaviorTreeEditUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillEditor
{
    internal class SelectorNodeCtor:NodeCtor<UI_SelectorNode>
    {
        public override ENodeType nodetype { get { return ENodeType.SelectorNodeCtor; } }
        public SelectorNodeCtor(UI_SelectorNode node, GComponent canvas) : base(node, node.m_base,canvas) { }

        protected override void OnNodeInit()
        {
            
        }
    }
}
