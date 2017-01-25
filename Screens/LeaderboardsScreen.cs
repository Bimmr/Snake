using BimmCore.MonoGame;
using BimmCore.MonoGame.Components;
using Microsoft.Xna.Framework;
using BimmCore.MonoGame.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using BimmCore.Misc;
using System.Linq;

namespace Snakez.Screens
{
    public class LeaderboardsScreen : Screen
    {

        private Button returnButton;
        private static FileHelper fileHelper;

        public LeaderboardsScreen()
        {
            if(fileHelper == null)
                fileHelper = new FileHelper("HighScores.txt");

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

        public static Dictionary<string, string> loadHighScores()
        {
            Dictionary<string, string> highScores = fileHelper.getAll();
            var ordered = highScores.OrderByDescending(i => i.Value);

            return highScores;
        }
        public static void addScore(string name, int time)
        {
            string foundLower = null;
            Dictionary<string, string> highScores = loadHighScores();

            foreach (KeyValuePair<string, string> e in highScores)
            {
                if (int.Parse(e.Value) > time)
                    foundLower = e.Key;
            }
            if (highScores.Count > 10 && foundLower != null)
            {
                highScores.Remove(foundLower);
                highScores.Add(name, ""+time);
            }
            if (foundLower != null)
            {
                fileHelper.clear();
                foreach (KeyValuePair<string, string> e in highScores)
                {
                    fileHelper.add(e.Key, "" + e.Value);
                }
            }
        }
    }
}
