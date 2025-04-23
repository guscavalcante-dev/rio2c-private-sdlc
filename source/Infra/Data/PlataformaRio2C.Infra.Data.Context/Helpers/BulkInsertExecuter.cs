// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Marcelo Bernart Schmidt
// Created          : 04-17-2024
//
// Last Modified By : Marcelo Bernart Schmidt
// Last Modified On : 04-23-2024
// ***********************************************************************
// <copyright file="BulkInsertExecutor.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Internal;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Context.Helpers
{
    public class BulkInsertExecuter
    {
        private BulkInsertExecuter(string tableToMap, SqlConnection connection) { this.tableToMap = tableToMap; this.connection = connection; }
        private string tableToMap;
        private SqlConnection connection;

        public static BulkInsertExecuter Create(string tableToMap, SqlConnection connection)
        {
            return new BulkInsertExecuter(tableToMap, connection);
        }

        public void BulkInsert<T>(List<T> list)
        {
            try
            {
                using (SqlBulkCopy bulkOperation = new SqlBulkCopy(connection))
                {
                    bulkOperation.DestinationTableName = tableToMap;
                    DataTable dataTable = this.ToDataTable(list);
                    connection.Open();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        bulkOperation.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                    }
                    bulkOperation.WriteToServer(dataTable);
                }
            }
            finally
            {
                if(connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    //do not change this to dispose, as it will clear the connection string property and break the EF that is executing outside this scope
                }
            }
        }

        private bool IsValidPropertyToMap(PropertyInfo propertyInfo)
        {
            string propertyName = propertyInfo.Name.ToLower();
            Type propertyType = propertyInfo.PropertyType;
            if (propertyName == "validationresult" || propertyName == "id")
            {
                return false;
            }
            if (propertyType == typeof(string))
            {
                return true;
            }
            if (propertyInfo.PropertyType.IsClass || propertyInfo.PropertyType.IsInterface || typeof(IEnumerable).IsAssignableFrom(propertyType))
            {
                return false;
            }
            return true;
        }
        private DataTable ToDataTable<T>(List<T> list)
        {
            DataTable dataTable = new DataTable();
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (IsValidPropertyToMap(property))
                {
                    dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
                }
            }
            foreach (T item in list)
            {
                DataRow row = dataTable.NewRow();
                foreach (PropertyInfo property in properties)
                {
                    if (IsValidPropertyToMap(property))
                    {
                        object value = property.GetValue(item) ?? DBNull.Value;
                        if (property.PropertyType == typeof(Guid) && value != DBNull.Value)
                        {
                            value = value.ToString();
                        }

                        row[property.Name] = value;
                    }
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}   