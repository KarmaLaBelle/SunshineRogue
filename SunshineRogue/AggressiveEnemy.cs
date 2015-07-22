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
        private readonly PathToPlayer _path;
        public int X { get; set; }
        public int Y { get; set; }
        public AggressiveEnemy( PathToPlayer path )
        {
            _path = path;
        }
    
        public void Draw()
        {
            Program.rootConsole.Write(Y, X, 'r', Color4.Gray);
            _path.Draw();
        }

        public void Update()
        {
            _path.CreateFrom(X, Y);
            X = _path.FirstCell.X;
            Y = _path.FirstCell.Y;
        }
    }
}
