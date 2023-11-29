/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_Tips : GComponent
    {
        public GLoader m_bg;
        public GTextField m_title;
        public UI_commonBtn m_okBtn;
        public const string URL = "ui://rhbzopc2hvy4x";

        public static UI_Tips CreateInstance()
        {
            return (UI_Tips)UIPackage.CreateObject("BehaviorTreeEditUI", "Tips");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GLoader)GetChild("bg");
            m_title = (GTextField)GetChild("title");
            m_okBtn = (UI_commonBtn)GetChild("okBtn");
        }
    }
}