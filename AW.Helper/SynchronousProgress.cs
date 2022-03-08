using System;
using System.Threading;

namespace AW.Helper
{
  /// <summary>Provides an synchronous <see cref="T:System.IProgress`1" /> that invokes callbacks for each reported progress value for use in .</summary>
  /// <typeparam name="T">Specifies the type of the progress report value.</typeparam>
  ///  <see cref="T:System.Progress`1" />
  public class SynchronousProgress<T> : IProgress<T>
  {
    readonly Action<T> _handler;
    readonly SendOrPostCallback _invokeHandlers;

    /// <summary>Initializes the <see cref="T:AW.Helper.SynchronousProgress`1" /> object.</summary>
    public SynchronousProgress()
    {
      _invokeHandlers = InvokeHandlers;
    }

    /// <summary>Initializes the <see cref="T:AW.Helper.SynchronousProgress`1" /> object with the specified callback.</summary>
    /// <param name="handler">
    ///   A handler to invoke for each reported progress value. This handler will be invoked in addition to any delegates registered with the
    ///   <see cref="E:System.Progress`1.ProgressChanged" /> event. 
    /// </param>
    public SynchronousProgress(Action<T> handler) : this()
    {
      _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    /// <summary>Raised for each reported progress value.</summary>
    public event EventHandler<T> ProgressChanged;

    /// <summary>Reports a progress change.</summary>
    /// <param name="value">The value of the updated progress.</param>
    protected virtual void OnReport(T value)
    {
      var handler = _handler;
      var progressChanged = ProgressChanged;
      if (handler == null && progressChanged == null)
        return;
      _invokeHandlers(value);
    }

    /// <summary>Reports a progress change.</summary>
    /// <param name="value">The value of the updated progress.</param>
    void IProgress<T>.Report(T value) => OnReport(value);

    void InvokeHandlers(object state)
    {
      var e = (T)state;
      var progressChanged = ProgressChanged;
      _handler?.Invoke(e);
      if (progressChanged == null)
        return;
      progressChanged(this, e);
    }
  }
}