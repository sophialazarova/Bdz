using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bdz.Utilities
{
   public class RemoteDataManager
    {
        private const string getStationInfoUrl = "http://localhost:11205/api/bdz/GetStationInfo";
        private const string getRouteInfoUrl = "http://localhost:11205/api/bdz/GetRouteInfo";

       private HttpClient client;

       public RemoteDataManager()
       {
           this.client = new HttpClient();
       }

       public async Task<HttpResponseMessage> GetStationInfo(string name, string date)
       {
           HttpRequestMessage request = new HttpRequestMessage();
           request.Method = HttpMethod.Get;
           request.RequestUri = new Uri(getStationInfoUrl + "?station=" + name + "&date=" + date);
           var response = await client.SendAsync(request);
           return response;
     
       }

       public void GetRouteInfo()
       {

       }
    }
}
