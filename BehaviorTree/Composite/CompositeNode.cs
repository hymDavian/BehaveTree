using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BehaviorTree.Composite
{
    /// <summary>
    /// 复合控制节点
    /// </summary>
    public abstract class CompositeNode:ContainerNode
    {
        public CompositeNode(string name, NodeBase[] nodes):base(name, nodes) { }

        /// <summary>
        /// 子节点请求关闭所有子节点时要做的事情
        /// </summary>
        /// <param name="child">请求节点</param>
        /// <param name="immediateRestart">请求的操作 是否需要重新启用这个触发的子节点</param>
        public abstract void StopLowerPriorityChildrenForChild(NodeBase child, bool immediateRestart);


    }
}
