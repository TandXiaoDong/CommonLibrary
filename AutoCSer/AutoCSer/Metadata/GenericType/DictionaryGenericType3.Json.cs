﻿using System;
using System.Collections.Generic;
using AutoCSer.Threading;
using System.Reflection;

namespace AutoCSer.Metadata
{
    /// <summary>
    /// 泛型类型元数据
    /// </summary>
    internal abstract partial class DictionaryGenericType3
    {
        /// <summary>
        /// 获取 JSON 反序列化函数信息
        /// </summary>
        internal abstract MethodInfo JsonParseDictionaryConstructorMethod { get; }
    }
    /// <summary>
    /// 泛型类型元数据
    /// </summary>
    internal sealed partial class DictionaryGenericType3<Type1, Type2, Type3> : DictionaryGenericType3
       where Type1 : IDictionary<Type2, Type3>
    {
        /// <summary>
        /// 获取 JSON 反序列化函数信息
        /// </summary>
        internal override MethodInfo JsonParseDictionaryConstructorMethod
        {
            get { return ((deSerialize)GenericType.JsonParser.dictionaryConstructor<Type1, Type2, Type3>).Method; }
        }
    }
}
