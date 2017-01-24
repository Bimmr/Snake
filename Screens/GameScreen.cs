using BimmCore.MonoGame.Components;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using BimmCore.MonoGame;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace Snakez.Screens
{

    public enum GameState { Before, Playing, After }
    public class GameScreen : Screen
    {


        private List<Snake> snakes;
        private GameState state;
        private int countDownCounter;
        private Snake lastAlive;
        private int time;

        private Button returnButton;

        public GameScreen()
        {
            this.snakes = new List<Snake>();
            this.state = GameState.Before;

            //3 Second Countdown
            this.countDownCounter = 30 * 3;

            Rectangle returnButton = new Rectangle((int)MonoHelper.Middle.X - 50, (int)MonoHelper.Middle.Y+15, 100, 30);
            this.returnButton = new Button(returnButton)
                .setSpriteFont(FontHandler.menuFont).setText("Return").setTextColor(Color.White)
                .setBoxColor(Color.SlateGray)
                .setClickEvent((b, s) => {
                    if (LastMouseState.LeftButton != ButtonState.Pressed)
                        screenHandler.show("main");
                })
                .setHoverEvent((b, s) => b.setBoxColor(Color.SlateGray))
                .setNotHoverEvent((b, s) => b.setBoxColor(Color.Gray));
        }

        public List<Snake> getSnakes()
        {
            return snakes;
        }
        public void clearSnakes()
        {
            snakes.Clear();
        }

        public void addSnake(Snake snake)
        {
            snakes.Add(snake);
        }

        public override void onHide()
        {

            //Reset Everything
            if (snakes != null)
            {
                foreach (Snake snake in snakes)
                    snake.body.Clear();

                snakes.Clear();
            }
            this.state = GameState.Before;
            this.countDownCounter = 30 * 3;
            this.lastAlive = null;
            this.time = 0;
        }

        public override void onShow()
        {
            Game.IsMouseVisible = false;
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteHandler.mainBackground.draw(MonoHelper.SpriteBatch,
                new Vector2(0, 0));


            foreach (Snake snake in snakes)
                if (snake.isAlive())
                    snake.draw();

            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "" + (time/30), Utils.centerText(FontHandler.menuFont, "" + (time / 30), new Rectangle((int)MonoHelper.Middle.X-15, 0, 30, 20)), Color.White);

            if (state == GameState.Before) {
                int timeLeft = (countDownCounter / 30) + 1;
                Vector2 fontSize = FontHandler.menuFont.MeasureString("" + timeLeft);
                MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "" + timeLeft, new Vector2(MonoHelper.Middle.X - fontSize.X / 2, MonoHelper.Middle.Y - fontSize.Y / 2), Color.White);
            }
            else if (state == GameState.After) {
                string winner = lastAlive.name;
                Vector2 fontSize = FontHandler.menuFont.MeasureString(winner + "has Won!");
                MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, winner + " has Won!", new Vector2(MonoHelper.Middle.X - fontSize.X / 2, MonoHelper.Middle.Y - fontSize.Y / 2), Color.White);

                returnButton.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {

            switch (state)
            {
                //Before the game
                case GameState.Before:
                    if (countDownCounter > 0)
                        countDownCounter--;
                    else
                    {
                        state = GameState.Playing;
                    }
                    break;

                //During the game
                case GameState.Playing:
                    time++;
                    int snakesAlive = 0;

                    foreach (Snake snake in snakes)
                        if (snake.isAlive())
                        {
                            snakesAlive++;
                            lastAlive = snake;
                            if (snake.name == "Mr. Snake")
                                ((SnakeAI)snake).update();
                            else
                                snake.update();
                        }
                    if (snakesAlive == 1)
                        endGame();
                    break;

                //After the game
                case GameState.After:
                    returnButton.Update(gameTime);
                    break;
            }
            base.Update(gameTime);
        }
        public void endGame()
        {
            this.state = GameState.After;
            Game.IsMouseVisible = true;
        }
        public void startGame()
        {
            this.state = GameState.Playing;
        }
    }
}