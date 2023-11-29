using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTree.Decorator
{
    /// <summary>
    /// 必然返回失败
    /// </summary>
    public class Failer : DecoratorNode
    {
        public Failer(NodeBase decoratee) : base("Failer", decoratee) { }

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
            //装饰目标执行完毕了，不管返回什么，失败装饰器都返回false
            this.Stop(false);
        }

        protected override void OnStoped()
        {
            
        }
    }
}
