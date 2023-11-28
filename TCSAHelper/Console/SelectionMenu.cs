using System.Text;

namespace TCSAHelper.Console;

public class SelectionMenu
{
    private const float _scrollFactor = 0.2f;

    private readonly string[] _contents;
    private readonly Func<int, int> _indexToLine;
    private readonly string _leftIndicator;
    private readonly string _rightIndicator;
    private readonly int _itemCount;
    private readonly int _maxHeight;
    private readonly int _zoneSize;

    private int _firstShownLine;
    private int _selectedIndex;

    public SelectionMenu(
        string contents, int itemCount,
        Func<int, int>? indexToLine = null,
        string leftIndicator = "--> ", string rightIndicator = "",
        int startSelectedIndex = 0,
        int? maxHeight = null)
    {
        _contents = contents.ReplaceLineEndings().Split(Environment.NewLine);
        _itemCount = itemCount;
        _indexToLine = indexToLine ?? ((i) => i);
        _leftIndicator = leftIndicator;
        _rightIndicator = rightIndicator;
        _selectedIndex = 0;
        _maxHeight = int.Min(maxHeight ?? _contents.Length, _contents.Length);

        _zoneSize = int.Max(1, (int)Math.Round(_scrollFactor * _maxHeight));
        for (int i = 0; i < startSelectedIndex; i++)
        {
            SelectedIndex++;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return _selectedIndex;
        }
        set
        {
            if (value != _selectedIndex)
            {
                int oldIndex = _selectedIndex;
                _selectedIndex = int.Clamp(value, 0, _itemCount - 1);
                Scroll(oldIndex);
            }
        }
    }

    private void Scroll(int oldIndex)
    {
        int oldSelectedLine = _indexToLine(oldIndex);
        int newSelectedLine = _indexToLine(SelectedIndex);
        int diff = newSelectedLine - oldSelectedLine;

        // Scroll only if selected item is within either outer part ("zone").
        int midTop = _firstShownLine + _zoneSize;
        int midBottom = _firstShownLine + _maxHeight - _zoneSize - 1;

        if (diff < 0 && newSelectedLine < midTop)
        {
            _firstShownLine = int.Max(0, _firstShownLine + diff);
        }
        else if (diff > 0 && newSelectedLine > midBottom)
        {
            _firstShownLine = int.Min(int.Max(0, _contents.Length - _maxHeight), _firstShownLine + diff);
        }
    }

    public string Show()
    {
        string leftMargin = "".PadLeft(_leftIndicator.Length);
        StringBuilder sb = new();

        string[] shownContents = _contents[_firstShownLine..int.Min(_contents.Length, _firstShownLine + _maxHeight)];

        int selectedLineInAllContents = _indexToLine(SelectedIndex);
        int selectedLineInShownContents = selectedLineInAllContents - _firstShownLine;

        for (int i = 0; i < _maxHeight; i++)
        {
            if (i == selectedLineInShownContents)
            {
                sb.Append(_leftIndicator).Append(shownContents[i]).AppendLine(_rightIndicator);
            }
            else
            {
                sb.Append(leftMargin).AppendLine(shownContents[i]);
            }
        }
        return sb.ToString();
    }
}
