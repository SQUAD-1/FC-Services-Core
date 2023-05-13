using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostNotificationController : ControllerBase
    {
        [HttpPost]
        [Authorize]


        public IActionResult Notification([FromBody] SendNotification notificacao)
        {
            Console.WriteLine($"Enviando notificação para o chamado ID {notificacao.idChamado}");
            Console.WriteLine($"Mensagem: {notificacao.Message}");

            SendMessageToUser(notificacao.Empregado_Matricula, notificacao.Message);

            return Ok(new { Message = "Notificação enviada com sucesso" });
        }

        private void SendMessageToUser(int userId, string message)
        {
            var url = "C:/Users/larissa.ferreira/Documents/Projetos/fc-services-back/Properties/firebase-credential.json";
            var credentials = GoogleCredential.FromFile(url);
            var messaging = FirebaseMessaging.GetMessaging(credentials);

            var notification = new Notification
            {
                Title = "Chamado Modificado",
                Body = message
            };

            messaging.SendAsync(new Message
            {
                Token = GetDeviceTokenForUser(userId),
                Notification = notification
            });
        }

        private string GetDeviceTokenForUser(int userId)
        {
            return "DEVICE_TOKEN";
        }
    }
}