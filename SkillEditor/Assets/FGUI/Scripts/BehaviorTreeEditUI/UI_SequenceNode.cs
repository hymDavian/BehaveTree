/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_SequenceNode : GComponent
    {
        public UI_NodeBase m_base;
        public const string URL = "ui://rhbzopc2qqrub";

        public static UI_SequenceNode CreateInstance()
        {
            return (UI_SequenceNode)UIPackage.CreateObject("BehaviorTreeEditUI", "SequenceNode");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_base = (UI_NodeBase)GetChild("base");
        }
    }
}