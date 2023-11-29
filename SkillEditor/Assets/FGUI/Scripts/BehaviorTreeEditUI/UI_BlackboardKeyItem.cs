/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_BlackboardKeyItem : GComponent
    {
        public GTextInput m_key;
        public GTextInput m_value;
        public UI_commonBtn m_closeBtn;
        public const string URL = "ui://rhbzopc2qqruo";

        public static UI_BlackboardKeyItem CreateInstance()
        {
            return (UI_BlackboardKeyItem)UIPackage.CreateObject("BehaviorTreeEditUI", "BlackboardKeyItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_key = (GTextInput)GetChild("key");
            m_value = (GTextInput)GetChild("value");
            m_closeBtn = (UI_commonBtn)GetChild("closeBtn");
        }
    }
}