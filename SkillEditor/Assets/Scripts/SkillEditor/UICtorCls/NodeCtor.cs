using FairyGUI;
using FGUICode.BehaviorTreeEditUI;
using System.Collections.Generic;
using System.Diagnostics;
using Vector3 = UnityEngine.Vector3;
using Debug = UnityEngine.Debug;
using System;

namespace SkillEditor
{
    interface INodeTree
    {
        public INodeTree parent { get; set; }
        public List<INodeTree> children { get; }
        public Vector3 parentOffset { get; }
        public Vector3 childOffset { get; }
        public void refreshDrawLine();
        public void SetCtorIndex(int n);

        public ENodeType nodetype { get; }
        public Vector3 position { get; }

        public string id { get; set; }
    }

    class SetParentClickInfo
    {

        public INodeTree oldNode = null;

        private bool _want = false;
        public bool wantParent
        {
            get { return _want; }
            set
            {
                _want = value;
            }
        }

        public readonly static SetParentClickInfo ins = new SetParentClickInfo();
    }





    internal abstract class NodeCtor<T> : UICtor<UI_NodeBase>, INodeTree
        where T:GComponent
    {

       public string id { get; set; }
        private T _selfUI;
        protected T selfUI { get { return this._selfUI; } }
        private readonly GComponent canvas;
        private INodeTree _parent;
        public INodeTree parent { 
            get { return this._parent; }
            set
            {
                setParentNode(value);
            }
        }

        private readonly List<INodeTree> _children = new List<INodeTree>();
        public List<INodeTree> children { get { return this._children; } }

        public Vector3 parentOffset
        {
            get { return uiObj.m_parentPoint.position; }
        }

        public Vector3 childOffset
        {
            get { return uiObj.m_childPoint.position; }
        }

        public Vector3 position
        {
            get { return this._selfUI.position; }
        }

        public NodeCtor(T selfui, UI_NodeBase baseui,GComponent canvas) : base(baseui)
        {
            if (selfui == null) { return; }
            id = Guid.NewGuid().ToString();
            SkillEditData.allNodes.Add(this);
            this._selfUI = selfui;
            this.canvas = canvas;
            _selfUI.onDragEnd.Add(() =>
            {
                this.refreshDrawLine();
                this.parent?.refreshDrawLine();
                this.children.ForEach(c =>
                {
                    c.refreshDrawLine();
                });
            });
            baseui.m_parentBtn.onClick.Clear();
            baseui.m_parentBtn.onClick.Add(() => //父接口按钮被点击
            {
                this.onClickParentBtn();
            });
            baseui.m_childBtn.onClick.Clear();
            baseui.m_childBtn.onClick.Add(() => //子接口按钮被点击
            {
                this.onClickChildBtn();
            });
            baseui.m_deleteBtn.onClick.Add(() => //删除自身
            {
                OnDelete();//执行自定义子类清理函数
                //解除父子级关系
                this.parent = null;
                if(this.children.Count > 0)
                {
                    foreach(var child in this.children)
                    {
                        child.parent = null;
                    }
                }
                selfui.Dispose();
                SkillEditData.allNodes.Remove(this);
            });

            OnNodeInit();
        }

        protected override void OnInit()
        {
            return;
        }

        public abstract ENodeType nodetype { get; }
        protected abstract void OnNodeInit();
        protected virtual void OnDelete() { }


        private GGraph _holder = null;
        private void setParentNode(INodeTree node)
        {
            if(_parent == node) {  return; }
            var oldparent = _parent;
            if (oldparent != null)
            {
                oldparent.children.Remove(this);
            }
            if (node != null)
            {
                node.children.Add(this);
            }
            this._parent = node;
            refreshDrawLine();
        }

        public void refreshDrawLine()
        {
            if(this._parent == null)
            {
                if (this._holder != null)
                {
                    this._holder.visible = false;
                }
                return;
            }
            //有父级
            if(this._holder == null)
            {
                this._holder = new GGraph();
                this.canvas.AddChild(this._holder);
            }
            Shape shape = _holder.shape;
            LineMesh line = shape.graphics.GetMeshFactory<LineMesh>();
            line.lineWidth = 3;
            line.roundEdge = true;
            line.path.Create(GetLineParentPoints());
            shape.graphics.SetMeshDirty();
            this._holder.visible = true;
        }

        private GPathPoint[] GetLineParentPoints()
        {
            return new GPathPoint[]
            {
                new GPathPoint(this.position+parentOffset,GPathPoint.CurveType.Straight),
                new GPathPoint(parent.position+parent.childOffset,GPathPoint.CurveType.Straight),
            };
        }


        protected void cancelLine()
        {
            SetParentClickInfo.ins.oldNode.SetCtorIndex(0);
            SetParentClickInfo.ins.oldNode = null;
        }
        //点击了父链接点
        protected virtual void onClickParentBtn()
        {
            if(SetParentClickInfo.ins.oldNode != null && SetParentClickInfo.ins.oldNode == this) 
            {
                cancelLine();
                return; 
            }
            if(SetParentClickInfo.ins.oldNode == null)
            {
                SetParentClickInfo.ins.oldNode = this;
                this.SetCtorIndex(1);
                SetParentClickInfo.ins.wantParent = true;
                return;
            }
            if (!SetParentClickInfo.ins.wantParent)//想要子节点
            {
                parent = SetParentClickInfo.ins.oldNode;
                cancelLine();
            }
        }
        //点击了子链接点
        protected virtual void onClickChildBtn()
        {
            if (SetParentClickInfo.ins.oldNode != null && SetParentClickInfo.ins.oldNode == this)
            {
                cancelLine();
                return;
            }
            if (SetParentClickInfo.ins.oldNode == null)
            {
                SetParentClickInfo.ins.oldNode = this;
                this.SetCtorIndex(1);
                SetParentClickInfo.ins.wantParent = false;
                return;
            }
            if (SetParentClickInfo.ins.wantParent)//想要父节点
            {
                SetParentClickInfo.ins.oldNode.parent = this;
                cancelLine();
            }
        }

        public void SetCtorIndex(int n)
        {
            uiObj.m_isSetLine.selectedIndex = n;
        }
    }
}
