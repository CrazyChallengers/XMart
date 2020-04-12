using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace XMart.Services
{
    public class LocalDatabaseHelper
    {
		readonly SQLiteAsyncConnection database;

		public LocalDatabaseHelper(string dbPath)
		{
			database = new SQLiteAsyncConnection(dbPath);
			//database.CreateTableAsync<TodoItem>().Wait();
		}
	}
}
