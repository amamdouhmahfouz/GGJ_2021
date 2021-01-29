using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyEngine;

namespace GGJ_2021
{
    public static class Commands
    {
        public static void DrawRectangle(Rectangle Destination, Color color)
        {
            GameObject Rectangle = new GameObject();
            Rectangle.Tag = "Command";
            Rectangle.AddComponent<Transform>(new Transform());
            Rectangle.AddComponent<SpriteRenderer>(new SpriteRenderer());
            Rectangle.GetComponent<SpriteRenderer>().Sprite = new Sprite(Rectangle.Transform);
            Rectangle.GetComponent<SpriteRenderer>().Sprite.Texture = HitBoxDebuger.RectTexture(color);

            Rectangle.Start();
            Rectangle.GetComponent<SpriteRenderer>().Sprite.Transform = Rectangle.Transform;

            Rectangle.Transform.Position = Destination.Location.ToVector2();
            Rectangle.Transform.Scale = Destination.Size.ToVector2();

            SceneManager.ActiveScene.AddGameObject(Rectangle);
        }

        public static void DrawCircle(Vector2 Position, int Radius, Color color)
        {
            GameObject Circle = new GameObject();
            Circle.Tag = "Command";
            Circle.AddComponent<Transform>(new Transform());
            Circle.AddComponent<SpriteRenderer>(new SpriteRenderer());
            Circle.GetComponent<SpriteRenderer>().Sprite = new Sprite(Circle.Transform);
            Circle.GetComponent<SpriteRenderer>().Sprite.Texture = HitBoxDebuger.CreateCircleTexture(Radius, color);

            Circle.Start();
            Circle.GetComponent<SpriteRenderer>().Sprite.Transform = Circle.Transform;

            Circle.Transform.Position = Position;

            SceneManager.ActiveScene.AddGameObject(Circle);
        }

        public static bool DrawATexture(string Path, Rectangle Destination)
        {
            GameObject texture = new GameObject();
            texture.AddComponent<Transform>(new Transform());
            texture.AddComponent<SpriteRenderer>(new SpriteRenderer());
            texture.GetComponent<SpriteRenderer>().Sprite = new Sprite(texture.Transform);
            try
            {
                texture.GetComponent<SpriteRenderer>().Sprite.Texture = Setup.Content.Load<Texture2D>(Path);
            }
            catch
            {
                return false;
            }

            texture.Start();

            texture.Transform.Position = Destination.Location.ToVector2();
            texture.Transform.Scale = Destination.Size.ToVector2();

            SceneManager.ActiveScene.AddGameObject(texture);

            return true;
        }


    }
}
