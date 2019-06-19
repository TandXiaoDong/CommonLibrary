using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using sqlserverdb.Model;

namespace sqlserverdb.DAL {

	public partial class F_user : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "[dbo].[f_user]";
			internal static readonly string Field = "a.[user_id], a.[create_date], a.[email], a.[last_update_date], a.[password], a.[phone], a.[picture], a.[status], a.[user_type], a.[username]";
			internal static readonly string Sort = "a.[user_id]";
			internal static readonly string Delete = "DELETE FROM [dbo].[f_user] OUTPUT " + Field.Replace(@"a.[", @"DELETED.[") + "WHERE ";
			internal static readonly string InsertField = "[user_id], [create_date], [email], [last_update_date], [password], [phone], [picture], [status], [user_type], [username]";
			internal static readonly string InsertValues = "@user_id, @create_date, @email, @last_update_date, @password, @phone, @picture, @status, @user_type, @username";
			internal static readonly string InsertMultiFormat = "INSERT INTO [dbo].[f_user](" + InsertField + ") OUTPUT " + Field.Replace(@"a.[", @"INSERTED.[") + " VALUES{0}";
			internal static readonly string Insert = string.Format(InsertMultiFormat, $"({InsertValues})");
		}
		#endregion

		#region common call
		protected static SqlParameter[] GetParameters(F_userInfo item) {
			return new SqlParameter[] {
				new SqlParameter { ParameterName = "@user_id", SqlDbType = SqlDbType.Int, Size = 4, Value = item.User_id }, 
				new SqlParameter { ParameterName = "@create_date", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.Create_date }, 
				new SqlParameter { ParameterName = "@email", SqlDbType = SqlDbType.NChar, Size = 30, Value = item.Email }, 
				new SqlParameter { ParameterName = "@last_update_date", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.Last_update_date }, 
				new SqlParameter { ParameterName = "@password", SqlDbType = SqlDbType.NChar, Size = 10, Value = item.Password }, 
				new SqlParameter { ParameterName = "@phone", SqlDbType = SqlDbType.NChar, Size = 20, Value = item.Phone }, 
				new SqlParameter { ParameterName = "@picture", SqlDbType = SqlDbType.Binary, Size = 50, Value = item.Picture }, 
				new SqlParameter { ParameterName = "@status", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Status }, 
				new SqlParameter { ParameterName = "@user_type", SqlDbType = SqlDbType.Int, Size = 4, Value = item.User_type }, 
				new SqlParameter { ParameterName = "@username", SqlDbType = SqlDbType.NChar, Size = 10, Value = item.Username }
			};
		}
		public F_userInfo GetItem(SqlDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as F_userInfo;
		}
		public object GetItem(SqlDataReader dr, ref int dataIndex) {
			F_userInfo item = new F_userInfo();
			if (!dr.IsDBNull(++dataIndex)) item.User_id = dr.GetInt32(dataIndex); if (item.User_id == null) { dataIndex += 9; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Create_date = dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Email = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Last_update_date = dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Password = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Phone = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Picture = GetBytes(dr, dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Status = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.User_type = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Username = dr.GetString(dataIndex);
			return item;
		}
		private void CopyItemAllField(F_userInfo item, F_userInfo newitem) {
			item.User_id = newitem.User_id;
			item.Create_date = newitem.Create_date;
			item.Email = newitem.Email;
			item.Last_update_date = newitem.Last_update_date;
			item.Password = newitem.Password;
			item.Phone = newitem.Phone;
			item.Picture = newitem.Picture;
			item.Status = newitem.Status;
			item.User_type = newitem.User_type;
			item.Username = newitem.Username;
		}
		public byte[] GetBytes(SqlDataReader dr, int dataIndex) {
			if (dr.IsDBNull(dataIndex)) return null;
			var ms = new MemoryStream();
			byte[] bt = new byte[1048576 * 8];
			int size = 0;
			while ((size = (int)dr.GetBytes(dataIndex, ms.Position, bt, 0, bt.Length)) > 0) ms.Write(bt, 0, size);
			return ms.ToArray();
		}
		#endregion

		public F_userInfo Delete(int? User_id) {
			F_userInfo item = null;
			SqlHelper.ExecuteReader(dr => { item = BLL.F_user.dal.GetItem(dr); }, string.Concat(TSQL.Delete, @"[user_id] = @user_id"),
				new SqlParameter { ParameterName = "@user_id", SqlDbType = SqlDbType.Int, Size = 4, Value = User_id });
			return item;
		}

		public SqlUpdateBuild Update(F_userInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<F_userInfo> { item }, false);
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("user_id") == false) sub.SetUser_id(item.User_id);
			if (ignore.ContainsKey("create_date") == false) sub.SetCreate_date(item.Create_date);
			if (ignore.ContainsKey("email") == false) sub.SetEmail(item.Email);
			if (ignore.ContainsKey("last_update_date") == false) sub.SetLast_update_date(item.Last_update_date);
			if (ignore.ContainsKey("password") == false) sub.SetPassword(item.Password);
			if (ignore.ContainsKey("phone") == false) sub.SetPhone(item.Phone);
			if (ignore.ContainsKey("picture") == false) sub.SetPicture(item.Picture);
			if (ignore.ContainsKey("status") == false) sub.SetStatus(item.Status);
			if (ignore.ContainsKey("user_type") == false) sub.SetUser_type(item.User_type);
			if (ignore.ContainsKey("username") == false) sub.SetUsername(item.Username);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<F_userInfo> _dataSource;
			protected bool _isRefershDataSource;
			protected Dictionary<string, F_userInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<SqlParameter> _parameters = new List<SqlParameter>();
			protected Dictionary<string, Action<F_userInfo, F_userInfo>> _setAs = new Dictionary<string, Action<F_userInfo, F_userInfo>>();
			public SqlUpdateBuild(List<F_userInfo> dataSource, bool isRefershDataSource) {
				_dataSource = dataSource;
				_isRefershDataSource = isRefershDataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.User_id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"[user_id] IN ({0})", _dataSource.Select(a => a.User_id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 sqlserverdb.DAL.F_user.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " OUTPUT ", TSQL.Field.Replace(@"a.[", @"INSERTED.["), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				if (_dataSource == null || _dataSource.Any() == false || _isRefershDataSource == false) {
					var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
					BLL.F_user.RemoveCache(_dataSource);
					return affrows;
				}
				var newitems = new List<F_userInfo>();
				SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.F_user.dal.GetItem(dr)); }, sql, _parameters.ToArray());
				BLL.F_user.RemoveCache(_dataSource.Concat(newitems));
				foreach (var newitem in newitems) {
					if (_itemsDic.TryGetValue($"{newitem.User_id}", out var olditem)) foreach (var a in _setAs.Values) a(olditem, newitem);
					else {
						_dataSource.Add(newitem);
						_itemsDic.Add($"{newitem.User_id}", newitem);
					}
				}
				return newitems.Count;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				if (_dataSource == null || _dataSource.Any() == false || _isRefershDataSource == false) {
					var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
					await BLL.F_user.RemoveCacheAsync(_dataSource);
					return affrows;
				}
				var newitems = new List<F_userInfo>();
				await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.F_user.dal.GetItemAsync(dr)); }, sql, _parameters.ToArray());
				await BLL.F_user.RemoveCacheAsync(_dataSource);
				foreach (var newitem in newitems) {
					if (_itemsDic.TryGetValue($"{newitem.User_id}", out var olditem)) foreach (var a in _setAs.Values) a(olditem, newitem);
					else {
						_dataSource.Add(newitem);
						_itemsDic.Add($"{newitem.User_id}", newitem);
					}
				}
				return newitems.Count;
			}
			public SqlUpdateBuild Where(string filterFormat, params object[] values) {
				if (!string.IsNullOrEmpty(_where)) _where = string.Concat(_where, " AND ");
				_where = string.Concat(_where, "(", SqlHelper.Addslashes(filterFormat, values), ")");
				return this;
			}
			public SqlUpdateBuild WhereExists<T>(SelectBuild<T> select) {
				return this.Where($"EXISTS({select.ToString("1")})");
			}
			public SqlUpdateBuild WhereNotExists<T>(SelectBuild<T> select) {
				return this.Where($"NOT EXISTS({select.ToString("1")})");
			}

			public SqlUpdateBuild Set(string field, string value, params SqlParameter[] parms) {
				if (value.IndexOf('\'') != -1) throw new Exception("sqlserverdb.DAL.F_user.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetUser_id(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("User_id") == false) _setAs.Add("User_id", (olditem, newitem) => olditem.User_id = newitem.User_id);
				return this.Set("[user_id]", string.Concat("@user_id_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@user_id_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetUser_idIncrement(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("User_id") == false) _setAs.Add("User_id", (olditem, newitem) => olditem.User_id = newitem.User_id);
				return this.Set("[user_id]", string.Concat("[user_id] + @user_id_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@user_id_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetCreate_date(DateTime? value) {
				if (_dataSource != null && _setAs.ContainsKey("Create_date") == false) _setAs.Add("Create_date", (olditem, newitem) => olditem.Create_date = newitem.Create_date);
				return this.Set("[create_date]", string.Concat("@create_date_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@create_date_", _parameters.Count), SqlDbType = SqlDbType.DateTime, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetEmail(string value) {
				if (_dataSource != null && _setAs.ContainsKey("Email") == false) _setAs.Add("Email", (olditem, newitem) => olditem.Email = newitem.Email);
				return this.Set("[email]", string.Concat("@email_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@email_", _parameters.Count), SqlDbType = SqlDbType.NChar, Size = 30, Value = value });
			}
			public SqlUpdateBuild SetLast_update_date(DateTime? value) {
				if (_dataSource != null && _setAs.ContainsKey("Last_update_date") == false) _setAs.Add("Last_update_date", (olditem, newitem) => olditem.Last_update_date = newitem.Last_update_date);
				return this.Set("[last_update_date]", string.Concat("@last_update_date_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@last_update_date_", _parameters.Count), SqlDbType = SqlDbType.DateTime, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetPassword(string value) {
				if (_dataSource != null && _setAs.ContainsKey("Password") == false) _setAs.Add("Password", (olditem, newitem) => olditem.Password = newitem.Password);
				return this.Set("[password]", string.Concat("@password_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@password_", _parameters.Count), SqlDbType = SqlDbType.NChar, Size = 10, Value = value });
			}
			public SqlUpdateBuild SetPhone(string value) {
				if (_dataSource != null && _setAs.ContainsKey("Phone") == false) _setAs.Add("Phone", (olditem, newitem) => olditem.Phone = newitem.Phone);
				return this.Set("[phone]", string.Concat("@phone_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@phone_", _parameters.Count), SqlDbType = SqlDbType.NChar, Size = 20, Value = value });
			}
			public SqlUpdateBuild SetPicture(byte[] value) {
				if (_dataSource != null && _setAs.ContainsKey("Picture") == false) _setAs.Add("Picture", (olditem, newitem) => olditem.Picture = newitem.Picture);
				return this.Set("[picture]", string.Concat("@picture_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@picture_", _parameters.Count), SqlDbType = SqlDbType.Binary, Size = 50, Value = value });
			}
			public SqlUpdateBuild SetStatus(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("Status") == false) _setAs.Add("Status", (olditem, newitem) => olditem.Status = newitem.Status);
				return this.Set("[status]", string.Concat("@status_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@status_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetStatusFlag(int _0_16, bool isUnFlag = false) {
				if (_dataSource != null && _setAs.ContainsKey("Status") == false) _setAs.Add("Status", (olditem, newitem) => olditem.Status = newitem.Status);
				int? tmp1 = (int?)Math.Pow(2, _0_16);
				return this.Set(@"[status]", $@"COALESCE([status],0) {(isUnFlag ? '^' : '|')} @status_{_parameters.Count}", 
					new SqlParameter { ParameterName = string.Concat("@status_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = tmp1 });
			}
			public SqlUpdateBuild SetStatusUnFlag(int _0_16) {
				return this.SetStatusFlag(_0_16, true);
			}
			public SqlUpdateBuild SetUser_type(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("User_type") == false) _setAs.Add("User_type", (olditem, newitem) => olditem.User_type = newitem.User_type);
				return this.Set("[user_type]", string.Concat("@user_type_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@user_type_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetUser_typeIncrement(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("User_type") == false) _setAs.Add("User_type", (olditem, newitem) => olditem.User_type = newitem.User_type);
				return this.Set("[user_type]", string.Concat("[user_type] + @user_type_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@user_type_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetUsername(string value) {
				if (_dataSource != null && _setAs.ContainsKey("Username") == false) _setAs.Add("Username", (olditem, newitem) => olditem.Username = newitem.Username);
				return this.Set("[username]", string.Concat("@username_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@username_", _parameters.Count), SqlDbType = SqlDbType.NChar, Size = 10, Value = value });
			}
		}
		#endregion

		public F_userInfo Insert(F_userInfo item) {
			F_userInfo newitem = null;
			SqlHelper.ExecuteReader(dr => { newitem = GetItem(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		public List<F_userInfo> Insert(IEnumerable<F_userInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<F_userInfo>();
			List<F_userInfo> newitems = new List<F_userInfo>();
			SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.F_user.dal.GetItem(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		public (string sql, SqlParameter[] parms) InsertMakeParam(IEnumerable<F_userInfo> items) {
			var itemsArr = items?.Where(a => a != null).ToArray();
			if (itemsArr == null || itemsArr.Any() == false) return (null, null);
			var values = "";
			var parms = new SqlParameter[itemsArr.Length * 10];
			for (var a = 0; a < itemsArr.Length; a++) {
				var item = itemsArr[a];
				values += $",({TSQL.InsertValues.Replace(", ", a + ", ")}{a})";
				var tmparms = GetParameters(item);
				for (var b = 0; b < tmparms.Length; b++) {
					tmparms[b].ParameterName += a;
					parms[a * 10 + b] = tmparms[b];
				}
			}
			return (string.Format(TSQL.InsertMultiFormat, values.Substring(1)), parms);
		}

		#region async
		async public Task<F_userInfo> GetItemAsync(SqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as F_userInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(SqlDataReader dr, int dataIndex) {
			F_userInfo item = new F_userInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.User_id = await dr.GetFieldValueAsync<int>(dataIndex); if (item.User_id == null) { dataIndex += 9; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Create_date = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Email = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Last_update_date = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Password = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Phone = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Picture = await dr.GetFieldValueAsync<byte[]>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Status = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.User_type = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Username = await dr.GetFieldValueAsync<string>(dataIndex);
			return (item, dataIndex);
		}
		async public Task<F_userInfo> DeleteAsync(int? User_id) {
			F_userInfo item = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { item = await BLL.F_user.dal.GetItemAsync(dr); }, string.Concat(TSQL.Delete, @"[user_id] = @user_id"),
				new SqlParameter { ParameterName = "@user_id", SqlDbType = SqlDbType.Int, Size = 4, Value = User_id });
			return item;
		}
		async public Task<F_userInfo> InsertAsync(F_userInfo item) {
			F_userInfo newitem = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { newitem = await GetItemAsync(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		async public Task<List<F_userInfo>> InsertAsync(IEnumerable<F_userInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<F_userInfo>();
			List<F_userInfo> newitems = new List<F_userInfo>();
			await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.F_user.dal.GetItemAsync(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		#endregion
	}
}