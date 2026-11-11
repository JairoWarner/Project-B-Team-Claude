namespace Project.Presentation;

public static class ArrowNavigation
{
    public static int PrintMenu(string[] menu)
    {
        int choice = 0;

        while (true)
        {
            Console.Clear();
            for (int i = 0; i < menu.Length; i++)
            {
                if (choice < 0)
                    choice = menu.Length - 1;

                if (choice > menu.Length - 1)
                    choice = 0;
                
                if (choice != i)
                    Style.Normal(menu[i]);

                if (choice == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Style.Normal($"> {Style.Groen}{menu[i]}{Style.Reset}");
                    Console.ResetColor();
                }
            }
        
            ConsoleKeyInfo InputKey = Console.ReadKey();
        
            if (InputKey.Key == ConsoleKey.UpArrow)
                choice--;
            if (InputKey.Key == ConsoleKey.DownArrow)
                choice++;
            if (InputKey.Key == ConsoleKey.Enter)
                return choice + 1;
        }
    }
}