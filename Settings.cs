using BimmCore.MonoGame.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakez
{
    public class Settings
    {
        //Controls for Player 1
        public static Keys[] ControlsP1 = new[] { Keys.W, Keys.S, Keys.A, Keys.D };

        //Controls for Player 2
        public static Keys[] ControlsP2 = new[] { Keys.Up, Keys.Down, Keys.Left, Keys.Right };

        //Controls for Player 3
        public static Keys[] ControlsP3 = new[] { Keys.O, Keys.L, Keys.K, Keys.OemSemicolon };

        public static bool ThreePlayers = true;

        public static bool SinglePlayerAI = true;

        public static string Name = "Player 1";
    }
}
