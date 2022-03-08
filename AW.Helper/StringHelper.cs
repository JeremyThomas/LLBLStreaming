using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SD.LLBLGen.Pro.LinqSupportClasses;

namespace AW.Helper
{
  public static class StringHelper
  {

    /// <summary>
    ///   Trims the string if is is not null or empty.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns></returns>
    public static string TrimIfNotNull(this string s)
    {
      return string.IsNullOrEmpty(s) ? s : s.Trim();
    }

    public static string TrimIfNotNull(this string s, params char[] trimChars)
    {
      return string.IsNullOrEmpty(s) ? s : s.Trim(trimChars);
    }

    /// <summary>
    ///   Checks wether the object is null or Convert.ToString is empty.
    /// </summary>
    /// <param name="s">The object.</param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(object s)
    {
      return s == null || string.IsNullOrEmpty(Convert.ToString(s));
    }

    /// <summary>
    ///   Checks the object is not null or, if it is a string, empty.
    /// </summary>
    /// <param name="o">The object.</param>
    /// <returns></returns>
    public static bool IsNullOrEmptyIfString(object o)
    {
      if (o == null)
        return true;
      return o is string value && string.IsNullOrEmpty(value);
    }

    /// <summary>
    ///   returns null if empty.
    /// </summary>
    /// <see cref="http://haacked.com/archive/2010/06/16/null-or-empty-coalescing.aspx" />
    /// <param name="item">The items.</param>
    /// <returns></returns>
    public static string AsNullIfEmpty(this string item)
    {
      return string.IsNullOrEmpty(item) ? null : item;
    }

    /// <summary>
    ///   Joins two non empty strings together as one string with a separator between each non empty original string.
    ///   Mapped to DB function call CONCAT_WS as more general version below can't be
    /// </summary>
    /// <param name="separator">The separator.</param>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    public static string JoinTwo(string separator, string first, string second)
    {
      return Join(separator, first, second);
    }

    /// <summary>
    ///   Joins an array of non empty strings together as one string with a separator between each non empty original string.
    /// </summary>
    /// <param name="separator">The separator.</param>
    /// <param name="values">The values.</param>
    /// <returns></returns>
    public static string Join(string separator, params string[] values)
    {
      return Join(separator, (IEnumerable<string>) values);
    }

    /// <summary>
    ///   Joins an array of non empty strings together as one string with a separator between each non empty original string.
    /// </summary>
    /// <param name="separator">The separator.</param>
    /// <param name="values">The values.</param>
    /// <returns></returns>
    public static string Join(string separator, IEnumerable<string> values)
    {
      return string.Join(separator, values.Where(s => !string.IsNullOrEmpty(s)));
    }

    /// <summary>
    ///   Splits the string into lines.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns></returns>
    public static string[] SplitStringIntoLines(string s)
    {
      return s.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
    }

    public static int CountUpperCharsInString(string s)
    {
      return s.Count(char.IsUpper);
    }

    /// <summary>
    ///   Counts the number of lines in string.
    /// </summary>
    /// <param name="s">The s.</param>
    /// <returns></returns>
    public static int CountLinesInString(string s)
    {
      return CountLinesInTextReader(new StringReader(s));
    }

    /// <summary>
    ///   Counts the lines in text reader.
    /// </summary>
    /// <param name="textReader">The text reader.</param>
    /// <returns></returns>
    public static int CountLinesInTextReader(TextReader textReader)
    {
      var temp = textReader.ReadLine();
      var count = 0;
      while (temp != null)
      {
        count++;
        temp = textReader.ReadLine();
      }

      return count;
    }

    /// <summary>
    ///   Count number of occurrences of a character in a string.
    /// </summary>
    /// <param name="s">The s.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static long CharCount(string s, char value)
    {
      long count = 0;
      var start = 0;
      while ((start = s.IndexOf(value, start)) != -1)
      {
        count++;
        start++;
      }

      return count;
    }

    /// <summary>
    ///   Count number of occurrences of a substring in a string.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <param name="subString">The sub string.</param>
    /// <returns></returns>
    public static long SubStringCount(string s, string subString)
    {
      long count = 0;
      var start = 0;
      while ((start = s.IndexOf(subString, start)) != -1)
      {
        count++;
        start++;
      }

      return count;
    }

    /// <summary>
    ///   Converts a comma separated list of guids without {} into and IList of Guids.
    /// </summary>
    /// <param name="guidsString">Comma separated quid strings</param>
    /// <returns>IList of Guids</returns>
    public static IList<Guid> ToGrids(string guidsString)
    {
      var guids = new List<Guid>();

      if (!string.IsNullOrEmpty(guidsString))
      {
        var st = guidsString.Split(',');
        guids.AddRange(from s in st where s != Guid.Empty.ToString() select new Guid("{" + s + "}"));
      }

      return guids;
    }

    /// <summary>
    ///   Converts a comma separated list of integers into an IList.
    /// </summary>
    /// <param name="commaDelimitedIntegers">String containing comma delimited integers</param>
    /// <returns>IList of integers</returns>
    public static IList<int> ToInts(string commaDelimitedIntegers)
    {
      return ToInts(commaDelimitedIntegers, ',');
    }

    public static IList<int> ToInts(string delimitedIntegers, char separator)
    {
      var ints = new List<int>();
      if (!string.IsNullOrEmpty(delimitedIntegers))
      {
        var st = delimitedIntegers.Split(separator);
        ints.AddRange(st.Select(s => int.Parse(StripNumeric(s))));
      }

      return ints;
    }

    /// <summary>
    ///   Converts a comma separated list of numbers into an IList.
    /// </summary>
    /// <param name="delimitedNumbers">The delimited numbers.</param>
    /// <param name="separator">The separator.</param>
    /// <returns></returns>
    public static IList<decimal> ToDecimals(string delimitedNumbers, char separator = ',')
    {
      var decimals = new List<decimal>();
      if (!string.IsNullOrEmpty(delimitedNumbers))
      {
        var st = delimitedNumbers.Split(separator).FilterOutEmpty();
        decimals.AddRange(st.Select(s => decimal.Parse(StripNumeric(s))));
      }

      return decimals;
    }

    public static bool Contains(this string source, string value, StringComparison comp)
    {
      if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(source)) return false;
      return source.IndexOf(value, comp) >= 0;
    }

    /// <summary>
    ///   Returns a value indicating whether the specified System.String object occurs
    ///   within this string - case-insensitive.
    ///   This is mapped to SQL so can be used in Linq-To-DB, see AQD.Model.Oracle.DataAccessAdapter
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="value">The string to seek.</param>
    /// <returns>
    ///   true if the value parameter occurs within this string, or if value is the
    ///   empty string (""); otherwise, false.
    /// </returns>
    [DatabaseMappedFunction]
    public static bool ContainsIgnoreCase(this string source, string value)
    {
      return Contains(source, value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///   Determines whether this string and a specified System.String object have the same value - case-insensitive.
    ///   This is mapped to SQL so can be used in Linq-To-DB.
    /// </summary>
    /// <see cref="AQD.Model.Oracle.DataAccessAdapter" />
    /// <param name="source">The source.</param>
    /// <param name="value">The value.</param>
    /// <returns>true if the value of the value parameter is the same as this string; otherwise, false.</returns>
    [DatabaseMappedFunction]
    public static bool EqualsIgnoreCase(this string source, string value)
    {
      if (source == null)
        return value == null;
      return source.Equals(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///   Determines whether this string and a specified System.String object have the same value - case-insensitive.
    ///   This is mapped to Oracle SQL REGEXP_LIKE so can be used in Linq-To-DB, see AQD.Model.Oracle.DataAccessAdapter
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="value">The value.</param>
    /// <returns>true if the value of the value parameter is the same as this string; otherwise, false.</returns>
    [DatabaseMappedFunction]
    public static bool EqualsIgnoreCaseForClob(string source, string value)
    {
      return EqualsIgnoreCase(source, value);
    }

    /// <summary>
    ///   String extension that escapes quotes and double quotes, typically in resource entries, for reuse in javascript etc.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string Escaped(this string source)
    {
      if (!string.IsNullOrEmpty(source))
      {
        source = source.Replace("'", "\\'");
        if (source.Contains("\""))
          source = source.Replace("\"", ""); //.Replace("\"", "\\\"");  BEK 26/10/2011 - only solution is to remove the double quotes as we use ' quotes to delim java strings.
      }

      return source;
    }

    /// <summary>
    ///   String extension that escapes quotes and double quotes, typically in resource entries, for reuse in javascript etc.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string Escaped(this string source, bool stripNewLine)
    {
      source = Escaped(source);
      if (!string.IsNullOrEmpty(source) && stripNewLine)
      {
        source = source.Trim();

        if (source.Contains("\n"))
          source = source.Replace("\n", ""); // only solution is to remove the new line as JS doesn't support strings on multiple lines
      }

      return source;
    }

    /// <summary>
    ///   Removes any full stops in the string
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string NoFullStops(this string source)
    {
      return source.Replace(".", "");
    }

    public static string Coalesce(params string[] args)
    {
      return (string) Coalesce((object[])args);
    }

    /// <summary>
    ///   Coalesces the specified args.
    ///   Equivalent to ?? operator except it treats empty strings as nulls
    /// </summary>
    /// <param name="args">The args.</param>
    /// <returns>The first object with a non-empty ToString value</returns>
    public static object Coalesce(params object[] args)
    {
      return args.FirstOrDefault(arg => arg != null && !string.IsNullOrEmpty(Convert.ToString(arg)));
    }

    public static object Coalesce(params Func<object>[] args)
    {
      return (from arg in args where arg != null select arg()).FirstOrDefault(value => !string.IsNullOrEmpty(Convert.ToString(value)));
    }

    public static string CoalesceToString(params object[] args)
    {
      return Convert.ToString(Coalesce(args));
    }

    public static string CoalesceToString(params Func<object>[] args)
    {
      return Convert.ToString(Coalesce(args));
    }

    public static string CoalesceToString(string first, params Func<object>[] args)
    {
      return string.IsNullOrEmpty(first) ? Convert.ToString(Coalesce(args)) : first;
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string FirstCharacterToLower(string str)
    {
      if (string.IsNullOrEmpty(str) || char.IsLower(str, 0))
        return str;

      return char.ToLowerInvariant(str[0]) + str.Substring(1);
    }

    #region From Legacy

    /// <summary>
    ///   Tests whether a char is in a set of chars.
    /// </summary>
    /// <param name="charToTest">The char to test.</param>
    /// <param name="chars">The set of chars.</param>
    /// <returns>True if member of set</returns>
    public static bool CharIsInChars(char charToTest, params char[] chars)
    {
      var index = Array.IndexOf(chars, charToTest);
      return index > -1;
    }

    public static bool CharIsInChars(char charToTest, string chars)
    {
      return chars.Contains(charToTest.ToString());
    }

    /// <summary>
    ///   Excludes or includes the chars in a string. If exclude the chars are removed if not exclude only the chars are kept
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="exclude">if set to <c>true</c> strip chars otherwise only include chars.</param>
    /// <param name="chars">The chars.</param>
    /// <returns></returns>
    public static string ExcludeChars(string text, bool exclude, params char[] chars)
    {
      if (!string.IsNullOrEmpty(text))
      {
        var sb = new StringBuilder();
        foreach (var character in from txt in text
          let charIsInChars = CharIsInChars(txt, chars)
          where charIsInChars && !exclude || !charIsInChars && exclude
          select txt)
          sb.Append(character);
        return sb.ToString();
      }

      return string.Empty;
    }

    /// <summary>
    ///   Excludes the chars.
    /// </summary>
    /// <remarks>
    ///   Use 'Inplace Char Array' method (TrimAllWithInplaceCharArray) from here if need more speed
    ///   http://www.codeproject.com/Articles/1014073/Fastest-method-to-remove-all-whitespace-from-Strin
    /// </remarks>
    /// <param name="text">The text.</param>
    /// <param name="exclude">if set to <c>true</c> strip chars otherwise only include chars.</param>
    /// <param name="chars">The chars to exclude or include.</param>
    /// <returns></returns>
    public static string ExcludeChars(string text, bool exclude, string chars)
    {
      if (!string.IsNullOrEmpty(text))
      {
        var sb = new StringBuilder();
        foreach (var character in from charToTest in text
          let charIsInChars = CharIsInChars(charToTest, chars)
          where charIsInChars && !exclude || !charIsInChars && exclude
          select charToTest)
          sb.Append(character);
        return sb.ToString();
      }

      return string.Empty;
    }

    /// <summary>
    ///   Strips the string of non numeric characters.
    /// </summary>
    /// <remarks>http://net-test2/mantis/view.php?id=4903</remarks>
    /// <param name="alphaNumericText">The alpha numeric text.</param>
    /// <returns></returns>
    public static string StripNumeric(string alphaNumericText)
    {
      return ExcludeChars(alphaNumericText, false, Numeric);
    }


    public static string Digits = "0123456789";

    public static string LowerCaseAlpha = new string(Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (char) i).ToArray());

    public static string UpperCaseAlpha = new string(Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (char) i).ToArray());

    public static string Alpha = LowerCaseAlpha + UpperCaseAlpha;

    public static string UpperCaseAlphaDigits = UpperCaseAlpha + Digits;

    public static string AlphaDigits = LowerCaseAlpha + UpperCaseAlphaDigits;

    public static string Numeric = Digits
                                   + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
                                   + NumberFormatInfo.CurrentInfo.NegativeSign;

    #endregion

    /// <summary>
    ///   Strips the string of numeric characters.
    /// </summary>
    /// <param name="alphaNumericText">The alpha numeric text.</param>
    /// <returns></returns>
    public static string StripAlpha(string alphaNumericText)
    {
      return ExcludeChars(alphaNumericText, true, Numeric);
    }

    /// <summary>
    ///   Strips all but alpha digits.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns></returns>
    public static string StripAllButUpperCaseAlphaDigits(string text)
    {
      return ExcludeChars(text, false, UpperCaseAlphaDigits);
    }

    public static string TrimCommaFromEnd(string text)
    {
      return text == null ? null : text.TrimEnd(',');
    }

    /// <summary>
    ///   Get string value between [first] a and [last] b.
    ///   <!--<see cref="http://www.dotnetperls.com/between-before-after"/>-->
    /// </summary>
    public static string Between(this string value, string a, string b)
    {
      if (value == null) return null;
      var posA = value.IndexOf(a);
      var posB = value.LastIndexOf(b);
      if (posA == -1)
        return "";
      if (posB == -1)
        return "";
      var adjustedPosA = posA + a.Length;
      if (adjustedPosA >= posB)
        return "";
      return value.Substring(adjustedPosA, posB - adjustedPosA);
    }

    /// <summary>
    ///   Get string value including [first] a.
    /// </summary>
    public static string GetSubstringEndingWith(this string value, string a, string notFoundValue = "")
    {
      if (value == null) return null;
      var posA = value.IndexOf(a, StringComparison.Ordinal);
      return posA == -1 ? notFoundValue : value.Substring(0, posA+a.Length);
    }

    public static string Before(this string value, char a, string notFoundValue = "")
    {
      return Before(value, a.ToString(), notFoundValue);
    }

    /// <summary>
    ///   Get string value before [first] a.
    /// </summary>
    public static string Before(this string value, string a, string notFoundValue = "")
    {
      if (value == null) return null;
      var posA = value.IndexOf(a, StringComparison.Ordinal);
      return posA == -1 ? notFoundValue : value.Substring(0, posA);
    }

    public static string BeforeLast(this string value, char a, string notFoundValue = "")
    {
      return BeforeLast(value, a.ToString(), notFoundValue);
    }

    /// <summary>
    ///   Get string value before [last] a.
    /// </summary>
    public static string BeforeLast(this string value, string a, string notFoundValue = "")
    {
      if (value == null) return null;
      var posA = value.LastIndexOf(a, StringComparison.Ordinal);
      if (posA == -1)
        return notFoundValue;
      return value.Substring(0, posA);
    }

    public static string AfterLast(this string value, char a, string notFoundValue = "")
    {
      return AfterLast(value, a.ToString(), notFoundValue);
    }

    /// <summary>
    ///   Get string value after [last] a.
    /// </summary>
    public static string AfterLast(this string value, string a, string notFoundValue = "")
    {
      if (value == null) return null;
      var posA = value.LastIndexOf(a, StringComparison.Ordinal);
      if (posA == -1)
        return notFoundValue;
      var adjustedPosA = posA + a.Length;
      if (adjustedPosA >= value.Length)
        return notFoundValue;
      return value.Substring(adjustedPosA);
    }

    public static string AfterFirst(this string value, params char[] anyOf)
    {
      if (value == null) return null;
      var posA = value.IndexOfAny(anyOf);
      if (posA == -1)
        return "";
      var adjustedPosA = posA + 1;
      if (adjustedPosA >= value.Length)
        return "";
      return value.Substring(adjustedPosA);
    }

    /// <summary>
    ///   Replaces the oldValue or removes it. If value is null returns null
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    /// <returns></returns>
    public static string ReplaceOrRemove(this string value, string oldValue, string newValue = null)
    {
      return value == null ? null : value.Replace(oldValue, newValue);
    }

    /// <summary>
    ///   Truncates the string to the maximum length.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="maxLength">The maximum length.</param>
    /// <returns></returns>
    public static string TruncateLongString(this string str, int maxLength)
    {
      return str == null ? null : str.Substring(0, Math.Min(str.Length, maxLength));
    }

    /// <summary>
    ///   Filters out the empty strings.
    /// </summary>
    /// <param name="strings">The strings.</param>
    /// <returns></returns>
    public static IEnumerable<string> FilterOutEmpty(this IEnumerable<string> strings)
    {
      return strings.Where(s => !string.IsNullOrWhiteSpace(s));
    }

    public static string MakeSingleLine(this string source)
    {
      if (source == null) return null;
      var cleaned = Regex.Replace(source,"[\r\n]*","");
      return cleaned;
    }

    public static string RemoveHtmlTags(this string source, bool normalise = true)
    {
      if (source == null) 
        return null;
      var noHtml = Regex.Replace(source, @"<[^>]+>|&nbsp;", "").Trim();
      var noHtmlNormalised = normalise ? Regex.Replace(noHtml, @"\s{2,}", " ") : noHtml;
      return noHtmlNormalised;
    }

    /// <summary>
    ///   Removes the empty nodes.
    ///   http://stackoverflow.com/questions/7318408/remove-empty-xml-tags
    /// </summary>
    /// <param name="xml">The XML.</param>
    /// <returns></returns>
    public static string RemoveEmptyNodes(string xml)
    {
      var xd = XDocument.Parse(xml);
      xd.Descendants().Attributes().Where(a => string.IsNullOrWhiteSpace(a.Value)).Remove();
      xd.Descendants()
        .Where(e => e.Attributes().All(a => a.IsNamespaceDeclaration)
                    && string.IsNullOrWhiteSpace(e.Value))
        .Remove();
      return xd.ToString();
    }

    public static string ReplaceIgnoreCase(this string source, string oldString,
      string newString)
    {
      var index = source.IndexOf(oldString, StringComparison.OrdinalIgnoreCase);

      while (index > -1)
      {
        source = source.Remove(index, oldString.Length);
        source = source.Insert(index, newString);

        index = source.IndexOf(oldString, index + newString.Length, StringComparison.OrdinalIgnoreCase);
      }

      return source;
    }

    public static string ToUpper(string s)
    {
      return s == null ? null : s.ToUpper();
    }

    static readonly byte[] BomMarkUtf8 = Encoding.UTF8.GetPreamble();

    static readonly string BomMarkUtf8String = Encoding.UTF8.GetString(BomMarkUtf8);

    public static string ToString(byte[] bytes)
    {
      return bytes.Aggregate(string.Empty, (current, b) => current + (char) b);
    }

    /// <summary>
    ///   Strip UTF8 Byte Order Mark from string.
    ///   http://stackoverflow.com/questions/1317700/strip-byte-order-mark-from-string-in-c-sharp
    /// </summary>
    /// <param name="p">The p.</param>
    /// <returns></returns>
    public static string RemoveBom(string p)
    {
      if (StartsWithUtf8Bom(p))
        p = p.Remove(0, BomMarkUtf8String.Length);
      return p.Replace("\0", "");
    }

    public static bool StartsWithUtf8Bom(string p)
    {
      return p.StartsWith(BomMarkUtf8String, StringComparison.Ordinal);
    }

    public static bool StartsWithUtf8Bom(Stream s)
    {
      var sPosition = s.Position;
      var startsWithUtf8Bom = IsUtf8Bom(s);
      s.Seek(sPosition, SeekOrigin.Begin);
      return startsWithUtf8Bom;
    }

    static bool IsUtf8Bom(Stream s)
    {
      var buffer = new byte[BomMarkUtf8.Length];
      s.Read(buffer, 0, BomMarkUtf8.Length);
      return StartsWithUtf8Bom(buffer);
    }

    public static bool StartsWithUtf8Bom(byte[] chars)
    {
      return chars.Take(BomMarkUtf8.Length).SequenceEqual(BomMarkUtf8);
    }

    /// <summary>
    ///   Splitting a string into chunks of a certain size
    /// </summary>
    /// <remarks>http://stackoverflow.com/questions/1450774/splitting-a-string-into-chunks-of-a-certain-size</remarks>
    /// <param name="str">The string.</param>
    /// <param name="maxChunkSize">Maximum size of the chunk.</param>
    /// <returns></returns>
    public static IEnumerable<string> SplitIntoChunksUpto(string str, int maxChunkSize)
    {
      for (var i = 0; i < str.Length; i += maxChunkSize)
        yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
    }

    /// <summary>
    /// Calculates a stable repeatable hash code for a string.
    /// </summary>
    /// <remarks>https://stackoverflow.com/questions/36845430/persistent-hashcode-for-strings</remarks>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int GetStableHashCode(this string str)
    {
      unchecked
      {
        int hash1 = 5381;
        int hash2 = hash1;

        for(int i = 0; i < str.Length && str[i] != '\0'; i += 2)
        {
          hash1 = ((hash1 << 5) + hash1) ^ str[i];
          if (i == str.Length - 1 || str[i+1] == '\0')
            break;
          hash2 = ((hash2 << 5) + hash2) ^ str[i+1];
        }

        return hash1 + (hash2*1566083941);
      }
    }

    /// <summary>
    /// Drop Trailing Zeros
    /// </summary>
    /// <remarks>https://stackoverflow.com/questions/3104615/best-way-to-display-decimal-without-trailing-zeroes</remarks>
    /// <param name="test"></param>
    /// <returns></returns>
    public static string DropTrailingZeros(string test)
    {
      if (test.Contains(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator))
      {
        test = test.TrimEnd('0');
      }

      if (test.EndsWith(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator))
      {
        test = test.Substring(0,
          test.Length - CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator.Length);
      }

      return test;
    }

  }
}