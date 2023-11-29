using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTree
{
    /// <summary>
    /// 节点基类
    /// </summary>
    public abstract class NodeBase
    {
        private ENodeState _state = ENodeState.INACTIVE;
        /// <summary>
        /// 节点状态
        /// </summary>
        public ENodeState state { get { return _state; } }

        private NodeBase _parent = null;
        /// <summary>
        /// 父节点
        /// </summary>
        public NodeBase parent { get { return _parent; } }

        private NodeBase _root = null;
        /// <summary>
        /// 根节点
        /// </summary>
        public NodeBase root
        {
            get
            {
                if (this._root == null)
                {
                    this._root = this._GetRoot();
                }
                return _root;
            }
        }
        /// <summary>
        /// 尝试获取根节点
        /// </summary>
        /// <returns></returns>
        private NodeBase _GetRoot()
        {
            NodeBase ret = null;
            if (this.parent != null && this.parent._root != null)//先直接尝试获取自身父级的根
            {
                ret = this.parent._root;
            }
            else
            {
                NodeBase tempParent = this._parent;
                while (ret == null && tempParent != null) //循环查找最顶级的祖宗节点
                {
                    tempParent = tempParent._parent;
                }
                if (tempParent != null)
                {
                    ret = tempParent;
                }
                else
                {
                    ret = this;
                }
            }
            return ret;
        }

        private readonly List<NodeBase> _children = new List<NodeBase>();
        /// <summary>
        /// 自身的所有子节点
        /// </summary>
        public virtual List<NodeBase> children { get { return _children; } }

        private Blackboard _blackboard = null;
        /// <summary>
        /// 黑板对象
        /// </summary>
        public Blackboard blackboard
        {
            get
            {
                if (this._blackboard == null)
                {
                    if (this.root._blackboard == null)
                    {
                        this.root._blackboard = new Blackboard();

                    }
                    this._blackboard = this.root._blackboard;
                }
                return this._blackboard;
            }

        }

        /// <summary>
        /// 节点名称
        /// </summary>
        public readonly string name;
        /// <summary>
        /// 唯一ID
        /// </summary>
        public readonly string guid;

        public NodeBase(string _name)
        {
            this.name = _name;
            this.guid = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 驱动此节点
        /// </summary>
        public void Update()
        {
            this._state = ENodeState.ACTIVE;
            this.OnUpdate();
        }
        /// <summary>
        /// 当被驱动一次时调用
        /// </summary>
        protected abstract void OnUpdate();

        /// <summary>
        /// 中断执行
        /// </summary>
        public void Abort()
        {
            this._state = ENodeState.STOP_REQUESTED;
            this.OnAborted();
        }
        /// <summary>
        /// 在复合节点中被中断执行时要做的事，此时状态应该为 STOP_REQUESTED
        /// </summary>
        protected abstract void OnAborted();

        /// <summary>
        /// 停止节点
        /// </summary>
        /// <param name="succeeded"></param>
        public void Stop(bool succeeded)
        {
            if (this.children.Count > 0)
            {
                foreach (NodeBase node in this.children)
                {
                    node.OnParentStop();
                }
            }
            this._state = ENodeState.INACTIVE;
            this.OnStoped();
            if (this.parent != null)
            {
                this.parent.OnChildStoped(this, succeeded);
            }
        }
        /// <summary>
        /// 自身被停止时调用
        /// </summary>
        protected virtual void OnStoped() { }
        /// <summary>
        /// 自身的某个子节点被停止时调用
        /// </summary>
        /// <param name="node"></param>
        /// <param name="succeeded"></param>
        protected virtual void OnChildStoped(NodeBase node, bool succeeded) { }
        /// <summary>
        /// 自身父节点正在停止时调用
        /// <para>注：此时父节点状态还未变为INACTIVE</para>
        /// </summary>
        protected virtual void OnParentStop() { }

        /// <summary>
        /// 节点路径
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            if (this.parent != null)
            {
                return this.parent.GetPath() + "/" + this.name;
            }
            else
            {
                return this.name;
            }
        }

        public override string ToString()
        {
            return this.GetPath();
        }
    }
}
