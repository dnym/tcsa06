using System.Text;

namespace TCSAHelper.General;

public static class Utils
{
    public static string LimitWidth(string s, int width, string ellipsis = "...")
    {
        if (s.Length <= width || width < ellipsis.Length)
        {
            return s;
        }
        else
        {
            return string.Concat(s.AsSpan(0, width - ellipsis.Length), ellipsis);
        }
    }
    public static string BreakLines(string s, int maxWidth)
    {
        StringBuilder sb = new();

        foreach (var line in s.ReplaceLineEndings().Split(Environment.NewLine))
        {
            if (line.Length < maxWidth)
            {
                sb.AppendLine(line);
                continue;
            }
            for (int i = 0; i < line.Length;)
            {
                var chunk = line[i..Math.Min(line.Length, i + maxWidth)];
                if (chunk.Length < maxWidth)
                {
                    sb.AppendLine(chunk);
                    break;
                }
                var lastSpace = chunk.LastIndexOf(' ');
                if (lastSpace == -1)
                {
                    sb.AppendLine(chunk);
                    i += chunk.Length;
                }
                else
                {
                    sb.AppendLine(chunk[..lastSpace]);
                    i += lastSpace + 1;
                }
            }
        }

        return sb.ToString();
    }
}
