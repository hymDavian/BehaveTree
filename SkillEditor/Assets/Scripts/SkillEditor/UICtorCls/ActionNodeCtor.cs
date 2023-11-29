using FairyGUI;
using FGUICode.BehaviorTreeEditUI;
using SkillEditor.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillEditor
{
    internal class ActionNodeCtor : NodeCtor<UI_ActionNode>
    {
        public override ENodeType nodetype { get { return ENodeType.ActionNodeCtor; } }
        public ActionNodeCtor(UI_ActionNode ui, GComponent canvas) : base(ui, ui.m_base, canvas) { }

        protected override void OnNodeInit()
        {
            SkillEditData.onCurrentTSClsChange += this.refreshList;
            refreshList();
            this.uiObj.m_childBtn.visible = false;
            this.uiObj.m_childPoint.visible = false;
        }

        protected override void OnDelete()
        {
            SkillEditData.onCurrentTSClsChange -= this.refreshList;
        }

        private void refreshList()
        {
            selfUI.m_nameList.RemoveChildrenToPool();
            List<FuncDescription> funcs = SkillEditData.GetCurrentClsFuncs();
            if (funcs != null)
            {
                for (int i = 0; i < funcs.Count; i++)
                {
                    var btn = selfUI.m_nameList.AddItemFromPool() as UI_listBtnNameItem;
                    var data = funcs[i];
                    btn.title = data.name;
                    btn.asButton.onClick.Clear();
                    btn.asButton.onClick.Add(() => { this.funcClick(data); });
                }
            }
        }

        private void funcClick(FuncDescription data)
        {
            this.refreshParams(data.paramsList);
            selfUI.m_desFuncNameText.text = $"{SkillEditData.currentClass}.{data.name}();";
        }

        private void refreshParams(List<string> pslist)
        {
            this.selfUI.m_paramsList.RemoveChildrenToPool();
            for (int i = 0; i < pslist.Count; i++)
            {
                var ps = pslist[i];
                var item = selfUI.m_paramsList.AddItemFromPool() as UI_actionParamItem;
                item.m_key.text = ps;
                //todo 获取此类的函数缓存参数数据值
                item.m_value.text = "";
            }
        }

        protected override void onClickChildBtn()
        {
            return;//动作节点无法设置子节点，也无法被设为父节点
        }
    }
}

