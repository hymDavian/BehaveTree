/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_NodeBase : GComponent
    {
        public Controller m_desShow;
        public Controller m_isSetLine;
        public GGraph m_dragArea;
        public GTextField m_title;
        public UI_commonBtn m_childBtn;
        public GGraph m_childPoint;
        public UI_commonBtn m_parentBtn;
        public GGraph m_parentPoint;
        public UI_commonBtn m_desBtn;
        public UI_DescriptionPanel m_desPanel;
        public UI_commonBtn m_deleteBtn;
        public const string URL = "ui://rhbzopc2qqru8";

        public static UI_NodeBase CreateInstance()
        {
            return (UI_NodeBase)UIPackage.CreateObject("BehaviorTreeEditUI", "NodeBase");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_desShow = GetController("desShow");
            m_isSetLine = GetController("isSetLine");
            m_dragArea = (GGraph)GetChild("dragArea");
            m_title = (GTextField)GetChild("title");
            m_childBtn = (UI_commonBtn)GetChild("childBtn");
            m_childPoint = (GGraph)GetChild("childPoint");
            m_parentBtn = (UI_commonBtn)GetChild("parentBtn");
            m_parentPoint = (GGraph)GetChild("parentPoint");
            m_desBtn = (UI_commonBtn)GetChild("desBtn");
            m_desPanel = (UI_DescriptionPanel)GetChild("desPanel");
            m_deleteBtn = (UI_commonBtn)GetChild("deleteBtn");
        }
    }
}