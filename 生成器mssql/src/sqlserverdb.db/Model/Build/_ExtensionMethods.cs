using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using sqlserverdb.Model;

public static partial class sqlserverdbExtensionMethods {

	public static string ToJson(this F_userInfo item) => string.Concat(item);
	public static string ToJson(this F_userInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<F_userInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this F_userInfo[] items, Func<F_userInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<F_userInfo> items, Func<F_userInfo, object> func = null) => GetBson(items, func);
	public static sqlserverdb.DAL.F_user.SqlUpdateBuild UpdateDiy(this List<F_userInfo> items) => sqlserverdb.BLL.F_user.UpdateDiy(items);

	public static string GetJson(IEnumerable items) {
		StringBuilder sb = new StringBuilder();
		sb.Append("[");
		IEnumerator ie = items.GetEnumerator();
		if (ie.MoveNext()) {
			while (true) {
				sb.Append(string.Concat(ie.Current));
				if (ie.MoveNext()) sb.Append(",");
				else break;
			}
		}
		sb.Append("]");
		return sb.ToString();
	}
	public static IDictionary[] GetBson(IEnumerable items, Delegate func = null) {
		List<IDictionary> ret = new List<IDictionary>();
		IEnumerator ie = items.GetEnumerator();
		while (ie.MoveNext()) {
			if (ie.Current == null) ret.Add(null);
			else if (func == null) ret.Add(ie.Current.GetType().GetMethod("ToBson").Invoke(ie.Current, new object[] { false }) as IDictionary);
			else {
				object obj = func.GetMethodInfo().Invoke(func.Target, new object[] { ie.Current });
				if (obj is IDictionary) ret.Add(obj as IDictionary);
				else {
					Hashtable ht = new Hashtable();
					PropertyInfo[] pis = obj.GetType().GetProperties();
					foreach (PropertyInfo pi in pis) ht[pi.Name] = pi.GetValue(obj);
					ret.Add(ht);
				}
			}
		}
		return ret.ToArray();
	}
}