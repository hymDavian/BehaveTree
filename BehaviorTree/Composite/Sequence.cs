using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BehaviorTree.Composite
{
    public class Sequence : CompositeNode
    {
        private int _curIndex = 0;
        public Sequence( params NodeBase[] nodes) : base("Sequence", nodes) { }

        protected override void OnUpdate()
        {
            this._curIndex = 0;
            ProccessChild();
        }
        private void ProccessChild()
        {
            if(_curIndex<this.children.Count)
            {
                if (this.state == ENodeState.STOP_REQUESTED)//自身处于中断状态
                {
                    Stop(false);
                }
                else
                {
                    this.children[this._curIndex++].Update();
                }
            }
            else //全部子节点执行完毕，返回成功
            {
                Stop(true );
            }
        }

        protected override void OnChildStoped(NodeBase node, bool succeeded)
        {
            if(!succeeded)//某个节点执行失败了
            {
                Stop(false);
            }
            else
            {
                ProccessChild();
            }
        }

        protected override void OnAborted()
        {
            children[_curIndex].Abort();
        }

        protected override void OnStoped()
        {
        }

        public override void StopLowerPriorityChildrenForChild(NodeBase abortForChild, bool immediateRestart)
        {
            int indexForChild = 0;
            bool found = false;
            foreach (NodeBase currentChild in children)
            {
                if (currentChild == abortForChild)
                {
                    found = true;
                }
                else if (!found)
                {
                    indexForChild++;
                }
                else if (found && currentChild.state == ENodeState.ACTIVE)
                {
                    if (immediateRestart)
                    {
                        _curIndex = indexForChild - 1;
                    }
                    else
                    {
                        _curIndex = children.Count;
                    }
                    currentChild.Abort();
                    break;
                }
            }
        }
    }
}
