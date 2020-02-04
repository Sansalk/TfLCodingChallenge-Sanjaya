using PresentationLayer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoadStatus : IRoadStatus
    {
        const string root = "Road/{0}";

        public async Task<List<RoadResponseObject>> CheckRoadStatusAsync(Authentication auth, RoadStatusRequest roadrequest)
        {
            List<RoadResponseObject> responseObject = new List<RoadResponseObject>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(roadrequest.baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add(auth.id, auth.key);
                client.DefaultRequestHeaders.Add(auth.app_id, auth.app_key);

                HttpResponseMessage response = await client.GetAsync(String.Format(root, roadrequest.roadId));
                   
                if (!response.IsSuccessStatusCode) 
                    return responseObject;

                var responseString = await response.Content.ReadAsStringAsync();
                responseObject = await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject<List<RoadResponseObject>>(responseString));

                return responseObject;
            }
        }
    }
}
