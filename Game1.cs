﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Monogmae_4___Timers_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bombTexture, explosionTexture, pliersTexture;
        Rectangle bombRect, greenWireRect, explosionRect, redWireRect;

        SpriteFont timefont;

        SoundEffect explode;
        SoundEffectInstance explodeInstance;

        float seconds;

        bool boom = false;

        bool detonated = false;

        MouseState mouseState, prevMouseState;




        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            bombRect = new Rectangle(50, 50, 700, 400);
            greenWireRect = new Rectangle(490, 160, 160, 15);
            redWireRect = new Rectangle(485, 175, 140, 25);
            explosionRect = new Rectangle(70, 50, 600, 400);


            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            seconds = 0f;

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            bombTexture = Content.Load<Texture2D>("bomb");
            explosionTexture = Content.Load<Texture2D>("boom");
            pliersTexture = Content.Load<Texture2D>("pliers");
            timefont = Content.Load<SpriteFont>("TimeFont");
            explode = Content.Load<SoundEffect>("explosion");
            explodeInstance = explode.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
            if (detonated == false)
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (seconds > 15)
            {
                boom = true;
                seconds = 0;
                explodeInstance.Play();
               
            }
            if (boom)
            {
                if (explodeInstance.State == SoundState.Stopped)    
                    Exit();
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                if (greenWireRect.Contains(mouseState.Position))
                    detonated = true;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                if (redWireRect.Contains(mouseState.Position))
                {
                    boom = true;
                    seconds = 0;
                    explodeInstance.Play();
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.DrawString(timefont, (15 - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);
            _spriteBatch.Draw(pliersTexture, mouseState.Position.ToVector2(), Color.White);
            if (boom == true)
            {
                _spriteBatch.Draw(explosionTexture, explosionRect, Color.White);      
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
