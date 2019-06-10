using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace CommonUtils.Others
{
    public static class ObservableCollect
    {

        #region 一般集合转绑定集合
        /// <summary>
        /// 一般集合转绑定集合
        /// </summary>
        /// <typeparam name="T">任意类型</typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> paras)
        {
            var tmp = new ObservableCollection<T>();
            if (paras != null && paras.Count > 0)//如果不为空 大于0就转换
            {
                for (int i = 0; i < paras.Count; i++)
                {
                    tmp.Add(paras[i]);
                }
            }
            return tmp;
        }
        #endregion

        #region 绑定集合转一般集合
        /// <summary>
        /// 绑定集合转一般集合
        /// </summary>
        /// <typeparam name="T">T类型</typeparam>
        /// <param name="paras">源集合</param>
        /// <returns>转换完成的集合</returns>
        public static List<T> ToList<T>(this ObservableCollection<T> paras)
        {
            var tmp = new List<T>();
            if (paras != null && paras.Count > 0)//如果不为空 大于0就转换
            {
                for (int i = 0; i < paras.Count; i++)
                {
                    tmp.Add(paras[i]);
                }
            }
            return tmp;
        }
        #endregion

        #region 批量添加
        /// <summary>
        /// 批量添加(倒叙)
        /// </summary>
        /// <typeparam name="T">具体类型</typeparam>
        /// <param name="lists">源集合</param>
        /// <param name="paras">需要添加的集合</param>
        /// <param name="offset">偏移量</param>
        /// <returns>对应集合</returns>
        public static ObservableCollection<T> InsertRange<T>(this ObservableCollection<T> lists, ObservableCollection<T> paras, int offset = 0)
        {
            if (paras == null || paras.Count == 0)
            {
                return lists;
            }
            for (int i = paras.Count - 1; i >= 0; i--)//倒叙插入
            {
                lists.Insert(offset, paras[i]);
            }
            return lists;
        }
        #endregion

        #region 批量添加
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T">具体类型</typeparam>
        /// <param name="lists">源集合</param>
        /// <param name="paras">需要添加的集合</param>
        /// <returns>对应集合</returns>
        public static ObservableCollection<T> AddRange<T>(this ObservableCollection<T> lists, ObservableCollection<T> paras)
        {
            if (paras == null || paras.Count == 0)
            {
                return lists;
            }
            //App.Current.Dispatcher.Invoke(()=> {
            //}) ;
            for (int i = 0; i < paras.Count; i++)
            {
                lists.Add(paras[i]);
            }
            return lists;
        }
        #endregion

        #region 绑定集合转一般集合
        /// <summary>
        /// 绑定集合转一般集合
        /// </summary>
        /// <typeparam name="T">任意类型</typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static List<T> ObservableCollectionToList<T>(ObservableCollection<T> paras)
        {
            var tmp = new List<T>();
            if (paras != null && paras.Count > 0)//如果不为空 大于0就转换
            {
                for (int i = 0; i < paras.Count; i++)
                {
                    tmp.Add(paras[i]);
                }
            }
            return tmp;
        }
        #endregion
    }
}
