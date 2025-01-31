using PostgreSQLCopyHelper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utils.PostgreSQL
{
    public static class PostgreSQLBulkHelper
    {

        /*  How to Use  Bulk  Insert
         
       
        public class Employee
        {
            public int Id { get; set; } // Will be skipped due to [Key] attribute
            public string Name { get; set; }
            public DateTime DateOfBirth { get; set; }
            public decimal Salary { get; set; }
        }

        public static void sampleUse()
        {

            var employees = new List<Employee>
            {
                new Employee { Name = "Alice", DateOfBirth = new DateTime(1990, 1, 1), Salary = 50000m },
                new Employee { Name = "Bob", DateOfBirth = new DateTime(1985, 6, 15), Salary = 60000m }
            };

            using (var connection = new NpgsqlConnection("YourConnectionString"))
            {
                connection.Open();
                var copyHelper = PostgreSQLBulkHelper.InsertHelper<Employee>("employees");
                copyHelper.SaveAll(connection, employees);
            }


        }

        */
        public static PostgreSQLCopyHelper<T> InsertHelper<T>(string tableName)
        {
            var helper = new PostgreSQLCopyHelper<T>("dbo", "\"" + tableName + "\"");
            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                var type = prop.PropertyType;
                if (Attribute.IsDefined(prop, typeof(KeyAttribute)) || Attribute.IsDefined(prop, typeof(ForeignKeyAttribute)))
                    continue;
                switch (type)
                {
                    case Type intType when intType == typeof(int) || intType == typeof(int?):
                        {
                            helper = helper.MapInteger("\"" + prop.Name + "\"", x => (int?)typeof(T).GetProperty(prop.Name).GetValue(x, null));
                            break;
                        }
                    case Type stringType when stringType == typeof(string):
                        {
                            helper = helper.MapText("\"" + prop.Name + "\"", x => (string)typeof(T).GetProperty(prop.Name).GetValue(x, null));
                            break;
                        }
                    case Type dateType when dateType == typeof(DateTime) || dateType == typeof(DateTime?):
                        {
                            helper = helper.MapTimeStamp("\"" + prop.Name + "\"", x => (DateTime?)typeof(T).GetProperty(prop.Name).GetValue(x, null));
                            break;
                        }
                    case Type decimalType when decimalType == typeof(decimal) || decimalType == typeof(decimal?):
                        {
                            helper = helper.MapMoney("\"" + prop.Name + "\"", x => (decimal?)typeof(T).GetProperty(prop.Name).GetValue(x, null));
                            break;
                        }
                    case Type doubleType when doubleType == typeof(double) || doubleType == typeof(double?):
                        {
                            helper = helper.MapDouble("\"" + prop.Name + "\"", x => (double?)typeof(T).GetProperty(prop.Name).GetValue(x, null));
                            break;
                        }
                    case Type floatType when floatType == typeof(float) || floatType == typeof(float?):
                        {
                            helper = helper.MapReal("\"" + prop.Name + "\"", x => (float?)typeof(T).GetProperty(prop.Name).GetValue(x, null));
                            break;
                        }
                    case Type guidType when guidType == typeof(Guid):
                        {
                            helper = helper.MapUUID("\"" + prop.Name + "\"", x => (Guid)typeof(T).GetProperty(prop.Name).GetValue(x, null));
                            break;
                        }
                }
            }
            return helper;
        }
    }
}
