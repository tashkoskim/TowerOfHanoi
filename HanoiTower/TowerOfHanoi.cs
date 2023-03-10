using HanoiTower.GameElements;
using HanoiTower.Services;
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
            GameService.PrintHeader();

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
            GameService.PrintHeader();

            // Define the 3 rods at the beginning
            Stack<int>[] rods = new Stack<int>[3];

            // Fill the first rod with disks
            rods[0] = GameService.FillRod(noDisks);
            rods[1] = new Stack<int>();
            rods[2] = new Stack<int>();

            // Print the beginning state of the the rods
            GameService.PrintRods(rods[0], rods[1], rods[2], noDisks);

            // Choose between You/Computer
            int mode = GameService.SubMenu();
            // Clear everything and print again the header and the towers
            Console.Clear();
            GameService.PrintHeader();
            GameService.PrintRods(rods[0], rods[1], rods[2], noDisks);
            if (mode == 0)
                UserPlay(rods);
            else
                ComputerPlay(noDisks, rods[0], rods[1], rods[2]);

            // When the game finish, choose between PlayAgain/Exit
            int playAgain = GameService.SubMenuLoop();
            if(playAgain == 0)
            {
                Play();
            }else
            {
                Environment.Exit(0);
            }
        }

        // Method that is used when the user play the game (index 0)
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
                int in1 = GameService.ReturnLabelIndex(k1);
                int in2 = GameService.ReturnLabelIndex(k2);

                // Clear the console display
                Console.Clear();
                //ClearPartOfConsole();
                GameService.PrintHeader();

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

                    GameService.PrintRods(rods[0], rods[1], rods[2], noDisks);
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
                        GameService.YouWonText("Y O U");
                        break;
                    }

                } // END for if(in1 != -1 && in2 != -1)

            }
        }

        // Method that is used when the computer play the game (index 1)
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

        #region ComputerPlay_Methods
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


                GameService.ClearPartOfConsole(4);
                GameService.PrintRods(source, auxiliary, destination, noDisks);
                Console.WriteLine(DesignCharConstants.LineBreak);
                Console.WriteLine("# " + noSteps + "\t No.Disks: " + noDisks);
                noSteps++;
                Thread.Sleep(1000);

            }
            GameService.YouWonText("T H E   C O M P U T E R");
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
        #endregion
    }
}
