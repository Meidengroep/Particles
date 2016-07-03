using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class Tester
    {
        Player player;

        public void Initialize(TheNextGeneration game)
        {
            Vector2 iets = Utility.RotateVector_Radians(new Vector2(0, 1), 0);
        }

        public void LoadContent(TheNextGeneration game)
        {
            player = new Player(Utility.Load<Texture2D>("Player/body"), new Vector2(200, 200), 1);
            game.PhysicsSystem.AddPlayer(player);
        }

        public void Update(float dt)
        {
            player.Update(dt, Global.Game.PhysicsSystem.ForceGrid);
        }

        public void Draw(SpriteBatch sb)
        {
            player.Draw(sb);
        }
    }
}
