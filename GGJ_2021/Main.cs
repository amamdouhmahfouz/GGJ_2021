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
            SceneManager.AddInitializer(MainMenu, 1);
            SceneManager.AddInitializer(Credits, 2);
            //SceneManager.AddInitializer(Intro, 3);
            //////////////////////////////////////////////////////////
            //SceneManager.LoadScene(new Scene("MainScene", 0)); //Credits
            SceneManager.LoadScene(new Scene("MainMenu", 1));
            //SceneManager.LoadScene(new Scene("Intro", 3));
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
            Screen.Name = "Screen";

            //GameObject Grid = new GameObject();
            //Grid.AddComponent<DrawGrid>(new DrawGrid(new Vector2(graphics.PreferredBackBufferWidth * 0.43f, graphics.PreferredBackBufferHeight * 0.15f), 15, 15, 50));
            //Grid.AddComponent<OutlineGrid>(new OutlineGrid());

            GameObject CommandTxt = new GameObject();
            CommandTxt.AddComponent<Transform>(new Transform());
            CommandTxt.AddComponent<WritableCommand>(new WritableCommand(spriteFont));
            CommandTxt.AddComponent<DrawGrid>(new DrawGrid(new Vector2(graphics.PreferredBackBufferWidth * 0.43f, graphics.PreferredBackBufferHeight * 0.15f), 15, 15, 50));
            CommandTxt.AddComponent<OutlineGrid>(new OutlineGrid());
            CommandTxt.AddComponent<ClosePopup>(new ClosePopup());
            CommandTxt.Name = "CommandTxt";


            GameObject BlueDeathScreen = new GameObject();
            BlueDeathScreen.AddComponent<Transform>(new Transform());
            BlueDeathScreen.AddComponent<SpriteRenderer>(new SpriteRenderer());
            BlueDeathScreen.GetComponent<SpriteRenderer>().Sprite = new Sprite(BlueDeathScreen.Transform);
            BlueDeathScreen.GetComponent<SpriteRenderer>().Sprite.LoadTexture("Restart_Screen");
            BlueDeathScreen.Tag = "BlueDeathScreen";

            GameObject DocumentationBook = new GameObject();
            DocumentationBook.Name = "DocumentationBook";
            DocumentationBook.AddComponent<Transform>(new Transform());
            DocumentationBook.AddComponent<SpriteRenderer>(new SpriteRenderer());
            DocumentationBook.GetComponent<SpriteRenderer>().Sprite = new Sprite(DocumentationBook.Transform);
            DocumentationBook.GetComponent<SpriteRenderer>().Sprite.LoadTexture("Book");

            GameObject DocumentationTxt = new GameObject();
            DocumentationTxt.Name = "DocumentationTxt";
            DocumentationTxt.AddComponent<Transform>(new Transform());
            DocumentationTxt.AddComponent<Text>(new Text("DocumentationTxt", spriteFont));

            GameObject DocumentationTxtRight = new GameObject();
            DocumentationTxtRight.Name = "DocumentationTxtRight";
            DocumentationTxtRight.AddComponent<Transform>(new Transform());
            DocumentationTxtRight.AddComponent<Text>(new Text("DocumentationTxtRight", spriteFont));

            GameObject DocumentationTxtTitle = new GameObject();
            DocumentationTxtTitle.Name = "DocumentationTxtTitle";
            DocumentationTxtTitle.AddComponent<Transform>(new Transform());
            DocumentationTxtTitle.AddComponent<Text>(new Text("DocumentationTxtTitle", spriteFont));

            GameObject F1_help = new GameObject();
            F1_help.Name = "F1_help";
            F1_help.AddComponent<Transform>(new Transform());
            F1_help.AddComponent<Text>(new Text("F1_help", spriteFont));


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

            SceneManager.ActiveScene.AddGameObject(Screen);
            //SceneManager.ActiveScene.AddGameObject(Grid);
            SceneManager.ActiveScene.AddGameObject(CommandTxt);
            SceneManager.ActiveScene.AddGameObject(BlueDeathScreen);
            SceneManager.ActiveScene.AddGameObject(DocumentationBook);
            SceneManager.ActiveScene.AddGameObject(DocumentationTxt);
            SceneManager.ActiveScene.AddGameObject(DocumentationTxtTitle);
            SceneManager.ActiveScene.AddGameObject(DocumentationTxtRight);
            SceneManager.ActiveScene.AddGameObject(F1_help);

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
            DocumentationBook.AddChild(DocumentationTxt);
            DocumentationBook.AddChild(DocumentationTxtTitle);
            DocumentationBook.AddChild(DocumentationTxtRight);

            Screen.Transform.Scale = 0.95f * Vector2.One;
            Screen.Layer = 1;

            CommandTxt.GetComponent<WritableCommand>().Color = Color.DarkSeaGreen;
            CommandTxt.GetComponent<WritableCommand>().CustomOrigin = true;
            //CommandTxt.Transform.Position = new Vector2(80, 110);
            CommandTxt.Transform.Position = new Vector2(80, 120);
            CommandTxt.Transform.Scale *= 0.5f;
            CommandTxt.GetComponent<DrawGrid>().Enabled = false;
            CommandTxt.GetComponent<OutlineGrid>().Enabled = false;
            CommandTxt.GetComponent<ClosePopup>().Enabled = false;
            CommandTxt.Layer = 0.5f;
            

            BlueDeathScreen.Layer = 0;
            BlueDeathScreen.Transform.Scale = 0.95f * Vector2.One;
            BlueDeathScreen.Active = false;


            
            //Documentation.GetComponent<Text>().Color = Color.White;
            //DocumentationTxt.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth / 2f, graphics.PreferredBackBufferHeight / 2f);
            //Documentation.Transform.Position = new Vector2(0f, 0f);
            DocumentationBook.Transform.Scale = 1.65f * Vector2.One;
            DocumentationBook.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth / 6.5f, -30f);

            DocumentationTxt.GetComponent<Text>().text = "          -----------------------------\n" +
                "Note: Commands must end\n" +
                "with semicolon\n\n" + 
                "$ COLOR: sets drawing color\n" +
                "0: Orange, 1: White, 2: Green\n"+
                "3: Blue, 4: Red, 5: Yellow\n" +
                "6: Cyan, 7: Aquamarine\n" +
                "USAGE example: COLOR=0;\n\n" +
                "$ DRAWTILE: draws a tile on grid\n" +
                "USAGE example: DRAWTILE();\n\n" +
                "$ DRAWCIRCLE: draws a circle\n" +
                "on grid\n" +
                "USAGE example: DRAWCIRCLE();\n\n" +
                "$ PUBLISH: publish the game after\n"+
                "after finishing\n" +
                "USAGE example: PUBLISH();\n\n" +
                "PS: Left Click to place shape\n" + 
                "    Right Click to erase shape\n";
            DocumentationTxt.GetComponent<Text>().Color = Color.Black;
            DocumentationTxt.GetComponent<Transform>().LocalPosition = new Vector2(170f, 258f);
            DocumentationTxt.GetComponent<Transform>().LocalScale = new Vector2(0.25f, 0.25f);

            DocumentationTxtTitle.GetComponent<Text>().text = "   Documentation\n ";
            DocumentationTxtTitle.GetComponent<Text>().Color = Color.Black;
            DocumentationTxtTitle.GetComponent<Transform>().LocalPosition = new Vector2(200f, 150f);
            DocumentationTxtTitle.GetComponent<Transform>().LocalScale = new Vector2(0.35f, 0.35f);

            DocumentationTxtRight.GetComponent<Text>().text = "~~~~~~~~~~~~~~~~~~~~~~~\n" +
                "~~~~~~~~~~~~~~~~~~~~~~~\n" +
                "~~~~~~~~~~~~~~~~~~~~~~~\n " +
                "~~~~~~~~~~~~~~~~~~~~~~~\n " +
                "~~~~~~~~~~~~~~~~~~~~~~~\n " +
                "~~~~~~~~~~~~~~~~~~~~~~~\n " +
                "~~~~~~~~~~~~~~~~~~~~~~~\n " +
                "~~~~~~~~~~~~~~~~~~~~~~~\n" +
                "~~~~~~~~~~~~~~~~~~~~~~~\n " +
                "~~~~~~~~~~~~~~~~~~~~~~~\n " +
                "~~~~~~~~~~~~~~~~~~~~~~~\n " +
                "~~~~~~~~~~~~~~~~~~~~~~~\n " ;
            DocumentationTxtRight.GetComponent<Text>().Color = Color.Black;
            DocumentationTxtRight.GetComponent<Transform>().LocalPosition = new Vector2(555f, 210f);
            DocumentationTxtRight.GetComponent<Transform>().LocalScale = new Vector2(0.25f, 0.25f);

            DocumentationBook.Active = false;
            DocumentationBook.Layer = 0.001f;
            DocumentationTxtTitle.Layer = 0.0005f;
            DocumentationTxt.Layer = 0.0005f;
            DocumentationTxtRight.Layer = 0.0005f;

            F1_help.Layer = 0.004f;
            F1_help.GetComponent<Text>().Color = Color.Beige;
            F1_help.GetComponent<Text>().text = "                 Press F1 for documentation\n";
            F1_help.GetComponent<Transform>().Position = new Vector2(150f, 103f);
            F1_help.GetComponent<Transform>().Scale *= 0.45f;

            EntryScene.Layer = 0;
            EntryScene.Transform.Scale = 1.5f * Vector2.One;
            EntryScene.GetComponent<SpriteRenderer>().Sprite.Transform = EntryScene.Transform;

            EntryText.GetComponent<Text>().Color = Color.Black;
            EntryText.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth * 0.78f, graphics.PreferredBackBufferHeight * 0.4f);
            EntryText.GetComponent<Text>().text = "A typical day\nin the life of Ahmed\nHatem who works\nin A company named\n'Our Engine Socks'\nwhere he is making\na game using\nthe engine...";
            EntryText.Layer = 0.0003f;

            Manuscript1.GetComponent<SpriteRenderer>().Sprite.Transform = Manuscript1.Transform;
            Manuscript1.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth * 0.57f, 0);
            Manuscript1.Layer = 0.0004f;
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

            Animation animation = new Animation(HatemScene1.GetComponent<SpriteRenderer>().Sprite,2);
            animation.Tag = "PissedOff";
            animation.Speed = 0.5f;
            animation.PlayOnAwake = false;
            animation.IsLooping = true;

            HatemScene1.GetComponent<Animator>().AnimationClips.Add(animation);
            HatemScene1.GetComponent<Animator>().Enabled = false;
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
                SceneManager.ActiveScene.FindGameObjectWithName("Screen").Active = true;
            }


            Manuscript1.AddChild(EntryText);


            STORY.Active = false;

            Finisher.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth * 0.5f, graphics.PreferredBackBufferHeight * 0.4f);
            Finisher.GetComponent<Text>().Color = Color.White;
            Finisher.GetComponent<Text>().text = "Ahmed might have felt that he lost all his time and effort\nworking on this game,"
+ " but eventually, he found appreciation\nand inspiration in the eyes of others...\n\n'You have to get lost before you can be found'\n                                                             'Jeff Rasley'\n\n                                        THE END";
            Finisher.Active = false;

            Screen.Active = false;
            CommandTxt.Active = false;
            F1_help.Active = false;

            if (DocumentationTxtRight.Parent == DocumentationBook)
                System.Console.WriteLine("7ader");
            F1_help.AddChild(DocumentationBook);

            //SceneManager.ActiveScene.SortGameObjectsWithLayer();

            //Errors.WindowSpam();
            //Grid.GetComponent<DrawGrid>().Width = 100;
            //Grid.GetComponent<DrawGrid>().Height = 100;
        }



        private void Credits()
        {
            // TODO: use this.Content to load your game content here
            spriteFont = Content.Load<SpriteFont>("Font");

            GameObject canvas = new GameObject();
            canvas.Name = "Canvas";
            canvas.AddComponent<Transform>(new Transform());
            canvas.AddComponent<Canvas>(new Canvas(Camera));
            canvas.AddComponent<Credits>(new Credits());

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

        private void MainMenu()
        {
            // TODO: use this.Content to load your game content here
            spriteFont = Content.Load<SpriteFont>("Font");

            GameObject canvas = new GameObject();
            canvas.Name = "Canvas";
            canvas.AddComponent<Transform>(new Transform());
            canvas.AddComponent<Canvas>(new Canvas(Camera));
            canvas.AddComponent<ChangeState>(new ChangeState());

            GameObject panel = new GameObject();
            panel.Name = "Panel";
            panel.AddComponent<Transform>(new Transform());
            panel.AddComponent<Panel>(new Panel("Panel"));

            GameObject Play = new GameObject();
            Play.Name = "Play";
            Play.AddComponent<Transform>(new Transform());
            Play.AddComponent<Text>(new Text("Play", spriteFont));
            Play.AddComponent<Button>(new Button());

            GameObject Credits_Text = new GameObject();
            Credits_Text.Name = "Credits_Text";
            Credits_Text.AddComponent<Transform>(new Transform());
            Credits_Text.AddComponent<Text>(new Text("Credits_Text", spriteFont));
            Credits_Text.AddComponent<Button>(new Button());

            GameObject ExitTxt = new GameObject();
            ExitTxt.Name = "ExitTxt";
            ExitTxt.AddComponent<Transform>(new Transform());
            ExitTxt.AddComponent<Text>(new Text("ExitTxt", spriteFont));
            ExitTxt.AddComponent<Button>(new Button());

            GameObject Oss_Photo = new GameObject();
            Oss_Photo.AddComponent<Transform>(new Transform());
            Oss_Photo.AddComponent<SpriteRenderer>(new SpriteRenderer());
            Oss_Photo.GetComponent<SpriteRenderer>().Sprite = new Sprite(Oss_Photo.Transform);
            Oss_Photo.GetComponent<SpriteRenderer>().Sprite.LoadTexture("Oss");


            SceneManager.ActiveScene.AddGameObject(canvas);
            SceneManager.ActiveScene.AddGameObject(panel);
            SceneManager.ActiveScene.AddGameObject(Credits_Text);
            SceneManager.ActiveScene.AddGameObject(Play);
            SceneManager.ActiveScene.AddGameObject(Oss_Photo);
            SceneManager.ActiveScene.AddGameObject(ExitTxt);

            SceneManager.ActiveScene.Start();

            //Initialization here
            canvas.AddChild(panel);
            panel.AddChild(Credits_Text);
            panel.AddChild(Oss_Photo);
            panel.AddChild(Play);
            panel.AddChild(ExitTxt);

            panel.GetComponent<Panel>().Color = Color.Black;
            panel.GetComponent<Panel>().FillTheScreen();

            Play.GetComponent<Text>().text = "Play";
            Play.GetComponent<Text>().Color = Color.White;
            Play.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth / 2.0f, graphics.PreferredBackBufferHeight * 0.25f);
            Play.GetComponent<Button>().Size = new Point(160,60);
            Play.GetComponent<Button>().IdleColor = Color.Red * 0.0f;
            Play.GetComponent<Button>().HighlightColor = Color.AliceBlue * 0.2f;

            Credits_Text.GetComponent<Text>().text = "Credits";
            Credits_Text.GetComponent<Text>().Color = Color.White;
            Credits_Text.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth / 2.0f, graphics.PreferredBackBufferHeight * 0.45f);
            Credits_Text.GetComponent<Button>().Size = new Point(160, 60);
            Credits_Text.GetComponent<Button>().IdleColor = Color.Red * 0.0f;
            Credits_Text.GetComponent<Button>().HighlightColor = Color.AliceBlue * 0.2f;


            ExitTxt.GetComponent<Text>().text = "Exit";
            ExitTxt.GetComponent<Text>().Color = Color.White;
            ExitTxt.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth / 2.0f, graphics.PreferredBackBufferHeight * 0.65f);
            ExitTxt.GetComponent<Button>().Size = new Point(160, 60);
            ExitTxt.GetComponent<Button>().IdleColor = Color.Red * 0.0f;
            ExitTxt.GetComponent<Button>().HighlightColor = Color.AliceBlue * 0.2f;

            //GameObject Engine_Text = GameObject.Instantiate(Credits_Text);
            //panel.AddChild(Engine_Text);
            //Engine_Text.GetComponent<Text>().text = "Engine Programming\n            & Art";
            //Engine_Text.Transform.Position = new Vector2(graphics.PreferredBackBufferWidth / 3.2f, graphics.PreferredBackBufferHeight * 0.3f);


        }


        private void Intro()
        {
            /////////////////////////////////////////////////
            /// Only called once at the start
            /////////////////////////////////////////////////

            spriteFont = Content.Load<SpriteFont>("Font");

            GameObject canvas = new GameObject();
            canvas.Name = "Canvas";
            canvas.AddComponent<Transform>(new Transform());
            canvas.AddComponent<Canvas>(new Canvas(Camera));

            GameObject panel = new GameObject();
            panel.Name = "Panel";
            panel.AddComponent<Transform>(new Transform());
            panel.AddComponent<Panel>(new Panel("Panel"));

            GameObject IntroTxt = new GameObject();
            IntroTxt.Name = "IntroTxt";
            IntroTxt.AddComponent<Transform>(new Transform());
            IntroTxt.AddComponent<Text>(new Text("IntroTxt", spriteFont));

            SceneManager.ActiveScene.AddGameObject(canvas);
            SceneManager.ActiveScene.AddGameObject(panel);
            SceneManager.ActiveScene.AddGameObject(IntroTxt);

            SceneManager.ActiveScene.Start();

            //Initialization here
            canvas.AddChild(panel);
            panel.AddChild(IntroTxt);

            IntroTxt.GetComponent<Text>().text = "From Newbies"; //© ®
            IntroTxt.GetComponent<Text>().Color = Color.AntiqueWhite;
            IntroTxt.Transform.Position = new Vector2(700, 300);

            panel.Transform.Scale *= 2f;

            //IntroTxt.Layer = 0.4f;
            //panel.Layer = 0f;
            //panel.GetComponent<Panel>().Color = Color.*100f;

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

            //if (Input.GetKeyDown(Keys.Escape))
            //    Exit();

            /////////Resolution related//////////// -> Mandatory
            if (Resolution != new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight))
                RIR.InitializeResolutionIndependence(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, Camera);

            Resolution = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            ///////////////////////////////////////


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