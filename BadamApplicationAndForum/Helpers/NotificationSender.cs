﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BadamApplicationAndForum.Helpers
{
    public class NotificationSender
    {
        static string token = "68814003c9d184056341533f627c261d6017b1b5";

        public static async void sendNotification(string title, string content, String[] topics)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Token " + token);

            var response = await client.PostAsync(
                "https://api.push-pole.com/v2/messaging/notifications/",
                getNotificationData(title,content, topics)
            );

            var status_code = (int)response.StatusCode;

            var reponse_json = JToken.Parse(await response.Content.ReadAsStringAsync());

            Console.WriteLine("status code => " + status_code);
            Console.WriteLine("response => " + reponse_json.ToString());
            Console.WriteLine("==========");

            if (status_code == 201)
            {
                Console.WriteLine("Success!");

                var hashed_id = reponse_json.SelectToken("hashed_id").ToString();
                String report_url;

                if (String.IsNullOrEmpty(hashed_id))
                {
                    report_url = "no report url for your plan";
                }
                else
                {
                    //report_url = "https://pushe.co/report?id=" + hashed_id;
                }
                //Console.WriteLine("report_url: " + report_url);

                var notif_id = reponse_json.SelectToken("wrapper_id").ToString();
                Console.WriteLine("notification id: " + notif_id);
            }
            else
            {
                Console.WriteLine("failed!");
            }
        }

        public static StringContent getNotificationData(string title, string content, String[] topics)
        {
            var data = new JObject();
            data.Add("title", $"{title}");
            data.Add("content", $"{content}");

            var request_data = new JObject();
            request_data.Add("app_ids", new JArray(new String[] { "com.mmi.badam.badamak" }));
            request_data.Add("data", data);
            request_data.Add("topics", new JArray(topics));

            Console.WriteLine("Request data: " + request_data.ToString());

            return new StringContent(request_data.ToString(), Encoding.UTF8, "application/json");
        }
    }
}
