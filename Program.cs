using System.Data;

namespace APIsAndJSON
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();

            var weatherApiCall = new OpenWeatherMapAPI(client);

            Console.WriteLine("Enter a city to use for the weather api");
            var cityinput = Console.ReadLine();
            
            var dt = weatherApiCall.currentDestinationLongLat(cityinput);
            weatherApiCall.PrintCityResults(dt);
            
            Console.WriteLine("Enter the ID number of the city you want to view the weater for:");
            var cityid = Console.ReadLine();

            DataRow[] selectedRow = dt.Select($"id = {cityid}");

            var curtemp = string.Empty;
            foreach (DataRow row in selectedRow) 
            {
                curtemp = weatherApiCall.currenttemp(row[4].ToString(),row[5].ToString());
            }

            Console.WriteLine($"Current Temp in {cityinput} is: {curtemp} degrees fahrenheit.");

            //var apiCall = new RonVSKanyeAPI(client);

            //for (int i = 0; i < 5; i++)
            //{
            //    Console.WriteLine("------------------------");
            //    Console.WriteLine($"Kanye: {apiCall.Kanye()}");
            //    Console.WriteLine($"Ron Swanson: {apiCall.RonSwanson()}");
            //}

        }
    }
}
