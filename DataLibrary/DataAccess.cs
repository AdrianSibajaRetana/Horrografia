using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class DataAccess : IDataAccess
    {
        /*
        Método encargado de leer información de la base de datos.
        Parámetros: 
            sql: Código sql a ejecutar.
            parameters: Tipo genérico que determina los parámetros del código sql a ejecutar.
            connectionString: Hilera de conexión a la base de datos.
        Devuelve: 
            Lista de tipo T, siendo T un modelo de una tabla a especificar. 
         */
        public async Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<T>(sql, parameters);

                return rows.ToList();
            }
        }

        /*
        Método encargado de escribir información de la base de datos.
        Parámetros: 
            sql: Código sql a ejecutar.
            parameters: Tipo genérico que determina los parámetros del código sql a ejecutar.
            connectionString: Hilera de conexión a la base de datos.
        Devuelve: 
            Nada
         */
        public Task SaveData<T>(string sql, T parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                return connection.ExecuteAsync(sql, parameters);
            }

        }
    }
}
