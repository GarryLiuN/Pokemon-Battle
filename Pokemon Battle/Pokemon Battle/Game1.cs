using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pokemon_Battle
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        #region Graphing Variables
        SpriteFont font;
        SpriteFont InstructionFont;

        Texture2D Background_Title;
        Texture2D Background_Game;
        Texture2D StartButton;
        Texture2D TextBox;
        Texture2D TextBox_Insctruction;
        Texture2D arrow;
        Texture2D GameBackground;
        int StartCount;

        Rectangle BackgroundRec;
        Rectangle TextBoxRec;
        Rectangle TextBox_InsctructionRec;
        Rectangle TextBox_menuRec;

        #endregion

        #region Booleans
        bool MusicState = true;
        bool manu = false;
        bool Player1Won = false;
        bool Player2Won = false;
        #endregion

        #region Player 1 Variables
        Texture2D Player1Sprite;
        Texture2D[] Player1Up;
        Texture2D[] Player1Left;
        Texture2D[] Player1Right;
        Texture2D Player1MateSprite;
        Texture2D[] Player1MateUp;
        Texture2D[] Player1MateLeft;
        Texture2D[] Player1MateRight;

        Texture2D Player1Attack;
        Vector2 Player1Position;
        Rectangle Player1Rec;
        List<Vector2> Player1AttackPosition;
        List<Rectangle> Player1AttackRec;
        int Player1count = 0;
        int Player1LifeCount = 4;
        int Player1InvincibleCount;
        bool Player1Mate = false;
        bool Player1Invincible = false;
        #endregion

        #region Player 2 Variables
        Texture2D Player2Sprite;
        Texture2D[] Player2Down;
        Texture2D[] Player2Left;
        Texture2D[] Player2Right;
        Texture2D Player2MateSprite;
        Texture2D[] Player2MateDown;
        Texture2D[] Player2MateLeft;
        Texture2D[] Player2MateRight;

        Texture2D Player2Attack;
        Vector2 Player2Position;
        Rectangle Player2Rec;
        List<Vector2> Player2AttackPosition;
        List<Rectangle> Player2AttackRec;
        int Player2count = 0;
        int Player2LifeCount = 4;
        int Player2InvincibleCount;
        bool Player2Mate = false;
        bool Player2Invincible = false;
        #endregion

        #region PokemonEgg Variables

        Texture2D PokemonEgg;
        List<Vector2> PokemonEggVec;
        List<Rectangle> PokemonEggRec;
        int PokemonEggMarker;
        int PokemonEggCount = 0;

        #endregion

        #region Music and SoundEffect
        Song PressStartScreen;
        Song TitleScreen;
        Song GameScreen;
        Song EndScreen;
        Song PlayingSong;

        SoundEffect Shoot;
        SoundEffect Hit;
        #endregion

        #region Keyboard and Gamepads
        KeyboardState kb;
        KeyboardState oldkb;

        GamePadState pad1;
        GamePadState oldpad1;
        GamePadState pad2;
        GamePadState oldpad2;
        #endregion

        #region Else
        enum GameState
        {
            PressStart,
            Title,
            Instruction,
            Game,
            EndGame,
        }
        GameState gameState = GameState.PressStart;

        Random random;
        Texture2D Player1Life;
        Texture2D Player2Life;
        Texture2D EmptyLife;
        int select = 1;
        int select_menu = 1;

        #endregion
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 400;
            graphics.PreferredBackBufferHeight = 500;
            Window.Title = "Pokemon Battle";
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            BackgroundRec = new Rectangle(0, 0, 400, 500);
            TextBoxRec = new Rectangle(197, 388, 6, 4);
            TextBox_InsctructionRec = new Rectangle(200, 290, 0, 0);
            TextBox_menuRec = new Rectangle(197, 240, 6, 4);

            Player1Sprite = Player1Up[0];
            Player2Sprite = Player2Down[0];
            Player1MateSprite = Player1MateUp[0];
            Player2MateSprite = Player2MateDown[0];

            Player1Position = new Vector2(180, 420);
            Player2Position = new Vector2(180, 20);
            Player1Rec = new Rectangle((int)Player1Position.X, (int)Player1Position.Y, Player1Sprite.Width, Player1Sprite.Height);
            Player2Rec = new Rectangle((int)Player2Position.X, (int)Player2Position.Y, Player1Sprite.Width, Player1Sprite.Height);

            random = new Random();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Background_Title = Content.Load<Texture2D>("Background_Title");
            Background_Game = Content.Load<Texture2D>("Background_Game");
            StartButton = Content.Load<Texture2D>("Start Button");
            TextBox = Content.Load<Texture2D>("TextBox");
            TextBox_Insctruction = Content.Load<Texture2D>("TextBox_Insctruction");
            arrow = Content.Load<Texture2D>("arrow");
            GameBackground = Content.Load<Texture2D>("GameBackground");
            Player1Attack = Content.Load<Texture2D>("fire_red");
            Player2Attack = Content.Load<Texture2D>("fire_blue");
            Player1Life = Content.Load<Texture2D>("Player1Life");
            Player2Life = Content.Load<Texture2D>("Player2Life");
            EmptyLife = Content.Load<Texture2D>("EmptyLife");
            PokemonEgg = Content.Load<Texture2D>("PokemonEgg");
            GameScreen = Content.Load<Song>("Music\\GameScreen");
            PressStartScreen = Content.Load<Song>("Music\\PressStartScreen");
            TitleScreen = Content.Load<Song>("Music\\TitleScreen");

            EndScreen = Content.Load<Song>("Music\\EndScreen");
            PlayingSong = PressStartScreen;

            Shoot = Content.Load<SoundEffect>("SoundEffect\\Shoot");
            Hit = Content.Load<SoundEffect>("SoundEffect\\Hit");

            font = Content.Load<SpriteFont>("Font");
            InstructionFont=Content.Load<SpriteFont>("InstructionFont");

            #region Player1 Movement
            Player1Up = new Texture2D[4];
            Player1Up[0] = Content.Load<Texture2D>("Charizard\\CharizardUp\\CharizardUp1");
            Player1Up[1] = Content.Load<Texture2D>("Charizard\\CharizardUp\\CharizardUp2");
            Player1Up[2] = Content.Load<Texture2D>("Charizard\\CharizardUp\\CharizardUp3");
            Player1Up[3] = Content.Load<Texture2D>("Charizard\\CharizardUp\\CharizardUp4");
            Player1Left = new Texture2D[4];
            Player1Left[0] = Content.Load<Texture2D>("Charizard\\CharizardLeft\\CharizardLeft1");
            Player1Left[1] = Content.Load<Texture2D>("Charizard\\CharizardLeft\\CharizardLeft2");
            Player1Left[2] = Content.Load<Texture2D>("Charizard\\CharizardLeft\\CharizardLeft3");
            Player1Left[3] = Content.Load<Texture2D>("Charizard\\CharizardLeft\\CharizardLeft4");
            Player1Right = new Texture2D[4];
            Player1Right[0] = Content.Load<Texture2D>("Charizard\\CharizardRight\\CharizardRight1");
            Player1Right[1] = Content.Load<Texture2D>("Charizard\\CharizardRight\\CharizardRight2");
            Player1Right[2] = Content.Load<Texture2D>("Charizard\\CharizardRight\\CharizardRight3");
            Player1Right[3] = Content.Load<Texture2D>("Charizard\\CharizardRight\\CharizardRight4");
            #endregion
            #region Player1Mate Movement
            Player1MateUp = new Texture2D[4];
            Player1MateUp[0] = Content.Load<Texture2D>("Charmander\\CharmanderUp\\CharmanderUp1");
            Player1MateUp[1] = Content.Load<Texture2D>("Charmander\\CharmanderUp\\CharmanderUp2");
            Player1MateUp[2] = Content.Load<Texture2D>("Charmander\\CharmanderUp\\CharmanderUp3");
            Player1MateUp[3] = Content.Load<Texture2D>("Charmander\\CharmanderUp\\CharmanderUp4");
            Player1MateLeft = new Texture2D[4];
            Player1MateLeft[0] = Content.Load<Texture2D>("Charmander\\CharmanderLeft\\CharmanderLeft1");
            Player1MateLeft[1] = Content.Load<Texture2D>("Charmander\\CharmanderLeft\\CharmanderLeft2");
            Player1MateLeft[2] = Content.Load<Texture2D>("Charmander\\CharmanderLeft\\CharmanderLeft3");
            Player1MateLeft[3] = Content.Load<Texture2D>("Charmander\\CharmanderLeft\\CharmanderLeft4");
            Player1MateRight = new Texture2D[4];
            Player1MateRight[0] = Content.Load<Texture2D>("Charmander\\CharmanderRight\\CharmanderRight1");
            Player1MateRight[1] = Content.Load<Texture2D>("Charmander\\CharmanderRight\\CharmanderRight2");
            Player1MateRight[2] = Content.Load<Texture2D>("Charmander\\CharmanderRight\\CharmanderRight3");
            Player1MateRight[3] = Content.Load<Texture2D>("Charmander\\CharmanderRight\\CharmanderRight4");
            #endregion
            #region Player2 Movement
            Player2Down = new Texture2D[4];
            Player2Down[0] = Content.Load<Texture2D>("Blastoise\\BlastoiseDown\\BlastoiseDown1");
            Player2Down[1] = Content.Load<Texture2D>("Blastoise\\BlastoiseDown\\BlastoiseDown2");
            Player2Down[2] = Content.Load<Texture2D>("Blastoise\\BlastoiseDown\\BlastoiseDown3");
            Player2Down[3] = Content.Load<Texture2D>("Blastoise\\BlastoiseDown\\BlastoiseDown4");
            Player2Left = new Texture2D[4];
            Player2Left[0] = Content.Load<Texture2D>("Blastoise\\BlastoiseLeft\\BlastoiseLeft1");
            Player2Left[1] = Content.Load<Texture2D>("Blastoise\\BlastoiseLeft\\BlastoiseLeft2");
            Player2Left[2] = Content.Load<Texture2D>("Blastoise\\BlastoiseLeft\\BlastoiseLeft3");
            Player2Left[3] = Content.Load<Texture2D>("Blastoise\\BlastoiseLeft\\BlastoiseLeft4");
            Player2Right = new Texture2D[4];
            Player2Right[0] = Content.Load<Texture2D>("Blastoise\\BlastoiseRight\\BlastoiseRight1");
            Player2Right[1] = Content.Load<Texture2D>("Blastoise\\BlastoiseRight\\BlastoiseRight2");
            Player2Right[2] = Content.Load<Texture2D>("Blastoise\\BlastoiseRight\\BlastoiseRight3");
            Player2Right[3] = Content.Load<Texture2D>("Blastoise\\BlastoiseRight\\BlastoiseRight4");
            #endregion
            #region Player2Mate Movement
            Player2MateDown = new Texture2D[4];
            Player2MateDown[0] = Content.Load<Texture2D>("Squirtle\\SquirtleDown\\SquirtleDown1");
            Player2MateDown[1] = Content.Load<Texture2D>("Squirtle\\SquirtleDown\\SquirtleDown2");
            Player2MateDown[2] = Content.Load<Texture2D>("Squirtle\\SquirtleDown\\SquirtleDown3");
            Player2MateDown[3] = Content.Load<Texture2D>("Squirtle\\SquirtleDown\\SquirtleDown4");
            Player2MateLeft = new Texture2D[4];
            Player2MateLeft[0] = Content.Load<Texture2D>("Squirtle\\SquirtleLeft\\SquirtleLeft1");
            Player2MateLeft[1] = Content.Load<Texture2D>("Squirtle\\SquirtleLeft\\SquirtleLeft2");
            Player2MateLeft[2] = Content.Load<Texture2D>("Squirtle\\SquirtleLeft\\SquirtleLeft3");
            Player2MateLeft[3] = Content.Load<Texture2D>("Squirtle\\SquirtleLeft\\SquirtleLeft4");
            Player2MateRight = new Texture2D[4];
            Player2MateRight[0] = Content.Load<Texture2D>("Squirtle\\SquirtleRight\\SquirtleRight1");
            Player2MateRight[1] = Content.Load<Texture2D>("Squirtle\\SquirtleRight\\SquirtleRight2");
            Player2MateRight[2] = Content.Load<Texture2D>("Squirtle\\SquirtleRight\\SquirtleRight3");
            Player2MateRight[3] = Content.Load<Texture2D>("Squirtle\\SquirtleRight\\SquirtleRight4");
            #endregion

            Player1AttackPosition = new List<Vector2>();
            Player1AttackRec = new List<Rectangle>();

            Player2AttackPosition = new List<Vector2>();
            Player2AttackRec = new List<Rectangle>();

            PokemonEggVec=new List<Vector2>();
            PokemonEggRec=new List<Rectangle>();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            kb = Keyboard.GetState();
            pad1 = GamePad.GetState(PlayerIndex.One);
            pad2 = GamePad.GetState(PlayerIndex.Two);

            if (MusicState)
            {
                if (MediaPlayer.State == MediaState.Paused || MediaPlayer.State == MediaState.Stopped)
                    MediaPlayer.Play(PlayingSong);
            }
            else if (!MusicState)
            {
                if (MediaPlayer.State == MediaState.Playing)
                    MediaPlayer.Stop();
            }

            switch (gameState)
            {
                case GameState.PressStart:
                    PressStart_Update();
                    break;

                case GameState.Title:
                    Title_Update();
                    break;

                case GameState.Instruction:
                    Instruction_Update();
                    break;

                case GameState.Game:
                    Game_BGM_Update();
                    if (!manu)
                    {
                        Game_PokemonEgg_Update();

                        Game_Movement_Update();
                        Game_AttackEffect_Update();
                        Game_Collision_Update();


                        if ((kb.IsKeyDown(Keys.Escape) && oldkb.IsKeyUp(Keys.Escape)) || (pad1.IsButtonDown(Buttons.Start) && oldpad1.IsButtonUp(Buttons.Start)))
                            manu = true;
                    }
                    else
                    {
                        Game_Manu_Update();
                    }
                    break;

                case GameState.EndGame:
                    EndGame_Update();
                    break;
            }

            oldkb = kb;
            oldpad1 = pad1;
            oldpad2 = pad2;
            base.Update(gameTime);
        }

        private void Game_PokemonEgg_Update()
        {
            PokemonEggCount++;
            if (PokemonEggCount == 300)
            {
                PokemonEggMarker = random.Next(2);
                PokemonEggVec.Add(new Vector2((PokemonEggMarker * 400), (250 - PokemonEgg.Height / 2)));
                PokemonEggRec.Add(new Rectangle((PokemonEggMarker * 400), (250 - PokemonEgg.Height / 2), PokemonEgg.Width, PokemonEgg.Height));
            }
            for (int i = 0; i < PokemonEggVec.Count(); i++)
            {
                if (PokemonEggMarker == 0)
                {

                    PokemonEggVec[i] = new Vector2(PokemonEggVec[i].X, (float)(250 - PokemonEgg.Height / 2 + 120 * Math.Sin(PokemonEggVec[i].X / 90 * Math.PI))) + new Vector2((float)1.5, 0);
                    PokemonEggRec[i] = new Rectangle((int)PokemonEggVec[i].X, (int)PokemonEggVec[i].Y, PokemonEgg.Width, PokemonEgg.Height);

                }
                if (PokemonEggMarker == 1)
                {

                    PokemonEggVec[i] = new Vector2(PokemonEggVec[i].X, (float)(250 - PokemonEgg.Height / 2 + 120 * Math.Sin(PokemonEggVec[i].X / 90 * Math.PI))) - new Vector2((float)1.5, 0);
                    PokemonEggRec[i] = new Rectangle((int)PokemonEggVec[i].X, (int)PokemonEggVec[i].Y, PokemonEgg.Width, PokemonEgg.Height);

                }
                if (!BackgroundRec.Intersects(PokemonEggRec[i]))
                {
                    PokemonEggCount = 0;
                    PokemonEggRec.RemoveAt(i);
                    PokemonEggVec.RemoveAt(i);
                    PokemonEggMarker = 3;
                }

                else if(Player1Rec.Intersects(PokemonEggRec[i]))
                {
                    Player1Mate = true;
                    PokemonEggCount = 0;
                    PokemonEggRec.RemoveAt(i);
                    PokemonEggVec.RemoveAt(i);
                    PokemonEggMarker = 3;
                }
                else if (Player2Rec.Intersects(PokemonEggRec[i]))
                {
                    Player2Mate = true;
                    PokemonEggCount = 0;
                    PokemonEggRec.RemoveAt(i);
                    PokemonEggVec.RemoveAt(i);
                    PokemonEggMarker = 3;
                }
            }
        }

        private void PressStart_Update()
        {
            if (kb.IsKeyDown(Keys.Enter))
            {
                gameState = GameState.Title;
            }
        }
        private void Title_Update()
        {
            //select option
            if ((pad1.IsButtonDown(Buttons.DPadDown) && oldpad1.IsButtonUp(Buttons.DPadDown)) || (kb.IsKeyDown(Keys.Down) && oldkb.IsKeyUp(Keys.Down)))
                select++;
            if ((pad1.IsButtonDown(Buttons.DPadUp) && oldpad1.IsButtonUp(Buttons.DPadUp)) || (kb.IsKeyDown(Keys.Up) && oldkb.IsKeyUp(Keys.Up)))
                select--;
            if (select == 0)
                select = 4;
            if (select == 5)
                select = 1;

            if (select == 1 && ((pad1.IsButtonDown(Buttons.A) && oldpad1.IsButtonUp(Buttons.A)) || (kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter))))
            {
                gameState = GameState.Game;
                TextBoxRec = new Rectangle(197, 388, 6, 4);
            }
            if (select == 2 && ((pad1.IsButtonDown(Buttons.A) && oldpad1.IsButtonUp(Buttons.A)) || (kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter))))
                gameState = GameState.Instruction;
            if (select == 3 && ((pad1.IsButtonDown(Buttons.A) && oldpad1.IsButtonUp(Buttons.A)) || (kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter))))
            {
                if (MusicState)
                    MusicState = false;
                else if (!MusicState)
                    MusicState = true;
            }
            if (select == 4 && ((pad1.IsButtonDown(Buttons.A) && oldpad1.IsButtonUp(Buttons.A)) || (kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter))))
                this.Exit();
            //BGM
            if (PlayingSong != TitleScreen)
            {
                MediaPlayer.Stop();
                PlayingSong = TitleScreen;
            }
        }
        private void Instruction_Update()
        {
            if ((kb.IsKeyDown(Keys.Escape) && oldkb.IsKeyUp(Keys.Escape)) || (pad1.IsButtonDown(Buttons.B) && oldpad1.IsButtonUp(Buttons.B)))
            {
                gameState = GameState.Title;
                TextBox_InsctructionRec = new Rectangle(200, 290, 0, 0);
            }
        }

        private void Game_BGM_Update()
        {
            //BGM
            if (PlayingSong != GameScreen)
            {
                MediaPlayer.Stop();
                PlayingSong = GameScreen;
            }
        }
        private void Game_Movement_Update()
        {
            #region Player 1 Movement
            #region Up Movement
            if (pad1.DPad.Up == ButtonState.Pressed || kb.IsKeyDown(Keys.W))
            {
                if (Player1Position.Y > 300)
                {
                    Player1Position.Y -= 3;
                }
                Player1count++;
                if (Player1count < 10)
                    Player1Sprite = Player1Up[1];
                if (Player1count >= 10 && Player1count < 20)
                    Player1Sprite = Player1Up[2];
                if (Player1count >= 20 && Player1count < 30)
                    Player1Sprite = Player1Up[3];
                if (Player1count >= 30 && Player1count < 40)
                    Player1Sprite = Player1Up[0];
                if (Player1count == 40)
                    Player1count = 0;
                if (Player1Mate == true)
                {
                    if (Player1count < 10)
                        Player1MateSprite = Player1MateUp[1];
                    if (Player1count >= 10 && Player1count < 20)
                        Player1MateSprite = Player1MateUp[2];
                    if (Player1count >= 20 && Player1count < 30)
                        Player1MateSprite = Player1MateUp[3];
                    if (Player1count >= 30 && Player1count < 40)
                        Player1MateSprite = Player1MateUp[0];
                    if (Player1count == 40)
                        Player1count = 0;
                }
            }
            #endregion
            #region Down Movement
            else if (pad1.DPad.Down == ButtonState.Pressed || kb.IsKeyDown(Keys.S))
            {
                if (Player1Position.Y < (500 - Player1Sprite.Height))
                {
                    Player1Position.Y += 3;
                }
                Player1count++;
                if (Player1count < 10)
                    Player1Sprite = Player1Up[3];
                if (Player1count >= 10 && Player1count < 20)
                    Player1Sprite = Player1Up[2];
                if (Player1count >= 20 && Player1count < 30)
                    Player1Sprite = Player1Up[1];
                if (Player1count >= 30 && Player1count < 40)
                    Player1Sprite = Player1Up[0];
                if (Player1count == 40)
                    Player1count = 0;
                if (Player1Mate == true)
                {
                    if (Player1count < 10)
                        Player1MateSprite = Player1MateUp[3];
                    if (Player1count >= 10 && Player1count < 20)
                        Player1MateSprite = Player1MateUp[2];
                    if (Player1count >= 20 && Player1count < 30)
                        Player1MateSprite = Player1MateUp[1];
                    if (Player1count >= 30 && Player1count < 40)
                        Player1MateSprite = Player1MateUp[0];
                    if (Player1count == 40)
                        Player1count = 0;
                }
            }
            #endregion
            #region Left Movement
            else if (pad1.DPad.Left == ButtonState.Pressed || kb.IsKeyDown(Keys.A))
            {
                if (Player1Position.X > 0)
                {
                    Player1Position.X -= 3;
                }
                Player1count++;
                if (Player1count < 10)
                    Player1Sprite = Player1Left[1];
                if (Player1count >= 10 && Player1count < 20)
                    Player1Sprite = Player1Left[2];
                if (Player1count >= 20 && Player1count < 30)
                    Player1Sprite = Player1Left[3];
                if (Player1count >= 30 && Player1count < 40)
                    Player1Sprite = Player1Left[0];
                if (Player1count == 40)
                    Player1count = 0;
                if (Player1Mate == true)
                {
                    if (Player1count < 10)
                        Player1MateSprite = Player1MateLeft[1];
                    if (Player1count >= 10 && Player1count < 20)
                        Player1MateSprite = Player1MateLeft[2];
                    if (Player1count >= 20 && Player1count < 30)
                        Player1MateSprite = Player1MateLeft[3];
                    if (Player1count >= 30 && Player1count < 40)
                        Player1MateSprite = Player1MateLeft[0];
                    if (Player1count == 40)
                        Player1count = 0;
                }
            }
            #endregion
            #region Right Movement
            else if (pad1.DPad.Right == ButtonState.Pressed || kb.IsKeyDown(Keys.D))
            {
                if (Player1Position.X < (400 - Player1Sprite.Width))
                {
                    Player1Position.X += 3;
                }
                Player1count++;
                if (Player1count < 10)
                    Player1Sprite = Player1Right[1];
                if (Player1count >= 10 && Player1count < 20)
                    Player1Sprite = Player1Right[2];
                if (Player1count >= 20 && Player1count < 30)
                    Player1Sprite = Player1Right[3];
                if (Player1count >= 30 && Player1count < 40)
                    Player1Sprite = Player1Right[0];
                if (Player1count == 40)
                    Player1count = 0;
                if (Player1Mate == true)
                {
                    if (Player1count < 10)
                        Player1MateSprite = Player1MateRight[1];
                    if (Player1count >= 10 && Player1count < 20)
                        Player1MateSprite = Player1MateRight[2];
                    if (Player1count >= 20 && Player1count < 30)
                        Player1MateSprite = Player1MateRight[3];
                    if (Player1count >= 30 && Player1count < 40)
                        Player1MateSprite = Player1MateRight[0];
                    if (Player1count == 40)
                        Player1count = 0;
                }
            }
            #endregion
            #region Stop
            if ((pad1.DPad.Up == ButtonState.Released && oldpad1.DPad.Up == ButtonState.Pressed) || (kb.IsKeyUp(Keys.W) && oldkb.IsKeyDown(Keys.W)))
            {
                Player1Sprite = Player1Up[0];
                Player1MateSprite = Player1MateUp[0];
                Player1count = 0;
            }

            if ((pad1.DPad.Down == ButtonState.Released && oldpad1.DPad.Down == ButtonState.Pressed) || (kb.IsKeyUp(Keys.S) && oldkb.IsKeyDown(Keys.S)))
            {
                Player1Sprite = Player1Up[0];
                Player1MateSprite = Player1MateUp[0];
                Player1count = 0;
            }

            if ((pad1.DPad.Left == ButtonState.Released && oldpad1.DPad.Left == ButtonState.Pressed) || (kb.IsKeyUp(Keys.A) && oldkb.IsKeyDown(Keys.A)))
            {
                Player1Sprite = Player1Up[0];
                Player1MateSprite = Player1MateUp[0];
                Player1count = 0;
            }

            if ((pad1.DPad.Right == ButtonState.Released && oldpad1.DPad.Right == ButtonState.Pressed) || (kb.IsKeyUp(Keys.D) && oldkb.IsKeyDown(Keys.D)))
            {
                Player1Sprite = Player1Up[0];
                Player1MateSprite = Player1MateUp[0];
                Player1count = 0;
            }
            #endregion
            Player1Rec = new Rectangle((int)Player1Position.X, (int)Player1Position.Y, Player1Sprite.Width, Player1Sprite.Height);
            #endregion
            #region Player 2 Movement
            #region Up Movement
            if (pad2.DPad.Up == ButtonState.Pressed || kb.IsKeyDown(Keys.Up))
            {
                if (Player2Position.Y > 0)
                {
                    Player2Position.Y -= 3;
                }
                Player2count++;
                if (Player2count < 10)
                    Player2Sprite = Player2Down[1];
                if (Player2count >= 10 && Player2count < 20)
                    Player2Sprite = Player2Down[2];
                if (Player2count >= 20 && Player2count < 30)
                    Player2Sprite = Player2Down[3];
                if (Player2count >= 30 && Player2count < 40)
                    Player2Sprite = Player2Down[0];
                if (Player2count == 40)
                    Player2count = 0;
                if (Player2Mate == true)
                {
                    if (Player2count < 10)
                        Player2MateSprite = Player2MateDown[3];
                    if (Player2count >= 10 && Player2count < 20)
                        Player2MateSprite = Player2MateDown[2];
                    if (Player2count >= 20 && Player2count < 30)
                        Player2MateSprite = Player2MateDown[1];
                    if (Player2count >= 30 && Player2count < 40)
                        Player2MateSprite = Player2MateDown[0];
                    if (Player2count == 40)
                        Player2count = 0;
                }
            }
            #endregion
            #region Down Movement
            else if (pad2.DPad.Down == ButtonState.Pressed || kb.IsKeyDown(Keys.Down))
            {
                if (Player2Position.Y < (200 - Player2Sprite.Height))
                {
                    Player2Position.Y += 3;
                }
                Player2count++;
                if (Player2count < 10)
                    Player2Sprite = Player2Down[3];
                if (Player2count >= 10 && Player2count < 20)
                    Player2Sprite = Player2Down[2];
                if (Player2count >= 20 && Player2count < 30)
                    Player2Sprite = Player2Down[1];
                if (Player2count >= 30 && Player2count < 40)
                    Player2Sprite = Player2Down[0];
                if (Player2count == 40)
                    Player2count = 0;
                if (Player2Mate == true)
                {
                    if (Player2count < 10)
                        Player2MateSprite = Player2MateDown[1];
                    if (Player2count >= 10 && Player2count < 20)
                        Player2MateSprite = Player2MateDown[2];
                    if (Player2count >= 20 && Player2count < 30)
                        Player2MateSprite = Player2MateDown[3];
                    if (Player2count >= 30 && Player2count < 40)
                        Player2MateSprite = Player2MateDown[0];
                    if (Player2count == 40)
                        Player2count = 0;
                }
            }
            #endregion
            #region Left Movement
            else if (pad2.DPad.Left == ButtonState.Pressed || kb.IsKeyDown(Keys.Left))
            {
                if (Player2Position.X > 0)
                {
                    Player2Position.X -= 3;
                }
                Player2count++;
                if (Player2count < 10)
                    Player2Sprite = Player2Left[1];
                if (Player2count >= 10 && Player2count < 20)
                    Player2Sprite = Player2Left[2];
                if (Player2count >= 20 && Player2count < 30)
                    Player2Sprite = Player2Left[3];
                if (Player2count >= 30 && Player2count < 40)
                    Player2Sprite = Player2Left[0];
                if (Player2count == 40)
                    Player2count = 0;
                if (Player2Mate == true)
                {
                    if (Player2count < 10)
                        Player2MateSprite = Player2MateLeft[1];
                    if (Player2count >= 10 && Player2count < 20)
                        Player2MateSprite = Player2MateLeft[2];
                    if (Player2count >= 20 && Player2count < 30)
                        Player2MateSprite = Player2MateLeft[3];
                    if (Player2count >= 30 && Player2count < 40)
                        Player2MateSprite = Player2MateLeft[0];
                    if (Player2count == 40)
                        Player2count = 0;
                }
            }
            #endregion
            #region Right Movement
            else if (pad2.DPad.Right == ButtonState.Pressed || kb.IsKeyDown(Keys.Right))
            {
                if (Player2Position.X < (400 - Player2Sprite.Width))
                {
                    Player2Position.X += 3;
                }
                Player2count++;
                if (Player2count < 10)
                    Player2Sprite = Player2Right[1];
                if (Player2count >= 10 && Player2count < 20)
                    Player2Sprite = Player2Right[2];
                if (Player2count >= 20 && Player2count < 30)
                    Player2Sprite = Player2Right[3];
                if (Player2count >= 30 && Player2count < 40)
                    Player2Sprite = Player2Right[0];
                if (Player2count == 40)
                    Player2count = 0;
                if (Player2Mate == true)
                {
                    if (Player2count < 10)
                        Player2MateSprite = Player2MateRight[1];
                    if (Player2count >= 10 && Player2count < 20)
                        Player2MateSprite = Player2MateRight[2];
                    if (Player2count >= 20 && Player2count < 30)
                        Player2MateSprite = Player2MateRight[3];
                    if (Player2count >= 30 && Player2count < 40)
                        Player2MateSprite = Player2MateRight[0];
                    if (Player2count == 40)
                        Player2count = 0;
                }
            } 

            #endregion
            #region Stop
            if ((pad2.DPad.Up == ButtonState.Released && oldpad2.DPad.Up == ButtonState.Pressed) || (kb.IsKeyUp(Keys.Up) && oldkb.IsKeyDown(Keys.Up)))
            {
                Player2Sprite = Player2Down[0];
                Player2count = 0;
                Player2MateSprite = Player2MateDown[0];
            }

            if ((pad2.DPad.Down == ButtonState.Released && oldpad2.DPad.Down == ButtonState.Pressed) || (kb.IsKeyUp(Keys.Down) && oldkb.IsKeyDown(Keys.Down)))
            {
                Player2Sprite = Player2Down[0];
                Player2count = 0;
                Player2MateSprite = Player2MateDown[0];
            }

            if ((pad2.DPad.Left == ButtonState.Released && oldpad2.DPad.Left == ButtonState.Pressed) || (kb.IsKeyUp(Keys.Left) && oldkb.IsKeyDown(Keys.Left)))
            {
                Player2Sprite = Player2Down[0];
                Player2count = 0;
                Player2MateSprite = Player2MateDown[0];
            }

            if ((pad2.DPad.Right == ButtonState.Released && oldpad2.DPad.Right == ButtonState.Pressed) || (kb.IsKeyUp(Keys.Right) && oldkb.IsKeyDown(Keys.Right)))
            {
                Player2Sprite = Player2Down[0];
                Player2count = 0;
                Player2MateSprite = Player2MateDown[0];
            }
            #endregion
            Player2Rec = new Rectangle((int)Player2Position.X, (int)Player2Position.Y, Player2Sprite.Width, Player2Sprite.Height);
            #endregion
        }
        private void Game_AttackEffect_Update()
        {
            #region Player 1 Attack
            if ((kb.IsKeyDown(Keys.Space) && oldkb.IsKeyUp(Keys.Space)) || (pad1.IsButtonDown(Buttons.A) && oldpad1.IsButtonUp(Buttons.A)))
            {
                if (!Player1Mate && Player1AttackPosition.Count() < 10)
                {
                    Player1AttackPosition.Add(new Vector2(Player1Position.X + (Player1Sprite.Width / 2) - (Player1Attack.Width / 2), Player1Position.Y));
                    Player1AttackRec.Add(new Rectangle((int)Player1Position.X + (Player1Sprite.Width / 2) - (Player1Attack.Width / 2), (int)Player1Position.Y, Player1Attack.Width, Player1Attack.Height));
                    Shoot.Play();
                }
                if (Player1Mate && Player1AttackPosition.Count() < 20)
                {
                    Player1AttackPosition.Add(new Vector2(Player1Position.X + (Player1Sprite.Width / 2) - (Player1Attack.Width / 2), Player1Position.Y));
                    Player1AttackRec.Add(new Rectangle((int)Player1Position.X + (Player1Sprite.Width / 2) - (Player1Attack.Width / 2), (int)Player1Position.Y, Player1Attack.Width, Player1Attack.Height));
                    Player1AttackPosition.Add(new Vector2(Player1Position.X + Player1Up[0].Width + 5 + (Player1MateSprite.Width / 2) - (Player1Attack.Width / 2), Player1Position.Y + 6));
                    Player1AttackRec.Add(new Rectangle((int)Player1Position.X + Player1Up[0].Width + 5 + (Player1MateSprite.Width / 2) - (Player1Attack.Width / 2), (int)Player1Position.Y + 6, Player1Attack.Width, Player1Attack.Height));
                    Shoot.Play();
                }
            }
            for (int i = 0; i < Player1AttackPosition.Count; i++)
            {

                Player1AttackPosition[i] -= new Vector2(0, 2);
                Player1AttackRec[i] = new Rectangle((int)Player1AttackPosition[i].X, (int)Player1AttackPosition[i].Y, Player1Attack.Width, Player1Attack.Height);

                if (!BackgroundRec.Intersects(Player1AttackRec[i]))
                {
                    Player1AttackPosition.RemoveAt(i);
                    Player1AttackRec.RemoveAt(i);
                    i--;
                }
            }
            #endregion
            #region Player 2 Attack
            if ((kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter)) || (pad2.IsButtonDown(Buttons.A) && oldpad2.IsButtonUp(Buttons.A)))
            {
                if (!Player2Mate && Player2AttackPosition.Count() < 10)
                {
                    Player2AttackPosition.Add(new Vector2(Player2Position.X + (Player2Sprite.Width / 2) - (Player2Attack.Width / 2), Player2Position.Y));
                    Player2AttackRec.Add(new Rectangle((int)Player2Position.X + (Player2Sprite.Width / 2) - (Player2Attack.Width / 2), (int)Player2Position.Y, Player2Attack.Width, Player2Attack.Height));
                    Shoot.Play();
                }
                if (Player2Mate && Player2AttackPosition.Count() < 20)
                {
                    Player2AttackPosition.Add(new Vector2(Player2Position.X + (Player2Sprite.Width / 2) - (Player2Attack.Width / 2), Player2Position.Y));
                    Player2AttackRec.Add(new Rectangle((int)Player2Position.X + (Player2Sprite.Width / 2) - (Player2Attack.Width / 2), (int)Player2Position.Y, Player2Attack.Width, Player2Attack.Height));
                    Player2AttackPosition.Add(new Vector2(Player2Position.X - Player2MateDown[0].Width - 5 + (Player2MateSprite.Width / 2) - (Player2Attack.Width / 2), Player2Position.Y + 3));
                    Player2AttackRec.Add(new Rectangle((int)Player2Position.X - Player2MateDown[0].Width - 5 + (Player2MateSprite.Width / 2) - (Player2Attack.Width / 2), (int)Player2Position.Y + 3, Player2Attack.Width, Player2Attack.Height));
                    Shoot.Play();
                }
            }
            for (int i = 0; i < Player2AttackPosition.Count; i++)
            {
                Player2AttackPosition[i] += new Vector2(0, 2);
                Player2AttackRec[i] = new Rectangle((int)Player2AttackPosition[i].X, (int)Player2AttackPosition[i].Y, Player2Attack.Width, Player2Attack.Height);
                if (!BackgroundRec.Intersects(Player2AttackRec[i]))
                {
                    Player2AttackPosition.RemoveAt(i);
                    Player2AttackRec.RemoveAt(i);
                    i--;
                }
            }
            #endregion
        }
        private void Game_Collision_Update()
        {
            for (int i = 0; i < Player2AttackPosition.Count(); i++)
            {
                if (Player1Rec.Intersects(Player2AttackRec[i]) && !Player1Invincible)
                {
                    Player2AttackPosition.RemoveAt(i);
                    Player2AttackRec.RemoveAt(i);
                    i--;
                    if (Player1Mate)
                        Player1Mate = false;
                    else
                        Player1LifeCount--;
                    if (Player1LifeCount == 0)
                        gameState = GameState.EndGame;

                    Hit.Play();

                    Player1Invincible = true;
                    Player1InvincibleCount = 0;
                }
            }
            for (int i = 0; i < Player1AttackPosition.Count(); i++)
            {
                if (Player2Rec.Intersects(Player1AttackRec[i]) && !Player2Invincible)
                {
                    Player1AttackPosition.RemoveAt(i);
                    Player1AttackRec.RemoveAt(i);
                    i--;
                    if (Player2Mate)
                        Player2Mate = false;
                    else
                        Player2LifeCount--;
                    if (Player2LifeCount == 0)
                        gameState = GameState.EndGame;

                    Hit.Play();

                    Player2Invincible = true;
                    Player2InvincibleCount = 0;
                }
            }
            // Invincible
            if (Player1Invincible)
            {
                Player1InvincibleCount++;
                if (Player1InvincibleCount == 90)
                    Player1Invincible = false;
            }
            if (Player2Invincible)
            {
                Player2InvincibleCount++;
                if (Player2InvincibleCount == 90)
                    Player2Invincible = false;
            }
        }
        private void Game_Manu_Update()
        {
            if ((kb.IsKeyDown(Keys.Escape) && oldkb.IsKeyUp(Keys.Escape)) || (pad1.IsButtonDown(Buttons.Start) && oldpad1.IsButtonUp(Buttons.Start)))
            {
                manu = false;
                TextBox_menuRec = new Rectangle(197, 240, 6, 4);
            }
            if ((pad1.IsButtonDown(Buttons.DPadDown) && oldpad1.IsButtonUp(Buttons.DPadDown)) || (kb.IsKeyDown(Keys.Down) && oldkb.IsKeyUp(Keys.Down)))
                select_menu++;
            if ((pad1.IsButtonDown(Buttons.DPadUp) && oldpad1.IsButtonUp(Buttons.DPadUp)) || (kb.IsKeyDown(Keys.Up) && oldkb.IsKeyUp(Keys.Up)))
                select_menu--;
            if (select_menu == 0)
                select_menu = 3;
            if (select_menu == 4)
                select_menu = 1;

            if (select_menu == 1 && ((pad1.IsButtonDown(Buttons.A) && oldpad1.IsButtonUp(Buttons.A)) || (kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter))))
            {
                manu = false;
                TextBox_menuRec = new Rectangle(197, 240, 6, 4);
            }
            if (select_menu == 2 && ((pad1.IsButtonDown(Buttons.A) && oldpad1.IsButtonUp(Buttons.A)) || (kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter))))
            {
                if (MusicState)
                    MusicState = false;
                else if (!MusicState)
                    MusicState = true;
            }
            if (select_menu == 3 && ((pad1.IsButtonDown(Buttons.A) && oldpad1.IsButtonUp(Buttons.A)) || (kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter))))
            {
                gameState = GameState.Title;
                Player1LifeCount = 4;
                Player2LifeCount = 4;
                Player1Mate = false;
                Player2Mate = false;
                Player1Position = new Vector2(180, 420);
                Player2Position = new Vector2(180, 20);
                Player1AttackPosition.Clear();
                Player1AttackRec.Clear();
                Player2AttackPosition.Clear();
                Player2AttackRec.Clear();
                select_menu = 1;
                manu = false;
                Player1Invincible = false;
                Player1InvincibleCount = 0;
                Player2Invincible = false;
                Player2InvincibleCount = 0;
                PokemonEggMarker = 3;
                PokemonEggCount = 0;
                PokemonEggRec.Clear();
                PokemonEggVec.Clear();
            }
        }

        private void EndGame_Update()
        {
            if (Player1LifeCount == 0 && Player2LifeCount != 0 && !Player1Won)
            {
                Player1Won = false;
                Player2Won = true;
            }
            else if (Player1LifeCount != 0 && Player2LifeCount == 0 && !Player2Won)
            {
                Player1Won = true;
                Player2Won = false;
            }
            if ((pad1.IsButtonDown(Buttons.DPadDown) && oldpad1.IsButtonUp(Buttons.DPadDown)) || (kb.IsKeyDown(Keys.Down) && oldkb.IsKeyUp(Keys.Down)))
                select_menu++;
            if ((pad1.IsButtonDown(Buttons.DPadUp) && oldpad1.IsButtonUp(Buttons.DPadUp)) || (kb.IsKeyDown(Keys.Up) && oldkb.IsKeyUp(Keys.Up)))
                select_menu--;
            if (select_menu == 0)
                select_menu = 3;
            if (select_menu == 4)
                select_menu = 1;

            if (select_menu == 1 && ((pad1.IsButtonDown(Buttons.A) && oldpad1.IsButtonUp(Buttons.A)) || (kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter))))
            {
                gameState = GameState.Game;
                Player1LifeCount = 4;
                Player2LifeCount = 4;
                Player1Mate = false;
                Player2Mate = false;
                Player1Position = new Vector2(180, 420);
                Player2Position = new Vector2(180, 20);
                Player1AttackPosition.Clear();
                Player1AttackRec.Clear();
                Player2AttackPosition.Clear();
                Player2AttackRec.Clear();
                Player1Won = false;
                Player2Won = false;
                TextBox_menuRec = new Rectangle(197, 240, 6, 4);
                Player1Invincible = false;
                Player1InvincibleCount = 0;
                Player2Invincible = false;
                Player2InvincibleCount = 0;
                PokemonEggMarker = 3;
                PokemonEggCount = 0;
                PokemonEggRec.Clear();
                PokemonEggVec.Clear();
            }
            if (select_menu == 2 && ((pad1.IsButtonDown(Buttons.A) && oldpad1.IsButtonUp(Buttons.A)) || (kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter))))
            {
                gameState = GameState.Title;
                Player1LifeCount = 4;
                Player2LifeCount = 4;
                Player1Mate = false;
                Player2Mate = false;
                Player1Position = new Vector2(180, 420);
                Player2Position = new Vector2(180, 20);
                Player1AttackPosition.Clear();
                Player1AttackRec.Clear();
                Player2AttackPosition.Clear();
                Player2AttackRec.Clear();
                Player1Won = false;
                Player2Won = false;
                select_menu = 1;
                TextBox_menuRec = new Rectangle(197, 240, 6, 4);
                Player1Invincible = false;
                Player1InvincibleCount = 0;
                Player2Invincible = false;
                Player2InvincibleCount = 0;
                PokemonEggMarker = 3;
                PokemonEggCount = 0;
                PokemonEggRec.Clear();
                PokemonEggVec.Clear();
            }
            if (select_menu == 3 && ((pad1.IsButtonDown(Buttons.A) && oldpad1.IsButtonUp(Buttons.A)) || (kb.IsKeyDown(Keys.Enter) && oldkb.IsKeyUp(Keys.Enter))))
            {
                this.Exit();
                select_menu = 1;
            }
            //BGM
            if (PlayingSong != EndScreen)
            {
                MediaPlayer.Stop();
                PlayingSong = EndScreen;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.PressStart:
                    PressStart_Draw();
                    break;

                case GameState.Title:
                    Title_Draw();
                    break;

                case GameState.Instruction:
                    Instruction_Draw();
                    break;

                case GameState.Game:
                    Game_Draw();

                    Game_Manu_Draw();
                    break;

                case GameState.EndGame:
                    Game_Draw();
                    EndGame_Draw();
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void PressStart_Draw()
        {
            spriteBatch.Draw(Background_Title, BackgroundRec, Color.White);
            StartCount++;
            if (StartCount >= 20)
            {
                spriteBatch.Draw(StartButton, new Vector2(43, 400), Color.White);
            }
            if (StartCount == 60)
            {
                StartCount = 0;
            }
        }
        private void Title_Draw()
        {
            spriteBatch.Draw(Background_Title, BackgroundRec, Color.White);
            spriteBatch.Draw(TextBox, TextBoxRec, Color.White);
            if (TextBoxRec.Location.X != 80)
            {
                TextBoxRec.X -= 3;
                TextBoxRec.Y -= 2;
                TextBoxRec.Width += 6;
                TextBoxRec.Height += 4;
            }
            if (TextBoxRec.Location.X <= 80)
            {
                spriteBatch.DrawString(font, "Start", new Vector2(130, 335), Color.Black);
                spriteBatch.DrawString(font, "Instruction", new Vector2(128, 365), Color.Black);
                if (MusicState)
                    spriteBatch.DrawString(font, "Music : On", new Vector2(130, 395), Color.Black);
                else
                    spriteBatch.DrawString(font, "Music : Off", new Vector2(130, 395), Color.Black);
                spriteBatch.DrawString(font, "Exit", new Vector2(130, 425), Color.Black);

                spriteBatch.Draw(arrow, new Vector2(110, (305 + select * 30)), Color.White);
            }
        }
        private void Instruction_Draw()
        {
            spriteBatch.Draw(Background_Title, BackgroundRec, Color.White);
            spriteBatch.Draw(TextBox_Insctruction, TextBox_InsctructionRec, Color.White);
            if (TextBox_InsctructionRec.Location.X != 10)
            {
                TextBox_InsctructionRec.X -= 10;
                TextBox_InsctructionRec.Y -= 10;
                TextBox_InsctructionRec.Width += 20;
                TextBox_InsctructionRec.Height += 20;
            }

            if (TextBox_InsctructionRec.Location.X == 10)
            {
                spriteBatch.DrawString(InstructionFont, "Greeting Players,", new Vector2(20, 118), Color.Black);
                spriteBatch.DrawString(InstructionFont, "Welcome to", new Vector2(20, 168), Color.Black);
                spriteBatch.DrawString(InstructionFont, "Pokemon:      vs", new Vector2(20, 180), Color.Black);
                spriteBatch.DrawString(InstructionFont, "Fire", new Vector2(129, 180), Color.Red);
                spriteBatch.DrawString(InstructionFont, "Water", new Vector2(225, 180), Color.Blue);

                spriteBatch.DrawString(InstructionFont, "Players will use          or", new Vector2(20, 222), Color.Black);
                spriteBatch.DrawString(InstructionFont, "charizard", new Vector2(216, 222), Color.Red);
                spriteBatch.DrawString(InstructionFont, "balastoise", new Vector2(20, 234), Color.Blue);
                spriteBatch.DrawString(InstructionFont, "to fight each other.", new Vector2(146, 234), Color.Black);
                spriteBatch.DrawString(InstructionFont, "Each player will have 2 life", new Vector2(20, 246), Color.Black);
                spriteBatch.DrawString(InstructionFont, "and the one who loses all life ", new Vector2(20, 258), Color.Black);
                spriteBatch.DrawString(InstructionFont, "loses.", new Vector2(20, 270), Color.Black);

                spriteBatch.DrawString(InstructionFont, "You can get a            or a", new Vector2(20, 312), Color.Black);
                spriteBatch.DrawString(InstructionFont, "charmander", new Vector2(190, 312), Color.Red);
                spriteBatch.DrawString(InstructionFont, "squirtle", new Vector2(20, 324), Color.Blue);
                spriteBatch.DrawString(InstructionFont, "to fight with you by", new Vector2(127, 324), Color.Black);
                spriteBatch.DrawString(InstructionFont, "touching the Pokemon egg, and", new Vector2(20, 336), Color.Black);
                spriteBatch.DrawString(InstructionFont, "they will take damage for you.", new Vector2(20, 348), Color.Black);
                spriteBatch.DrawString(InstructionFont, "Good Luck and have fun!", new Vector2(20, 390), Color.Black);
            }
        }
        private void Game_Draw()
        {
            spriteBatch.Draw(GameBackground, Vector2.Zero, Color.White);
            spriteBatch.Draw(EmptyLife, new Vector2(15, 220), Color.White);
            spriteBatch.Draw(EmptyLife, new Vector2(15, 260), Color.White);
            if (Player1LifeCount != 0)
            {
                for (int i = 0; i < Player1LifeCount; i++)
                    spriteBatch.Draw(Player1Life, new Vector2(15 + 34 * i, 260), Color.White);
            }
            if (Player2LifeCount != 0)
            {
                for (int i = 0; i < Player2LifeCount; i++)
                    spriteBatch.Draw(Player2Life, new Vector2(15 + 34 * i, 220), Color.White);
            }
            for (int i = 0; i < Player1AttackPosition.Count(); i++)
                spriteBatch.Draw(Player1Attack, Player1AttackRec[i], Color.White);
            for (int i = 0; i < Player2AttackPosition.Count(); i++)
                spriteBatch.Draw(Player2Attack, Player2AttackRec[i], Color.White);
            if (!Player1Invincible)
            {
                spriteBatch.Draw(Player1Sprite, Player1Rec, Color.White);
            }
            else if (Player1Invincible)
            {
                if ((Player1InvincibleCount > 15 && Player1InvincibleCount < 30) || (Player1InvincibleCount > 45 && Player1InvincibleCount < 60) || (Player1InvincibleCount > 75 && Player1InvincibleCount < 90))
                    spriteBatch.Draw(Player1Sprite, Player1Rec, Color.White);
            }
            if (Player1Mate)
                spriteBatch.Draw(Player1MateSprite, new Vector2(Player1Position.X + Player1Up[0].Width + 5, Player1Position.Y + 6), Color.White);
            if (Player2Mate)
                spriteBatch.Draw(Player2MateSprite, new Vector2(Player2Position.X - Player2MateDown[0].Width - 5, Player2Position.Y + 3), Color.White);
            if (!Player2Invincible)
            {
                spriteBatch.Draw(Player2Sprite, Player2Rec, Color.White);
            }
            else if (Player2Invincible)
            {
                if ((Player2InvincibleCount > 15 && Player2InvincibleCount < 30) || (Player2InvincibleCount > 45 && Player2InvincibleCount < 60) || (Player2InvincibleCount > 75 && Player2InvincibleCount < 90))
                    spriteBatch.Draw(Player2Sprite, Player2Rec, Color.White);
            }
            for (int i = 0; i < PokemonEggVec.Count(); i++)
            {
                spriteBatch.Draw(PokemonEgg, PokemonEggRec[i], Color.White);
            }
        }
        private void Game_Manu_Draw()
        {
            if (manu)
            {
                spriteBatch.Draw(TextBox, TextBox_menuRec, Color.White);
                if (TextBox_menuRec.Location.X != 80)
                {
                    TextBox_menuRec.X -= 3;
                    TextBox_menuRec.Y -= 2;
                    TextBox_menuRec.Width += 6;
                    TextBox_menuRec.Height += 4;
                }
                if (TextBox_menuRec.Location.X <= 80)
                {
                    spriteBatch.DrawString(font, "Resume", new Vector2(100, 195), Color.Black);
                    if (MusicState)
                        spriteBatch.DrawString(font, "Music : On", new Vector2(100, 225), Color.Black);
                    else
                        spriteBatch.DrawString(font, "Music : Off", new Vector2(100, 225), Color.Black);
                    spriteBatch.DrawString(font, "Reture to Title", new Vector2(100, 255), Color.Black);

                    spriteBatch.Draw(arrow, new Vector2(80, (165 + select_menu * 30)), Color.White);
                }
            }
        }
        private void EndGame_Draw()
        {
            spriteBatch.Draw(TextBox, TextBox_menuRec, Color.White);
            if (TextBox_menuRec.Location.X != 80)
            {
                TextBox_menuRec.X -= 3;
                TextBox_menuRec.Y -= 2;
                TextBox_menuRec.Width += 6;
                TextBox_menuRec.Height += 4;
            }
            if (TextBox_menuRec.Location.X <= 80)
            {
                string Winner;
                if (Player1Won)
                {
                    Winner = "Charizard";
                    spriteBatch.DrawString(font, Winner, new Vector2(200 - font.MeasureString(Winner).X / 2, 180), Color.Red);
                }
                if (Player2Won)
                {
                    Winner = "blastoise";
                    spriteBatch.DrawString(font, Winner, new Vector2(200 - font.MeasureString(Winner).X / 2, 180), Color.Blue);
                }
                spriteBatch.DrawString(font, "Won!!", new Vector2(200 - font.MeasureString("won!!").X / 2, 200), Color.Black);

                spriteBatch.DrawString(font, "Restar", new Vector2(100, 225), Color.Black);
                spriteBatch.DrawString(font, "Return to Title", new Vector2(100, 245), Color.Black);
                spriteBatch.DrawString(font, "Exit", new Vector2(100, 265), Color.Black);
                spriteBatch.Draw(arrow, new Vector2(80, (205 + select_menu * 20)), Color.White);
            }
        }
    }
}
