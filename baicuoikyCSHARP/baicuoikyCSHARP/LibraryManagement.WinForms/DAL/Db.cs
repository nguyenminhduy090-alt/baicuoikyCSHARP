using System.Data;
using Npgsql;

namespace LibraryManagement.WinForms.DAL;

public static class Db
{
    public static NpgsqlConnection CreateConnection() => new(AppSettings.ConnectionString);

    public static DataTable Query(string sql, params NpgsqlParameter[] parameters)
    {
        using var conn = CreateConnection();
        using var cmd = new NpgsqlCommand(sql, conn);
        if (parameters?.Length > 0) cmd.Parameters.AddRange(parameters);
        using var adapter = new NpgsqlDataAdapter(cmd);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static object? Scalar(string sql, params NpgsqlParameter[] parameters)
    {
        using var conn = CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        if (parameters?.Length > 0) cmd.Parameters.AddRange(parameters);
        return cmd.ExecuteScalar();
    }

    public static int Execute(string sql, params NpgsqlParameter[] parameters)
    {
        using var conn = CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        if (parameters?.Length > 0) cmd.Parameters.AddRange(parameters);
        return cmd.ExecuteNonQuery();
    }

    public static int ExecuteTransaction(Func<NpgsqlConnection, NpgsqlTransaction, int> action)
    {
        using var conn = CreateConnection();
        conn.Open();
        using var tran = conn.BeginTransaction();
        try
        {
            int affected = action(conn, tran);
            tran.Commit();
            return affected;
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }
}
