using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using XMart.Models;

namespace XMart.Services
{
    public class LocalDatabaseHelper
	{
		readonly SQLiteAsyncConnection database;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="dbPath">数据库路径</param>
		public LocalDatabaseHelper(string dbPath)
		{
			database = new SQLiteAsyncConnection(dbPath);
		}

		#region 可见操作
		public Task<CreateTableResult> CreateUserInfo()
		{
			var result = database.CreateTableAsync<UserInfo>();
			database.CreateIndexAsync("UserInfo", "id", true);
			return result;
		}

		public Task<UserInfo> GetUserInfo()
		{
			return database.Table<UserInfo>().FirstOrDefaultAsync();
		}

		public Task<int> SaveUserInfo(UserInfo userInfo)
		{
			database.DeleteAllAsync<UserInfo>();   //先清空
			return database.InsertOrReplaceAsync(userInfo);
		}

		public Task<int> SaveCategories(List<Category> list)
		{
			//database.DeleteAllAsync<UserInfo>();   //先清空
			return database.InsertAllAsync(list);
		}

		public Task<List<Category>> GetCategories()
		{
			return database.Table<Category>().ToListAsync();
		}
		#endregion

		/*
		#region 基本操作
		/// <summary>
		/// 创建表
		/// </summary>
		/// <param name="objectName">创建对象的类名</param>
		/// <returns>Created=0;Migrated=1</returns>
		private Task<CreateTableResult> CreatTable(string objectName)
		{
			Type type = Type.GetType(objectName);
			var result = database.CreateTableAsync(type);
			return result;
		}

		/// <summary>
		/// 创建表
		/// </summary>
		/// <returns>Created=0;Migrated=1</returns>
		private Task<CreateTableResult> CreatTable()
		{
			return database.CreateTableAsync<T>();
		}

		/// <summary>
		/// 删除表
		/// </summary>
		/// <param name="objectName">创建对象的类名</param>
		/// <returns></returns>
		private Task<int> DropTable(string objectName)
		{
			Type type = Type.GetType(objectName);
			TableMapping map = new TableMapping(type);
			var result = database.DropTableAsync(map);
			return result;
		}

		/// <summary>
		/// 删除表
		/// </summary>
		/// <returns></returns>
		private Task<int> DropTable()
		{
			return database.DropTableAsync<T>();
		}

		/// <summary>
		/// 获取表中所有数据
		/// </summary>
		/// <returns></returns>
		private Task<List<T>> GetAllItems()
		{
			return database.Table<T>().ToListAsync();
		}

		private Task<List<T>> Query(string sql)
		{
			return database.QueryAsync<T>(sql);
		}
		#endregion
		*/
	}
}
