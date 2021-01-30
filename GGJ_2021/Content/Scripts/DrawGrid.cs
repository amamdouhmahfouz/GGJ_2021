using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyEngine;

namespace GGJ_2021
{
    public class DrawGrid: GameObjectComponent
    {
        public Vector2 InitialPosition;
        public int Width = 10;
        public int Height = 10;
        public int Spacing = 100;

        private Rectangle[] Rects;
        private Rectangle[] Rects2;

        public DrawGrid(Vector2 InitPos, int Width, int Height, int Spacing)
        {
            InitialPosition = InitPos;
            this.Width = Width;
            this.Height = Height;
            this.Spacing = Spacing;
        }

        public override void Start()
        {
            Rects = new Rectangle[Height];
            Rects2 = new Rectangle[Width];

            for (int i = 0; i < Height; i++)
                Rects[i] = new Rectangle((int)InitialPosition.X, (int)InitialPosition.Y + i*Spacing, 1, Spacing * 14);

            for (int i = 0; i < Width; i++)
                Rects2[i] = new Rectangle((int)InitialPosition.X + i * Spacing, (int)InitialPosition.Y, 1, 12 * Spacing);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //for(int i=0; i<Width;i++)
            //HitBoxDebuger.DrawLine(R, Color.White, 0, ;
            Color color = Color.White * 0.4f;

            for (int i = 0; i < Height; i++)
                HitBoxDebuger.DrawLine(Rects[i], color, -90, gameObject.Layer, Vector2.Zero);

            for (int i = 0; i < Width; i++)
                HitBoxDebuger.DrawLine(Rects2[i], color, 0, gameObject.Layer, Vector2.Zero);
        }
    }
}
