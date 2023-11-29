/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_MainCanvas : GComponent
    {
        public GComponent m_nodesViewPanel;
        public UI_commonBtn m_inputTreeBtn;
        public UI_commonBtn m_outputTreeBtn;
        public UI_RightMouseMenu m_menuList;
        public UI_Blackboard m_blackboard;
        public GSlider m_sizeScroll;
        public GComboBox m_TSClassComboBox;
        public UI_commonBtn m_inputClassBtn;
        public UI_commonInput m_fileNameInput;
        public UI_Tips m_Tips;
        public const string URL = "ui://rhbzopc2qe6x0";

        public static UI_MainCanvas CreateInstance()
        {
            return (UI_MainCanvas)UIPackage.CreateObject("BehaviorTreeEditUI", "MainCanvas");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_nodesViewPanel = (GComponent)GetChild("nodesViewPanel");
            m_inputTreeBtn = (UI_commonBtn)GetChild("inputTreeBtn");
            m_outputTreeBtn = (UI_commonBtn)GetChild("outputTreeBtn");
            m_menuList = (UI_RightMouseMenu)GetChild("menuList");
            m_blackboard = (UI_Blackboard)GetChild("blackboard");
            m_sizeScroll = (GSlider)GetChild("sizeScroll");
            m_TSClassComboBox = (GComboBox)GetChild("TSClassComboBox");
            m_inputClassBtn = (UI_commonBtn)GetChild("inputClassBtn");
            m_fileNameInput = (UI_commonInput)GetChild("fileNameInput");
            m_Tips = (UI_Tips)GetChild("Tips");
        }
    }
}