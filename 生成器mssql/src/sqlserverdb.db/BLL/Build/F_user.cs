using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using sqlserverdb.Model;

namespace sqlserverdb.BLL {

	public partial class F_user {

		internal static readonly sqlserverdb.DAL.F_user dal = new sqlserverdb.DAL.F_user();
		internal static readonly int itemCacheTimeout;

		static F_user() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_F_user"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static F_userInfo Delete(int User_id) {
			var item = dal.Delete(User_id);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}

		#region enum _
		public enum _ {
			User_id = 1, 
			Create_date, 
			Email, 
			Last_update_date, 
			Password, 
			Phone, 
			Picture, 
			/// <summary>
			/// 0-离线，1-在线
			/// </summary>
			Status, 
			/// <summary>
			/// 0-普通用户，1-管理员，2-游客
			/// </summary>
			User_type, 
			Username
		}
		#endregion

		public static int Update(F_userInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(F_userInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static sqlserverdb.DAL.F_user.SqlUpdateBuild UpdateDiy(int User_id) => new sqlserverdb.DAL.F_user.SqlUpdateBuild(new List<F_userInfo> { new F_userInfo { User_id = User_id } }, false);
		public static sqlserverdb.DAL.F_user.SqlUpdateBuild UpdateDiy(List<F_userInfo> dataSource) => new sqlserverdb.DAL.F_user.SqlUpdateBuild(dataSource, true);
		public static sqlserverdb.DAL.F_user.SqlUpdateBuild UpdateDiyDangerous => new sqlserverdb.DAL.F_user.SqlUpdateBuild();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 F_user.Insert(F_userInfo item)
		/// </summary>
		[Obsolete]
		public static F_userInfo Insert(int? User_id, DateTime? Create_date, string Email, DateTime? Last_update_date, string Password, string Phone, byte[] Picture, int? Status, int? User_type, string Username) {
			return Insert(new F_userInfo {
				User_id = User_id, 
				Create_date = Create_date, 
				Email = Email, 
				Last_update_date = Last_update_date, 
				Password = Password, 
				Phone = Phone, 
				Picture = Picture, 
				Status = Status, 
				User_type = User_type, 
				Username = Username});
		}
		public static F_userInfo Insert(F_userInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		public static List<F_userInfo> Insert(IEnumerable<F_userInfo> items) {
			var newitems = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(newitems);
			return newitems;
		}
		internal static void RemoveCache(F_userInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<F_userInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("sqlserverdb_BLL:F_user:", item.User_id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static F_userInfo GetItem(int User_id) => SqlHelper.CacheShell(string.Concat("sqlserverdb_BLL:F_user:", User_id), itemCacheTimeout, () => Select.WhereUser_id(User_id).ToOne());

		public static List<F_userInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<F_userInfo> DeleteAsync(int User_id) {
			var item = await dal.DeleteAsync(User_id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<F_userInfo> GetItemAsync(int User_id) => await SqlHelper.CacheShellAsync(string.Concat("sqlserverdb_BLL:F_user:", User_id), itemCacheTimeout, () => Select.WhereUser_id(User_id).ToOneAsync());
		public static Task<int> UpdateAsync(F_userInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(F_userInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 F_user.Insert(F_userInfo item)
		/// </summary>
		[Obsolete]
		public static Task<F_userInfo> InsertAsync(int? User_id, DateTime? Create_date, string Email, DateTime? Last_update_date, string Password, string Phone, byte[] Picture, int? Status, int? User_type, string Username) {
			return InsertAsync(new F_userInfo {
				User_id = User_id, 
				Create_date = Create_date, 
				Email = Email, 
				Last_update_date = Last_update_date, 
				Password = Password, 
				Phone = Phone, 
				Picture = Picture, 
				Status = Status, 
				User_type = User_type, 
				Username = Username});
		}
		async public static Task<F_userInfo> InsertAsync(F_userInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<List<F_userInfo>> InsertAsync(IEnumerable<F_userInfo> items) {
			var newitems = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(newitems);
			return newitems;
		}
		internal static Task RemoveCacheAsync(F_userInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<F_userInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("sqlserverdb_BLL:F_user:", item.User_id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<F_userInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<F_userInfo, SelectBuild> {
			public SelectBuild WhereUser_id(params int[] User_id) => this.Where1Or(@"a.[user_id] = {0}", User_id);
			public SelectBuild WhereUser_idRange(int? begin) => base.Where(@"a.[user_id] >= {0}", begin);
			public SelectBuild WhereUser_idRange(int? begin, int? end) => end == null ? this.WhereUser_idRange(begin) : base.Where(@"a.[user_id] between {0} and {1}", begin, end);
			public SelectBuild WhereCreate_dateRange(DateTime? begin) => base.Where(@"a.[create_date] >= {0}", begin);
			public SelectBuild WhereCreate_dateRange(DateTime? begin, DateTime? end) => end == null ? this.WhereCreate_dateRange(begin) : base.Where(@"a.[create_date] between {0} and {1}", begin, end);
			public SelectBuild WhereEmail(params string[] Email) => this.Where1Or(@"a.[email] = {0}", Email);
			public SelectBuild WhereEmailLike(string pattern, bool isNotLike = false) => this.Where($@"a.[email] {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereLast_update_dateRange(DateTime? begin) => base.Where(@"a.[last_update_date] >= {0}", begin);
			public SelectBuild WhereLast_update_dateRange(DateTime? begin, DateTime? end) => end == null ? this.WhereLast_update_dateRange(begin) : base.Where(@"a.[last_update_date] between {0} and {1}", begin, end);
			public SelectBuild WherePassword(params string[] Password) => this.Where1Or(@"a.[password] = {0}", Password);
			public SelectBuild WherePasswordLike(string pattern, bool isNotLike = false) => this.Where($@"a.[password] {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WherePhone(params string[] Phone) => this.Where1Or(@"a.[phone] = {0}", Phone);
			public SelectBuild WherePhoneLike(string pattern, bool isNotLike = false) => this.Where($@"a.[phone] {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			/// <summary>
			/// 0-离线，1-在线，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereStatus(params int?[] Status) => this.Where1Or(@"a.[status] = {0}", Status);
			public SelectBuild WhereStatusRange(int? begin) => base.Where(@"a.[status] >= {0}", begin);
			public SelectBuild WhereStatusRange(int? begin, int? end) => end == null ? this.WhereStatusRange(begin) : base.Where(@"a.[status] between {0} and {1}", begin, end);
			/// <summary>
			/// 0-普通用户，1-管理员，2-游客，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereUser_type(params int?[] User_type) => this.Where1Or(@"a.[user_type] = {0}", User_type);
			public SelectBuild WhereUser_typeRange(int? begin) => base.Where(@"a.[user_type] >= {0}", begin);
			public SelectBuild WhereUser_typeRange(int? begin, int? end) => end == null ? this.WhereUser_typeRange(begin) : base.Where(@"a.[user_type] between {0} and {1}", begin, end);
			public SelectBuild WhereUsername(params string[] Username) => this.Where1Or(@"a.[username] = {0}", Username);
			public SelectBuild WhereUsernameLike(string pattern, bool isNotLike = false) => this.Where($@"a.[username] {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}