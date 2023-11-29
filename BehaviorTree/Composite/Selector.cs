using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BehaviorTree.Composite
{
    /// <summary>
    /// 选择节点
    /// </summary>
    public class Selector : CompositeNode
    {
        private int _curIndex = 0;
        public Selector( params NodeBase[] nodes) : base("Selector", nodes)
        {

        }

        protected override void OnUpdate()
        {
            this._curIndex = 0;
            ProccessChild();
        }

        private void ProccessChild()
        {
            if(_curIndex<this.children.Count)//还有剩余节点没有执行
            {
                if(this.state == ENodeState.STOP_REQUESTED)//自身处于中断状态
                {
                    Stop(false);
                }
                else
                {
                    this.children[this._curIndex++].Update();
                }
            }
            else //没有节点需要执行了，且没有完成的话，说明此选择节点下所有节点都返回的false
            {
                Stop(false);
            }
            
        }

        //自身被中断了，让当前正在执行的子节点中断
        protected override void OnAborted()
        {
            children[_curIndex].Abort();
        }

        //某个子节点执行完毕
        protected override void OnChildStoped(NodeBase node, bool succeeded)
        {
            this._curIndex++;
            if (succeeded)//这个子节点已经返回true了
            {
                Stop(true);
                return;
            }
            ProccessChild();//继续执行下一个子节点
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
