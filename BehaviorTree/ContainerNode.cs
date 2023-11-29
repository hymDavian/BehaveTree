using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BehaviorTree
{
    /// <summary>
    /// 必然带有子节点的节点
    /// </summary>
    public abstract class ContainerNode:NodeBase
    {
        public ContainerNode(string name,params NodeBase[] nodes ) : base(name)
        {
            this.children.AddRange(nodes);
        }
    }
}
