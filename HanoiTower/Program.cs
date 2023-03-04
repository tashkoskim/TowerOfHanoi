
using HanoiTower;

Console.WriteLine("\t Tower of Hanoi");
Console.WriteLine("''''''''''''''''''''''''''''''''''");
int n = 1;
Console.Write("Number of disks:");
string input = Console.ReadLine();
if (int.TryParse(input, out n))
{
    TowerOfHanoi.Play(3);
}
else
{
    Console.WriteLine("Invalid input. Please enter a valid number.");
}
