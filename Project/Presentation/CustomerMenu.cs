
using System;
using System.IO;
using System.Text;
using Project.Presentation;

public class CustomerMenu
{
    public static void Start()
    {
        string[] Menu =
        [
            "My details",
            "Make a reservation",
            "My reservations",
            "Restaurant layout",
            "Food menu",
            "About us",
            "Log out"
        ];

        while (true)
        {
            Style.Setup();
            Style.Logo();

            int choice = ShowCustomerMenu(Menu);

            switch (choice)
            {
                case 1:
                    AccountInfo accountInfo = new();
                    accountInfo.PersonalMenu();
                    break;

                case 2:
                    AddReservation addReservation = new();
                    addReservation.start();
                    break;

                case 3:
                    ShowReservation showReservations = new();
                    showReservations.ShowReservations();
                    break;

                case 4:
                    RestaurantMap.Start();
                    break;

                case 5:
                    FoodMenu menu = new();
                    menu.ChooseMenu();
                    break;

                case 6:
                    RestaurantInfo.PrintInfo();
                    break;

                case 7:
                    Console.Clear();
                    Console.WriteLine("You have been logged out");
                    UserLogin.LoginStart();
                    return;

                default:
                    Style.Error("That option isn't available");
                    Style.Normal("Press any key to continue..");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static int ShowCustomerMenu(string[] Menu)
    {
        int width = 54;
        int top = Console.CursorTop + 1;

        if (Console.WindowWidth < 56 || Console.WindowHeight < top + 20)
        {
            Style.Title("Homepage");
            Style.Normal("What would you like to do?");
            return ArrowNavigation.PrintMenu(Menu);
        }

        int left = (Console.WindowWidth - width) / 2;

        PrintFrame(left, top, width);

        TextWriter originalOutput = Console.Out;
        MenuWriter menuWriter = new(originalOutput, left + 4, top + 7, width - 6, Menu.Length);

        try
        {
            Console.CursorVisible = false;
            Console.SetOut(menuWriter);

            int choice = ArrowNavigation.PrintMenu(Menu);
            return choice;
        }
        finally
        {
            Console.SetOut(originalOutput);
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, top + 19);
        }
    }

    private static void PrintFrame(int left, int top, int width)
    {
        void PrintBorder(int row)
        {
            Console.SetCursorPosition(left, top + row);
            Console.Write("+" + new string('-', width - 2) + "+");
        }

        void PrintLine(int row, string text)
        {
            Console.SetCursorPosition(left, top + row);
            Console.Write("|" + text.PadRight(width - 2) + "|");
        }

        void PrintCenter(int row, string text)
        {
            int spaces = (width - 2 - text.Length) / 2;
            PrintLine(row, new string(' ', spaces) + text);
        }

        PrintBorder(0);
        PrintCenter(1, "YOUR RESTAURANT");
        PrintCenter(2, "Homepage");
        PrintBorder(3);

        for (int i = 4; i <= 15; i++)
        {
            PrintLine(i, "");
        }

        PrintLine(5, "   What would you like to do?");
        PrintBorder(16);

        Console.SetCursorPosition(left + 8, top + 18);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("Up / Down: Navigate    Enter: Select");
        Console.ResetColor();
    }

    private class MenuWriter : TextWriter
    {
        private readonly TextWriter originalOutput;
        private readonly int left;
        private readonly int firstRow;
        private readonly int width;
        private readonly int menuLength;

        private readonly StringBuilder text = new();
        private int currentLine = 0;

        public override Encoding Encoding => originalOutput.Encoding;

        public MenuWriter(TextWriter output, int left, int firstRow, int width, int menuLength)
        {
            originalOutput = output;
            this.left = left;
            this.firstRow = firstRow;
            this.width = width;
            this.menuLength = menuLength;
        }

        public override void Write(char value)
        {
            if (value == '\n')
            {
                PrintLine();
            }
            else if (value != '\r')
            {
                text.Append(value);
            }
        }

        public override void Write(string? value)
        {
            if (value == null)
                return;

            foreach (char c in value)
            {
                Write(c);
            }
        }

        public override void WriteLine(string? value)
        {
            Write(value);
            PrintLine();
        }

        public override void WriteLine()
        {
            PrintLine();
        }

        private void PrintLine()
        {
            if (currentLine >= menuLength)
                currentLine = 0;

            int row = firstRow + currentLine;

            if (currentLine == menuLength - 1)
                row++;

            Console.SetCursorPosition(left, row);
            originalOutput.Write(new string(' ', width));

            Console.SetCursorPosition(left, row);
            originalOutput.Write(text.ToString());
            originalOutput.Flush();

            text.Clear();
            currentLine++;
        }
    }
}
