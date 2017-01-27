using BimmCore.MonoGame;
using BimmCore.MonoGame.Components;
using Microsoft.Xna.Framework;
using BimmCore.MonoGame.Graphics;
using Microsoft.Xna.Framework.Input;
using BimmCore.Misc;

namespace Snakez.Screens
{
    public class MainScreen : Screen
    {
        private Menu menu;
        private ScrollingBackground background;

        public MainScreen()
        {
            background = new ScrollingBackground(SpriteHandler.mainBackground, new Vector2(0, 0), 2, Direction.Up);

            string[] menuElements = { "Single Player", "Multiplayer", "Leaderboards", "Settings", "Exit" };
            float menuX = MonoHelper.Middle.X - Menu.Size.Width / 2;
            float menuY = MonoHelper.Middle.Y - (Menu.Size.Height * (menuElements.Length)) / 2 + 50;
            menu = new Menu(new Vector2(menuX, menuY), (Color.LightGray * 0.6f), Color.White, FontHandler.menuFont);

            //Single Player
            menu.addOption(menuElements[0], (b, s) =>
            {
                if (LastMouseState.LeftButton != ButtonState.Pressed)
                {
                    screenHandler.show("game");
                    GameScreen gs = (GameScreen)screenHandler.getCurrent();
                    gs.clearSnakes();
                    Vector2 pos = new Vector2(MonoHelper.Middle.X - Snake.size / 2, MonoHelper.Middle.Y - Snake.size / 2);
                    gs.addSnake(new Snake(Settings.Name, gs, new Vector2(pos.X * (Settings.SinglePlayerAI ? .5f : 1f), pos.Y), Settings.ControlsP1[0], Settings.ControlsP1[1], Settings.ControlsP1[2], Settings.ControlsP1[3]));
                    if (Settings.SinglePlayerAI)
                        gs.addSnake(new SnakeAI(gs, new Vector2(pos.X * 1.5f, pos.Y)));
                }
            });


            //Multiplayer
            menu.addOption(menuElements[1], (b, s) =>
            {
                if (LastMouseState.LeftButton != ButtonState.Pressed)
                {
                    screenHandler.show("game");
                    GameScreen gs = (GameScreen)screenHandler.getCurrent();
                    gs.clearSnakes();
                    Vector2 pos = new Vector2(MonoHelper.Middle.X - Snake.size / 2, MonoHelper.Middle.Y - Snake.size / 2);
                    gs.addSnake(new Snake(Settings.Name, gs, new Vector2(pos.X * .3f, pos.Y), Settings.ControlsP1[0], Settings.ControlsP1[1], Settings.ControlsP1[2], Settings.ControlsP1[3]));
                    gs.addSnake(new Snake("Player 2", gs, new Vector2(pos.X * (Settings.ThreePlayers ? 1f : 1.5f), pos.Y), Settings.ControlsP2[0], Settings.ControlsP2[1], Settings.ControlsP2[2], Settings.ControlsP2[3]));
                    if(Settings.ThreePlayers)
                        gs.addSnake(new Snake("Player 3", gs, new Vector2(pos.X * 1.9f, pos.Y), Settings.ControlsP3[0], Settings.ControlsP3[1], Settings.ControlsP3[2], Settings.ControlsP3[3]));
                }
            });

            //Leaderboards(Single Player)
            menu.addOption(menuElements[2], (b, s) =>
            {
                if (LastMouseState.LeftButton != ButtonState.Pressed)
                {
                    screenHandler.show("leaderboards");
                }
            });

            //Settings
            menu.addOption(menuElements[3], (b, s) =>
            {
                if (LastMouseState.LeftButton != ButtonState.Pressed)
                {
                    screenHandler.show("settings");
                }
            });
            //Exit
            menu.addOption(menuElements[4], (b, s) =>
            {
                if (LastMouseState.LeftButton != ButtonState.Pressed)
                {
                    MonoHelper.Game.Exit();
                }
            });
        }

        public override void onHide()
        {
        }

        public override void onShow()
        {
            Game.IsMouseVisible = true;
        }

        public override void Draw(GameTime gameTime)
        {
            background.draw();
            menu.Draw(gameTime);
            SpriteHandler.logoHeader.draw(MonoHelper.SpriteBatch,
                new Vector2(MonoHelper.Middle.X - SpriteHandler.logoHeader.getWidth() / 2, 0));

            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, SnakeGame.AUTHOR,
                new Vector2(5, MonoHelper.Size.Y - 20), Color.Gray);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, SnakeGame.VERSION,
                new Vector2(MonoHelper.Size.X - 5 - FontHandler.menuFont.MeasureString(SnakeGame.VERSION).X,
                    MonoHelper.Size.Y - 20), Color.Gray);
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