using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace AQD.Helpers
{
  /// <summary>
  ///   Methods to help with DataSet, DataTable, DataRow, DataRowView and DataColumn
  /// </summary>
  public static class DataSetHelper
  {
    /// <summary>
    ///   Gets the data row from a data row view.
    /// </summary>
    /// <param name="dataRowView">The data row view.</param>
    /// <returns></returns>
    public static T GetDataRowFromDataRowView<T>(DataRowView dataRowView) where T : DataRow
    {
      return dataRowView == null ? null : dataRowView.Row as T;
    }

    /// <summary>
    ///   Gets the data rows from data row views.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rows">The rows.</param>
    /// <returns></returns>
    public static IEnumerable<T> GetDataRowsFromDataRowViews<T>(IEnumerable<DataRowView> rows) where T : DataRow
    {
      return rows.Select(GetDataRowFromDataRowView<T>);
    }

    /// <summary>
    ///   Gets the data rows from data row views.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rows">The rows.</param>
    /// <returns></returns>
    public static IEnumerable<T> GetDataRowsFromDataRowViews<T>(IEnumerable rows) where T : DataRow
    {
      return rows == null ? Enumerable.Empty<T>() : GetDataRowsFromDataRowViews<T>(rows.OfType<DataRowView>());
    }

    /// <summary>
    ///   Gets the data rows from data row views.
    /// </summary>
    /// <param name="rows">The rows.</param>
    /// <returns></returns>
    public static IEnumerable<DataRow> GetDataRowsFromDataRowViews(IEnumerable rows)
    {
      return GetDataRowsFromDataRowViews<DataRow>(rows);
    }

    /// <summary>
    ///   Gets the data from data rows.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataRows">The data rows.</param>
    /// <param name="columnIndex">Index of the column the data is in.</param>
    /// <returns></returns>
    public static IEnumerable<T> GetDataFromDataRows<T>(IEnumerable<DataRow> dataRows, int columnIndex)
    {
      var objects = dataRows.Select(dr => Convert.ChangeType(dr.ItemArray[columnIndex], typeof(T))).Cast<T>();
      return objects;
    }

    /// <summary>
    ///   Gets the data from data rows.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataRows">The data rows.</param>
    /// <param name="columnName">Name of the column the data is in.</param>
    /// <returns></returns>
    public static IEnumerable<T> GetDataFromDataRows<T>(IEnumerable<DataRow> dataRows, string columnName)
    {
// ReSharper disable PossibleMultipleEnumeration
      var dataRow = dataRows.FirstOrDefault();
      if (dataRow != null)
      {
        var columnIndex = dataRow.Table.Columns.IndexOf(columnName);
        if (columnIndex < 0)
          throw new ArgumentException(string.Format("Column {0} was not found in table {1}", columnName, dataRow.Table.TableName));
        return GetDataFromDataRows<T>(dataRows, columnIndex);
      }
      // ReSharper restore PossibleMultipleEnumeration

      return null;
    }

    /// <summary>
    ///   Gets the data from data row views.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rows">The rows.</param>
    /// <param name="columnName">Name of the column the data is in.</param>
    /// <returns></returns>
    public static IEnumerable<T> GetDataFromDataRowViews<T>(IEnumerable rows, string columnName)
    {
      return GetDataFromDataRows<T>(GetDataRowsFromDataRowViews(rows), columnName);
    }

    /// <summary>
    ///   DataRowCollection as IEnumerable DataRow.
    /// </summary>
    /// <param name="columns">The columns.</param>
    /// <returns></returns>
    public static IEnumerable<DataRow> AsEnumerable(this DataRowCollection columns)
    {
      return columns.Cast<DataRow>();
    }

    /// <summary>
    ///   DataColumnCollection as IEnumerable DataColumns.
    /// </summary>
    /// <param name="columns">The columns.</param>
    /// <returns></returns>
    public static IEnumerable<DataColumn> AsEnumerable(this DataColumnCollection columns)
    {
      return columns.Cast<DataColumn>();
    }

    /// <summary>
    ///   DataView the enumerable DataRowViews.
    /// </summary>
    /// <param name="dataView">The data view.</param>
    /// <returns></returns>
    public static IEnumerable<DataRowView> AsEnumerable(this DataView dataView)
    {
      return dataView.Cast<DataRowView>();
    }

    public static IEnumerable<DataTable> AsEnumerable(this DataSet dataSet)
    {
      return dataSet.Tables.AsEnumerable();
    }

    public static IEnumerable<DataTable> AsEnumerable(this DataTableCollection dataTableCollection)
    {
      return dataTableCollection.Cast<DataTable>();
    }

    /// <summary>
    ///   Changes the System.Data.DataRow.Rowstate of a System.Data.DataRow to Added.
    /// </summary>
    /// <param name="table">The table.</param>
    public static void SetAdded(DataTable table)
    {
      foreach (DataRow row in table.Rows)
        row.SetAdded();
    }

    /// <summary>
    ///   Loads the data row.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <param name="values">The values.</param>
    /// <returns></returns>
    public static DataRow LoadDataRow(this DataTable table, IEnumerable<object> values)
    {
      return table.LoadDataRowWithValues(values.ToArray());
    }

    /// <summary>
    ///   Loads the data row.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <param name="values">The values.</param>
    /// <returns></returns>
    public static DataRow LoadDataRowWithValues(this DataTable table, params object[] values)
    {
      return table.LoadDataRow(values, true);
    }

    public static string ToCSV(this DataView defaultView)
    {
      return defaultView.ToTable().ToCSV();
    }

    public static string ToCSV(this DataTable table, bool includeHeader = true, bool errorsOnly = false)
    {
      return DataTableToCSV(table, includeHeader, errorsOnly: errorsOnly).ToString();
    }

    public static StringBuilder DataTableToCSV(DataTable table, bool includeHeader = true, bool quoteAll = false, bool errorsOnly = false)
    {
      var strWriter = new StringWriter();
      DataTableToCSV(strWriter, table, includeHeader, quoteAll, errorsOnly);
      var stringBuilder = strWriter.GetStringBuilder();
      return stringBuilder;
    }

    /// <summary>
    ///   Converts DataTable to CSV stream.
    /// </summary>
    /// <see cref="http://knab.ws/blog/index.php?/archives/3-CSV-file-parser-and-writer-in-C-Part-1.html" />
    /// <param name="stream">The stream.</param>
    /// <param name="table">The table.</param>
    /// <param name="header">if set to <c>true</c> [header].</param>
    /// <param name="quoteAll">if set to <c>true</c> [quoteAll].</param>
    /// <param name="errorsOnly"></param>
    public static void DataTableToCSV(TextWriter stream, DataTable table, bool header = true, bool quoteAll = false, bool errorsOnly = false)
    {
      if (header)
        for (var i = 0; i < table.Columns.Count; i++)
        {
          WriteItem(stream, table.Columns[i].Caption, quoteAll);
          if (i < table.Columns.Count - 1)
            stream.Write(',');
          else
            stream.WriteLine();
        }

      var dataRows = errorsOnly ? table.GetErrors() : (IEnumerable<DataRow>) table.Rows.AsEnumerable();
      foreach (var row in dataRows)
        for (var i = 0; i < table.Columns.Count; i++)
        {
          WriteItem(stream, row[i], quoteAll);
          if (i < table.Columns.Count - 1)
            stream.Write(',');
          else
            stream.WriteLine();
        }
    }

    static void WriteItem(TextWriter stream, object item, bool quoteAll)
    {
      if (item == null)
        return;
      var s = item.ToString();
      if (quoteAll || s.IndexOfAny("\",\x0A\x0D, ".ToCharArray()) > -1)
        stream.Write("\"" + s.Replace("\"", "\"\"") + "\"");
      else
        stream.Write(s);
    }



    public static void AddMessage(this DataTable dataTable, string message)
    {
      dataTable.ExtendedProperties.Add("", message);
    }
  }
}