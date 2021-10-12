using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Helpers
{
    public class FirebaseHelper
    {
        public static async Task SendMessages(string topic, string title, string body)
        {
            
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                },

                //Token = "d3aLewjvTNw:APA91bE94LuGCqCSInwVaPuL1RoqWokeSLtwauyK-r0EmkPNeZmGavSG6ZgYQ4GRjp0NgOI1p-OAKORiNPHZe2IQWz5v1c3mwRE5s5WTv6_Pbhh58rY0yGEMQdDNEtPPZ_kJmqN5CaIc",
                Topic = topic
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
            
        }
    }
}
