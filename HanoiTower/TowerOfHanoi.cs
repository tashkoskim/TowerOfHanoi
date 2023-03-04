using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiTower
{
    public class TowerOfHanoi
    {
        public static void Play(int noDisks)
        {
            if(noDisks > 0)
            {
                TheGame();
            }
            else
            {
                Console.WriteLine("Invalid input. The number of disks n must be n > 0!");
            }
        }

        private static void TheGame()
        {

        }
    }
}
