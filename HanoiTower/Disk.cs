using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiTower
{
    public class Disk
    {
        private int _size = 1;
        private string _disk;
        public Disk(int size)
        {
            if(size<0)
            {
                Size = Math.Abs(size);
            }
            else if (size == 0)
            {
                Size = 1;
            }

            Size = size;
            GetDisk = createDisk();
        }

        public int Size { get => _size; set => _size = value; }
        public string GetDisk { get => _disk; set => _disk = value; }

        private string createDisk()
        {
            string peg = "«";

            for (int i=0;i < Size - 1; i++)
            {
                peg += "=";
            }

            peg += "¦";

            for (int i = 0; i < Size - 1; i++)
            {
                peg += "=";
            }

            peg += "»";

            return peg;
        }
    }
}
