﻿using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace PushNotificationService.FireBaseSetUp
{
   public class IlaroFirebaseConnector : IlaroIFirebaseConnector
    {
        private readonly FirebaseMessaging messaging; 
      
        public IlaroFirebaseConnector()
        {
           // "ConfigFiles/absu-e3ea9-firebase-adminsdk-jkwsb-bd3aa866dc.json"
            //var app = FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.FromFile(ConfigPath).CreateScoped("https://www.googleapis.com/auth/firebase.messaging") });
            //messaging = FirebaseMessaging.GetMessaging(app);
            var app = FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.FromFile("fpi-quick-check-firebase-adminsdk-zo1lz-69ae1c08fd.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging") });
            messaging = FirebaseMessaging.GetMessaging(app);


        }

        private Message CreateNotification(string title, string notificationBody, string token,Dictionary<string,string> keypairs)
        {
            try
            {
                return new Message()
                {

                    Token = token,
                    Data = keypairs,

                    Notification = new Notification()
                    {
                        Body = notificationBody,
                        Title = title,

                    },
                    Android = new AndroidConfig()
                    {
                        Priority = Priority.High,
                        Notification = new AndroidNotification()
                        {
                            Sound = "default",
                            Color = "#1d1072",
                            // ImageUrl = baseUrl + "Images/ProfilePictures/Avatar.jpg"
                        }
                    },

                };
            }
            catch(Exception ex)
            {
                throw;
            }
        }
  

        public async Task SendNotification(string token, string title, string body, Dictionary<string, string> keypairs)
        {
            var result = await messaging.SendAsync(CreateNotification(title, body, token,keypairs));
        }

      
    }
}
