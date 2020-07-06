using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ProjektConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string sqlconnection = @"SERVER=MSSQLServer; " +
             "INITIAL CATALOG=projekt; INTEGRATED SECURITY=SSPI;";
            string sqlcommand1 = "SELECT [dbo].[Temperature.uda_CountOfNegatives](temp) FROM pogoda";
            string sqlcommand2 = "SELECT [dbo].[Temperature.uda_CountOfVegetation](temp) FROM pogoda";
            //string sqlcommand3;
            string sqlcommand4 = "SELECT [dbo].[Temperature.Median](temp) FROM pogoda";
            string sqlcommand5 = "SELECT [dbo].[Dominanta.Dominanta](temp) FROM pogoda";

            string sqlcommand6 = "SELECT [dbo].[Rainfall.uda_SumOfRange](opady, data, '2014-10-19 19:52:00.000', '2020-10-19 19:52:00.000') FROM pogoda";
            string sqlcommand7 = "SELECT [dbo].[Rainfall.uda_GeoAvg](opady) FROM pogoda";
            string sqlcommand8 = "SELECT [dbo].[Rainfall.uda_SnowPercent](opady, snieg) FROM pogoda";

            SqlConnection connection = null;
            SqlCommand command = null;
            try
            {
                connection = new SqlConnection(sqlconnection);
                connection.Open();
                Console.WriteLine("====== Ilość dni z ujemną temperaturą ======");
                command = new SqlCommand(sqlcommand1, connection);
                Object count = command.ExecuteScalar();
                Console.WriteLine(count.ToString());

                Console.WriteLine("====== Ilość dni z temperaturą powyżej temperatury wegetacji traw ======");
                command = new SqlCommand(sqlcommand2, connection);
                count = command.ExecuteScalar();
                Console.WriteLine(count.ToString());

                Console.WriteLine("====== Ile dni temperatura w zakresie min max ======");
                Console.WriteLine("Podaj min temperaturę");
                int min ;
                Int32.TryParse(Console.ReadLine(), out min);
                Console.WriteLine("Podaj max temperaturę");
                int max ;
                Int32.TryParse(Console.ReadLine(), out max);
                Console.WriteLine("Wynik:");
                string sqlcommand3 = "SELECT [dbo].[Temperature.uda_CountOfRange](temp, " + Convert.ToString(min) + ", " + Convert.ToString(max) + ") FROM pogoda";
                command = new SqlCommand(sqlcommand3, connection);
                count = command.ExecuteScalar();
                Console.WriteLine(count.ToString());

                Console.WriteLine("====== Mediana wszystkich temperatur ======");
                command = new SqlCommand(sqlcommand4, connection);
                count = command.ExecuteScalar();
                Console.WriteLine(count.ToString());

                Console.WriteLine("====== Dominanta wszystkich temperatur ======");
                command = new SqlCommand(sqlcommand5, connection);
                Object c = command.ExecuteScalar();
                Console.WriteLine(c.ToString());


                Console.WriteLine("====== Ilość opadów w danym przedziale czasu ======");
                command = new SqlCommand(sqlcommand6, connection);
                count = command.ExecuteScalar();
                Console.WriteLine(count.ToString());

                Console.WriteLine("====== Średnia geometryczna opadów ======");
                command = new SqlCommand(sqlcommand7, connection);
                count = command.ExecuteScalar();
                Console.WriteLine(count.ToString());

                Console.WriteLine("====== Procent śniegu z wszystkich opadów ======");
                command = new SqlCommand(sqlcommand8, connection);
                count = command.ExecuteScalar();
                Console.WriteLine(count.ToString());

            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
            finally
            { connection.Close(); }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}


 