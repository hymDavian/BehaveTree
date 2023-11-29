/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_listBtnNameItem : GButton
    {
        public GGraph m_bg;
        public const string URL = "ui://rhbzopc2qqrul";

        public static UI_listBtnNameItem CreateInstance()
        {
            return (UI_listBtnNameItem)UIPackage.CreateObject("BehaviorTreeEditUI", "listBtnNameItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GGraph)GetChild("bg");
        }
    }
}