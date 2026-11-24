namespace Project.Presentation;

public static class ArrowNavigation
{
    public static string CurrentChoice { get; set; }
    
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

    // public static int ReadArray(Array array)
    // {
    //     if (array is string[] normalArray)
    //     {
    //         return PrintMenu(normalArray);
    //     }
    //
    //     if (array is string[][] jaggedArray)
    //     {
    //         return PrintJaggedMenu(jaggedArray);
    //     }
    //
    //     return 0;
    // }

    public static (int, int) PrintJaggedMenu(string[][] menu)
    {
        int choice = 0;
        int index = 1;
        bool drop = false;

        while (true)
        {
            Console.Clear();
            
            for (int i = 0; i < menu.Length; i++)
            {
                if (choice != i)
                {
                    Style.Normal(menu[i][0]);
                }

                if (choice == i)
                {
                    if (drop == false)
                        Style.Normal($"> {Style.Groen}{menu[i][0]}{Style.Reset}");
                    else
                    {
                        Style.Normal($"{menu[i][0]}");
                        
                        for (int i2 = 1; i2 < menu[i].Length; i2++)
                        {
                            if (drop == true && index == i2)
                            {
                                Style.Normal($"   > {Style.Groen}{menu[i][i2]}{Style.Reset}");
                            }
                            else
                            {
                                Style.Normal($"     {menu[i][i2]}");
                            }
                        }
                    }
                }
            }

            ConsoleKeyInfo InputKey = Console.ReadKey(true);

            if (drop == false)
            {
                if (InputKey.Key == ConsoleKey.UpArrow)
                    choice--;

                if (InputKey.Key == ConsoleKey.DownArrow)
                    choice++;

                if (choice < 0)
                    choice = menu.Length - 1;

                if (choice >= menu.Length)
                    choice = 0;

                if (InputKey.Key == ConsoleKey.Enter)
                {
                    if (menu[choice].Length > 1)
                    {
                        drop = true;
                        index = 1;
                    }
                    else
                        return (choice + 1, index);
                }
            }
            else
            {
                if (InputKey.Key == ConsoleKey.UpArrow)
                    index--;

                if (InputKey.Key == ConsoleKey.DownArrow)
                    index++;

                if (index < 1)
                    index = menu[choice].Length - 1;

                if (index >= menu[choice].Length)
                    index = 1;

                if (InputKey.Key == ConsoleKey.Enter)
                    return (choice + 1, index);
                
                if (InputKey.Key == ConsoleKey.Escape)
                    drop = false;
            }
        }
    }
}