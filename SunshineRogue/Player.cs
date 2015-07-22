using SunshineConsole;
using OpenTK.Graphics;
using OpenTK.Input;
using RogueSharp;
using RogueSharp.Random;
using System;


namespace SunshineRogue
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Draw()
        {
            Program.rootConsole.Write(Y, X, 'r', Color4.Gray);
        }

        public void GetInput()
        {
            if (Program.rootConsole.KeyPressed)
            {
                Key key = Program.rootConsole.GetKey();

                if (key == Key.Up || key == Key.Keypad8)
                {
                    // Check the RogueSharp map to make sure the Cell is walkable before moving
                    if (Program.map.GetCell(X, Y - 1).IsWalkable)
                    {
                        Global.GameState = GameStates.EnemyTurn;
                        // Update the player position
                        Y--;
                    }
                }

                if (key == Key.Down || key == Key.Keypad2)
                {
                    // Check the RogueSharp map to make sure the Cell is walkable before moving
                    if (Program.map.GetCell(X, Y + 1).IsWalkable)
                    {
                        Global.GameState = GameStates.EnemyTurn;
                        // Update the player position
                        Y++;
                    }
                }

                if (key == Key.Left || key == Key.Keypad4)
                {
                    // Check the RogueSharp map to make sure the Cell is walkable before moving
                    if (Program.map.GetCell(X - 1, Y).IsWalkable)
                    {
                        Global.GameState = GameStates.EnemyTurn;
                        // Update the player position
                        X--;
                    }
                }

                if (key == Key.Right || key == Key.Keypad6)
                {
                    // Check the RogueSharp map to make sure the Cell is walkable before moving
                    if (Program.map.GetCell(X + 1, Y).IsWalkable)
                    {
                        Global.GameState = GameStates.EnemyTurn;
                        // Update the player position
                        X++;
                    }
                }

                if (key == Key.Keypad7)
                {
                    // Check the RogueSharp map to make sure the Cell is walkable before moving
                    if (Program.map.GetCell(X - 1, Y - 1).IsWalkable)
                    {
                        Global.GameState = GameStates.EnemyTurn;
                        // Update the player position
                        X--;
                        Y--;
                    }
                }

                if (key == Key.Keypad9)
                {
                    // Check the RogueSharp map to make sure the Cell is walkable before moving
                    if (Program.map.GetCell(X + 1, Y - 1).IsWalkable)
                    {
                        Global.GameState = GameStates.EnemyTurn;
                        // Update the player position
                        X++;
                        Y--;
                    }
                }

                if (key == Key.Keypad1)
                {
                    // Check the RogueSharp map to make sure the Cell is walkable before moving
                    if (Program.map.GetCell(X - 1, Y + 1).IsWalkable)
                    {
                        Global.GameState = GameStates.EnemyTurn;
                        // Update the player position
                        X--;
                        Y++;
                    }
                }

                if (key == Key.Keypad3)
                {
                    // Check the RogueSharp map to make sure the Cell is walkable before moving
                    if (Program.map.GetCell(X + 1, Y + 1).IsWalkable)
                    {
                        Global.GameState = GameStates.EnemyTurn;
                        // Update the player position
                        X++;
                        Y++;
                    }
                }

                if (key == Key.Space)
                {
                    if (Global.GameState == GameStates.PlayerTurn)
                    {
                        Global.GameState = GameStates.Debugging;
                    }
                    else if (Global.GameState == GameStates.Debugging)
                    {
                        Global.GameState = GameStates.PlayerTurn;
                    }
                }

                if (key == Key.Escape)
                {
                    Program.rootConsole.Close();
                }
            }
        }
    }
}
