using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTree.Composite
{
    /// <summary>
    /// 并行节点
    /// </summary>
    public class Parallel : CompositeNode
    {

        private readonly int _failCheck;//失败中断所需数量
        private readonly int _sucesCheck;//成功中断所需数量
        private int _runningCount = 0;//正在执行的数量
        private int _failCount = 0;//已经失败的数量
        private int _sucesCount = 0;//已经成功的数量
        private readonly Dictionary<NodeBase, bool> _childRets = new Dictionary<NodeBase, bool>();//子节点的执行结果
        private bool _childrenAborted = false;//并行中断标记，中间的某个动作满足了并行结束条件时触发

        /// <summary>
        /// 并行节点
        /// </summary>
        /// <param name="stopByFailNum">失败多少个就中断</param>
        /// <param name="stopBySuccNum">成功多少个就中断</param>
        /// <param name="isRoot">是否为根节点</param>
        /// <param name="nodes">持有的子节点</param>
        public Parallel(int stopByFailNum, int stopBySuccNum, params NodeBase[] nodes) : base("Parallel", nodes)
        {
            this._failCheck = stopByFailNum;
            this._sucesCheck = stopBySuccNum;
        }

        protected override void OnUpdate()
        {
            _childrenAborted = false;//标记没有中断
            _runningCount = 0;
            _sucesCount = 0;
            _failCount = 0;
            foreach (var node in this.children)
            {
                _runningCount++;
                node.Update();
            }
        }
        protected override void OnAborted()
        {
            //自身被上层中断执行了
            //遍历所有子节点进行中断
            foreach (var node in this.children)
            {
                if (node.state == ENodeState.ACTIVE)
                {
                    node.Abort();

                }
            }

        }

        protected override void OnChildStoped(NodeBase node, bool succeeded)
        {
            _runningCount--;
            _ = succeeded ? (this._sucesCount++) : (this._failCount++);
            _childRets[node] = succeeded;
            bool allFinish = _runningCount + _sucesCount + _failCount == children.Count;//是否全部子节点都已经执行过了
            if(!allFinish) { return; }//并行节点需要全部子节点执行完毕才会出结果
            if (!_childrenAborted)
            {
                if(_runningCount == 0)//全部节点都运行完毕了，此并行节点停止
                {
                    //开始计算停止结果
                    abortCheck(out bool succ);
                    Stop(succ);
                    return;
                }
                if (!_childrenAborted)//还没有子节点触发中断所有节点运行
                {
                    _childrenAborted = abortCheck(out bool succ);//本次检查结果
                    if (_childrenAborted)//有中间节点已经触发跳出并行了
                    {
                        foreach(var child in this.children)
                        {
                            if(child.state == ENodeState.ACTIVE)
                            {
                                child.Abort();
                            }
                        }
                    }
                }
            }
        }
        //检查是否需要跳出此并行节点了
        private bool abortCheck(out bool success)
        {
            bool ret = false;//默认不跳出
            success = false;//默认失败
            if (this._failCount >= this._failCheck)//先判断失败节点数是否满足
            {
                success = false;
                ret = true;
            }
            else if (this._sucesCount >= this._sucesCheck)
            {
                success = true;
                ret = true;
            }
            return ret;
        }

        public override void StopLowerPriorityChildrenForChild(NodeBase child, bool immediateRestart)
        {
            if (immediateRestart)//是否需要立即重新执行此节点
            {
                bool curNodeRet = this._childRets[child];//此节点本身的执行结果
                _runningCount++;
                if (curNodeRet)
                {
                    _sucesCount--;
                }
                else
                {
                    _failCount--;
                }
                child.Update();//此触发节点重新开始执行
            }
        }





        protected override void OnStoped()
        {
            
        }
    }
}
