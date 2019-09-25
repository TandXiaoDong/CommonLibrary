﻿using System;
using System.Threading;

namespace AutoCSer.TestCase.TcpInternalStreamClientPerformance
{
    /// <summary>
    /// 客户端同步线程
    /// </summary>
    sealed class ClientTaskAsync
    {
        /// <summary>
        /// 测试客户端
        /// </summary>
        internal AutoCSer.TestCase.TcpInternalStreamServerPerformance.InternalStreamServer.TcpInternalStreamClient Client;
        /// <summary>
        /// 
        /// </summary>
        internal int Left;
        /// <summary>
        /// 
        /// </summary>
        internal int Right;
        /// <summary>
        /// 
        /// </summary>
        internal async void Run()
        {
            for (int left = Left, right = Right; right != 0;)
            {
                if ((await Client.addAsync(left, --right)).Value != left + right) ++TcpInternalStreamClientPerformance.Client.ErrorCount;
            }
            if (Interlocked.Decrement(ref TcpInternalStreamClientPerformance.Client.ThreadCount) == 0)
            {
                TcpInternalStreamClientPerformance.Client.Time.Stop();
                TcpInternalStreamClientPerformance.Client.WaitHandle.Set();
            }
        }
    }
}
