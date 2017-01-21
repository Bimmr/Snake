using BimmCore.MonoGame.Components;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using BimmCore.MonoGame;

namespace Snakez.Screens
{
    public class GameScreen : Screen
    {
        private List<Snake> snakes;

        public GameScreen()
        {
            this.snakes = new List<Snake>();
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
        }

        public override void onShow()
        {
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteHandler.mainBackground.draw(MonoHelper.SpriteBatch,
                new Vector2(0, 0));


            foreach (Snake snake in snakes)
                if (snake.isAlive())
                    snake.draw();

            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            bool foundAlive = false;

            foreach (Snake snake in snakes)
                if (snake.isAlive())
                {
                    foundAlive = true;
                    if (snake.name == "AI")
                        ((SnakeAI)snake).update();
                    else
                        snake.update();
                }
            if (!foundAlive)
            {

            }

            base.Update(gameTime);
        }
    }
}