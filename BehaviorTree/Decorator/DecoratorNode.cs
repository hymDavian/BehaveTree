using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTree.Decorator
{
    public abstract class DecoratorNode:ContainerNode
    {
        private readonly List<NodeBase> _onlyDecorateeNode = new List<NodeBase>();
        private readonly NodeBase _decoratee;
        /// <summary>
        /// 装饰的目标子节点
        /// </summary>
        public NodeBase Decoratee
        {
            get { return _decoratee; }
        }
        public DecoratorNode(string name, NodeBase decoratee) : base(name, decoratee) { 
            this._decoratee = decoratee;
            this._onlyDecorateeNode.Add(this);
        }

        public override List<NodeBase> children
        {
            get
            {
                return this._onlyDecorateeNode;
            }
        }


    }
}
