using BimmCore.MonoGame;
using BimmCore.MonoGame.Components;
using Microsoft.Xna.Framework;
using BimmCore.MonoGame.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snakez.Screens
{
    public class SettingsScreen : Screen
    {
        private Button returnButton;

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

            returnButton.Draw(gameTime);
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            returnButton.Update(gameTime);

            base.Update(gameTime);
        }
    }
}
