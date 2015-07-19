using SunshineConsole;
using OpenTK.Graphics;
using OpenTK.Input;
using RogueSharp;
using RogueSharp.Random;

namespace SunshineRogue
{
    class Program
    {

        // The screen height and width are in number of tiles
        private static readonly int _screenWidth = 150;
        private static readonly int _screenHeight = 40;
        // The starting position for the player
        private static int _playerX;
        private static int _playerY;
        private static ConsoleWindow _rootConsole;
        private static IMap _map;

        static void Main(string[] args)
        {
            // The title will appear at the top of the console window
            string consoleTitle = "Dungeons and @ Signs";
            // Create the console
            _rootConsole = new ConsoleWindow(_screenHeight, _screenWidth, consoleTitle);

            GenerateMap();
            StartPlayer();
            GameLoop();
        }

        private static void GameLoop()
        {
            while (!_rootConsole.KeyPressed && _rootConsole.WindowUpdate())
            {
                GetInput();
                RootConsoleRender();
            }
        }

        private static void GetInput()
        {
            if (_rootConsole.KeyPressed)
            {
                Key key = _rootConsole.GetKey();
                
                if (key == Key.Up)
                {
                    // Check the RogueSharp map to make sure the Cell is walkable before moving
                    if (_map.GetCell(_playerX, _playerY - 1).IsWalkable)
                    {
                        // Update the player position
                        _playerY--;
                    }
                }

                if (key == Key.Down)
                {
                    // Check the RogueSharp map to make sure the Cell is walkable before moving
                    if (_map.GetCell(_playerX, _playerY + 1).IsWalkable)
                    {
                        // Update the player position
                        _playerY++;
                    }
                }

                if (key == Key.Left)
                {
                    // Check the RogueSharp map to make sure the Cell is walkable before moving
                    if (_map.GetCell(_playerX - 1, _playerY).IsWalkable)
                    {
                        // Update the player position
                        _playerX--;
                    }
                }

                if (key == Key.Right)
                {
                    // Check the RogueSharp map to make sure the Cell is walkable before moving
                    if (_map.GetCell(_playerX + 1, _playerY).IsWalkable)
                    {
                        // Update the player position
                        _playerX++;
                    }
                }
            }
        }

        private static void RootConsoleRender()
        {
            for (int row = 0; row <= _screenHeight; row++)
            {
                for (int col = 0; col <= _screenWidth; col++)
                {
                    _rootConsole.Write(row, col, " ", Color4.Black);
                }
            }

                // Use RogueSharp to calculate the current field-of-view for the player
                _map.ComputeFov(_playerX, _playerY, 15, true);
 
          foreach ( var cell in _map.GetAllCells() )
          {
            // When a Cell is in the field-of-view set it to a brighter color
            if ( cell.IsInFov )
            {
              _map.SetCellProperties( cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true );
              if ( cell.IsWalkable )
              {
                _rootConsole.Write( cell.Y, cell.X, '.', Color4.Yellow );
              }
              else
              {
                _rootConsole.Write( cell.Y, cell.X, '#', Color4.White );
              }
            }
            // If the Cell is not in the field-of-view but has been explored set it darker
            else if ( cell.IsExplored )
            {
              if ( cell.IsWalkable )
              {
                  _rootConsole.Write(cell.Y, cell.X, '.', Color4.Gray);
              }
              else
              {
                  _rootConsole.Write(cell.Y, cell.X, '#', Color4.DarkGray);
              }
            }
          }
 
          // Set the player's symbol after the map symbol to make sure it is draw
          _rootConsole.Write( _playerY, _playerX, '@', Color4.LightSkyBlue );
        }

        private static void GenerateMap()
        {
            //Generate random rooms map
            _map = Map.Create(new RandomRoomsMapCreationStrategy<Map>(_screenWidth - 20, _screenHeight, 45, 10, 6));
        }

        private static Cell GetRandomEmptyCell()
        {
            IRandom random = new DotNetRandom();

            while (true)
            {
                int x = random.Next(49);
                int y = random.Next(29);
                if (_map.IsWalkable(x, y))
                {
                    return _map.GetCell(x, y);
                }
            }
        }

        private static void StartPlayer()
        {
            Cell startingCell = GetRandomEmptyCell();
            _playerX = startingCell.X;
            _playerY = startingCell.Y;
        }
    }
}