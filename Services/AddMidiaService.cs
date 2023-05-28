using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace backend_squad1.Services
{
    public class AddMidiaService
    {
        private readonly IWebHostEnvironment _env;

        public AddMidiaService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<List<string>> UploadMedia(List<IFormFile> files, int chamadoIdChamado, string googleCredentialsPath, string bucketName, string databaseConnectionString)
        {
            var storage = StorageClient.Create();
            var urls = new List<string>();

            foreach (var file in files)
            {
                var filename = $"{DateTime.Now:yyyyMMddHHmmss}-{file.FileName}";
                var objectName = filename;

                using (var stream = file.OpenReadStream())
                {
                    var contentType = file.ContentType;
                    var result = await storage.UploadObjectAsync(bucketName, objectName, contentType, stream);
                }

                var url = $"https://firebasestorage.googleapis.com/v0/b/{bucketName}/o/{Uri.EscapeDataString(filename)}?alt=media";
                urls.Add(url);

                var midia = new Midia
                {
                    TipoMidia = GetMediaType(file.ContentType),
                    LinkMidia = url,
                    ChamadoIdChamado = chamadoIdChamado
                };

                using (MySqlConnection connection = new MySqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO Midia (tipoMidia, linkMidia, chamado_idChamado) VALUES (@tipoMidia, @linkMidia, @chamadoIdChamado)";
                    command.Parameters.AddWithValue("@tipoMidia", midia.TipoMidia);
                    command.Parameters.AddWithValue("@linkMidia", midia.LinkMidia);
                    command.Parameters.AddWithValue("@chamadoIdChamado", midia.ChamadoIdChamado);

                    command.ExecuteNonQuery();
                }
            }

            return urls;
        }

        private string GetMediaType(string contentType)
        {
            if (contentType.StartsWith("image/"))
            {
                return "Foto";
            }
            else if (contentType.StartsWith("video/"))
            {
                return "Vídeo";
            }
            else
            {
                return "Outros";
            }
        }
    }
}
