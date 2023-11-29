using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTree
{
    /// <summary>
    /// 节点状态
    /// </summary>
    public enum ENodeState
    {
        /// <summary>
        /// 停用,如何是流程控制节点，会主动停用所有子节点
        /// </summary>
        INACTIVE,
        /// <summary>
        /// 启用
        /// </summary>
        ACTIVE,
        /// <summary>
        /// 中断停止，一般用于复合节点的中间某节点没有成功执行完毕所有节点
        /// </summary>
        STOP_REQUESTED
    }


    public enum EBlackboardKeyEvent
    {
        /// <summary>
        /// 当黑板值被添加
        /// </summary>
        ADD,
        /// <summary>
        /// 当黑板值被移除
        /// </summary>
        REMOVE,
        /// <summary>
        /// 当黑板值被修改
        /// </summary>
        CHANGE
    }
}
