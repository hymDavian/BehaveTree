using FairyGUI;
using FGUICode.BehaviorTreeEditUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillEditor
{
    internal class SequenceNodeCtor : NodeCtor<UI_SequenceNode>
    {
        public override ENodeType nodetype { get { return ENodeType.SequenceNodeCtor; } }
        public SequenceNodeCtor(UI_SequenceNode ui, GComponent canvas) : base(ui, ui.m_base, canvas) { }
        protected override void OnNodeInit()
        {
            ;
        }
    }
}

