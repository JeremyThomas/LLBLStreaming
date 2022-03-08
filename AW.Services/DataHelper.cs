using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;

using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AW.Helper;
using SD.LLBLGen.Pro.ORMSupportClasses;


namespace AQD.Helpers
{
  public static class DataHelper
  {
    #region Consts

    const string OrmprofilerAssemblyString = "SD.Tools.OrmProfiler.Interceptor";
    const string OrmprofilerInterceptorTypeName = OrmprofilerAssemblyString + ".InterceptorCore";
    const string InitializeMethodName = "Initialize";

    /// <summary>
    ///   EXEC sp_MSforeachtable @command1 = 'ALTER TABLE ? {0} CONSTRAINT ALL'
    /// </summary>
    const string SQLAlterAllConstraints = "EXEC sp_MSforeachtable @command1 = 'ALTER TABLE ? {0} CONSTRAINT ALL'";

    /// <summary>
    ///   {0} TRIGGER ALL On DATABASE'
    /// </summary>
    /// <remarks>https://blog.sqlauthority.com/2015/09/05/sql-server-how-to-enable-or-disable-all-the-triggers-on-a-table-and-database/</remarks>
    const string SQLAlterAllTriggers = "{0} TRIGGER ALL On DATABASE"; // Was EXEC sp_MSforeachtable @command1 = 'ALTER TABLE ? {0} TRIGGER ALL'

    /// <summary>
    ///   select * from
    /// </summary>
    public const string SQLSelectAllFrom = "select * from ";

    /// <summary>
    ///   Delete from
    /// </summary>
    const string SQLDeleteFrom = "Delete from ";

    #endregion

    public static string SQLDisableAllTriggers => string.Format(SQLAlterAllTriggers, "DISABLE");

    public static string SQLEnableAllTriggers => string.Format(SQLAlterAllTriggers, "ENABLE");

    public static string SQLDisableAllConstraints => string.Format(SQLAlterAllConstraints, "NOCHECK");

    public static string SQLEnableAllConstraints => string.Format(SQLAlterAllConstraints, "CHECK");

    /// <remarks>
    ///   https://stackoverflow.com/questions/31917301/execute-multiple-queries-in-single-oracle-command-in-c-sharp
    ///   Will get  ORA-02089: COMMIT is not allowed in a subordinate session in DTS transaction
    /// </remarks>
    /// <param name="enable"></param>
    /// <param name="constraints"></param>
    /// <returns></returns>
    public static string OracleEnableConstraints(bool enable = true, params Tuple<string, string>[] constraints)
    {
      if (constraints == null)
        return null;
      if (constraints.Length == 1)
        return OracleEnableConstraint(constraints[0], enable);
      var result = new StringBuilder();
      foreach (var tuple in constraints)
        result.AppendLine("  execute immediate '" + OracleEnableConstraint(tuple, enable) + "';");
      return "begin " + result + " end;";
    }

    public static string OracleEnableConstraint(Tuple<string, string> constraint, bool enable = true)
    {
      return string.Format("ALTER TABLE {0} {1} CONSTRAINT {2}", constraint.Item1, enable ? "ENABLE VALIDATE" : "DISABLE", constraint.Item2);
    }

    public static string SqlEnableConstraints(bool enable = true, params Tuple<string, string>[] constraints)
    {
      var result = new StringBuilder();
      foreach (var tuple in constraints)
        result.AppendLine(SqlEnableConstraint(tuple, enable));
      return result.ToString();
    }

    public static string SqlEnableConstraint(Tuple<string, string> constraint, bool enable = true)
    {
      return string.Format("ALTER TABLE {0} {1} CONSTRAINT {2};", constraint.Item1, enable ? "WITH CHECK CHECK" : "NOCHECK", constraint.Item2);
    }

    #region Dump Executing SQL queries

    static readonly string[] CommonKeywords = { "FROM", "WHERE", "GROUP BY", "HAVING", "INNER JOIN", "LEFT JOIN", "RIGHT JOIN" };

    [Conditional("DEBUG")]
    static void DumpIfDebug(ref bool dumpFormattedQuery)
    {
      dumpFormattedQuery = true;
    }

 
    static string ParameterValueToString(IDataParameter param)
    {
      string value;

      if (param.Value == null || param.Value == DBNull.Value)
        value = "null";
      else
        switch (param.DbType)
        {
          case DbType.VarNumeric:
          case DbType.Binary:
          case DbType.Object:
            value = "binary lob";
            break;

          case DbType.AnsiStringFixedLength:
          case DbType.StringFixedLength:
          case DbType.String:
          case DbType.AnsiString:
            value = string.Format("'{0}'", param.Value);
            break;

          case DbType.Boolean:
            value = Convert.ToBoolean(param.Value) ? "1" : "0";
            break;

          case DbType.DateTime:
            value = "'" + ((DateTime)param.Value).ToString("yyyyMMdd HH:mm:ss") + "'";
            break;

          case DbType.Date:
            value = "'" + ((DateTime)param.Value).ToString("dd/MM/yy") + "'";
            break;

          default:
            var paramAsEnum = param.Value as Enum;
            if (paramAsEnum != null)
            {
              value = string.Format("{0:d} /* {1}.{0:f} */", paramAsEnum, paramAsEnum.GetType().Name);
              break;
            }

            value = string.Format(CultureInfo.InvariantCulture, "{0}", param.Value);
            break;
        }

      return value;
    }

    static string ReplaceAllKeywords(this string text, string before, string after, params string[] keywords)
    {
      var text1 = text;
      return keywords.Where(text1.Contains).Aggregate(text, (current, keyword) => current.Replace(keyword, before + keyword + after));
    }

    static string ReplaceAllWholeKeywords(this string text, string before, string after, params string[] keywords)
    {
      foreach (var keyword in keywords)
      {
        var wholeKeyword = " " + keyword + " ";
        if (!text.Contains(wholeKeyword)) continue;
        if (string.IsNullOrEmpty(before))
          before = " ";
        if (string.IsNullOrEmpty(after))
          after = " ";
        text = text.Replace(wholeKeyword, before + keyword + after);
      }

      return text;
    }
    

    #endregion Dump Executing SQL queries

    #region DbCommand

    /// <summary>
    ///   Fixes the SQL. Some FieldDefinition lookup SQL has ; in them - this needs to be removed before executing
    /// </summary>
    /// <param name="sqlQueryText">The SQL query text.</param>
    /// <returns></returns>
    public static string FixSQL(string sqlQueryText)
    {
      return sqlQueryText != null ? sqlQueryText.Replace(";", "") : null;
    }



    /// <summary>
    ///   Creates and returns a System.Data.Common.DbCommand object associated with the supplied connection.
    /// </summary>
    /// <param name="dbSpecificCreator">The db specific creator.</param>
    /// <param name="commandText">The command text.</param>
    /// <param name="commandParameters">The command parameters.</param>
    /// <returns></returns>
    public static DbCommand CreateDbCommand(IDbSpecificCreator dbSpecificCreator, string commandText, params DbParameter[] commandParameters)
    {
      var command = dbSpecificCreator.CreateCommand();
      command.CommandText = commandText;
      AddParameters(command, commandParameters);
      return command;
    }

    static void AddParameters(DbCommand command, params DbParameter[] commandParameters)
    {
      if (!commandParameters.IsNullOrEmpty())
        command.Parameters.AddRange(commandParameters);
    }

    /// <summary>
    ///   Creates and returns a System.Data.Common.DbCommand object associated with the supplied connection.
    /// </summary>
    /// <param name="dbSpecificCreator">The db specific creator.</param>
    /// <param name="commandText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="commandParameters">The command parameters.</param>
    /// <returns></returns>
    public static DbCommand CreateDbCommand(IDbSpecificCreator dbSpecificCreator, string commandText, CommandType commandType, params DbParameter[] commandParameters)
    {
      var command = CreateDbCommand(dbSpecificCreator, commandText);
      command.CommandType = commandType;
      AddParameters(command, commandParameters);
      return command;
    }

    /// <summary>
    ///   Executes a SQL statement against a connection object.
    /// </summary>
    /// <param name="dbConnection">The db connection.</param>
    /// <param name="transaction">The transaction.</param>
    /// <param name="commandText">The command text.</param>
    /// <param name="traceMethod">The trace method.</param>
    /// <returns>
    ///   The number of rows affected.
    /// </returns>
    /// .
    public static int ExecuteNonQuery(DbConnection dbConnection, DbTransaction transaction, string commandText, Action<string> traceMethod)
    {
      if (traceMethod != null) traceMethod(commandText);
      return ExecuteNonQuery(dbConnection, transaction, commandText, CommandType.Text);
    }

    /// <summary>
    ///   Executes a SQL statement against a connection object.
    /// </summary>
    /// <param name="dbConnection">The db connection.</param>
    /// <param name="transaction">The transaction.</param>
    /// <param name="commandText">The command text.</param>
    /// <returns>
    ///   The number of rows affected.
    /// </returns>
    /// .
    public static int ExecuteNonQuery(DbConnection dbConnection, DbTransaction transaction, string commandText)
    {
      return ExecuteNonQuery(dbConnection, transaction, commandText, CommandType.Text);
    }

    /// <summary>
    ///   Executes a SQL statement against a connection object.
    /// </summary>
    /// <param name="dbConnection">The db connection.</param>
    /// <param name="transaction">The transaction.</param>
    /// <param name="commandText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns></returns>
    public static int ExecuteNonQuery(DbConnection dbConnection, DbTransaction transaction, string commandText, CommandType commandType)
    {
      var sqlCommand = CreateDbCommand(dbConnection, commandText, commandType);
      sqlCommand.Transaction = transaction;
      return sqlCommand.ExecuteNonQuery();
    }

    /// <summary>
    ///   Creates and returns a System.Data.Common.DbCommand object associated with the supplied connection.
    /// </summary>
    /// <param name="dbConnection">The db connection.</param>
    /// <param name="commandText">The command text.</param>
    /// <returns></returns>
    public static DbCommand CreateDbCommand(DbConnection dbConnection, string commandText, params DbParameter[] commandParameters)
    {
      var command = dbConnection.CreateCommand();
      command.CommandText = commandText.Replace("\r\n", "\n");
      AddParameters(command, commandParameters);
      return command;
    }

    /// <summary>
    ///   Creates and returns a System.Data.Common.DbCommand object associated with the supplied connection.
    /// </summary>
    /// <param name="dbConnection">The db connection.</param>
    /// <param name="commandText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns></returns>
    public static DbCommand CreateDbCommand(DbConnection dbConnection, string commandText, CommandType commandType)
    {
      var command = CreateDbCommand(dbConnection, commandText);
      command.CommandType = commandType;
      return command;
    }

    #endregion


  }

  public class ConnectionPool
  {
    public string PoolIdentifier { get; set; }
    public int NumberOfConnections { get; set; }
    public MethodInfo ConnectionPoolCountMethod { get; set; }
    public object ListDbConnections { get; set; }

    public int GetNumberOfConnections()
    {
      var numberOfConnections = ConnectionPoolCountMethod.Invoke(ListDbConnections, null);
      NumberOfConnections = (Int32)numberOfConnections;
      return NumberOfConnections;
    }
  }
}