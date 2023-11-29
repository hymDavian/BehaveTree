using System;
using System.Collections.Generic;

namespace BehaviorTree
{
    /// <summary>
    /// 当黑板键变动监听
    /// </summary>
    public delegate void onBlackboardKeyLis(string key);
    /// <summary>
    /// 当黑板值变更监听
    /// </summary>
    /// <param name="before"></param>
    /// <param name="after"></param>
    public delegate void onBlackboardKeyRis(object before, object after);

    /// <summary>
    /// 黑板对象
    /// </summary>
    public class Blackboard
    {
        private readonly Dictionary<string, object> _dataset = new Dictionary<string, object>();//数据集

        /// <summary>
        /// 当key被新增时要做的事
        /// </summary>
        private readonly Dictionary<string, List<onBlackboardKeyLis>> onKeyAddEvents = new Dictionary<string, List<onBlackboardKeyLis>>();

        /// <summary>
        /// 当key被移除时要做的事
        /// </summary>
        private readonly Dictionary<string, List<onBlackboardKeyLis>> onKeyRemoveEvents = new Dictionary<string, List<onBlackboardKeyLis>>();
        /// <summary>
        /// 当值被变更时要做的事
        /// </summary>
        private readonly Dictionary<string, List<onBlackboardKeyRis>> onValueChangeEvents = new Dictionary<string, List<onBlackboardKeyRis>>();

        public object this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Set(key, value);
            }
        }

        /// <summary>
        /// 获取黑板值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            object ret = null;
            if (this._dataset.TryGetValue(key, out ret))
            {
                return ret;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 设置黑板key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, object value=null)
        {
            bool hasKey = ContainsKey(key);
            this._dataset[key] = value;
            if (hasKey)//之前有这个key
            {
                object oldVal = Get(key);
                //触发修改事件
                var events = onValueChangeEvents[key];
                foreach (var e in events)
                {
                    e.Invoke(oldVal, value);
                }
            }
            else//触发新增事件
            {
                if (!this.onValueChangeEvents.ContainsKey(key))
                {
                    onValueChangeEvents[key] = new List<onBlackboardKeyRis>();
                }
                if (!this.onKeyAddEvents.ContainsKey(key))
                {
                    onKeyAddEvents[key] = new List<onBlackboardKeyLis>();
                }
                if (!this.onKeyRemoveEvents.ContainsKey(key))
                {
                    onKeyRemoveEvents[key] = new List<onBlackboardKeyLis>();
                }

                var events = onKeyAddEvents[key];
                foreach (var e in events)
                {
                    e.Invoke(key);
                }
            }
        }
        /// <summary>
        /// 移除黑板key
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            bool haskey = ContainsKey(key);
            if (!haskey) { return; }
            var events = onKeyRemoveEvents[key];
            foreach (var e in events)
            {
                e.Invoke(key);
            }
            this._dataset.Remove(key);
            this.RemoveLisEvents(key);
        }

        private void RemoveLisEvents(string key)
        {
            if(this.onKeyRemoveEvents.TryGetValue(key, out var events1))
            {
                events1.Clear();
            }
            if(onKeyAddEvents.TryGetValue(key,out var events2))
            {
                events2.Clear();
            }
            if(onValueChangeEvents.TryGetValue(key,out var events3))
            {
                events3.Clear();
            }

        }
        /// <summary>
        /// 判断是否存在黑板key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return this._dataset.ContainsKey(key);
        }

        /// <summary>
        /// 新增某个黑板key的add回调
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        public void AddKeyCallback_onAdd(string key, onBlackboardKeyLis callback)
        {
            List<onBlackboardKeyLis> list = null;
            if (!this.onKeyAddEvents.ContainsKey(key))
            {
                list = onKeyAddEvents[key] = new List<onBlackboardKeyLis>();
            }
            if (!list.Contains(callback))
            {
                list.Add(callback);
            }
        }
        /// <summary>
        /// 新增黑板key的remove回调
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        public void AddKeyCallback_onRemove(string key,onBlackboardKeyLis callback)
        {
            List<onBlackboardKeyLis> list = null;
            if (!this.onKeyRemoveEvents.ContainsKey(key))
            {
                list = onKeyRemoveEvents[key] = new List<onBlackboardKeyLis>();
            }
            if (!list.Contains(callback))
            {
                list.Add(callback);
            }
        }
        /// <summary>
        /// 新增黑板key的change回调
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        public void AddKeyCallback_onChange(string key,onBlackboardKeyRis callback)
        {
            List<onBlackboardKeyRis> list = null;
            if (!this.onValueChangeEvents.ContainsKey(key))
            {
                list = onValueChangeEvents[key] = new List<onBlackboardKeyRis>();
            }
            if (!list.Contains(callback))
            {
                list.Add(callback);
            }
        }

    }
}
