/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_SelectorNode : GComponent
    {
        public UI_NodeBase m_base;
        public const string URL = "ui://rhbzopc2qqru9";

        public static UI_SelectorNode CreateInstance()
        {
            return (UI_SelectorNode)UIPackage.CreateObject("BehaviorTreeEditUI", "SelectorNode");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_base = (UI_NodeBase)GetChild("base");
        }
    }
}