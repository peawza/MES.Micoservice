using Npgsql;
using System.Data;
using System.Reflection;

namespace Utils.PostgreSql
{
    public static class PostgreSqlParameterHelper
    {
        public static string CallStoredProcedure(string name, NpgsqlParameter[]? parameters)
        {
            int size = parameters?.Length ?? 0;
            var parameterString = string.Join(", ", Enumerable.Range(0, size).Select(i => $"@p{i}"));
            var sqlQueryStoredProcedure = $"CALL {name}({parameterString})";
            return sqlQueryStoredProcedure;
        }

        public static NpgsqlParameter Create(string name, DateTime? value)
        {
            return value.HasValue ? new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Timestamp) { Value = value.Value } : new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Timestamp) { Value = DBNull.Value };
        }

        public static NpgsqlParameter Create(string name, DateOnly? value)
        {
            return value.HasValue ? new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Date) { Value = value.Value } : new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Date) { Value = DBNull.Value };
        }

        public static NpgsqlParameter Create(string name, string value, bool encode, int size)
        {
            var dbType = NpgsqlTypes.NpgsqlDbType.Text; // PostgreSQL uses Text for large string types
            return value == null ? new NpgsqlParameter(name, dbType) { Value = DBNull.Value } : new NpgsqlParameter(name, dbType) { Value = value };
        }

        public static NpgsqlParameter Create(string name, Guid value)
        {
            return new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Uuid) { Value = value };
        }

        public static NpgsqlParameter Create(string name, int value)
        {
            return new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Integer) { Value = value };
        }

        public static NpgsqlParameter Create(string name, int? value)
        {
            return value.HasValue ? new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Integer) { Value = value } : new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Integer) { Value = DBNull.Value };
        }

        public static NpgsqlParameter Create(string name, long value)
        {
            return new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Bigint) { Value = value };
        }

        public static NpgsqlParameter Create(string name, double value)
        {
            return new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Double) { Value = value };
        }

        public static NpgsqlParameter Create(string name, decimal value)
        {
            return new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Numeric) { Value = value };
        }

        public static NpgsqlParameter Create(string name, byte[] value)
        {
            return new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Bytea) { Value = value };
        }

        public static NpgsqlParameter Create(string name, decimal? value)
        {
            return value.HasValue ? new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Numeric) { Value = value } : new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Numeric) { Value = DBNull.Value };
        }

        public static NpgsqlParameter Create(string name, TimeSpan? value)
        {
            return value.HasValue ? new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Interval) { Value = value.Value }
            : new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Interval) { Value = DBNull.Value };
        }

        public static NpgsqlParameter Create(string name, bool? value)
        {
            return value.HasValue ? new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Boolean) { Value = value.Value }
            : new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Boolean) { Value = DBNull.Value };
        }

        public static NpgsqlParameter CreateOutput(string name, NpgsqlTypes.NpgsqlDbType dbType = NpgsqlTypes.NpgsqlDbType.Integer)
        {
            var p = new NpgsqlParameter(name, dbType)
            {
                Direction = ParameterDirection.Output
            };
            return p;
        }

        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        try
                        {
                            var value = row[pro.Name];
                            if (value != DBNull.Value)
                            {
                                if (pro.PropertyType == typeof(DateTime) || pro.PropertyType == typeof(DateTime?))
                                {
                                    pro.SetValue(objT, Convert.ChangeType(value, pro.PropertyType));
                                }
                                else if (pro.PropertyType == typeof(DateOnly) || pro.PropertyType == typeof(DateOnly?))
                                {
                                    DateTime dateValue;
                                    if (DateTime.TryParse(value.ToString(), out dateValue))
                                    {
                                        var dateOnlyValue = DateOnly.FromDateTime(dateValue);
                                        pro.SetValue(objT, dateOnlyValue);
                                    }
                                }
                                else
                                {
                                    pro.SetValue(objT, Convert.ChangeType(value, pro.PropertyType));
                                }
                            }
                        }
                        catch (Exception )
                        {
                            
                            // Handle exception
                        }
                    }
                }
                return objT;
            }).ToList();
        }

        public static async Task<T?> ConvertDataTableToModel<T>(DataTable dt) where T : class, new()
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            return await Task.Run(() =>
            {
                try
                {
                    var row = dt.Rows[0];
                    var model = new T();

                    foreach (var prop in properties)
                    {
                        if (columnNames.Contains(prop.Name))
                        {
                            var value = row[prop.Name];
                            if (value != DBNull.Value)
                            {
                                if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                                {
                                    if (DateTime.TryParse(value.ToString(), out DateTime dateValue))
                                    {
                                        prop.SetValue(model, dateValue);
                                    }
                                }
                                else if (prop.PropertyType == typeof(DateOnly) || prop.PropertyType == typeof(DateOnly?))
                                {
                                    if (DateTime.TryParse(value.ToString(), out DateTime dateValue))
                                    {
                                        var dateOnlyValue = DateOnly.FromDateTime(dateValue);
                                        prop.SetValue(model, dateOnlyValue);
                                    }
                                }
                                else
                                {
                                    prop.SetValue(model, Convert.ChangeType(value, prop.PropertyType));
                                }
                            }
                        }
                    }

                    return model;
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }
    }
}
