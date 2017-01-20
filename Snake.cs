using BimmCore.MonoGame;
using BimmCore.MonoGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Snakez.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakez
{
    public class Snake
    {
        private GameScreen gameScreen;
        private Keys up, down, left, right;
        private Color color;
        private Direction direction;
        private List<Vector2> body;

        private int growDelay = 50;
        private int growCounter = 0;

        private Vector2 head
        {
            get{
                return body[0];
            }
        }

        public Snake(GameScreen gameScreen, Color color, Keys up, Keys down, Keys left, Keys right)
        {
            this.body = new List<Vector2>();
            this.gameScreen = gameScreen;
            this.color = color;
            this.direction = Direction.Up;

            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }

        private void removeTail()
        {
            body.RemoveAt(body.Count - 1);
        }
        private void move()
        {
            body.Insert(0, getNextPoint());
        }

        private Vector2 getNextPoint()
        {
            Vector2 next = head;
            switch (direction)
            {
                case Direction.Up:
                    next.Y = -1 ;
                    break;
                case Direction.Down:
                    next.Y = +1;
                    break;
                case Direction.Left:
                    next.X = -1;
                    break;
                case Direction.Right:
                    next.X = +1;
                    break;
            }
            return head;
        }
        private bool collided()
        {
            bool collided = false;
            if (head.X < 0 || head.X > MonoHelper.Size.X)
                collided = true;
            if (head.Y < 0 || head.Y > MonoHelper.Size.Y)
                collided = true;

            return collided;
                
        }
        public void update()
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(up))
                direction = Direction.Up;
            else if (ks.IsKeyDown(down))
                direction = Direction.Down;
            else if (ks.IsKeyDown(left))
                direction = Direction.Left;
            else if (ks.IsKeyDown(right))
                direction = Direction.Right;

            move();

            growCounter++;
            if (growCounter > growDelay)
                growCounter = 0;
            else
                removeTail();
            
        } 
        public void draw()
        {
            foreach(Vector2 vec in body)
            {
                Drawer.drawRectangle(new Rectangle((int)vec.X, (int)vec.Y, 1, 1), color);
            }

        }
    }
}
