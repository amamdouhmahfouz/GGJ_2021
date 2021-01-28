using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyEngine;

namespace GGJ_2021
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ResolutionIndependentRenderer RIR;
        Camera2D Camera;

        ////////<Variables>/////
        Vector2 Resolution;
        SpriteFont spriteFont;
        ////////////////////////

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            RIR = new ResolutionIndependentRenderer(this);

            graphics.PreferMultiSampling = true;
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        private void ImportantIntialization()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            RIR.VirtualWidth = 1366;
            RIR.VirtualHeight = 768;
            graphics.ApplyChanges();
            /////////Camera And Resolution Independent Renderer/////// -> Mandatory
            Camera = new Camera2D(RIR);
            Camera.Zoom = 1f;
            Camera.Position = new Vector2(RIR.VirtualWidth / 2, RIR.VirtualHeight / 2);

            Setup.Initialize(graphics, Content, spriteBatch, RIR, Window, Camera, this);

            RIR.InitializeResolutionIndependence(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, Camera);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            ImportantIntialization();

            SceneManager.Start();

            SceneManager.AddInitializer(MainScene, 0);
            SceneManager.AddInitializer(MainMenu, 1);
            SceneManager.AddInitializer(PlayScene, 2);
            //////////////////////////////////////////////////////////
            //SceneManager.LoadScene(new Scene("MainScene", 0)); //Main Scene
            //SceneManager.LoadScene(new Scene("MainMenu", 1));
            SceneManager.LoadScene(new Scene("PlayScene", 2));
        }

        private void MainScene()
        {
            // TODO: use this.Content to load your game content here
            spriteFont = Content.Load<SpriteFont>("Font");

            SceneManager.ActiveScene.Start();

            //Initialization here
        }

        private void PlayScene()
        {
            // TODO: use this.Content to load your game content here
            spriteFont = Content.Load<SpriteFont>("Font");

            GameObject Monitor = new GameObject();
            Monitor.AddComponent<Transform>(new Transform());
            Monitor.AddComponent<SpriteRenderer>(new SpriteRenderer());
            Monitor.GetComponent<SpriteRenderer>().Sprite = new Sprite(Monitor.Transform);
            Monitor.GetComponent<SpriteRenderer>().Sprite.LoadTexture("Screen");
            //Monitor.GetComponent<SpriteRenderer>().Sprite.DynamicScaledRect();


            GameObject CommandTxt = new GameObject();
            CommandTxt.AddComponent<Transform>(new Transform());
            CommandTxt.AddComponent<WritableCommand>(new WritableCommand(spriteFont));
            //PlayTxt.AddComponent<SpriteRenderer>(new SpriteRenderer());
            CommandTxt.Tag = "CommandTxt";


            SceneManager.ActiveScene.AddGameObject(Monitor);
            SceneManager.ActiveScene.AddGameObject(CommandTxt);
            SceneManager.ActiveScene.Start();

            //Initialization here
            CommandTxt.GetComponent<WritableCommand>().Color = Color.DarkSeaGreen;
            CommandTxt.GetComponent<WritableCommand>().CustomOrigin = true;
            //CommandTxt.GetComponent<WritableCommand>().Origin = new Vector2(200, 100);
            CommandTxt.Transform.Position = new Vector2(900, 100);
            CommandTxt.Transform.Scale *= 0.5f;
            Monitor.Transform.Scale = new Vector2(0.75f, 0.75f);
            Monitor.Transform.Position = new Vector2(150,100);
        }


        private void MainMenu()
        {

            spriteFont = Content.Load<SpriteFont>("Font");
            // TODO: use this.Content to load your game content here
            GameObject Canvas = new GameObject();
            Canvas.AddComponent<Transform>(new Transform());
            Canvas.AddComponent<Canvas>(new Canvas(Camera));
            Canvas.Tag = "Canvas";

            GameObject MenuPanel = new GameObject();
            MenuPanel.AddComponent<Transform>(new Transform());
            MenuPanel.AddComponent<Panel>(new Panel("Main Menu"));
            MenuPanel.Tag = "Menu";
            
            
            Canvas.AddChild(MenuPanel);

            GameObject PlayBtn = new GameObject();
            PlayBtn.AddComponent<Transform>(new Transform());
            PlayBtn.AddComponent<Button>(new Button());

           

            GameObject PlayTxt = new GameObject();
            PlayTxt.AddComponent<Transform>(new Transform());
            PlayTxt.AddComponent<Text>(new Text("PlayText", spriteFont));
            //PlayTxt.AddComponent<SpriteRenderer>(new SpriteRenderer());
            PlayTxt.Tag = "PlayTxt";

            //MenuPanel.AddChild(PlayBtn);
            PlayBtn.AddChild(PlayTxt);
            
            


            SceneManager.ActiveScene.AddGameObject(Canvas);
            SceneManager.ActiveScene.AddGameObject(MenuPanel);
            SceneManager.ActiveScene.AddGameObject(PlayBtn);
            SceneManager.ActiveScene.AddGameObject(PlayTxt);


            SceneManager.ActiveScene.Start();

            //Initialization here
            MenuPanel.GetComponent<Panel>().Color = Color.White;
            MenuPanel.GetComponent<Panel>().Texture = Content.Load<Texture2D>("2k_earth_daymap");
            MenuPanel.GetComponent<Panel>().Origin = MenuPanel.GetComponent<Panel>().Texture.Bounds.Size.ToVector2() / 2;
            MenuPanel.GetComponent<Panel>().FillTheScreen();
            PlayBtn.GetComponent<Button>().IdleColor = Color.White;
            PlayBtn.GetComponent<Button>().HighlightColor = Color.DarkRed;
            PlayBtn.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight/2);
            PlayTxt.GetComponent<Text>().text = "Hello";
            PlayTxt.GetComponent<Text>().Color = Color.Green;
            PlayTxt.GetComponent<Transform>().LocalScale = new Vector2(0.5f);

            //MenuPanel.Layer = 0;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Input.GetState(); //This has to be called at the start of update method!!

            if (Input.GetKeyDown(Keys.Escape))
                Exit();

            /////////Resolution related//////////// -> Mandatory
            if (Resolution != new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight))
                RIR.InitializeResolutionIndependence(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, Camera);

            Resolution = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            ///////////////////////////////////////

            //passing a property as a refrence using delegates
            //Arrow.GetComponent<PropertiesAnimator>().GetKeyFrame("Rotate360").GetFeedback(value => Arrow.Transform.Rotation = value);


            //var keyboardState = Keyboard.GetState();
            //var keys = keyboardState.GetPressedKeys();
            //if (keys.Length > 0)
            //{
            //    var keyValue = keys[0].ToString();
            //    SceneManager.ActiveScene.FindGameObjectWithTag("PlayTxt").GetComponent<Text>().text += keyValue.ToString();
            //}


            SceneManager.ActiveScene.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            RIR.BeginDraw(); //Resolution related -> Mandatory
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Camera.GetViewTransformationMatrix()); // -> Mandatory

            SceneManager.ActiveScene.Draw(spriteBatch);
            //spriteBatch.DrawString(spriteFont, "Play", new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), Color.Red);
            //spriteBatch.DrawString(spriteFont, ((int)(1/this.TargetElapsedTime.TotalSeconds)).ToString(), Vector2.Zero, Color.Red); =>FPS

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


/*
 * class WritableCommand : public GameObjectComponent {
 * 
 *  public string command;
 *  
 *  
 * 
 * 
 * 
 * 
 * 
 * 
 * }
 */