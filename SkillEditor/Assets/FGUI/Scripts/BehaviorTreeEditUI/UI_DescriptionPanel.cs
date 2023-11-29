/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_DescriptionPanel : GComponent
    {
        public GTextInput m_text;
        public const string URL = "ui://rhbzopc2qqrua";

        public static UI_DescriptionPanel CreateInstance()
        {
            return (UI_DescriptionPanel)UIPackage.CreateObject("BehaviorTreeEditUI", "DescriptionPanel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_text = (GTextInput)GetChild("text");
        }
    }
}