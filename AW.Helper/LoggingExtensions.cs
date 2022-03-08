using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AW.Helper.Annotations;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using log4net.Util;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace AW.Helper
{
  public static class LoggingExtensions
  {
    const string ExceptionUserMessageKey = "UserMessage";
    const string ExceptionLoggerName = "Exceptions";
    public static readonly ILog ExceptionLogger = LogManager.GetLogger(ExceptionLoggerName);

    public static bool LoggerIncludeFilePath { get; }

    public static void SetUserMessage(this Exception e, string userMessage)
    {
      e.Data[ExceptionUserMessageKey] = userMessage;
    }

    public static string GetUserMessage(this Exception e)
    {
      return Convert.ToString(e.Data[ExceptionUserMessageKey]);
    }

    /// <summary>
    ///   Logs the exception including any SQL statement(s) involved in the exception stack.
    ///   If the exception logger isn't supplied it uses the general exception logger,
    ///   for classes that don't have a logger defined.
    /// </summary>
    /// <param name="e">The exception.</param>
    /// <param name="description">The description.</param>
    /// <param name="exceptionLogger">The exception logger.</param>
    /// <param name="appendExceptionMessageToDescription"></param>
    public static void LogException(this Exception e, string description = null, ILog exceptionLogger = null, bool appendExceptionMessageToDescription = false)
    {
      exceptionLogger = exceptionLogger ?? ExceptionLogger;
      var realException = e.StripOutTargetInvocationException();
      var message = GeneralHelper.Join(Environment.NewLine, description, GetUserMessage(e));
      if (string.IsNullOrWhiteSpace(message))
        message = realException.Message;
      else if (appendExceptionMessageToDescription)
        message = GeneralHelper.Join(Environment.NewLine, message, realException.Message);
      exceptionLogger.Error(message, realException);
      var queriesInvolvedInException = GetOrmQueryExecutionExceptions(e);
      if (!queriesInvolvedInException.IsNullOrEmpty())
        exceptionLogger.ErrorFormat("SQL statement(s) involved in the above {0}:{1}", e.GetType().Name, queriesInvolvedInException.JoinAsString(Environment.NewLine));
      var baseException = realException.GetBaseException();
      if (baseException is FileNotFoundException)
        exceptionLogger.Error($"FileNotFoundException from the above {message}{Environment.NewLine}{Environment.StackTrace.Replace("\r\n",Environment.NewLine)}", baseException);
      //var fusionLogs = GetFileNotFoundExceptionExceptions(e);
      //if (!fusionLogs.IsNullOrEmpty())
      //  exceptionLogger.ErrorFormatExt (baseException.GetBaseException(), "Fusion Log from the above {0}:{1}", message, fusionLogs.JoinAsString(Environment.NewLine));
    }

    public static void ErrorFormatExt(this ILog logger, Exception e, string format, params object[] args)
    {
      logger.Error(string.Format(format, args), e);
    }

    static List<string> GetOrmQueryExecutionExceptions(Exception e)
    {
      return GetExceptionAndInners(e).OfType<ORMQueryExecutionException>().Select(oe => oe.QueryExecuted).ToList();
    }

    static List<string> GetFileNotFoundExceptionExceptions(Exception e)
    {
      return GetExceptionAndInners(e).OfType<FileNotFoundException>().Select(oe => oe.FusionLog).ToList();
    }

    public static string GetSQLInvolvedInException(Exception e)
    {
      var queriesInvolvedInException = GetOrmQueryExecutionExceptions(e);
      return queriesInvolvedInException.IsNullOrEmpty() ? "" : queriesInvolvedInException.JoinAsString(Environment.NewLine);
    }

    /// <summary>
    ///   Gets the exception followed by all the inner exceptions.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns></returns>
    public static IEnumerable<Exception> GetExceptionAndInners(Exception exception)
    {
      //Need to create overload where child is not Enumerable LinqToObjectsExtensionMethods.DescendantsAndSelf(exception, e => e.InnerException);
      while (exception != null)
      {
        yield return exception;
        exception = exception.InnerException;
      }
    }

    public static void LogError(Exception e, StringBuilder errors, params object[] details)
    {
      LogException(e);
      foreach (var detail in details) errors.AppendLine(Convert.ToString(detail));
      errors.AppendLine(e.Message);
    }

    static LoggingExtensions()
    {
      LoggerIncludeFilePath = false;
    }

    public static void ErrorWithCallerContext(this ILog log, object message = null, [CallerFilePath] string filePath = "",
      [CallerMemberName] string memberName = "",
      [CallerLineNumber] int lineNumber = 0
    )
    {
      if (LoggerIncludeFilePath)
        log.ErrorFormat("{0}|{1}|{2}|{3}", filePath, memberName, lineNumber, message);
      else
        log.ErrorFormat("{0}|{1}|{2}", memberName, lineNumber, message);
    }

    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public static void ErrorWithCallerContext(this ILog log, Func<string> formattingCallback, [CallerFilePath] string filePath = "",
      [CallerMemberName] string memberName = "",
      [CallerLineNumber] int lineNumber = 0
    )
    {
      if (log.IsErrorEnabled)
        log.ErrorWithCallerContext(formattingCallback(), filePath, memberName, lineNumber);
    }

    public static void WarnWithCallerContext(this ILog log, Func<string> formattingCallback, [CallerFilePath] string filePath = "",
      [CallerMemberName] string memberName = "",
      [CallerLineNumber] int lineNumber = 0
    )
    {
      if (log.IsInfoEnabled)
        if (LoggerIncludeFilePath)
          log.WarnFormat("{0}|{1}|{2}|{3}", filePath, memberName, lineNumber, formattingCallback());
        else
          log.WarnFormat("{0}|{1}|{2}", memberName, lineNumber, formattingCallback());
    }

    public static void InfoWithCallerContext(this ILog log, object message = null, [CallerFilePath] string filePath = "",
      [CallerMemberName] string memberName = "",
      [CallerLineNumber] int lineNumber = 0
    )
    {
      if (LoggerIncludeFilePath)
        log.InfoFormat("{0}|{1}|{2}|{3}", filePath, memberName, lineNumber, message);
      else
        log.InfoFormat("{0}|{1}|{2}", memberName, lineNumber, message);
    }

    public static void InfoWithCallerContext(this ILog log, Func<string> formattingCallback, [CallerFilePath] string filePath = "",
      [CallerMemberName] string memberName = "",
      [CallerLineNumber] int lineNumber = 0
    )
    {
      if (log.IsInfoEnabled)
        if (LoggerIncludeFilePath)
          log.InfoFormat("{0}|{1}|{2}|{3}", filePath, memberName, lineNumber, formattingCallback());
        else
          log.InfoFormat("{0}|{1}|{2}", memberName, lineNumber, formattingCallback());
    }

    public static void DebugWithCallerContext(this ILog log, object message = null, [CallerFilePath] string filePath = "",
      [CallerMemberName] string memberName = "",
      [CallerLineNumber] int lineNumber = 0
    )
    {
      if (LoggerIncludeFilePath)
        log.DebugFormat("{0}|{1}|{2}|{3}", filePath, memberName, lineNumber, message);
      else
        log.DebugFormat("{0}|{1}|{2}", memberName, lineNumber, message);
    }

    public static void DebugWithCallerContext(this ILog log, Func<string> formattingCallback, [CallerFilePath] string filePath = "",
      [CallerMemberName] string memberName = "",
      [CallerLineNumber] int lineNumber = 0
    )
    {
      if (log.IsDebugEnabled)
        if (LoggerIncludeFilePath)
          log.DebugFormat("{0}|{1}|{2}|{3}", filePath, memberName, lineNumber, formattingCallback());
        else
          log.DebugFormat("{0}|{1}|{2}", memberName, lineNumber, formattingCallback());
    }

    public static CallerContext WithCallerContext(this ILog logger, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
      return new CallerContext(logger, filePath, memberName, lineNumber);
    }

    /// <summary>
    ///   Logs the exception including any SQL statement(s) involved in the exception stack.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="description">The description.</param>
    /// <param name="e">The e.</param>
    public static void ErrorWithSql(this ILog logger, string description, Exception e)
    {
      e.LogException(description, logger);
    }

    public static void ErrorWithSql(this ILog logger, Exception e)
    {
      ErrorWithSql(logger, null, e);
    }

    public static void LogMethodParameters(this ILog logger, Level level, params object[] parameters)
    {
      try
      {
        if (logger == null)
          throw new ArgumentNullException(nameof(logger));

        if (logger.Logger.IsEnabledFor(level))
        {
          var stackTrace = new StackTrace(false);
          var stackFrame = stackTrace.GetFrame(2);
          var method = stackFrame.GetMethod();
          if (method.Name == "MoveNext")
          {
            stackFrame = stackTrace.GetFrame(4);
            method = stackFrame.GetMethod();
          }
          var builder = GetMethodParameters(method, parameters);
          if (level == Level.Debug)
            logger.Debug(builder.ToString());
          else
            logger.Info(builder.ToString());
        }
      }
      catch (Exception ex)
      {
        LogLog.Error(typeof(LoggingExtensions), "Exception while logging exception", ex);
      }
    }

    public static StringBuilder GetThisMethodsParameters(params object[] parameters)
    {
      return GetMethodParameters(new StackTrace(false).GetFrame(1).GetMethod(), parameters);
    }

    public static StringBuilder GetMethodParameters(MethodBase method, params object[] parameters)
    {
      if (method == null)
        method = new StackTrace(false).GetFrame(1).GetMethod();
      var builder = new StringBuilder();
      builder.AppendFormat("Method: [{0}]. ", method.Name);
      if (parameters != null && parameters.Length > 0)
      {
        var param = method.GetParameters();
        if (parameters.Length == param.Length)
        {
          // parameters.Length is at least 1 
          builder.AppendFormat("Parameters: ({0}: [{1}]", param[0].Name, GetParameterDisplayValue(parameters[0]));
          for (var i = 1; i < parameters.Length; i++) builder.AppendFormat(", {0}: [{1}]", param[i].Name, GetParameterDisplayValue(parameters[i]));
          builder.Append(")");
        }
        else
        {
          // parameters.Length is at least 1 
          builder.AppendFormat("Parameters: (p0: [{0}]", GetParameterDisplayValue(parameters[0]));
          for (var i = 1; i < parameters.Length; i++) builder.AppendFormat(", p{0}: [{1}]", i, GetParameterDisplayValue(parameters[i]));
          builder.Append(")");
        }
      }

      return builder;
    }

    static object GetParameterDisplayValue(object param)
    {
      if (param != null)
      {
        var type = param.GetType();
        if (type.IsGenericType)
        {
          var genericType = type.GetGenericTypeDefinition();
          if (genericType == typeof(IEnumerable<>))
            param = typeof(GeneralHelper)
              .GetMethod("JoinAsString", BindingFlags.Static | BindingFlags.Public, null, new[] {type}, null)
              ?.MakeGenericMethod(type.GetGenericArguments()[0])
              .Invoke(null, new[] {param});
          else
          {
            if (param is IEnumerable enumerable)
              param = GeneralHelper.JoinAsString(enumerable);
          }
        }
        else if (type.IsArray) param = GeneralHelper.JoinAsString((Array) param);
      }

      return param ?? "(null)";
    }

    public static void DebugMethod(this ILog logger, params object[] parameters)
    {
      LogMethodParameters(logger, Level.Debug, parameters);
    }

    public static void InfoMethod(this ILog logger, params object[] parameters)
    {
      LogMethodParameters(logger, Level.Info, parameters);
    }

    public static void DebugPropertySet(this ILog logger, params object[] parameters)
    {
      LogMethodParameters(logger, Level.Debug, parameters);
    }

    public static void DebugPropertyGet(this ILog logger, params object[] parameters)
    {
      LogMethodParameters(logger, Level.Debug, parameters);
    }

    public static ICollection ConfigureLog4Net()
    {
      //Trace.Listeners.Add(new Log4NetTraceListener());
      var log4NetConfigFileInfo = new FileInfo(AppContext.BaseDirectory + "log4net.config");
      if (log4NetConfigFileInfo.Exists)
      {
        XmlConfigurator.Configure(log4NetConfigFileInfo);
        if (!LogManager.GetRepository().Configured)
          return XmlConfigurator.Configure();
      }
      else
        return XmlConfigurator.Configure();
      return null;
    }

    public static string GetLogFileDirectory()
    {
      var logFileAppender = GetLogFileAppender();
      if (logFileAppender != null)
      {
        var directoryName = Path.GetDirectoryName(logFileAppender.File);
        return directoryName;
      }

      return AppContext.BaseDirectory;
    }

    public static FileAppender GetLogFileAppender()
    {
      //Get the logger repository hierarchy.  
      if (LogManager.GetRepository() is not Hierarchy repository) 
        return null;
      var fileAppender = repository.Root.Appenders.OfType<FileAppender>().FirstOrDefault();
      return fileAppender;
    }
  }

  /// <summary>
  ///   Used to Contain [CallerFilePath] [CallerMemberName] [CallerLineNumber]
  /// </summary>
  /// <remarks>
  ///   https://logging.apache.org/log4net/release/sdk/log4net.Layout.PatternLayout.html
  ///   Log4net can already display this info but as the above link says it:
  ///   Note about caller location information.The following patterns %type %file %line %method %location %class %C %F %L %l %M all generate caller location information. Location information uses the
  ///   System.Diagnostics.StackTrace class to generate a call stack. The caller's information is then extracted from this stack.
  /// </remarks>
  public struct CallerContext
  {
    readonly ILog _logger;
    readonly string _filePath;
    readonly string _memberName;
    readonly int _lineNumber;

    public CallerContext(ILog logger, string filePath, string memberName, int lineNumber)
    {
      _logger = logger;
      _filePath = filePath;
      _memberName = memberName;
      _lineNumber = lineNumber;
    }

    [StringFormatMethod("message")] //https://www.jetbrains.com/resharper/help/Code_Analysis__Annotations_in_Source_Code.html
    public void DebugFormat(string message, params object[] args)
    {
      _logger.DebugFormat(BuildMessage(_filePath, _memberName, _lineNumber, message), args);
    }

    [StringFormatMethod("message")]
    public void InfoFormat(string message, params object[] args)
    {
      _logger.InfoFormat(BuildMessage(_filePath, _memberName, _lineNumber, message), args);
    }

    [StringFormatMethod("message")]
    public void WarnFormat(string message, params object[] args)
    {
      _logger.WarnFormat(BuildMessage(_filePath, _memberName, _lineNumber, message), args);
    }

    [StringFormatMethod("message")]
    public void ErrorFormat(string message, params object[] args)
    {
      _logger.ErrorFormat(BuildMessage(_filePath, _memberName, _lineNumber, message), args);
    }

    static string BuildMessage(string filePath, string memberName, int lineNumber, string message)
    {
      if (LoggingExtensions.LoggerIncludeFilePath)
        return filePath + ":" + memberName + ":" + lineNumber + "|" + message;
      return memberName + ":" + lineNumber + "|" + message;
    }
  }
}