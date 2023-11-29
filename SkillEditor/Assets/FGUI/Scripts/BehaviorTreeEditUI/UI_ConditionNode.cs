/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUICode.BehaviorTreeEditUI
{
    public partial class UI_ConditionNode : GComponent
    {
        public UI_NodeBase m_base;
        public GComboBox m_operatorComboBox;
        public GComboBox m_stopTypeComboBox;
        public UI_commonInput m_valueInput;
        public GComboBox m_keyCombBox;
        public const string URL = "ui://rhbzopc2qqrun";

        public static UI_ConditionNode CreateInstance()
        {
            return (UI_ConditionNode)UIPackage.CreateObject("BehaviorTreeEditUI", "ConditionNode");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_base = (UI_NodeBase)GetChild("base");
            m_operatorComboBox = (GComboBox)GetChild("operatorComboBox");
            m_stopTypeComboBox = (GComboBox)GetChild("stopTypeComboBox");
            m_valueInput = (UI_commonInput)GetChild("valueInput");
            m_keyCombBox = (GComboBox)GetChild("keyCombBox");
        }
    }
}