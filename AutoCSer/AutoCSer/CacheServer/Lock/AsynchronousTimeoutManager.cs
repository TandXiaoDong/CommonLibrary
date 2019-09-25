﻿using System;
using System.Threading;
using System.Runtime.CompilerServices;

namespace AutoCSer.CacheServer.Lock
{
    /// <summary>
    /// 锁管理对象
    /// </summary>
    public sealed class AsynchronousTimeoutManager : ManagerBase
    {
        /// <summary>
        /// 获取锁以后的回调
        /// </summary>
        private readonly Action<ReturnValue<AsynchronousTimeoutManager>> onEnter;
        /// <summary>
        /// 申请锁
        /// </summary>
        private int enterLock;
        /// <summary>
        /// 锁管理对象
        /// </summary>
        /// <param name="node">锁节点</param>
        /// <param name="timeoutMilliseconds">锁的超时毫秒数</param>
        /// <param name="onEnter">获取锁以后的回调</param>
        internal AsynchronousTimeoutManager(DataStructure.Lock node, uint timeoutMilliseconds, Action<ReturnValue<AsynchronousTimeoutManager>> onEnter) : base(node, timeoutMilliseconds)
        {
            this.onEnter = onEnter;
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        [MethodImpl(AutoCSer.MethodImpl.AggressiveInlining)]
        internal void Error()
        {
            Step = Step.Exit;
            onEnter(default(ReturnValue<AsynchronousTimeoutManager>));
        }
        /// <summary>
        /// 申请锁回调
        /// </summary>
        /// <param name="returnParameter"></param>
        private void enter(AutoCSer.Net.TcpServer.ReturnValue<ReturnParameter> returnParameter)
        {
            if (Interlocked.CompareExchange(ref enterLock, 1, 0) == 0)
            {
                if (returnParameter.Value.Parameter.ReturnType == ReturnType.Success)
                {
                    randomNo = returnParameter.Value.Parameter.Int64.ULong;
                    timeout = Date.NowTime.Now.AddTicks(timeoutTicks - TimeSpan.TicksPerSecond);
                    if (Step == Step.None)
                    {
                        Step = Step.Lock;
                        onEnter(this);
                    }
                    else exit();
                }
                else onEnter(new ReturnValue<AsynchronousTimeoutManager> { Type = returnParameter.Value.Parameter.ReturnType, TcpReturnType = returnParameter.Type });
            }
            else if (returnParameter.Value.Parameter.ReturnType == ReturnType.Success)
            {
                randomNo = returnParameter.Value.Parameter.Int64.ULong;
                timeout = Date.NowTime.Now.AddTicks(timeoutTicks - TimeSpan.TicksPerSecond);
                exit();
            }
        }
        /// <summary>
        /// 申请锁超时处理
        /// </summary>
        private void enterTimeout()
        {
            if (Interlocked.CompareExchange(ref enterLock, 1, 0) == 0) onEnter(new ReturnValue<AsynchronousTimeoutManager> { Type = ReturnType.EnterLockTimeout });
        }
        /// <summary>
        /// 申请锁
        /// </summary>
        /// <param name="timeoutMilliseconds">申请超时毫秒数</param>
        [MethodImpl(AutoCSer.MethodImpl.AggressiveInlining)]
        internal void Enter(uint timeoutMilliseconds)
        {
            node.ClientDataStructure.Client.MasterQueryAsynchronous(node.GetEnterNode(timeoutTicks), enter);
            AutoCSer.Threading.TimerTask.Default.Add(enterTimeout, Date.NowTime.Set().AddTicks(timeoutMilliseconds * TimeSpan.TicksPerMillisecond));
        }
        /// <summary>
        /// 申请锁
        /// </summary>
        /// <param name="timeoutMilliseconds">申请超时毫秒数</param>
        [MethodImpl(AutoCSer.MethodImpl.AggressiveInlining)]
        internal void EnterStream(uint timeoutMilliseconds)
        {
            node.ClientDataStructure.Client.MasterQueryAsynchronousStream(node.GetEnterNode(timeoutTicks), enter);
            AutoCSer.Threading.TimerTask.Default.Add(enterTimeout, Date.NowTime.Set().AddTicks(timeoutMilliseconds * TimeSpan.TicksPerMillisecond));
        }
    }
}
