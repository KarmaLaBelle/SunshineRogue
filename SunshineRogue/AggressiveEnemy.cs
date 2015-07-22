using SunshineConsole;
using OpenTK.Graphics;
using OpenTK.Input;
using RogueSharp;
using RogueSharp.Random;
using System;

namespace SunshineRogue
{
    public class AggressiveEnemy
    {
        public int X { get; set; }
        public int Y { get; set; }
    
        public void Draw()
        {
            Program.rootConsole.Write(Y, X, 'r', Color4.Gray);
        }
    }
}
