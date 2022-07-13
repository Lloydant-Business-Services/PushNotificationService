using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PushNotificationService.FireBaseSetUp;
using PushNotificationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushNotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlaroController : ControllerBase
    {
        private readonly IlaroIFirebaseConnector _ilaroIFirebaseConnector;
        public IlaroController(IlaroIFirebaseConnector ilaroIFirebaseConnector)
        {
            _ilaroIFirebaseConnector = ilaroIFirebaseConnector;
        }
        [HttpPost]
        public async Task<bool> PushIlaro(IList<NotificationModel> model)
        {
            foreach (var item in model)
            {
                try
                {
                    Dictionary<string, string> Objectsformessage = new Dictionary<string, string>();


                    Objectsformessage.Add("PictureUrl", item.SchoolLogo);

                    Objectsformessage.Add("Id", $"{item.Id.ToString()}");

                    if (item.FcmToken != "")
                    {
                        await _ilaroIFirebaseConnector.SendNotification(item.FcmToken, item.Title, item.Body, Objectsformessage);
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            return true;
        }
    }
}
