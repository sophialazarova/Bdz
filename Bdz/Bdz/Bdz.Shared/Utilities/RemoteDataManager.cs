namespace Bdz.Utilities
{
    using Bdz.Models;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
   public class RemoteDataManager
    {
        private const string getStationInfoUrl = "http://localhost:11205/api/bdz/GetStationInfo";
        private const string getRouteInfoUrl = "http://localhost:11205/api/bdz/GetRouteInfo";

       private HttpClient client;

       public RemoteDataManager()
       {
           this.client = new HttpClient();
       }

       public async Task<StationInfoRequestObject> GetStationInfo(string name, string date)
       {
           HttpRequestMessage request = new HttpRequestMessage();
           request.Method = HttpMethod.Get;
           request.RequestUri = new Uri(getStationInfoUrl + "?station=" + name + "&date=" + date);
           var response = await client.SendAsync(request);
           if (response.IsSuccessStatusCode)
           {
               var content = await response.Content.ReadAsStringAsync();
               var contentAsJson = this.DeserializeJson<StationInfoRequestObject>(content);
               return contentAsJson;
           }
           else
           {
               return new StationInfoRequestObject();
           }
       }

       public async Task<RouteInfoRequestObject> GetRouteInfo(string from, string to, string date)
       {
           HttpRequestMessage request = new HttpRequestMessage();
           request.Method = HttpMethod.Get;
           request.RequestUri = new Uri(getRouteInfoUrl + "?from=" + from + "&to=" + to + "&date=" + date);
           var response = await client.SendAsync(request);
           var content = await response.Content.ReadAsStringAsync();
           var contentAsJson = this.DeserializeJson<RouteInfoRequestObject>(content);
           int a = 9;
           return contentAsJson;
       }

       private T DeserializeJson<T>(string json)
       {
           var contentAsJson = JsonConvert.DeserializeObject<T>(json);
           int a = 8;
           return contentAsJson;

       }
    }
}
