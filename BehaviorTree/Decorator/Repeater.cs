using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTree.Decorator
{
    public class Repeater : DecoratorNode
    {
        private int _loopCount = -1;
        private int _curLoop = 0;
        public Repeater(int loop, NodeBase decoratee) : base("Repeater", decoratee)
        {
            this._loopCount = loop;
        }

        protected override void OnUpdate()
        {
            if (_loopCount > 0)
            {
                _curLoop = 0;
            }
            Decoratee.Update();
        }

        protected override void OnAborted()
        {
            if (Decoratee.state == ENodeState.ACTIVE)
            {
                Decoratee.Abort();
            }
            else
            {
                Stop(false);
            }
        }

        protected override void OnChildStoped(NodeBase node, bool succeeded)
        {
            if (succeeded)//子节点执行成功了！
            {
                if (_loopCount > 0)//有执行次数限制
                {
                    if (_curLoop < _loopCount)//次数还没到
                    {
                        _curLoop++;
                        Decoratee.Update();//再次执行
                        return;
                    }
                    else//全部次数成功执行完毕
                    {
                        Stop(true);
                        return;
                    }
                }
                else//没有执行次数
                {
                    Decoratee.Update();//再次执行
                    return;
                }
            }
            Stop(false);
        }

        protected override void OnStoped()
        {
            
        }
    }
}
