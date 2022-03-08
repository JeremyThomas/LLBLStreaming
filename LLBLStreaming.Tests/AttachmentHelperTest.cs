﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using AW.Dal.SqlServer;
using AW.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StealFocus.MSTestExtensions;

namespace LLBLStreaming.Tests
{
  [TestClass]
  public class AttachmentHelperTest : MSTestExtensionsTestClass
  {
    public static readonly ILog Logger = LogManager.GetLogger("Tests");

    const string BinarydataFileName = "binarydata.bin";

    public static void TraceOut(string msg, [CallerMemberName] string memberName = "")
    {
      msg = memberName + ": " + msg;
      WriteLine(msg);
      Logger.Info(msg);
    }

    static void WriteLine(string msg)
    {
      Trace.WriteLine(msg);
      Console.WriteLine(msg);
    }

    [TestMethod]
    [TestTransaction]
    public void TestStreamBlobToServer()
    {
      var fileLength = CreateDemoFiles();

      // The Progress<T> constructor captures our UI context,
      //  so the lambda will be run on the UI thread.
      var progress = new Progress<long>(percent => TraceOut(percent.ToString()));

      var tokenSource = new CancellationTokenSource();
      var dataAccessAdapter = new DataAccessAdapter();
      var task = AttachmentHelper.StreamBlobToDataBase(dataAccessAdapter, tokenSource.Token, progress, new UploadedFile(BinarydataFileName, BinarydataFileName));
      task.Wait(tokenSource.Token);
      task.Result.Should().BeGreaterOrEqualTo(1);
      var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), BinarydataFileName);
      var downLoadFileLength = AttachmentHelper
        .CopyBinaryValueToFile(dataAccessAdapter, task.Result, filePath, tokenSource.Token, progress).Result;
      File.Exists(filePath).Should().BeTrue();
      downLoadFileLength.Should().Be(fileLength);
      File.Delete(filePath);
    }

    /// <summary>
    ///   This is used to generate the files which are used by the other sample methods
    /// </summary>
    static long CreateDemoFiles()
    {
      var rand = new Random();
      var data = new byte[1024];
      rand.NextBytes(data);

      using var file = File.Open(BinarydataFileName, FileMode.Create);
      file.Write(data, 0, data.Length);
      return file.Length;
    }
  }
}