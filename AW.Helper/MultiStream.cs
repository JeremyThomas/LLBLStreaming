using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using log4net;

namespace AW.Helper
{
  /// <summary>
  ///   https://www.c-sharpcorner.com/article/combine-multiple-streams-in-a-single-net-framework-stream-o/
  /// </summary>
  public class MultiStream<T> : Stream where T : Stream
  {
    protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
    protected readonly List<T> _streamList;

    /// <inheritdoc />
    public MultiStream(IEnumerable<T> streams, bool dispose = false)
    {
      _dispose = dispose;
      _streamList = streams == null ? new List<T>() : streams.ToList();
      Logger.DebugFormat("Created with {0} streams", _streamList.Count);
    }

    public MultiStream(params T[] streams) : this((IEnumerable<T>)streams)
    {
    }

    long _position;
    protected bool _dispose;

    public override bool CanRead => _streamList.All(s => s.CanRead);

    public override bool CanSeek => _streamList.All(s => s.CanSeek);

    public override bool CanWrite => false;

    public override long Length
    {
      get { return _streamList.Where(s => s.CanSeek).Sum(stream => stream.Length); }
    }

    public override long Position
    {
      get => _position;
      set => Seek(value, SeekOrigin.Begin);
    }

    public override void Flush()
    {
      foreach (var stream in _streamList.Where(stream => stream.CanRead))
        stream.Flush();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      var len = Length;
      switch (origin)
      {
        case SeekOrigin.Begin:
          _position = offset;
          break;
        case SeekOrigin.Current:
          _position += offset;
          break;
        case SeekOrigin.End:
          _position = len - offset;
          break;
      }

      if (_position > len)
        _position = len;
      else if (_position < 0) _position = 0;
      return _position;
    }

    public override void SetLength(long value)
    {
    }

    public void AddStream(T stream)
    {
      _streamList.Add(stream);
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      long len = 0;
      var result = 0;
      var bufPos = offset;
      foreach (var stream in _streamList)
      {
        if (_position < len + stream.Length)
        {
          stream.Position = _position - len;
          var bytesRead = stream.Read(buffer, bufPos, count);
          result += bytesRead;
          bufPos += bytesRead;
          _position += bytesRead;
          if (bytesRead < count)
            count -= bytesRead;
          else
            break;
        }

        len += stream.Length;
      }

      return result;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
      Logger.DebugMethod(disposing);
      if (_dispose && disposing)
        foreach (var stream in _streamList)
          Dispose(stream);

      base.Dispose(disposing);
    }

    protected virtual void Dispose(T stream)
    {
      Logger.Debug("stream.Dispose()");
      stream.Dispose();
    }
  }

  public class MultiFileStream : MultiStream<FileStream>
  {
    public MultiFileStream(IEnumerable<string> filePaths) : base(FilePathsToStreams(filePaths), true)
    {
    }

    static IEnumerable<FileStream> FilePathsToStreams(IEnumerable<string> filePaths)
    {
      if (filePaths != null)
        foreach (var filePath in filePaths)
        {
          Logger.DebugFormat("Reading {0}", filePath);
          yield return File.OpenRead(filePath);
        }
    }

    /// <inheritdoc />
    protected override void Dispose(FileStream stream)
    {
      base.Dispose(stream);
      Logger.DebugFormat("Deleting {0}", stream.Name);
      File.Delete(stream.Name);
    }
  }
}