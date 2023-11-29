using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTree.Decorator
{
    /// <summary>
    /// 随机是否运行装饰节点,百分比几率
    /// </summary>
    public class RandomNode : DecoratorNode
    {
        private readonly int _ratio = 50;
        public RandomNode(int ratio, NodeBase decoratee) : base("RandomNode", decoratee)
        {
            _ratio = ratio;
        }

        protected override void OnUpdate()
        {
            int rd = new Random().Next(0,100);
            if(rd<this._ratio)
            {
                Decoratee.Update();
            }
            else
            {
                Stop(false);
            }
        }

        protected override void OnAborted()
        {
            Decoratee.Abort();
        }

        protected override void OnChildStoped(NodeBase node, bool succeeded)
        {
            Stop(succeeded);
        }

        protected override void OnStoped()
        {
            
        }
    }
}
