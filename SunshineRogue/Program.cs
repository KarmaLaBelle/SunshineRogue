using SunshineConsole;
using OpenTK.Graphics;
using OpenTK.Input;
using RogueSharp;
using RogueSharp.Random;
using System;

namespace SunshineRogue
{
    class Program
    {

        // The screen height and width are in number of tiles
        private static readonly int _screenWidth = 125;
        private static readonly int _screenHeight = 60;
        public static ConsoleWindow rootConsole;
        public static IMap map;
        private static AggressiveEnemy _aggressiveEnemy;
        private static Player _player;

        static void Main(string[] args)
        {
            // The title will appear at the top of the console window
            string consoleTitle = "Dungeons and @ Signs";
            // Create the console
            rootConsole = new ConsoleWindow(_screenHeight, _screenWidth, consoleTitle);

            GenerateMap();
            StartPlayer();
            StartEnemy();
            Global.GameState = GameStates.PlayerTurn;
            GameLoop();
            var stats = Creature.GetDictionary();

            Creature rat = stats["Rat"];
            Console.Write("{0}, {1}, {2}", rat.Name, rat.Health, rat.Damage);
        }

        private static void GameLoop()
        {
            while (!rootConsole.KeyPressed && rootConsole.WindowUpdate())
            {
                if (Global.GameState == GameStates.EnemyTurn)
                {
                    _aggressiveEnemy.Update();
                    Global.GameState = GameStates.PlayerTurn;
                }
                _player.GetInput();
                RootConsoleRender();
            }
        }

        private static void RootConsoleRender()
        {
            

            // Use RogueSharp to calculate the current field-of-view for the player
            map.ComputeFov(_player.X, _player.Y, 15, true);
 
            foreach ( var cell in map.GetAllCells() )
            {
                // When a Cell is in the field-of-view set it to a brighter color
                if (cell.IsInFov || Global.GameState == GameStates.Debugging)
                {
                    map.SetCellProperties( cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true );
                    if ( cell.IsWalkable )
                    {
                    rootConsole.Write( cell.Y, cell.X, '.', Color4.Yellow );
                    }
                    else
                    {
                    rootConsole.Write( cell.Y, cell.X, '#', Color4.White );
                    }
                }
                // If the Cell is not in the field-of-view but has been explored set it darker
                else if (cell.IsExplored)
                {
                    if ( cell.IsWalkable )
                    {
                        rootConsole.Write(cell.Y, cell.X, '.', Color4.Gray);
                    }
                    else
                    {
                        rootConsole.Write(cell.Y, cell.X, '#', Color4.DarkGray);
                    }
                }
            }

            //DrawUI();

            // Write rat
            if (Global.GameState == GameStates.Debugging || map.IsInFov(_aggressiveEnemy.X, _aggressiveEnemy.Y))
            {
                _aggressiveEnemy.Draw();
            }

            // Set the player's symbol after the map symbol to make sure it is draw
            rootConsole.Write( _player.Y, _player.X, '@', Color4.LightSkyBlue );
        }

        private static void GenerateMap()
        {
            //Generate random rooms map
            map = Map.Create(new RandomRoomsMapCreationStrategy<Map>(_screenWidth, _screenHeight, 45, 10, 6));
        }

        private static Cell GetRandomEmptyCell()
        {
            while (true)
            {
                int x = Global.Random.Next(49);
                int y = Global.Random.Next(29);
                if (map.IsWalkable(x, y))
                {
                    return map.GetCell(x, y);
                }
            }
        }

        private static void StartPlayer()
        {
            Cell startingCell = GetRandomEmptyCell();
            _player = new Player()
            {
                X = startingCell.X,
                Y = startingCell.Y
            };
        }

        private static void StartEnemy()
        {
            Cell startingCell = GetRandomEmptyCell();
            var pathFromAggressiveEnemy = new PathToPlayer(_player, map);
            pathFromAggressiveEnemy.CreateFrom(startingCell.X, startingCell.Y);
            _aggressiveEnemy = new AggressiveEnemy(pathFromAggressiveEnemy)
            {
                X = startingCell.X,
                Y = startingCell.Y
            };
        }

        private static void Clear()
        {
            for (int row = 0; row <= _screenHeight; row++)
            {
                for (int col = 0; col <= _screenWidth; col++)
                {
                    rootConsole.Write(row, col, " ", Color4.Black);
                }
            }
        }

        private static void DrawUI()
        {
            for (int row = 0; row <= _screenHeight; row++)
            {
                for (int col = _screenWidth - 40; col <= _screenWidth; col++)
                {
                    rootConsole.Write(row, col, " ", Color4.Black, new Color4(0, 40, 40, 255));
                }
            }
        }
    }
}