/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_RightMouseMenu : GComponent
    {
        public GList m_list;
        public const string URL = "ui://rhbzopc28cic1";

        public static UI_RightMouseMenu CreateInstance()
        {
            return (UI_RightMouseMenu)UIPackage.CreateObject("BehaviorTreeEditUI", "RightMouseMenu");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChild("list");
        }
    }
}