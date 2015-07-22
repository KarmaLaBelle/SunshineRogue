using SunshineConsole;
using OpenTK.Graphics;
using OpenTK.Input;
using RogueSharp;
using RogueSharp.Random;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SunshineRogue
{
    public class PathToPlayer
    {
        private readonly Player _player;
        private readonly IMap _map;
        private readonly PathFinder _pathFinder;
        private IEnumerable<Cell> _cells;

        public PathToPlayer(Player player, IMap map)
        {
            _player = player;
            _map = map;
            _pathFinder = new PathFinder(map);
        }
        public Cell FirstCell
        {
            get
            {
                return _cells.First();
            }
        }
        public void CreateFrom(int x, int y)
        {
            _cells = _pathFinder.ShortestPath(_map.GetCell(x, y), _map.GetCell(_player.X, _player.Y));
        }
        public void Draw()
        {
            if (_cells != null && Global.GameState == GameStates.Debugging)
            {
                foreach (Cell cell in _cells)
                {
                    if (cell != null)
                    {
                        Program.rootConsole.Write(cell.Y, cell.X, '.', Color4.Khaki, Color4.Beige);
                    }
                }
            }
        }
    }
}
