
namespace HanoiTower.GameElements
{
    public class Rod
    {
        private int _height = 1;
        private string _rodBase;
        private string _label;
        public Rod(int noDisks, int index)
        {
            if (noDisks < 0)
            {
                noDisks = Math.Abs(noDisks);
            }
            else if (noDisks == 0)
            {
                noDisks = 1;
            }
            Height = noDisks;

            Label = $"[{DesignCharConstants.RodLabels[index]}]";

            RodBase = createRodBase();
        }

        public int Height { get => _height; set => _height = value; }
        public string Label { get => _label; set => _label = value; }
        public string RodBase { get => _rodBase; set => _rodBase = value; }

        private string createRodBase()
        {
            string rodBase = DesignCharConstants.RodBaseChar;
            for(int i=0;i<(Height*4);i++)
            {
                rodBase += DesignCharConstants.RodBaseChar;
            }
            return rodBase;
        }

    }
}
