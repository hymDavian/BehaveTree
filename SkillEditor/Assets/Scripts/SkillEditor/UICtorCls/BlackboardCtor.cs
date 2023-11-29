using FGUICode.BehaviorTreeEditUI;
using System.Collections.Generic;

namespace SkillEditor
{
    internal class BlackboardCtor : UICtor<UI_Blackboard>
    {
        public BlackboardCtor(UI_Blackboard ui) : base(ui) { }


        private Dictionary<string, UI_BlackboardKeyItem> uidic = new Dictionary<string, UI_BlackboardKeyItem>();

        protected override void OnInit()
        {
            uiObj.m_okBtn.onClick.Add(() =>
            {
                this.setItem(uiObj.m_newInput.text, "");
                uiObj.m_newInput.text = "";
            });
            SkillEditData.onBlackboardDataRefresh += this.refreshList;
        }

        /// <summary>
        /// 刷新所有变量
        /// </summary>
        /// <param name="_dataset"></param>
        public void refreshList(KeyValuePair<string, string>[] _dataset)
        {
            this.uiObj.m_keyList.RemoveChildrenToPool();
            uidic.Clear();
            foreach (var kv in _dataset)
            {
                this.setItem(kv.Key, kv.Value);
            }
        }

        

        private void setItem(string k, string v)
        {
            SkillEditData.SetBlackboardValue(k, v);
            UI_BlackboardKeyItem item = null;
            if (uidic.ContainsKey(k))
            {
                uidic[k].m_value.text = v;
                item = uidic[k];
            }
            else
            {
                item = uiObj.m_keyList.AddItemFromPool() as UI_BlackboardKeyItem;
                item.m_key.text = k;
                item.m_value.text = v;
                item.m_closeBtn.onClick.Clear();
                item.m_closeBtn.onClick.Add(() =>
                {
                    this.removeItem(k);
                });

                item.m_value.onChanged.Add(() =>
                {
                    setItem(k, item.m_value.text);
                });

                uidic[k] = item;

            }
        }

        private void removeItem(string k)
        {
            SkillEditData.RemoveBlackboard(k);
            if (uidic.ContainsKey(k))
            {
                var ui = this.uidic[k];
                uidic.Remove(k);
                uiObj.m_keyList.RemoveChildToPool(ui);
            }
        }

    }
}
