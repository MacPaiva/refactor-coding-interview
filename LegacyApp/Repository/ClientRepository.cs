﻿using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using LegacyApp.Model;

namespace LegacyApp.Repository
{
    public class ClientRepository : IClientRepository
    {
        public Client GetById(int id)
        {
            Client client = null;
            var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "uspGetClientById"
                };

                var parameter = new SqlParameter("@ClientId", SqlDbType.Int) { Value = id };
                command.Parameters.Add(parameter);

                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    client = new Client
                                      {
                                          Id = int.Parse(reader["ClientId"].ToString()),
                                          Name = reader["Name"].ToString(),
                                          ClientStatusEnum = (ClientStatusEnum)int.Parse(reader["ClientStatusId"].ToString())
                                      };
                }
            }

            return client;
        }
    }
}
