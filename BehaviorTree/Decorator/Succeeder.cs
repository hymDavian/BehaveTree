using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTree.Decorator
{
    /// <summary>
    /// 一定会返回成功的装饰节点
    /// </summary>
    public class Succeeder:DecoratorNode
    {
        public Succeeder(NodeBase decoratee) : base("Succeeder", decoratee) { }

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
            //装饰目标执行完毕了，不管返回什么，成功装饰器都返回true
            this.Stop(true);
        }

        protected override void OnStoped()
        {

        }
    }
}
