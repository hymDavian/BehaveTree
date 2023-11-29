using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTree.Decorator
{
    /// <summary>
    /// 节点取相反值
    /// </summary>
    public class Inverter:DecoratorNode
    {
        public Inverter(NodeBase decoratee) : base("Inverter", decoratee) { }

        protected override void OnUpdate()
        {
            Decoratee.Update();//开始执行装饰的节点
        }

        protected override void OnAborted()
        {
            Decoratee.Abort();
        }

        protected override void OnChildStoped(NodeBase node, bool succeeded)
        {
            //返回装饰目标节点的相反值
            this.Stop(!succeeded);
        }

        protected override void OnStoped()
        {

        }
    }
}
