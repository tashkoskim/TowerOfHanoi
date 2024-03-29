﻿
namespace HanoiTower.GameElements
{
    // During choosing which ASCII characters to use, this website helped me a lot:
    // https://theasciicode.com.ar/
    public class DesignCharConstants
    {
        public static readonly List<string> Header = new()
        { "\t╔╦╗┌─┐┬ ┬┌─┐┬─┐  ┌─┐┌─┐  ╦ ╦┌─┐┌┐┌┌─┐┬",
          "\t ║ │ ││││├┤ ├┬┘  │ │├┤   ╠═╣├─┤││││ ││",
          "\t ╩ └─┘└┴┘└─┘┴└─  └─┘└    ╩ ╩┴ ┴┘└┘└─┘┴"
        };

        // Characters needed for constructing a disk
        public const string DiskStartChar = "«";
        public const string DiskMiddleChar = "¦";
        public const string DiskStructureChar = "=";
        public const string DiskEndChar = "»";

        // Characters needed for contructing a rod
        public const string RodBaseChar = "▀";
        public const string RodChar = "│";
        public static readonly char[] RodLabels = { '1', '2', '3'};
        
        // Fixed space betweet two towers
        public const string SpaceBetweenTowers = "   ";

        public const string LineBreak = "----------------------------------------------------------";

        
    }
}
