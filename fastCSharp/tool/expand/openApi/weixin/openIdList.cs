﻿using System;

namespace fastCSharp.openApi.weixin
{
    /// <summary>
    /// 用户列表
    /// </summary>
    public sealed class openIdList : isValue
    {
        /// <summary>
        /// 拉取列表的最后一个用户的OPENID
        /// </summary>
        public string next_openid;
        /// <summary>
        /// 列表数据，OPENID的列表
        /// </summary>
        public struct openids
        {
            /// <summary>
            /// 列表数据，OPENID的列表
            /// </summary>
            public string openid;
        }
        /// <summary>
        /// 列表数据，OPENID的列表
        /// </summary>
        public openids data;
        /// <summary>
        /// 关注该公众账号的总用户数
        /// </summary>
        public int total;
        /// <summary>
        /// 拉取的OPENID个数，最大值为10000
        /// </summary>
        public int count;
    }
}
