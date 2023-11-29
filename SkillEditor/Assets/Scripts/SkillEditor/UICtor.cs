using FairyGUI;
using FGUICode.BehaviorTreeEditUI;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace SkillEditor
{
    internal abstract class UICtor<T>
        where T : GComponent
    {
        private readonly T _ui;
        public T uiObj { get { return this._ui; } }
        public UICtor(T ui)
        {
            this._ui = ui;
            this.OnInit();
        }
        protected abstract void OnInit();

    }

}
