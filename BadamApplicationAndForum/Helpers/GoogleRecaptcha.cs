using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Helpers
{
    public class GoogleRecaptcha
    {
        public bool Verify(string googleResponse)
        {
            string sec = "6LdKFtgbAAAAAK18sal0UgIcJcknuvTwjltsn83O";
            HttpClient httpClient = new HttpClient();
            var result = httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={sec}&response={googleResponse}", null).Result;
            if(result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }

            string content = result.Content.ReadAsStringAsync().Result;
            dynamic jsonData = JObject.Parse(content);

            if(jsonData.success =="true")
            {
                return true;
            }

            return false;
        }
    }
}
