using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTree.Decorator
{
    /// <summary>
    /// 监听黑板值变化的装饰节点
    /// </summary>
    public abstract class ListenerNode :DecoratorNode
    {
        private readonly string _checkKey;
        /// <summary>
        /// 监听的黑板变量key
        /// </summary>
        public string lisKey
        {
            get { return _checkKey; }
        }
        public ListenerNode(string key,string nodeName, NodeBase decoratee) : base(nodeName, decoratee)
        {
            this._checkKey = key;
        }


    }
}
