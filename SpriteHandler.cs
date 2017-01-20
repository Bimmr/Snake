using BimmCore.MonoGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snakez
{
    public class SpriteHandler
    {

        public static Sprite mainBackground;
        public static Sprite logoHeader;


        public static void setup(Game game)
        {
            Texture2D mainBackText = game.Content.Load<Texture2D>("Images/Background");
            Texture2D logoHeaderTex = game.Content.Load<Texture2D>("Images/MenuHeader");

            mainBackground = new Sprite(mainBackText, new Rectangle(0, 0, mainBackText.Width, mainBackText.Height));
            logoHeader = new Sprite(logoHeaderTex, new Rectangle(0, 0, logoHeaderTex.Width, logoHeaderTex.Height));
        }
    }
}
