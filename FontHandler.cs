using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakez
{
    public class FontHandler
    {

        public static SpriteFont menuFont;


        public static void setup(Game game)
        {
            menuFont = game.Content.Load<SpriteFont>("Font/MenuFont");
        }
    }
}
