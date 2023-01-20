using Microsoft.Data.SqlClient;
using System.Data;

namespace Dapper.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task<IEnumerable<T>> ExecuteProcedureAsync<T>(this SqlConnection connection, string storedProcedure, object? parameters = null, int commandTimeout = 180)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Close();
                    connection.Open();
                }

                if (parameters != null)
                {
                    return await connection.QueryAsync<T>(storedProcedure, parameters,
                        commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
                }
                else
                {
                    return await connection.QueryAsync<T>(storedProcedure,
                        commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
                }
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
            finally
            {
                connection.Close();
            }

        }
        public static async Task<IEnumerable<T>> ExecuteProcedureAsync<T>(this SqlConnection connection, string storedProcedure, DynamicParameters dynamicParameters, int commandTimeout = 180)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Close();
                    connection.Open();
                }

                if (dynamicParameters != null)
                {
                    return await connection.QueryAsync<T>(storedProcedure, dynamicParameters,
                        commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
                }
                else
                {
                    return await connection.QueryAsync<T>(storedProcedure,
                        commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
                }
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
