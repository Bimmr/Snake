using BimmCore.MonoGame;
using BimmCore.MonoGame.Components;
using Microsoft.Xna.Framework;
using BimmCore.MonoGame.Graphics;

namespace Snakez.Screens
{
    public class MainScreen : Screen
    {
        private Menu menu;
        private ScrollingBackground background;

        public MainScreen()
        {
            background = new ScrollingBackground(SpriteHandler.mainBackground, new Vector2(0, 0), 2, Direction.Down);

            string[] menuElements = { "Single Player", "Multiplayer", "Leaderboards", "Settings", "Exit" };
            float menuX = (MonoHelper.Middle.X - Menu.Size.Width / 2);
            float menuY = (MonoHelper.Middle.Y - (Menu.Size.Height * (menuElements.Length))/2)+50;
            menu = new Menu(new Vector2(menuX, menuY), (Color.LightGray*0.6f), Color.White, FontHandler.menuFont);
            menu.addOption(menuElements[0], (b, s) => { screenHandler.show("singleplayer"); });
            menu.addOption(menuElements[1], (b, s) => { screenHandler.show("multiplayer"); });
            menu.addOption(menuElements[2], (b, s) => { screenHandler.show("leaderboards"); });
            menu.addOption(menuElements[3], (b, s) => { screenHandler.show("settings"); });
            menu.addOption(menuElements[4], (b, s) => { MonoHelper.Game.Exit(); });
        }

        public override void onHide()
        {
            Game.IsMouseVisible = false;
        }

        public override void onShow()
        {
            Game.IsMouseVisible = true;
        }

        public override void Draw(GameTime gameTime)
        {
            background.draw();
            menu.Draw(gameTime);
            SpriteHandler.logoHeader.draw(MonoHelper.SpriteBatch, new Vector2(MonoHelper.Middle.X - SpriteHandler.logoHeader.getWidth()/2, 0));

            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, SnakeGame.AUTHOR, new Vector2(5, MonoHelper.Size.Y - 20), Color.Gray);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, SnakeGame.VERSION, new Vector2(MonoHelper.Size.X-5- FontHandler.menuFont.MeasureString(SnakeGame.VERSION).X, MonoHelper.Size.Y - 20), Color.Gray);
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            background.move();
            menu.Update(gameTime);

            base.Update(gameTime);
        }
    }
}
