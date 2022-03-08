using System;
using System.Linq.Expressions;

namespace AW.Helper
{
  public class ErrorReasonInfo
  {
    public string PropertyName { get; set; }

    public string Error { get; set; }

    public ErrorReasonInfo()
    {
    }

    public ErrorReasonInfo(string error)
    {
      Error = error;
    }

    /// <inheritdoc />
    public ErrorReasonInfo(string error, string propertyName): this(error)
    {
      PropertyName = propertyName;
    }

    public static ErrorReasonInfo CreateErrorReasonInfo<T>(string errorMessage, Expression<Func<T, object>> propertySpecifier)
    {
      return new ErrorReasonInfo(errorMessage,  MemberName.For(propertySpecifier));
    }

    #region Overrides of Object

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    public override string ToString()
    {
      return GeneralHelper.Join(GeneralHelper.StringJoinSeperator, base.ToString(), PropertyName, Error);
    }

    #endregion
  }
}