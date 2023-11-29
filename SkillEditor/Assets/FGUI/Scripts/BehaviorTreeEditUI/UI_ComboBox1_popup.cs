/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_ComboBox1_popup : GComponent
    {
        public GList m_list;
        public const string URL = "ui://rhbzopc2hah1r";

        public static UI_ComboBox1_popup CreateInstance()
        {
            return (UI_ComboBox1_popup)UIPackage.CreateObject("BehaviorTreeEditUI", "ComboBox1_popup");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChild("list");
        }
    }
}