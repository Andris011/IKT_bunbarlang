namespace IKT_bunbarlang.Utility;

public class StringUtil
{
    public static string CombineMultiLine(string first, string second, string joiner)
    {
        string[] linesFirst = first.Split('\n');
        string[] linesSecond = second.Split('\n');

        int maxLength = Math.Max(linesFirst.Length, linesSecond.Length);

        string combined = "";

        for (int i = 0; i < maxLength; i++)
        {
            if (i > 0) combined += '\n';
            if (i < linesFirst.Length) combined += linesFirst[i];
            if (i < linesSecond.Length) combined += joiner + linesSecond[i];
        }

        return combined;
    }

    public static string IndentString(string content, int indentSize)
    {
        string output = "";
        string indent = new string(' ', indentSize);
        string[] lines = content.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            if (i > 0)
            {
                output += '\n';
            }

            if (lines[i] != "")
            {
                output += indent + lines[i];
            }
        }

        return output;
    }

    public static int StringLength(string text)
    {
        int length = 0;

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '\x1B')
            {
                for (; i < text.Length && text[i] != 'm'; i++)
                {
                }
            }
            else
            {
                length++;
            }
        }

        return length;
    }

    public static string CenterString(int length, string content)
    {
        return CenterString(length, content, true);
    }

    public static string CenterString(int length, string content, bool addRight)
    {
        int contentLength = StringLength(content);
        if (contentLength > length)
        {
            return content;
        }

        string right = new string(' ', (length - contentLength) / 2);
        string output = new string(' ', length - right.Length - contentLength) + content;

        if (addRight)
        {
            output += right;
        }

        return output;
    }

    public static string PadRightString(int length, string content)
    {
        int contentLength = StringLength(content);

        if (contentLength > length)
        {
            return content;
        }

        return content + new string(' ', length - contentLength);
    }

    public static string PadLeftString(int length, string content)
    {
        int contentLength = StringLength(content);

        if (contentLength > length)
        {
            return content;
        }

        return new string(' ', length - contentLength) + content;
    }

    public static string CenterMultiLine(int length, string content)
    {
        string output = "";
        string[] lines = content.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            if (i > 0)
            {
                output += '\n';
            }

            output += CenterString(length, lines[i]);
        }

        return output;
    }

    public static (string, string) StringsEqualWidth(string first, string second)
    {
        string[] linesFirst = first.Split('\n');
        string[] linesSecond = second.Split('\n');

        int maxLength = Math.Max(linesFirst.Length, linesSecond.Length);
        int maxWidth = 0;

        for (int i = 0; i < maxLength; i++)
        {
            if (i < linesFirst.Length) maxWidth = Math.Max(maxWidth, StringUtil.StringLength(linesFirst[i]));
            if (i < linesSecond.Length) maxWidth = Math.Max(maxWidth, StringUtil.StringLength(linesSecond[i]));
        }

        string stringFirst = "";
        string stringSecond = "";

        for (int i = 0; i < maxLength; i++)
        {
            if (i > 0 && i < linesFirst.Length) stringFirst += '\n';
            if (i > 0 && i < linesSecond.Length) stringSecond += '\n';

            if (i < linesFirst.Length) stringFirst += StringUtil.PadRightString(maxWidth, linesFirst[i]);
            if (i < linesSecond.Length) stringSecond += StringUtil.PadRightString(maxWidth, linesSecond[i]);
        }

        return (stringFirst, stringSecond);
    }
}