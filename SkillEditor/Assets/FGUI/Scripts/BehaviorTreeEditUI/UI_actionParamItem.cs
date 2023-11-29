/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_actionParamItem : GComponent
    {
        public GTextField m_key;
        public GTextInput m_value;
        public const string URL = "ui://rhbzopc2qqrue";

        public static UI_actionParamItem CreateInstance()
        {
            return (UI_actionParamItem)UIPackage.CreateObject("BehaviorTreeEditUI", "actionParamItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_key = (GTextField)GetChild("key");
            m_value = (GTextInput)GetChild("value");
        }
    }
}