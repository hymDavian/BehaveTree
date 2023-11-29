/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_commonBtn : GButton
    {
        public GLoader m_bg;
        public const string URL = "ui://rhbzopc2qqru7";

        public static UI_commonBtn CreateInstance()
        {
            return (UI_commonBtn)UIPackage.CreateObject("BehaviorTreeEditUI", "commonBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GLoader)GetChild("bg");
        }
    }
}