using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace ConsoleApp1
{
    struct data 
    {
        public string one;
        public string two;
        public string tree;
        public string four;
        public string five;
    }
        
    class Program
    {
        MySqlConnection connect;
     
        public List<data> conclusion()
        {
            //...............................................[Connect]..........................
            MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
            stringBuilder.Server = "localhost";
            stringBuilder.UserID = "root";
            stringBuilder.Database = "liorkin";
            stringBuilder.SslMode = MySqlSslMode.None;
            //..................................................................................

            connect = new MySqlConnection(stringBuilder.ConnectionString);

            MySqlCommand cmd = connect.CreateCommand();
            cmd.CommandText = "SELECT cars.id, person1.person, cars.number1, cars.cars, person1.number FROM liorkin.cars INNER JOIN liorkin.person1 ON cars.person = person1.id";


            List<data> bd = new List<data>();

            try
            {
                connect.Open();
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data databd = new data
                        {
                            one = reader.GetString(0),
                            two = reader.GetString(1),
                            tree = reader.GetString(2),
                            four = reader.GetString(3),
                            five = reader.GetString(4)
                        };
                        bd.Add(databd);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nПодробные ошибки: " +ex.StackTrace + "\n\nКоротко об ошибке: " + ex.Message);
            }
            return bd;
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            var list = pr.conclusion();

            foreach (var data in list) 
            {
                Console.WriteLine(data.one + " ||| " + data.two + " ||| " + data.tree.PadRight(10) + " ||| " + data.four.PadRight(10) + " ||| " + data.five.PadRight(10));
            }
            Console.ReadLine();
        }
    }
}
