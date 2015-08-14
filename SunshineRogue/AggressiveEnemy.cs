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
        private readonly IMap _map;
        private bool _isAwareOfPlayer;
        public int X { get; set; }
        public int Y { get; set; }
        public AggressiveEnemy( IMap map, PathToPlayer path )
        {
            _map = map;
            _path = path;
        }
    
        public void Draw()
        {
            Program.rootConsole.Write(Y, X, 'r', Color4.Gray);
            _path.Draw();
        }

        public void Update()
        {
            if (!_isAwareOfPlayer)
            {
                // When the enemy is not aware of the player
                // check the map to see if they are in field-of-view
                if (_map.IsInFov(X, Y))
                {
                    _isAwareOfPlayer = true;
                }
            }
            // Once the enemy is aware of the player
            // they will never lose track of the player
            // and will pursue relentlessly
            if (_isAwareOfPlayer)
            {
                _path.CreateFrom(X, Y);
                X = _path.FirstCell.X;
                Y = _path.FirstCell.Y;
            }
        }
    }
}
