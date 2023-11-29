using BehaviorTree.Composite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTree.Decorator.BlackboardEventNode
{
    /// <summary>
    /// 装饰节点，当黑板的某个键被添加后，对这个节点做中断操作
    /// </summary>
    public class OnBlackboardKeyAddAbort : ListenerNode
    {
        //触发中断后是否需要重启此节点树
        private readonly bool _reStart = false;
        private bool isListening = false;
        public OnBlackboardKeyAddAbort(string key, NodeBase decoratee, bool reStart) : base(key, "OnBlackboardKeyAddAbort", decoratee)
        {
            this._reStart = reStart;
            
        }


        private void AbortTargetNode(string key)
        {
            if (key != this.lisKey)//不是自身监听的key被新增了
            {
                return;
            }
            else
            {

                if (!_reStart)//并不需要重新开始
                {
                    Decoratee.Abort();//只进行中断
                }
                else //需要重启
                {
                    NodeBase parent = this.parent;
                    NodeBase child = this;
                    while(parent != null && !(parent is CompositeNode))//父级不是复合节点
                    {
                        //继续往上找
                        child = parent;
                        parent = parent.parent;
                    }
                    (parent as CompositeNode).StopLowerPriorityChildrenForChild(child, true);
                }
            }
        }

        protected override void OnUpdate()
        {
            this.blackboard.AddKeyCallback_onAdd(this.lisKey, this.AbortTargetNode);
        }

        protected override void OnAborted()
        {
            
        }
    }
}
