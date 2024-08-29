// Box i box time!
using OpgaveBesvarelser.Common;

class WindowMenu : Buffer
{
    //
    private readonly int _itemsPerPage = 15;
    private int _currentPage = 0;
    private int _selectedIndex = 0;
    private readonly int _maxPage;
    private Dictionary<string, Action> _options;
    private readonly Buffer _buffer;
    private bool _isActive;
    private readonly int height;
    private readonly int width;

    public WindowMenu(Dictionary<string, Action> options, string menuName)
    {
        _options = options;
        height = Console.WindowHeight - 1;
        width = Console.WindowWidth - 1;
        _buffer = new Buffer(height, width); // -1 to no scrolbar

        DrawBox(_buffer, 0, 0, height, width);

        // Add Title
        var textPosition = width / 2 - menuName.Length/2;
        AddText(_buffer, 1, textPosition, menuName, ConsoleColor.Yellow);

        // Add ESC Hint
        AddText(_buffer, 1, Constants.PadderHorizontal, "[ESC]", ConsoleColor.Yellow);


        // Calculate max page based on number of options (to show on the UI)
        _maxPage = (int)Math.Ceiling((double)options.Count / _itemsPerPage) - 1;

        _isActive = true;
    }

    public void Start()
    {
        while (_isActive)
        {
            RenderMenu();
            HandleInput();
        }
    }

    private void RenderMenu()
    {
        // Clear the portion of the buffer where the list is drawn
        for (int i = 2; i < 18; i++)
        {
            AddText(_buffer, i, 2, new string(' ', 56));
        }

        // Update Page Text
        string pageText = $"Side {_currentPage + 1} / {_maxPage + 1}"; // +1 to make more sense on the front end
        string noText = "              ";

        AddText(_buffer, Constants.PadderTop, width - Constants.PadderHorizontal - noText.Length, noText); // right
        AddText(_buffer, Constants.PadderTop, width - Constants.PadderHorizontal - pageText.Length, pageText, ConsoleColor.Yellow);

        
        //AddText(_buffer, Constants.PadderLeft, Console.WindowWidth
        //    - Constants.PadderLeft
        //    - Constants.PadderRight
        //    - noText.Length, noText); // right
        //AddText(_buffer, Constants.PadderLeft, Console.WindowWidth - pageText.Length, pageText);

        // Calculate the starting row for centering the text
        int startRow = 10 - (_itemsPerPage / 2);

        var optionList = new List<string>(_options.Keys);
        for (int i = _itemsPerPage * _currentPage; i < optionList.Count && i < _itemsPerPage * (_currentPage + 1); i++)
        {
            string displayText = optionList[i];
            ConsoleColor foregroundColor = ConsoleColor.White;
            ConsoleColor backgroundColor = ConsoleColor.Black;
            if (i == _selectedIndex)
            {
                foregroundColor = ConsoleColor.Black;
                backgroundColor = ConsoleColor.White;
            }

            // Calculate horizontal padding to center the text
            int padding = (width - Constants.PadderHorizontal*2 - displayText.Length) / 2;
            AddText(_buffer, startRow + i - _itemsPerPage * _currentPage, 2 + padding, displayText, foregroundColor, backgroundColor);
        }

        // Check state to show arrows or not
        if (_currentPage < _maxPage)
        {
            AddText(_buffer, height-Constants.PadderBottom, width-Constants.PadderHorizontal*2, "->", ConsoleColor.Yellow);
        }
        else
        {
            AddText(_buffer, height - Constants.PadderBottom, width - Constants.PadderHorizontal*2, "  ");
        }
        if (_currentPage != 0)
        {
            AddText(_buffer, height - Constants.PadderBottom, Constants.PadderHorizontal, "<-", ConsoleColor.Yellow);
        }
        else
        {
            AddText(_buffer, height - Constants.PadderBottom, Constants.PadderHorizontal, "  ");
        }

        _buffer.Render();
    }

    private void HandleInput()
    {
        // Reset the input queue so multiple old events don't get run
        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }

        // Get user input
        ConsoleKey Key = Console.ReadKey(true).Key;

        // Update selection based on arrow keys
        switch (Key)
        {
            case ConsoleKey.UpArrow:
                if (_selectedIndex > 0)
                {
                    _selectedIndex--;
                }
                break;
            case ConsoleKey.DownArrow:
                if (_selectedIndex < _options.Count - 1)
                {
                    _selectedIndex++;
                }
                break;
            case ConsoleKey.Enter:
                // Handle the selection
                var selectedOption = new List<string>(_options.Keys)[_selectedIndex];

                Console.Clear();
                Console.CursorVisible = true;
                _options[selectedOption]?.Invoke(); // Invoke the associated action
                Console.CursorVisible = false;

                Console.Clear();
                // Render with priority ( refresh everything no matter what)
                _buffer.Render(true);
                break;

            case ConsoleKey.RightArrow:
                if (_currentPage < _maxPage)
                {
                    _currentPage++;
                    _selectedIndex = _itemsPerPage * _currentPage;
                }
                break;
            case ConsoleKey.LeftArrow:
                if (_currentPage != 0)
                {
                    _currentPage--;
                    _selectedIndex = _itemsPerPage * _currentPage;
                }
                break;
            case ConsoleKey.Escape:
                _isActive = false; // Exit the menu when Escape is pressed
                break;
        }
    }

}