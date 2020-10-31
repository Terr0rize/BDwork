//подключи через NuGet MySQL.Data
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace ConsoleApp1
{
    struct dbrow
    {
        public string one;
        public string two;
        public string tree;
        public string four;
        public string five;
    }
    class dbworker 
    {
        MySqlConnection conn;
        public dbworker()
        {
            MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
            stringBuilder.Server = "localhost";
            stringBuilder.UserID = "root";
            stringBuilder.Database = "liorkin";
            stringBuilder.SslMode = MySqlSslMode.None;
            conn = new MySqlConnection(stringBuilder.ConnectionString);
        }

        public List<dbrow> selecter()
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT cars.id, person1.person, cars.number1, cars.cars, person1.number FROM liorkin.cars INNER JOIN liorkin.person1 ON cars.person = person1.id";

            List<dbrow> output = new List<dbrow>();
            try
            {
                conn.Open();
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dbrow row = new dbrow
                        {
                            one = reader.GetString(0),
                            two = reader.GetString(1),
                            tree = reader.GetString(2),
                            four = reader.GetString(3),
                            five = reader.GetString(4)
                        };
                        output.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return output;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            dbworker dbw = new dbworker();
            var list = dbw.selecter();
            foreach (var row in list)
            {
                Console.WriteLine(row.one + " | " + row.two + " | " + row.tree.PadRight(10) + " | " + row.four.PadRight(10) + " | " + row.five.PadRight(10));
            }
            Console.ReadLine();
        }
    }
}
