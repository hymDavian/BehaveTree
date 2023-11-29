/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_ActionNode : GComponent
    {
        public Controller m_editParams;
        public Controller m_editName;
        public UI_NodeBase m_base;
        public GList m_paramsList;
        public GTextField m_desFuncNameText;
        public GList m_nameList;
        public const string URL = "ui://rhbzopc2qqrud";

        public static UI_ActionNode CreateInstance()
        {
            return (UI_ActionNode)UIPackage.CreateObject("BehaviorTreeEditUI", "ActionNode");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_editParams = GetController("editParams");
            m_editName = GetController("editName");
            m_base = (UI_NodeBase)GetChild("base");
            m_paramsList = (GList)GetChild("paramsList");
            m_desFuncNameText = (GTextField)GetChild("desFuncNameText");
            m_nameList = (GList)GetChild("nameList");
        }
    }
}