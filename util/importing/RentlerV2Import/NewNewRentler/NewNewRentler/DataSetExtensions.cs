using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NewNewRentler
{
	public static class DataSetConverter
	{
		public static DataSet ToDataSet<T>(this IList<T> list, string tableName)
		{
			Type elementType = typeof(T);
			DataSet ds = new DataSet();
			DataTable t = new DataTable();
			t.TableName = tableName;
			ds.Tables.Add(t);

			//grab all the column types first
			var props = elementType.GetProperties();
			var columns = new List<string>();

			//add a column to table for each public property on T
			foreach(var propInfo in props)
			{
				Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

				if(ColType.Name == "EntityKey" ||
					ColType.Name == "EntityName")
					continue;

				if(propInfo.PropertyType.IsPrimitive ||
					ColType == typeof(string) ||
					ColType == typeof(Int32) ||
					ColType == typeof(Int64) ||
					ColType == typeof(Guid) ||
					ColType == typeof(Guid?) ||
					ColType == typeof(DateTime) ||
					ColType == typeof(DateTime?) ||
					ColType == typeof(decimal) ||
					ColType == typeof(float) ||
					ColType == typeof(float?))
				{
					columns.Add(propInfo.Name);
					t.Columns.Add(propInfo.Name, ColType);
				}
			}

			//go through each property on T and add each value to the table
			foreach(T item in list)
			{
				DataRow row = t.NewRow();

				foreach(var propInfo in props)
				{
					//filter out anything but primitives and Guid types.

					if(columns.Contains(propInfo.Name))
						row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
				}

				t.Rows.Add(row);
			}

			return ds;
		}
	}
}
