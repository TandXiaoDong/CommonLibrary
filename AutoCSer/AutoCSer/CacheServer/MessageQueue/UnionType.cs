﻿using System;
using System.Runtime.InteropServices;

namespace AutoCSer.CacheServer.MessageQueue
{
    /// <summary>
    /// 类型转换
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct UnionType
    {
        /// <summary>
        /// 目标对象
        /// </summary>
        [FieldOffset(0)]
        public object Value;
        /// <summary>
        /// 队列数据 读取配置
        /// </summary>
        [FieldOffset(0)]
        public ConsumerConfig ConsumerConfig;
        /// <summary>
        /// 消息分发 读取配置
        /// </summary>
        [FieldOffset(0)]
        public DistributionConsumerConfig DistributionConsumerConfig;
    }
}
