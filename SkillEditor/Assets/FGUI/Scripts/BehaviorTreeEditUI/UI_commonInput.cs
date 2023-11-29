/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_commonInput : GComponent
    {
        public GLoader m_icon;
        public GTextInput m_title;
        public const string URL = "ui://rhbzopc2hah1t";

        public static UI_commonInput CreateInstance()
        {
            return (UI_commonInput)UIPackage.CreateObject("BehaviorTreeEditUI", "commonInput");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_icon = (GLoader)GetChild("icon");
            m_title = (GTextInput)GetChild("title");
        }
    }
}