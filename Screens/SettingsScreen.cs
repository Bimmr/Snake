using BimmCore.MonoGame;
using BimmCore.MonoGame.Components;
using BimmCore.MonoGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Snakez.Screens
{
    public class SettingsScreen : Screen
    {
        private Button returnButton;
        private Input playerName;

        public SettingsScreen()
        {
            Rectangle returnButton = new Rectangle(0, 0, 150, 30);
            this.returnButton = new Button(returnButton)
                .setSpriteFont(FontHandler.menuFont).setText("Back To Main Menu").setTextColor(Color.White)
                .setBoxColor(Color.SlateGray)
                .setClickEvent((b, s) => {
                    if (LastMouseState.LeftButton != ButtonState.Pressed)
                        screenHandler.show("main");
                })
                .setHoverEvent((b, s) => b.setBoxColor(Color.SlateGray))
                .setNotHoverEvent((b, s) => b.setBoxColor(Color.Gray));

            Rectangle nameBox = new Rectangle(80, 130, 150, 30);
            this.playerName = new Input(nameBox, false)
                .setText("Player 1").setTextFont(FontHandler.menuFont).setTextColor(Color.Black)
                .setKeyTypeEvent((i, k) => { Settings.Name = i.getText(); });

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
            SpriteHandler.mainBackground.draw(MonoHelper.SpriteBatch,
                new Vector2(0, 0));

            Drawer.drawRectangle(new Rectangle(15, 80, 225, 350), Color.White);
            Drawer.drawRectangle(new Rectangle(16, 81, 223, 348), Color.Gray);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Player 1 Settings", new Vector2(50, 100), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Name: ", new Vector2(25, 135), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Up: ", new Vector2(25, 205), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Down: ", new Vector2(25, 240), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Left: ", new Vector2(25, 275), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Right: ", new Vector2(25, 310), Color.White);


            Drawer.drawRectangle(new Rectangle(285, 80, 225, 350), Color.White);
            Drawer.drawRectangle(new Rectangle(286, 81, 223, 348), Color.Gray);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Player 1 Settings", new Vector2(50, 100), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Name: ", new Vector2(25, 135), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Up: ", new Vector2(25, 205), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Down: ", new Vector2(25, 240), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Left: ", new Vector2(25, 275), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Right: ", new Vector2(25, 310), Color.White);


            Drawer.drawRectangle(new Rectangle(585, 80, 225, 350), Color.White);
            Drawer.drawRectangle(new Rectangle(586, 81, 223, 348), Color.Gray);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Player 1 Settings", new Vector2(50, 100), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Name: ", new Vector2(25, 135), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Up: ", new Vector2(25, 205), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Down: ", new Vector2(25, 240), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Left: ", new Vector2(25, 275), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Right: ", new Vector2(25, 310), Color.White);

            returnButton.Draw(gameTime);
            playerName.Draw(gameTime);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            returnButton.Update(gameTime);
            playerName.Update(gameTime);

            base.Update(gameTime);
        }
    }
}
