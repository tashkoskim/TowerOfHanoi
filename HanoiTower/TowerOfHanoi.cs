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

            PrintHeader();

            string input = string.Empty;

            while (!int.TryParse(input, out noDisks))
            {
                Console.Write("Number of disks:");
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
            PrintRods(rods);

            int noSteps = 0;

            // The user should enter the tower labels From -> To
            string input = string.Empty;
            while(input.ToLower() != "exit")
            {
                char k1 = ' ';
                char k2 = ' ';
                Console.WriteLine(DesignCharConstants.LineBreak);
                Console.WriteLine("# " + (noSteps + 1) + "\t No.Disks: "+ noDisks);
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
                PrintHeader();

                string error = string.Empty;

                // If we have valid index of a label i.e. the number of the stack
                if (in1 != -1 && in2 != -1)
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
                        }else
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
                        
                    }else
                    {
                        error = "!!! You can't take disk from an empty tower !!!";
                        
                    }

                    PrintRods(rods);
                    if(!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine(DesignCharConstants.LineBreak);
                        Console.WriteLine(error);
                    }
                    noSteps++;

                    // Game win
                    if (rods[1].Count == noDisks || rods[2].Count == noDisks)
                    {
                        Console.WriteLine(DesignCharConstants.LineBreak);
                        Console.WriteLine();
                        Console.WriteLine("> > > !!! Y O U   W I N !!! < < <");
                        Console.WriteLine();
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

        private static void PrintRods(Stack<int>[] rods)
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
                if (rods[0].Count == 0)
                {
                    space = Space(noDisks * 2);
                    line += ConstructLine(space, DesignCharConstants.RodChar);
                }
                else
                {
                    if( i <= (noDisks - rods[0].Count))
                    {
                        space = Space(noDisks * 2);
                        line += ConstructLine(space, DesignCharConstants.RodChar);
                    }else
                    {
                        // if there are disks
                        Disk disk = new Disk(rods[0].ElementAt(s1));
                        string diskString = disk.GetDisk;
                        //space = Space((noDisks * 2) - i);
                        space = Space((noDisks * 2) - rods[0].ElementAt(s1));
                        line += ConstructLine(space, diskString);
                        s1++;
                    }
                    
                }
                line += DesignCharConstants.SpaceBetweenTowers;
                // 2nd rod
                if (rods[1].Count == 0)
                {
                    space = Space(noDisks * 2);
                    line += ConstructLine(space, DesignCharConstants.RodChar);
                }
                else
                {
                    if (i <= (noDisks - rods[1].Count))
                    {
                        space = Space(noDisks * 2);
                        line += ConstructLine(space, DesignCharConstants.RodChar);
                    }else
                    {
                        // if there are disks
                        Disk disk = new Disk(rods[1].ElementAt(s2));
                        string diskString = disk.GetDisk;
                        space = Space((noDisks * 2) - rods[1].ElementAt(s2));
                        line += ConstructLine(space, diskString);
                        s2++;
                    }
                        
                }
                line += DesignCharConstants.SpaceBetweenTowers;
                // 3rd rod
                if (rods[2].Count == 0)
                {
                    space = Space(noDisks * 2);
                    line += ConstructLine(space, DesignCharConstants.RodChar);
                }
                else
                {
                    if (i <= (noDisks - rods[2].Count))
                    {
                        space = Space(noDisks * 2);
                        line += ConstructLine(space, DesignCharConstants.RodChar);
                    }else
                    {
                        // if there are disks
                        Disk disk = new Disk(rods[2].ElementAt(s3));
                        string diskString = disk.GetDisk;
                        space = Space((noDisks * 2) - rods[2].ElementAt(s3));
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

        private static string ConstructLine(string space, string el)
        {
            return space + el + space;
        }

        private static string Space(int size)
        {
            string s = string.Empty;
            for(int i=0; i < size;i++)
            {
                s += " ";
            }
            return s;
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
        
        // This website was used to generate the ASCII header:
        // https://patorjk.com/software/taag/#p=display&f=Graffiti&t=Type%20Something%20
        private static void PrintHeader()
        {
            Console.WriteLine("\t╔╦╗┌─┐┬ ┬┌─┐┬─┐  ┌─┐┌─┐  ╦ ╦┌─┐┌┐┌┌─┐┬");
            Console.WriteLine("\t ║ │ ││││├┤ ├┬┘  │ │├┤   ╠═╣├─┤││││ ││");
            Console.WriteLine("\t ╩ └─┘└┴┘└─┘┴└─  └─┘└    ╩ ╩┴ ┴┘└┘└─┘┴");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine();
        }
    }
}
