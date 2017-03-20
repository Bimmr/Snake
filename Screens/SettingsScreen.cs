using BimmCore.MonoGame;
using BimmCore.MonoGame.Components;
using BimmCore.MonoGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Snakez.Screens
{
    public class SettingsScreen : Screen
    {

        private KeyBoardListener keyboardListener;
        private Button returnButton;
        private Input playerName;

        private Button editing;
        private Button threePlayers;
        private Input aiBots;

        private List<Button> buttons;

        public SettingsScreen()
        {
            this.aiBots = new Input(new Rectangle(200, 50, 100, 50), false)
                .setTextFont(FontHandler.menuFont)
                .setInputFilter(InputFilter.NUMBER)
                .setText("9")
                .setKeyTypeEvent((i, k) =>
                {
                    KeysConverter kc = new KeysConverter();
                    Console.Write(""+i.getText());
                    
                })
                .setSubmitEvent(i =>
                {
                    try
                    {
                        int value = int.Parse(i.getText());
                        if (value <= 9 && value >= 2)
                        {
                            Settings.AISnakes = value;
                        }
                    }
                    catch (Exception e) { }
                });


            keyboardListener = new KeyBoardListener((k) =>
            {
                if (editing != null)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (buttons[i] == editing)
                        {
                            int p = i / 4;
                            int control = i % 4;
                            Debug.WriteLine(p + " -- " + control);
                            if (p == 0)
                                Settings.ControlsP1[control] = k;
                            else if (p == 1)
                                Settings.ControlsP2[control] = k;
                            else if (p == 2)
                                Settings.ControlsP3[control] = k;

                            editing.setText(k.ToString());
                            editing = null;

                            break;
                        }
                    }
                }
            }, null);
            buttons = new List<Button>();

            Rectangle returnButton = new Rectangle(0, 0, 150, 30);
            this.returnButton = new Button(returnButton)
                .setSpriteFont(FontHandler.menuFont).setText("Back To Main Menu").setTextColor(Color.White)
                .setBoxColor(Color.SlateGray)
                .setClickEvent((b, s) =>
                {
                    if (LastMouseState.LeftButton != ButtonState.Pressed)
                        screenHandler.show("main");
                })
                .setHoverEvent((b, s) => b.setBoxColor(Color.SlateGray))
                .setNotHoverEvent((b, s) => b.setBoxColor(Color.Gray));


            Vector2 recPos = new Vector2(15, 80);
            Rectangle nameBox = new Rectangle((int)recPos.X + 75, (int)recPos.Y + 50, 125, 20);
            this.playerName = new Input(nameBox, false)
                .setText("Player 1").setTextFont(FontHandler.menuFont).setTextColor(Color.Black)
                .setKeyTypeEvent((i, k) => { Settings.Name = i.getText(); });

            recPos = new Vector2(560, 80);
            threePlayers = new Button(new Rectangle((int)recPos.X + 75, (int)recPos.Y + 50, 125, 20))
                .setSpriteFont(FontHandler.menuFont).setTextColor(Color.White).setText(Settings.ThreePlayers.ToString())
                .setClickEvent((b, s) => { if (LastMouseState.LeftButton != ButtonState.Pressed) { Settings.ThreePlayers = !Settings.ThreePlayers; b.setText(Settings.ThreePlayers.ToString()); } });


            recPos = new Vector2(15, 80);
            //P1 Up
            buttons.Add(new Button(new Rectangle((int)recPos.X + 75, (int)recPos.Y + 100, 125, 20)).setSpriteFont(FontHandler.menuFont).setTextColor(Color.White).setText(Settings.ControlsP1[0].ToString())
                .setClickEvent((b, s) => { if (LastMouseState.LeftButton != ButtonState.Pressed) editing = b; }));
            //P1 Down
            buttons.Add(new Button(new Rectangle((int)recPos.X + 75, (int)recPos.Y + 150, 125, 20)).setSpriteFont(FontHandler.menuFont).setTextColor(Color.White).setText(Settings.ControlsP1[1].ToString())
                .setClickEvent((b, s) => { if (LastMouseState.LeftButton != ButtonState.Pressed) editing = b; }));
            //P1 Left
            buttons.Add(new Button(new Rectangle((int)recPos.X + 75, (int)recPos.Y + 200, 125, 20)).setSpriteFont(FontHandler.menuFont).setTextColor(Color.White).setText(Settings.ControlsP1[2].ToString())
                .setClickEvent((b, s) => { if (LastMouseState.LeftButton != ButtonState.Pressed) editing = b; }));
            //P1 Right
            buttons.Add(new Button(new Rectangle((int)recPos.X + 75, (int)recPos.Y + 250, 125, 20)).setSpriteFont(FontHandler.menuFont).setTextColor(Color.White).setText(Settings.ControlsP1[3].ToString())
                .setClickEvent((b, s) => { if (LastMouseState.LeftButton != ButtonState.Pressed) editing = b; }));


            recPos = new Vector2(287, 80);
            //P2 Up
            buttons.Add(new Button(new Rectangle((int)recPos.X + 75, (int)recPos.Y + 100, 125, 20)).setSpriteFont(FontHandler.menuFont).setTextColor(Color.White).setText(Settings.ControlsP2[0].ToString())
                .setClickEvent((b, s) => { if (LastMouseState.LeftButton != ButtonState.Pressed) editing = b; }));
            //P2 Down
            buttons.Add(new Button(new Rectangle((int)recPos.X + 75, (int)recPos.Y + 150, 125, 20)).setSpriteFont(FontHandler.menuFont).setTextColor(Color.White).setText(Settings.ControlsP2[1].ToString())
                .setClickEvent((b, s) => { if (LastMouseState.LeftButton != ButtonState.Pressed) editing = b; }));
            //P2 Left
            buttons.Add(new Button(new Rectangle((int)recPos.X + 75, (int)recPos.Y + 200, 125, 20)).setSpriteFont(FontHandler.menuFont).setTextColor(Color.White).setText(Settings.ControlsP2[2].ToString())
                .setClickEvent((b, s) => { if (LastMouseState.LeftButton != ButtonState.Pressed) editing = b; }));
            //P2 Right
            buttons.Add(new Button(new Rectangle((int)recPos.X + 75, (int)recPos.Y + 250, 125, 20)).setSpriteFont(FontHandler.menuFont).setTextColor(Color.White).setText(Settings.ControlsP2[3].ToString())
                .setClickEvent((b, s) => { if (LastMouseState.LeftButton != ButtonState.Pressed) editing = b; }));


            recPos = new Vector2(560, 80);
            //P3 Up
            buttons.Add(new Button(new Rectangle((int)recPos.X + 75, (int)recPos.Y + 100, 125, 20)).setSpriteFont(FontHandler.menuFont).setTextColor(Color.White).setText(Settings.ControlsP3[0].ToString())
                .setClickEvent((b, s) => { if (LastMouseState.LeftButton != ButtonState.Pressed) editing = b; }));
            //P3 Down
            buttons.Add(new Button(new Rectangle((int)recPos.X + 75, (int)recPos.Y + 150, 125, 20)).setSpriteFont(FontHandler.menuFont).setTextColor(Color.White).setText(Settings.ControlsP3[1].ToString())
                .setClickEvent((b, s) => { if (LastMouseState.LeftButton != ButtonState.Pressed) editing = b; }));
            //P3 Left
            buttons.Add(new Button(new Rectangle((int)recPos.X + 75, (int)recPos.Y + 200, 125, 20)).setSpriteFont(FontHandler.menuFont).setTextColor(Color.White).setText(Settings.ControlsP3[2].ToString())
                .setClickEvent((b, s) => { if (LastMouseState.LeftButton != ButtonState.Pressed) editing = b; }));
            //P3 Right
            buttons.Add(new Button(new Rectangle((int)recPos.X + 75, (int)recPos.Y + 250, 125, 20)).setSpriteFont(FontHandler.menuFont).setTextColor(Color.White).setText(Settings.ControlsP3[3].ToString())
                .setClickEvent((b, s) => { if (LastMouseState.LeftButton != ButtonState.Pressed) editing = b; }));

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

            Vector2 recPos = new Vector2(15, 80);
            Drawer.drawRectangle(new Rectangle((int)recPos.X, (int)recPos.Y, 225, 350), Color.White);
            Drawer.drawRectangle(new Rectangle((int)recPos.X + 1, (int)recPos.Y + 1, 223, 348), Color.Gray);
            Drawer.drawRectangle(new Rectangle((int)recPos.X + 1, (int)recPos.Y + 1, 223, 35), Color.DarkGray);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Player 1 Settings", new Vector2(recPos.X + 45, recPos.Y + 10), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Name: ", new Vector2(recPos.X + 10, recPos.Y + 50), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Up: ", new Vector2(recPos.X + 10, recPos.Y + 100), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Down: ", new Vector2(recPos.X + 10, recPos.Y + 150), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Left: ", new Vector2(recPos.X + 10, recPos.Y + 200), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Right: ", new Vector2(recPos.X + 10, recPos.Y + 250), Color.White);

            recPos = new Vector2(287, 80);
            Drawer.drawRectangle(new Rectangle((int)recPos.X, (int)recPos.Y, 225, 350), Color.White);
            Drawer.drawRectangle(new Rectangle((int)recPos.X + 1, (int)recPos.Y + 1, 223, 348), Color.Gray);
            Drawer.drawRectangle(new Rectangle((int)recPos.X + 1, (int)recPos.Y + 1, 223, 35), Color.DarkGray);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Player 2 Settings", new Vector2(recPos.X + 45, recPos.Y + 10), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Up: ", new Vector2(recPos.X + 10, recPos.Y + 100), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Down: ", new Vector2(recPos.X + 10, recPos.Y + 150), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Left: ", new Vector2(recPos.X + 10, recPos.Y + 200), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Right: ", new Vector2(recPos.X + 10, recPos.Y + 250), Color.White);


            recPos = new Vector2(560, 80);
            Drawer.drawRectangle(new Rectangle((int)recPos.X, (int)recPos.Y, 225, 350), Color.White);
            Drawer.drawRectangle(new Rectangle((int)recPos.X + 1, (int)recPos.Y + 1, 223, 348), Color.Gray);
            Drawer.drawRectangle(new Rectangle((int)recPos.X + 1, (int)recPos.Y + 1, 223, 35), Color.DarkGray);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Player 3 Settings", new Vector2(recPos.X + 45, recPos.Y + 10), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Enabled: ", new Vector2(recPos.X + 10, recPos.Y + 50), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Up: ", new Vector2(recPos.X + 10, recPos.Y + 100), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Down: ", new Vector2(recPos.X + 10, recPos.Y + 150), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Left: ", new Vector2(recPos.X + 10, recPos.Y + 200), Color.White);
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "Right: ", new Vector2(recPos.X + 10, recPos.Y + 250), Color.White);

            foreach (Button b in buttons)
            {
                if (editing == b)
                    b.setBoxColor(Color.DarkGray);
                else
                    b.setBoxColor(Color.DarkSlateGray);

                b.Draw(gameTime);
            }

            if (Settings.ThreePlayers)
                threePlayers.setBoxColor(Color.DarkGray);
            else
                threePlayers.setBoxColor(Color.DarkSlateGray);

            threePlayers.Draw(gameTime);

            returnButton.Draw(gameTime);
            playerName.Draw(gameTime);
            aiBots.Draw(gameTime);

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            returnButton.Update(gameTime);
            playerName.Update(gameTime);
            keyboardListener.update();
            threePlayers.Update(gameTime);
            aiBots.Update(gameTime);

            if (editing != null)
            {
                foreach (Button b in buttons)
                    if (editing == b)
                    {
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed && !b.getSize().Contains(Mouse.GetState().Position))
                        {
                            editing = null;
                            break;
                        }
                    }
            }

            foreach (Button b in buttons)
                b.Update(gameTime);

            base.Update(gameTime);
        }
    }
}
