using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StepChat.Common.CommonModels;
using StepChat.Server.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StepChat.UI2_2

{
   public  class HTTP_SocketForUser
    {
        public static async Task<JObject> Auth_UserAsync(string login,string Password)
        {
            var serverAddress = new Uri("http://192.168.1.7:15006/api/auth/login");

            User requestObject = new User() { ContactNickname = login, Password = Password };

            var body = JsonConvert.SerializeObject(requestObject);

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage();

                request.Headers.Accept.TryParseAdd("application/json");
                request.Method = HttpMethod.Post;
                request.RequestUri = serverAddress;
                request.Content = new StringContent(body);

                var response = await httpClient.SendAsync(request);

                var responseAsString = await response.Content.ReadAsStringAsync();



                var parsed = JObject.Parse(responseAsString);

                return parsed;



                


            }
        }
        public static async Task<JObject> Create_Group(List<User> users, string name, Local_Group.GroupTypes type )
        {
            var serverAddress = new Uri("http://192.168.1.7:15006/api/auth/CreateGroup");

            Local_Group requestObject = new Local_Group() { Title = name, Users = users, GroupType=type };

            var body = JsonConvert.SerializeObject(requestObject);

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage();

                request.Headers.Accept.TryParseAdd("application/json");
                request.Method = HttpMethod.Post;
                request.RequestUri = serverAddress;
                request.Content = new StringContent(body);

                var response = await httpClient.SendAsync(request);

                var responseAsString = await response.Content.ReadAsStringAsync();



                var parsed = JObject.Parse(responseAsString);

                return parsed;






            }
        }

    }
}
