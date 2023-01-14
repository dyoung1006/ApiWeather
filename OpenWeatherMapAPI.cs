using Newtonsoft.Json.Linq;
using System.Data;

namespace APIsAndJSON
{
    public class OpenWeatherMapAPI
    {
        private HttpClient _client;
        const string apiKey = "363ab15ff6a51a176470514c30aaaa90";
        
        public OpenWeatherMapAPI(HttpClient client)
        {
            _client = client;
        }
        public DataTable currentDestinationLongLat(string city)
        {
            var url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=5&appid={apiKey}";

            var urlResponse = _client.GetStringAsync(url).Result;
            var payload = JArray.Parse(urlResponse);

            var dt = CityLatLongDetails(JArray.Parse(urlResponse));
            return dt;
        }

        internal string currenttemp(string latitude, string longitude) 
        {
            var url = $"https://api.openweathermap.org/data/3.0/onecall?lat={latitude}&lon={longitude}&exclude=hourly,daily,minutely&units=imperial&appid={apiKey}";

            var urlResponse = _client.GetStringAsync(url).Result;
            var payload = JObject.Parse(urlResponse);

            return payload.SelectToken($"current.temp") != null ? payload.SelectToken($"current.temp").ToString() : string.Empty;
        }

        internal DataTable CityLatLongDetails(JArray payload)
        {
            DataTable latlonDt = CreateDataTable();

            foreach (JObject item in payload)
            {
                DataRow dr;

                dr = latlonDt.NewRow();
                dr["Country"] = (string)item.GetValue("country");
                dr["State"] = (string)item.GetValue("state");
                dr["CityName"] = (string)item.GetValue("name");
                dr["Latitude"] = (string)item.GetValue("lat");
                dr["Longitude"] = (string)item.GetValue("lon");
                latlonDt.Rows.Add(dr);
            }
            return latlonDt;
        }

        internal DataTable CreateDataTable()
        {
            DataTable latlongdt = new DataTable();
            DataColumn dtColumn;

            //creating ID column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Int32);
            dtColumn.ColumnName = "ID";
            dtColumn.ReadOnly = true;
            dtColumn.Unique = true; 
            dtColumn.AutoIncrement= true;
            dtColumn.AutoIncrementSeed = 1;
            latlongdt.Columns.Add(dtColumn);

            //ceating Country column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "Country";
            latlongdt.Columns.Add(dtColumn);

            //ceating State name column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "State";
            latlongdt.Columns.Add(dtColumn);

            //ceating City name column
            dtColumn =new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName= "CityName";
            latlongdt.Columns.Add(dtColumn);

            //ceating Latitude column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "Latitude";
            latlongdt.Columns.Add(dtColumn);

            //ceating Longitude column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "Longitude";
            latlongdt.Columns.Add(dtColumn);

            return latlongdt;
        }

        internal void PrintCityResults(DataTable dt)
        {
            Console.WriteLine("Here is a list of your results:");
            foreach(DataRow dr in dt.Rows) 
            {
                Console.WriteLine($"ID:{dr[0]} - Country:'{dr[1]}' - State:'{dr[2]}' - City:'{dr[3]}' - Latitude:'{dr[4]}' - Longitude:'{dr[5]}'");       
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
        }
    }
}
