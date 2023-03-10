using HanoiTower.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiTower
{
    public class TowerOfHanoi
    {
        private static int noDisks;

        public static void Play()
        {
            // Setup the console to be able to print special ASCII chars
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();
            PrintHeader();

            string input = string.Empty;

            // The first screen
            while (!int.TryParse(input, out noDisks))
            {
                Console.Write("> Please write the number of disks you want to start with: ");
                input = Console.ReadLine();          
            }

            TheGame();
        }

        private static void TheGame()
        {
            Console.Clear();
            PrintHeader();

            // Define the 3 rods
            Stack<int>[] rods = new Stack<int>[3];

            // Fill the first rod with disks
            rods[0] = FillRod(noDisks);
            rods[1] = new Stack<int>();
            rods[2] = new Stack<int>();

            // Print the beginning state of the the rods
            PrintRods(rods[0], rods[1], rods[2]);

            int mode = SubMenu();
            Console.Clear();
            PrintHeader();
            PrintRods(rods[0], rods[1], rods[2]);
            if (mode == 0)
                UserPlay(rods);
            else
                ComputerPlay(noDisks, rods[0], rods[1], rods[2]);

            // When the game finish, choose to play again or exit
            int playAgain = SubMenuLoop();
            if(playAgain == 0)
            {
                Play();
            }else
            {
                Environment.Exit(0);
            }
        }

        private static void ComputerPlay(int n, Stack<int> source, Stack<int> auxiliary, Stack<int> destination)
        {
            if (noDisks % 2 == 0)
            {
                SolveAutomatically(noDisks, source, destination, auxiliary);
            }
            else
            {
                SolveAutomatically(noDisks, source, auxiliary, destination);
            }
        }

        // Method that is used when the computer play the game
        private static void SolveAutomatically(int n, Stack<int> source, Stack<int> auxiliary, Stack<int> destination)
        {
            int noSteps = 1;
            Console.WriteLine(DesignCharConstants.LineBreak);
            Console.WriteLine("# 0\t No.Disks: " + noDisks);
            Thread.Sleep(1000);
            int totalMoves = (int)Math.Pow(2, n) - 1;
            for (int i = 1; i <= totalMoves; i++)
            {
                if (i % 3 == 1)
                {
                    MoveDisk(source, destination);
                }
                else if (i % 3 == 2)
                {
                    MoveDisk(source, auxiliary);
                }
                else if (i % 3 == 0)
                {
                    MoveDisk(destination, auxiliary);
                }


                ClearPartOfConsole(4);
                PrintRods(source, auxiliary, destination);
                Console.WriteLine(DesignCharConstants.LineBreak);
                Console.WriteLine("# " + noSteps + "\t No.Disks: " + noDisks);
                noSteps++;
                Thread.Sleep(1000);

            }
            YouWonText("T H E   C O M P U T E R");
        }

        // Method that is used when the computer play the game for moving the disks
        static void MoveDisk(Stack<int> source, Stack<int> destination)
        {
            if(destination.Count == 0)
            {
                int disk = source.Pop();
                destination.Push(disk);
            }else if(source.Count == 0)
            {
                int disk = destination.Pop();
                source.Push(disk);
            }else if(destination.Peek() < source.Peek())
            {
                int disk = destination.Pop();
                source.Push(disk);
            }else if (destination.Peek() > source.Peek())
            {
                int disk = source.Pop();
                destination.Push(disk);
            }
        }

        // Method that is used when the user play the game
        private static void UserPlay(Stack<int>[] rods)
        {
            int noSteps = 0;

            // The user should enter the tower labels From -> To
            string input = string.Empty;
            while (input.ToLower() != "exit")
            {
                char k1 = ' ';
                char k2 = ' ';
                Console.WriteLine(DesignCharConstants.LineBreak);
                Console.WriteLine("# " + noSteps + "\t No.Disks: " + noDisks);
                Console.WriteLine();
                Console.Write("> Move disk from tower: ");
                input = Console.ReadLine();
                if (input.ToLower() == "exit")
                {
                    break;
                }
                k1 = input[0];
                Console.Write("> Move disk to tower: ");
                input = Console.ReadLine();
                if (input.ToLower() == "exit")
                {
                    break;
                }
                k2 = input[0];

                // These should be valid stack indexes
                int in1 = ReturnLabelIndex(k1);
                int in2 = ReturnLabelIndex(k2);

                // Clear the console display
                Console.Clear();
                //ClearPartOfConsole();
                PrintHeader();

                string error = string.Empty;

                // If we have valid index of a label i.e. the number of the stack
                if (in1 != -1 && in2 != -1 && in1 != in2)
                {
                    // If we took disk from the tower with index 'in1'...
                    if (rods[in1].Count > 0)
                    {
                        int topDisk = rods[in1].Peek();
                        // The disk should be moved to the tower with index 'in2'...
                        if (rods[in2].Count == 0)
                        {
                            rods[in1].Pop();
                            rods[in2].Push(topDisk);
                        }
                        else
                        {
                            if (rods[in2].Peek() > topDisk)
                            {
                                rods[in1].Pop();
                                rods[in2].Push(topDisk);
                            }
                            else
                            {
                                error = "!!! You can't move bigger on top of a smaller disk !!!";

                            }
                        }

                    }
                    else
                    {
                        error = "!!! You can't take disk from an empty tower !!!";

                    }

                    PrintRods(rods[0], rods[1], rods[2]);
                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine(DesignCharConstants.LineBreak);
                        Console.WriteLine(error);
                    }
                    noSteps++;

                    // Game win
                    if (rods[1].Count == noDisks || rods[2].Count == noDisks)
                    {
                        Console.WriteLine(DesignCharConstants.LineBreak);
                        Console.WriteLine("# " + noSteps + "\t No.Disks: " + noDisks);
                        YouWonText("Y O U");
                        break;
                    }

                } // END for if(in1 != -1 && in2 != -1)

            }
        }


        private static Stack<int> FillRod(int n)
        {
            Stack<int> stack = new Stack<int>();

            for(int i = n; i > 0; i--)
            {
                stack.Push(i);
            }

            return stack;
        }

        private static void PrintRods(Stack<int> stack1, Stack<int> stack2, Stack<int> stack3)
        {
            Rod rod1 = new Rod(noDisks, 0);
            Rod rod2 = new Rod(noDisks, 1);
            Rod rod3 = new Rod(noDisks, 2);

            string space = string.Empty;
            int s1 = 0;
            int s2 = 0;
            int s3 = 0;
            // 1. Print the disks (if there are any)
            for(int i=1; i <= noDisks; i++)
            {
                string line = string.Empty;
                // 1st rod
                if (stack1.Count == 0)
                {
                    space = Space(noDisks * 2);
                    line += ConstructLine(space, DesignCharConstants.RodChar);
                }
                else
                {
                    if( i <= (noDisks - stack1.Count))
                    {
                        space = Space(noDisks * 2);
                        line += ConstructLine(space, DesignCharConstants.RodChar);
                    }else
                    {
                        // if there are disks
                        Disk disk = new Disk(stack1.ElementAt(s1));
                        string diskString = disk.GetDisk;
                        space = Space((noDisks * 2) - stack1.ElementAt(s1));
                        line += ConstructLine(space, diskString);
                        s1++;
                    }
                    
                }
                line += DesignCharConstants.SpaceBetweenTowers;
                // 2nd rod
                if (stack2.Count == 0)
                {
                    space = Space(noDisks * 2);
                    line += ConstructLine(space, DesignCharConstants.RodChar);
                }
                else
                {
                    if (i <= (noDisks - stack2.Count))
                    {
                        space = Space(noDisks * 2);
                        line += ConstructLine(space, DesignCharConstants.RodChar);
                    }else
                    {
                        // if there are disks
                        Disk disk = new Disk(stack2.ElementAt(s2));
                        string diskString = disk.GetDisk;
                        space = Space((noDisks * 2) - stack2.ElementAt(s2));
                        line += ConstructLine(space, diskString);
                        s2++;
                    }
                        
                }
                line += DesignCharConstants.SpaceBetweenTowers;
                // 3rd rod
                if (stack3.Count == 0)
                {
                    space = Space(noDisks * 2);
                    line += ConstructLine(space, DesignCharConstants.RodChar);
                }
                else
                {
                    if (i <= (noDisks - stack3.Count))
                    {
                        space = Space(noDisks * 2);
                        line += ConstructLine(space, DesignCharConstants.RodChar);
                    }else
                    {
                        // if there are disks
                        Disk disk = new Disk(stack3.ElementAt(s3));
                        string diskString = disk.GetDisk;
                        space = Space((noDisks * 2) - stack3.ElementAt(s3));
                        line += ConstructLine(space, diskString);
                        s3++;
                    }
                        
                }
                Console.WriteLine(line);
            }

            // 2. Print the rod base
            Console.WriteLine(rod1.RodBase + DesignCharConstants.SpaceBetweenTowers + rod2.RodBase + DesignCharConstants.SpaceBetweenTowers + rod3.RodBase);

            string labelRods = string.Empty;
            // 3. Print the rod label
            int spaceLabel = (rod1.RodBase.Length / 2) - ((rod1.Label.Length) / 2);
            labelRods += Space(spaceLabel) + rod1.Label + Space(spaceLabel);

            labelRods += DesignCharConstants.SpaceBetweenTowers;

            spaceLabel = (rod2.RodBase.Length / 2) - ((rod2.Label.Length) / 2);
            labelRods += Space(spaceLabel) + rod2.Label + Space(spaceLabel);

            labelRods += DesignCharConstants.SpaceBetweenTowers;

            spaceLabel = (rod3.RodBase.Length / 2) - ((rod3.Label.Length) / 2);
            labelRods += Space(spaceLabel) + rod3.Label + Space(spaceLabel);

            Console.WriteLine(labelRods);
        }

        // This was done in order to reduce the display flickering 
        // 4 is the number of rows of the header
        public static void ClearPartOfConsole(int rowIndex)
        {
            Console.CursorTop = rowIndex;
            Console.Write(new string(' ', Console.WindowWidth));
        }

        private static string ConstructLine(string space, string el)
        {
            return space + el + space;
        }

        // The space first is calculated in order to find the correct length
        private static string Space(int size)
        {
            string s = string.Empty;
            for(int i=0; i < size;i++)
            {
                s += " ";
            }
            return s;
        }

        private static int SubMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("> Please choose who do you want to play the game:");
            string[] items = { "○ You", "○ The Computer" };
            int selectedItemIndex = 0;
            int pom = Console.CursorTop;
            while (true)
            {
                //Console.Clear();
                Console.SetCursorPosition(0, pom);
                ClearToEndOfCurrentLine();

                
                // Display the items with the selected item highlighted
                for (int i = 0; i < items.Length; i++)
                {
                    if (i == selectedItemIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        items[i] = items[i].Replace("○", "•");

                    }
                    else
                    {
                        items[i] = items[i].Replace("•", "○");
                    }
                    Console.WriteLine($"{items[i]} ");
                    Console.ResetColor();
                }

                // Read the arrow keys input
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedItemIndex--;
                    if (selectedItemIndex < 0)
                    {
                        selectedItemIndex = items.Length - 1;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedItemIndex++;
                    if (selectedItemIndex >= items.Length)
                    {
                        selectedItemIndex = 0;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    // The user has selected an item
                    //Console.Clear();
                    Console.SetCursorPosition(0, pom);
                    ClearToEndOfCurrentLine();
                    //Console.WriteLine("You have selected: " + items[selectedItemIndex]);
                    //Console.ReadLine();
                    return selectedItemIndex;
                }
            }
        }

        private static int SubMenuLoop()
        {
            Console.WriteLine();
            string[] items = { "○ Play again", "○ Exit" };
            int selectedItemIndex = 0;
            int pom = Console.CursorTop - 1;
            while (true)
            {
                //Console.Clear();
                Console.SetCursorPosition(0, pom);
                ClearToEndOfCurrentLine();


                // Display the items with the selected item highlighted
                for (int i = 0; i < items.Length; i++)
                {
                    if (i == selectedItemIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        items[i] = items[i].Replace("○", "•");

                    }
                    else
                    {
                        items[i] = items[i].Replace("•", "○");
                    }
                    Console.WriteLine($"{items[i]} ");
                    Console.ResetColor();
                }

                // Read the arrow keys input
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedItemIndex--;
                    if (selectedItemIndex < 0)
                    {
                        selectedItemIndex = items.Length - 1;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedItemIndex++;
                    if (selectedItemIndex >= items.Length)
                    {
                        selectedItemIndex = 0;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    // The user has selected an item
                    //Console.Clear();
                    Console.SetCursorPosition(0, pom);
                    ClearToEndOfCurrentLine();
                    return selectedItemIndex;
                }
            }
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - (Console.WindowWidth >= Console.BufferWidth ? 1 : 0));
        }

        public static void ClearToEndOfCurrentLine()
        {
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;
            Console.Write(new String(' ', Console.WindowWidth - currentLeft));
            Console.SetCursorPosition(currentLeft, currentTop);
        }

        // Return the index of a label name
        private static int ReturnLabelIndex(char l)
        {
            for(int i = 0; i < DesignCharConstants.RodLabels.Length; i++)
            {
                if (DesignCharConstants.RodLabels[i] == l)
                {
                    return i;
                }
            }
            return -1;
        }
        
        private static void YouWonText(string role)
        {
            Console.WriteLine(DesignCharConstants.LineBreak);
            Console.WriteLine();
            Console.WriteLine($"> > > !!! {role}   W O N !!! < < <");
            Console.WriteLine();
        }

        // This website was used to generate the ASCII header:
        // https://patorjk.com/software/taag/#p=display&f=Graffiti&t=Type%20Something%20
        private static void PrintHeader()
        {
            Console.WriteLine("\t╔╦╗┌─┐┬ ┬┌─┐┬─┐  ┌─┐┌─┐  ╦ ╦┌─┐┌┐┌┌─┐┬");
            Console.WriteLine("\t ║ │ ││││├┤ ├┬┘  │ │├┤   ╠═╣├─┤││││ ││");
            Console.WriteLine("\t ╩ └─┘└┴┘└─┘┴└─  └─┘└    ╩ ╩┴ ┴┘└┘└─┘┴");
            //Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
