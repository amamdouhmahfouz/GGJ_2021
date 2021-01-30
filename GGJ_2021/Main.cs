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
            Window.AllowUserResizing = false;
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
            SceneManager.AddInitializer(Credits, 2);
            //////////////////////////////////////////////////////////
            SceneManager.LoadScene(new Scene("MainScene", 0)); //Credits
        }

        private void MainScene()
        {
            // TODO: use this.Content to load your game content here
            spriteFont = Content.Load<SpriteFont>("Font");

            GameObject Screen = new GameObject();
            Screen.AddComponent<Transform>(new Transform());
            Screen.AddComponent<SpriteRenderer>(new SpriteRenderer());
            Screen.GetComponent<SpriteRenderer>().Sprite = new Sprite(Screen.Transform);
            Screen.GetComponent<SpriteRenderer>().Sprite.LoadTexture("Screen");

            GameObject Grid = new GameObject();
            Grid.AddComponent<DrawGrid>(new DrawGrid(new Vector2(graphics.PreferredBackBufferWidth * 0.43f, graphics.PreferredBackBufferHeight * 0.15f), 15, 15, 50));
            Grid.AddComponent<OutlineGrid>(new OutlineGrid());
            Grid.Tag = "Grid";

            GameObject EntryScene = new GameObject();
            EntryScene.AddComponent<Transform>(new Transform());
            EntryScene.AddComponent<SpriteRenderer>(new SpriteRenderer());
            EntryScene.GetComponent<SpriteRenderer>().Sprite = new Sprite(EntryScene.Transform);
            EntryScene.GetComponent<SpriteRenderer>().Sprite.LoadTexture("EntryScene");

            GameObject EntryText = new GameObject();
            EntryText.AddComponent<Transform>(new Transform());
            EntryText.AddComponent<Text>(new Text("EntryText", spriteFont));

            GameObject Manuscript1 = new GameObject();
            Manuscript1.AddComponent<Transform>(new Transform());
            Manuscript1.AddComponent<SpriteRenderer>(new SpriteRenderer());
            Manuscript1.GetComponent<SpriteRenderer>().Sprite = new Sprite(Manuscript1.Transform);
            Manuscript1.GetComponent<SpriteRenderer>().Sprite.LoadTexture("Manuscript");

            GameObject Transition = new GameObject();
            Transition.AddComponent<Transform>(new Transform());
            Transition.AddComponent<Panel>(new Panel("Transition"));
            Transition.AddComponent<PropertiesAnimator>(new PropertiesAnimator());
            Transition.AddComponent<TransitionClass>(new TransitionClass());

            GameObject HatemScene1 = new GameObject();
            HatemScene1.AddComponent<Transform>(new Transform());
            HatemScene1.AddComponent<SpriteRenderer>(new SpriteRenderer());
            HatemScene1.GetComponent<SpriteRenderer>().Sprite = new Sprite(HatemScene1.Transform);
            HatemScene1.GetComponent<SpriteRenderer>().Sprite.LoadTexture("Hatem_Interactions");
            HatemScene1.AddComponent<AudioSource>(new AudioSource("VL1"));
            HatemScene1.AddComponent<Animator>(new Animator());

            GameObject STORY = new GameObject();
            STORY.Name = "STORY";
            STORY.AddComponent<Story>(new Story());

            GameObject Finisher = new GameObject();
            Finisher.Name = "Finisher";
            Finisher.AddComponent<Transform>(new Transform());
            Finisher.AddComponent<Text>(new Text("Finisher", spriteFont));

            //GameObject Scene1 = new GameObject();
            //Scene1.AddComponent<>

            //SceneManager.ActiveScene.AddGameObject(Screen);
            //SceneManager.ActiveScene.AddGameObject(Grid);

            SceneManager.ActiveScene.AddGameObject(EntryScene);
            SceneManager.ActiveScene.AddGameObject(EntryText);
            SceneManager.ActiveScene.AddGameObject(Manuscript1);
            SceneManager.ActiveScene.AddGameObject(Transition);
            SceneManager.ActiveScene.AddGameObject(HatemScene1);
            SceneManager.ActiveScene.AddGameObject(STORY);
            SceneManager.ActiveScene.AddGameObject(Finisher);

            MediaSource.IsLooping = true;
            MediaSource.LoadTrack("Dreams");
            MediaSource.Volume = 0.25f;
            MediaSource.Play();

            SceneManager.ActiveScene.Start();

            //Initialization here
            //Screen.Transform.Scale = 0.95f * Vector2.One;
            //Screen.Layer = 1;

            Errors.WindowSpam();
            Grid.GetComponent<DrawGrid>().Width = 100;
            Grid.GetComponent<DrawGrid>().Height = 100;

            EntryScene.Layer = 0;
            EntryScene.Transform.Scale = 1.5f * Vector2.One;
            EntryScene.GetComponent<SpriteRenderer>().Sprite.Transform = EntryScene.Transform;

            EntryText.GetComponent<Text>().Color = Color.Black;
            EntryText.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth * 0.78f, graphics.PreferredBackBufferHeight * 0.4f);
            EntryText.GetComponent<Text>().text = "A typical day\nin the life of Ahmed\nHatem who works\nin A company named\n'Our Engine Socks'\nwhere he is making\na game using\nthe engine...";

            Manuscript1.GetComponent<SpriteRenderer>().Sprite.Transform = Manuscript1.Transform;
            Manuscript1.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth * 0.57f, 0);
            Manuscript1.Layer = 0.4f;
            Manuscript1.Name = "Manuscript1";

            Transition.Layer = 0;
            Transition.Name = "Transition";
            Transition.GetComponent<Panel>().FillTheScreen();

            KeyFrame keyFrame = new KeyFrame(1, 0, 2.5f, "FadeIn");
            Transition.GetComponent<PropertiesAnimator>().AddKeyFrame(keyFrame, true);

            HatemScene1.Name = "HatemScene1";
            HatemScene1.GetComponent<SpriteRenderer>().Sprite.SourceRectangle = new Rectangle(0, 0, HatemScene1.GetComponent<SpriteRenderer>().Sprite.Texture.Width / 4, HatemScene1.GetComponent<SpriteRenderer>().Sprite.Texture.Height);
            HatemScene1.Layer = 0;
            HatemScene1.Transform.Scale = 1.5f * Vector2.One;
            HatemScene1.GetComponent<Animator>().AnimationClips.Add(new Animation(HatemScene1.GetComponent<SpriteRenderer>().Sprite, 2));
            HatemScene1.GetComponent<Animator>().GetActiveClip().PlayOnAwake = false;
            HatemScene1.Active = false;

            Threader.Invoke(EnableHatem1, 6000);

            void EnableHatem1()
            {
                HatemScene1.Active = true;
                HatemScene1.GetComponent<AudioSource>().Play();

                Threader.Invoke(EnableGame, (uint)(HatemScene1.GetComponent<AudioSource>().ClipLength() + 1) * 1000);
            }

            void EnableGame()
            {
                EntryScene.Active = false;
                Manuscript1.Active = false;
                HatemScene1.Active = false;
                SceneManager.ActiveScene.FindGameObjectWithName("CommandTxt").Active = true;
                SceneManager.ActiveScene.FindGameObjectWithName("F1_help").Active = true;
            }


            STORY.Active = false;

            Finisher.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth * 0.5f, graphics.PreferredBackBufferHeight * 0.4f);
            Finisher.GetComponent<Text>().Color = Color.White;
            Finisher.GetComponent<Text>().text = "Ahmed might have felt that he lost all his time and effort\nworking on this game,"
+ " but eventually, he found appreciation\nand inspiration in the eyes of others...\n\n'You have to get lost before you can be found'\n                                                             'Jeff Rasley'\n\n                                        THE END";
            Finisher.Active = false;
        }

        private void Credits()
        {
            // TODO: use this.Content to load your game content here
            spriteFont = Content.Load<SpriteFont>("Font");

            GameObject canvas = new GameObject();
            canvas.Name = "Canvas";
            canvas.AddComponent<Transform>(new Transform());
            canvas.AddComponent<Canvas>(new Canvas(Camera));

            GameObject panel = new GameObject();
            panel.Name = "Panel";
            panel.AddComponent<Transform>(new Transform());
            panel.AddComponent<Panel>(new Panel("Panel"));

            GameObject Credits_Text = new GameObject();
            Credits_Text.Name = "Credits_Text";
            Credits_Text.AddComponent<Transform>(new Transform());
            Credits_Text.AddComponent<Text>(new Text("Credits_Text", spriteFont));

            GameObject Oss_Photo = new GameObject();
            Oss_Photo.AddComponent<Transform>(new Transform());
            Oss_Photo.AddComponent<SpriteRenderer>(new SpriteRenderer());
            Oss_Photo.GetComponent<SpriteRenderer>().Sprite = new Sprite(Oss_Photo.Transform);
            Oss_Photo.GetComponent<SpriteRenderer>().Sprite.LoadTexture("Oss");

            SceneManager.ActiveScene.AddGameObject(canvas);
            SceneManager.ActiveScene.AddGameObject(panel);
            SceneManager.ActiveScene.AddGameObject(Credits_Text);
            SceneManager.ActiveScene.AddGameObject(Oss_Photo);

            SceneManager.ActiveScene.Start();

            //Initialization here
            canvas.AddChild(panel);
            panel.AddChild(Credits_Text);
            panel.AddChild(Oss_Photo);

            panel.GetComponent<Panel>().Color = Color.Black;
            panel.GetComponent<Panel>().FillTheScreen();

            Credits_Text.GetComponent<Text>().text = "Credits";
            Credits_Text.GetComponent<Text>().Color = Color.White;
            Credits_Text.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth / 1.9f, graphics.PreferredBackBufferHeight * 0.1f);

            GameObject Engine_Text = GameObject.Instantiate(Credits_Text);
            panel.AddChild(Engine_Text);
            Engine_Text.GetComponent<Text>().text = "Engine Programming\n            & Art";
            Engine_Text.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth / 3.2f, graphics.PreferredBackBufferHeight * 0.3f);

            GameObject Oss = GameObject.Instantiate(Engine_Text);
            panel.AddChild(Oss);
            Oss.GetComponent<Text>().text = "Osama Yahya";
            Oss.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth / 3.2f, graphics.PreferredBackBufferHeight * 0.5f);

            LayerUI.AddLayer("Images", 1);
            Oss_Photo.Transform.Position = Oss.Transform.Position + new Vector2(-graphics.PreferredBackBufferWidth * 0.05f, graphics.PreferredBackBufferHeight * 0.1f);
            Oss_Photo.Layer = LayerUI.GetLayer("Images");

            GameObject Programming = GameObject.Instantiate(Engine_Text);
            Programming.Transform.Position = Engine_Text.Transform.Position + new Vector2(graphics.PreferredBackBufferWidth * 0.4f, -graphics.PreferredBackBufferHeight * 0.03f);
            Programming.GetComponent<Text>().text = "Co-Programmer";

            GameObject Mamdouh = GameObject.Instantiate(Oss);
            Mamdouh.Transform.Position += Vector2.UnitX * graphics.PreferredBackBufferWidth * 0.4f;
            Mamdouh.GetComponent<Text>().text = "Abdelrahman Mamdouh";

            GameObject Mamdouh_Photo = GameObject.Instantiate(Oss_Photo);
            Mamdouh_Photo.GetComponent<SpriteRenderer>().Sprite.LoadTexture("Mamdouh");
            Mamdouh_Photo.Transform.Position += Vector2.UnitX * graphics.PreferredBackBufferWidth * 0.42f;
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

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
                Camera.Zoom += (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (Keyboard.GetState().IsKeyDown(Keys.X))
                Camera.Zoom -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Input.GetKeyUp(Keys.R))
                SceneManager.ActiveScene.FindGameObjectWithTag("Grid").GetComponent<DrawGrid>().Enabled = false;

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

            //spriteBatch.DrawString(spriteFont, ((int)(1/this.TargetElapsedTime.TotalSeconds)).ToString(), Vector2.Zero, Color.Red); =>FPS

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}