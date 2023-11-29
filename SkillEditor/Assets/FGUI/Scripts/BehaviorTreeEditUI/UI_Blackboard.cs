/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_Blackboard : GComponent
    {
        public Controller m_newKeyInput;
        public GList m_keyList;
        public UI_commonBtn m_addBtn;
        public GTextInput m_newInput;
        public UI_commonBtn m_okBtn;
        public UI_commonBtn m_noBtn;
        public const string URL = "ui://rhbzopc2hah1p";

        public static UI_Blackboard CreateInstance()
        {
            return (UI_Blackboard)UIPackage.CreateObject("BehaviorTreeEditUI", "Blackboard");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_newKeyInput = GetController("newKeyInput");
            m_keyList = (GList)GetChild("keyList");
            m_addBtn = (UI_commonBtn)GetChild("addBtn");
            m_newInput = (GTextInput)GetChild("newInput");
            m_okBtn = (UI_commonBtn)GetChild("okBtn");
            m_noBtn = (UI_commonBtn)GetChild("noBtn");
        }
    }
}