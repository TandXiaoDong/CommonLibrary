using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace sqlserverdb.Model {
	[JsonObject(MemberSerialization.OptIn)]
	public partial class F_userInfo {
		#region fields
		private int? _User_id;
		private DateTime? _Create_date;
		private string _Email;
		private DateTime? _Last_update_date;
		private string _Password;
		private string _Phone;
		private byte[] _Picture;
		private int? _Status;
		private int? _User_type;
		private string _Username;
		#endregion

		public F_userInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<F_user(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_User_id == null ? "null" : _User_id.ToString(), "|",
				_Create_date == null ? "null" : _Create_date.Value.Ticks.ToString(), "|",
				_Email == null ? "null" : _Email.Replace("|", StringifySplit), "|",
				_Last_update_date == null ? "null" : _Last_update_date.Value.Ticks.ToString(), "|",
				_Password == null ? "null" : _Password.Replace("|", StringifySplit), "|",
				_Phone == null ? "null" : _Phone.Replace("|", StringifySplit), "|",
				_Picture == null ? "null" : Convert.ToBase64String(_Picture), "|",
				_Status == null ? "null" : _Status.ToString(), "|",
				_User_type == null ? "null" : _User_type.ToString(), "|",
				_Username == null ? "null" : _Username.Replace("|", StringifySplit));
		}
		public static F_userInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 10, StringSplitOptions.None);
			if (ret.Length != 10) throw new Exception($"格式不正确，F_userInfo：{stringify}");
			F_userInfo item = new F_userInfo();
			if (string.Compare("null", ret[0]) != 0) item.User_id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Create_date = new DateTime(long.Parse(ret[1]));
			if (string.Compare("null", ret[2]) != 0) item.Email = ret[2].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[3]) != 0) item.Last_update_date = new DateTime(long.Parse(ret[3]));
			if (string.Compare("null", ret[4]) != 0) item.Password = ret[4].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[5]) != 0) item.Phone = ret[5].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[6]) != 0) item.Picture = Convert.FromBase64String(ret[6]);
			if (string.Compare("null", ret[7]) != 0) item.Status = int.Parse(ret[7]);
			if (string.Compare("null", ret[8]) != 0) item.User_type = int.Parse(ret[8]);
			if (string.Compare("null", ret[9]) != 0) item.Username = ret[9].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(F_userInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("User_id") ? string.Empty : string.Format(", User_id : {0}", User_id == null ? "null" : User_id.ToString()), 
				__jsonIgnore.ContainsKey("Create_date") ? string.Empty : string.Format(", Create_date : {0}", Create_date == null ? "null" : string.Concat("", Create_date.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), 
				__jsonIgnore.ContainsKey("Email") ? string.Empty : string.Format(", Email : {0}", Email == null ? "null" : string.Format("'{0}'", Email.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Last_update_date") ? string.Empty : string.Format(", Last_update_date : {0}", Last_update_date == null ? "null" : string.Concat("", Last_update_date.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), 
				__jsonIgnore.ContainsKey("Password") ? string.Empty : string.Format(", Password : {0}", Password == null ? "null" : string.Format("'{0}'", Password.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Phone") ? string.Empty : string.Format(", Phone : {0}", Phone == null ? "null" : string.Format("'{0}'", Phone.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Picture") ? string.Empty : string.Format(", Picture : {0}", Picture == null ? "null" : Convert.ToBase64String(Picture)), 
				__jsonIgnore.ContainsKey("Status") ? string.Empty : string.Format(", Status : {0}", Status == null ? "null" : Status.ToString()), 
				__jsonIgnore.ContainsKey("User_type") ? string.Empty : string.Format(", User_type : {0}", User_type == null ? "null" : User_type.ToString()), 
				__jsonIgnore.ContainsKey("Username") ? string.Empty : string.Format(", Username : {0}", Username == null ? "null" : string.Format("'{0}'", Username.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (!__jsonIgnore.ContainsKey("User_id")) ht["User_id"] = User_id;
			if (!__jsonIgnore.ContainsKey("Create_date")) ht["Create_date"] = Create_date;
			if (!__jsonIgnore.ContainsKey("Email")) ht["Email"] = Email;
			if (!__jsonIgnore.ContainsKey("Last_update_date")) ht["Last_update_date"] = Last_update_date;
			if (!__jsonIgnore.ContainsKey("Password")) ht["Password"] = Password;
			if (!__jsonIgnore.ContainsKey("Phone")) ht["Phone"] = Phone;
			if (!__jsonIgnore.ContainsKey("Picture")) ht["Picture"] = Picture;
			if (!__jsonIgnore.ContainsKey("Status")) ht["Status"] = Status;
			if (!__jsonIgnore.ContainsKey("User_type")) ht["User_type"] = User_type;
			if (!__jsonIgnore.ContainsKey("Username")) ht["Username"] = Username;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public int? User_id {
			get { return _User_id; }
			set { _User_id = value; }
		}

		[JsonProperty] public DateTime? Create_date {
			get { return _Create_date; }
			set { _Create_date = value; }
		}

		[JsonProperty] public string Email {
			get { return _Email; }
			set { _Email = value; }
		}

		[JsonProperty] public DateTime? Last_update_date {
			get { return _Last_update_date; }
			set { _Last_update_date = value; }
		}

		[JsonProperty] public string Password {
			get { return _Password; }
			set { _Password = value; }
		}

		[JsonProperty] public string Phone {
			get { return _Phone; }
			set { _Phone = value; }
		}

		[JsonProperty] public byte[] Picture {
			get { return _Picture; }
			set { _Picture = value; }
		}

		/// <summary>
		/// 0-离线，1-在线
		/// </summary>
		[JsonProperty] public int? Status {
			get { return _Status; }
			set { _Status = value; }
		}

		/// <summary>
		/// 0-普通用户，1-管理员，2-游客
		/// </summary>
		[JsonProperty] public int? User_type {
			get { return _User_type; }
			set { _User_type = value; }
		}

		[JsonProperty] public string Username {
			get { return _Username; }
			set { _Username = value; }
		}

		#endregion

		public sqlserverdb.DAL.F_user.SqlUpdateBuild UpdateDiy => _User_id == null ? null : BLL.F_user.UpdateDiy(new List<F_userInfo> { this });

		#region sync methods

		public F_userInfo Save() {
			if (this.User_id != null) {
				if (BLL.F_user.Update(this) == 0) return BLL.F_user.Insert(this);
				return this;
			}
			return BLL.F_user.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<F_userInfo> SaveAsync() {
			if (this.User_id != null) {
				if (await BLL.F_user.UpdateAsync(this) == 0) return await BLL.F_user.InsertAsync(this);
				return this;
			}
			return await BLL.F_user.InsertAsync(this);
		}
		#endregion
	}
}
