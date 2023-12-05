public abstract class ConsoleHelper
{
    public const ConsoleColor HigllightColor = ConsoleColor.Green;

    public static bool Proceed(){
        Console.WriteLine("\nVuoi Procedere [Y/N]: ");
        var c = Console.ReadKey().KeyChar;
        Console.ReadKey();
        return char.ToLower(c) == 'y';
    }

    public static int MultipleChoice(bool canCancel, string[] options, string? title = null)
    {
        const int startX = 1;
        const int startY = 1;
        
        int currentSelection = 0;
        int page = 0;
        int offset = 0;

        ConsoleKey key;

        Console.CursorVisible = false;

        do
        {
            Console.Clear();

            if(title is not null){
                Console.WriteLine(title + '\n');
            }

            var h = Console.WindowHeight -1;
            var start = h * page;
            var max = h + start;
            if(max > options.Length) max = options.Length;

            for (int i = start; i < max; i++)
            {
                Console.SetCursorPosition(startX, startY + (i - start));

                if(i == currentSelection)
                    Console.ForegroundColor = HigllightColor;

                var l = Console.WindowWidth-1;

                if(l+offset < options[i].Length)
                    Console.Write(options[i][offset..(l + offset)]);
                else if(offset < options[i].Length)
                    Console.Write(options[i][offset..options[i].Length]);
                else
                    Console.Write("\n");

                Console.ResetColor();
            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                {
                    if(offset > 0)
                        offset--;
                    break;
                }
                case ConsoleKey.RightArrow:
                {
                    offset++;
                    break;
                }
                case ConsoleKey.UpArrow:
                {
                    if (currentSelection >= 1)
                        currentSelection -= 1;
                    break;
                }
                case ConsoleKey.DownArrow:
                {
                    if (currentSelection + 1 < options.Length)
                        currentSelection += 1;
                    break;
                }
                case ConsoleKey.Escape:
                {
                    if (canCancel)
                        return -1;
                    break;
                }
            }

            if(currentSelection >= max){
                page++;
            } else if (currentSelection < start){
                page--;
            }
        } while (key != ConsoleKey.Enter);

        Console.CursorVisible = true;

        return currentSelection;
    }
}
