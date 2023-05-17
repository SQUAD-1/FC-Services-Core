// using System;
// using System.Collections.Generic;
// using System.Globalization;
// using Microsoft.AspNetCore.Mvc;
// using MySql.Data.MySqlClient;
// using Microsoft.AspNetCore.Authorization;

// namespace FC_Services_Core.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class FiltrarChamadosController : ControllerBase
//     {
//         [HttpPost]
//         [Authorize]
//         public IActionResult FiltrarChamados(FiltroChamados filtro)
//         {
//             string connectionString = "server=containers-us-west-209.railway.app;port=6938;database=railway;user=root;password=5cu1Y8DVEYLMeej8yleH";
//             MySqlConnection connection = new MySqlConnection(connectionString);
//             MySqlCommand command = connection.CreateCommand();

//             string query = "SELECT * FROM Chamado WHERE ";

//             List<MySqlParameter> parameters = new List<MySqlParameter>();

//             if (filtro.DataRelato != null)
//             {
//                 query += "DataRelato = @DataRelato AND ";
//                 parameters.Add(new MySqlParameter("@DataRelato", filtro.DataRelato));
//             }

//             if (filtro.Prioridade != null)
//             {
//                 query += "Prioridade = @Prioridade AND ";
//                 parameters.Add(new MySqlParameter("@Prioridade", filtro.Prioridade));
//             }

//             if (filtro.Status != null)
//             {
//                 query += "Status = @Status AND ";
//                 parameters.Add(new MySqlParameter("@Status", filtro.Status));
//             }

//             if (filtro.TempoDecorrido != null)
//             {
//                 query += "TempoDecorrido = @TempoDecorrido AND ";
//                 parameters.Add(new MySqlParameter("@TempoDecorrido", filtro.TempoDecorrido));
//             }

//             if (filtro.Empregado_Matricula != null)
//             {
//                 query += "Empregado_Matricula = @Empregado_Matricula AND ";
//                 parameters.Add(new MySqlParameter("@Empregado_Matricula", filtro.Empregado_Matricula));
//             }

//             if (filtro.Tipo != null)
//             {
//                 query += "Tipo = @Tipo AND ";
//                 parameters.Add(new MySqlParameter("@Tipo", filtro.Tipo));
//             }

//             // Remove o último "AND" da consulta
//             query = query.TrimEnd(' ', 'A', 'N', 'D');

//             command.CommandText = query;
//             command.Parameters.AddRange(parameters.ToArray());

//             connection.Open();

//             MySqlDataReader reader = command.ExecuteReader();

//             List<Chamado> chamados = new List<Chamado>();

//             while (reader.Read())
//             {
//                 Chamado chamado = new Chamado
//                 {
//                     Nome = reader["Nome"].ToString(),
//                     DataRelato = reader["DataRelato"].ToString(),
//                     Descricao = reader["Descricao"].ToString(),
//                     Prioridade = reader["Prioridade"].ToString(),
//                     HorarioAbertura = reader["HorarioAbertura"].ToString(),
//                     HorarioUltimaAtualizacao = reader["HorarioUltimaAtualizacao"].ToString(),
//                     Status = reader["Status"].ToString(),
//                     TempoDecorrido = reader["TempoDecorrido"].ToString(),
//                     Empregado_Matricula = Convert.ToInt32(reader["Empregado_Matricula"]),
//                     Tipo = reader["Tipo"].ToString()
//                 };

//                 chamados.Add(chamado);
//             }

//             connection.Close();

//             return Ok(chamados);
//         }
//     }
// }