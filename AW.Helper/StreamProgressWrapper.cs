using System;
using System.IO;
using System.Reflection;
using log4net;

namespace AW.Helper
{
  /// <summary>
  ///   https://blog.stephencleary.com/2012/02/reporting-progress-from-async-tasks.html
  ///   https://stackoverflow.com/questions/39742515/stream-copytoasync-with-progress-reporting-progress-is-reported-even-after-cop
  /// </summary>
  public class StreamProgressWrapper : Stream
  {
    enum ProgressType
    {
      /// <summary>
      ///   The number of bytes read or written since the last call
      /// </summary>
      CurrentBytes,

      /// <summary>
      ///   The position of the stream
      /// </summary>
      Position,

      /// <summary>
      ///   The total bytes read or written since the start
      /// </summary>
      TotalBytes,

      /// <summary>
      ///   The percentage complete 0-100
      /// </summary>
      Percentage
    }

    static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

    Stream _stream;
    readonly IProgress<long> _progress;
    readonly bool _disposeStream;
    readonly ProgressType _progressType;
    long _totalBytes;
    readonly long? _length;

    /// <summary>
    ///   Initializes a new instance of the <see cref="StreamProgressWrapper" /> class.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="progress">The progress.</param>
    /// <param name="incrementalProgress">if set to <c>true</c> [incremental progress].</param>
    /// <param name="disposeStream">if set to <c>true</c> [dispose stream].</param>
    /// <param name="length">The stream length.</param>
    /// <exception cref="System.ArgumentNullException">
    ///   stream
    ///   or
    ///   progress
    /// </exception>
    public StreamProgressWrapper(Stream stream, IProgress<long> progress, bool incrementalProgress = true, bool disposeStream = true, long length = 0)
    {
      _stream = stream ?? throw new ArgumentNullException(nameof(stream));
      _progress = progress ?? throw new ArgumentNullException(nameof(progress));
      _disposeStream = disposeStream;
      Logger.DebugMethod(stream.GetType(), null, incrementalProgress, disposeStream, length);

      if (incrementalProgress)
        _progressType = ProgressType.CurrentBytes;
      else if (length > 0)
      {
        _length = length;
        _progressType = ProgressType.Percentage;
      }
      else
        _progressType = _stream.CanSeek ? ProgressType.Position : ProgressType.TotalBytes;

      Logger.InfoFormat("Progress type: {0} disposeStream: {1}", _progressType, disposeStream);
    }

    public override bool CanRead => _stream.CanRead;

    public override bool CanSeek => _stream.CanSeek;

    public override bool CanWrite => _stream.CanWrite;

    public override long Length => _length.HasValue ? _length.Value : _stream.Length;

    public override long Position
    {
      get => _stream.Position;
      set => _stream.Position = value;
    }

    public override void Flush()
    {
      _stream.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      if (_stream == null)
        return 0;
      var numBytesRead = _stream.Read(buffer, offset, count);
      ReportProgress(numBytesRead);
      return numBytesRead;
    }

    void ReportProgress(int numBytesRead)
    {
      _totalBytes += numBytesRead;
      switch (_progressType)
      {
        case ProgressType.TotalBytes:
          Logger.DebugFormat("Progress total: {0:n0}{1}", _totalBytes, GeneralHelper.ByteSuffix);
          _progress.Report(_totalBytes);
          break;
        case ProgressType.Position:
          Logger.DebugFormat("Progress position: {0:n0}{1}", _stream.Position, GeneralHelper.ByteSuffix);
          _progress.Report(_stream.Position);
          break;
        case ProgressType.Percentage:
          var percentComplete = Convert.ToByte(Math.Round((decimal)_totalBytes / Length * 100, 0));
          Logger.DebugFormat("Progress percentage: {0}%", percentComplete);
          _progress.Report(percentComplete);
          break;
        default:
          Logger.DebugFormat("Progress: {0:n0}{1}", numBytesRead, GeneralHelper.ByteSuffix);
          _progress.Report(numBytesRead);
          break;
      }
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      return _stream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
      _stream.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      _stream.Write(buffer, offset, count);
      ReportProgress(count);
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
      try
      {
        Logger.DebugMethod(disposing);
        if (disposing && _stream != null)
          try
          {
            Logger.Debug("Flush");
            Flush();
          }
          finally
          {
            Logger.Debug("_stream.Close()");
            _stream.Close();
            if (_disposeStream)
            {
              Logger.Debug("_stream.Dispose()");
              _stream.Dispose();
            }
          }
      }
      finally
      {
        _stream = null;

        // Call base.Dispose(bool) to cleanup async IO resources
        base.Dispose(disposing);
      }
    }
  }
}