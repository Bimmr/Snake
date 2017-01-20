using BimmCore.MonoGame.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakez.Screens
{
    public class GameScreen : Screen
    {
        private List<Snake> snakes;

        public GameScreen()
        {
            snakes = new List<Snake>();
        }

        public void addSnake(Snake snake)
        {
            snakes.Add(snake);
        }
        public List<Snake> getSnakes()
        {
            return snakes;
        }
        
        public override void onHide()
        {

        }

        public override void onShow()
        {

        }
    }
}
