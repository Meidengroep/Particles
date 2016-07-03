using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Particles_The_Next_Generation
{
    public partial class TheNextGeneration : Game
    {

        protected IParticleSystem SystemFactory(string type)
        {
            switch (type)
            {
                case "simple":
                    return CreateSimpleSystem();
                case "gravity":
                    return CreateGravitySystem();
            }
            return CreateSimpleSystem();
        }

        protected IPhysicsParticleSystem PSystemFactory(string type)
        {
            switch (type)
            {
                case "physics":
                    return CreatePhysicsSystem();
            }
            return CreatePhysicsSystem();
        }

        protected Simple_System CreateSimpleSystem()
        {
            List<Texture2D> sprites = new List<Texture2D>();
            Texture2D simple = Content.Load<Texture2D>("simple");
            sprites.Add(simple);

            Vector2 startPosition = new Vector2(200, 200);
            Vector2 initialVelocity = new Vector2(0.3f, 0.3f);
            Vector2 velocityDeviation = new Vector2(0.1f, 0.1f);

            float initialTTL = 1000;
            float ttlDeviation = 300;

            byte initialOpacity = 255;
            byte opacityDeviation = 0;

            float initialScale = 1;
            float scaleDeviation = 0.8f;

            float initialRotation = 0;
            float rotationDeviation = 0;


            return new Simple_System(sprites, startPosition,
                initialVelocity, initialTTL, initialOpacity, initialScale, initialRotation,
                velocityDeviation, ttlDeviation, opacityDeviation, scaleDeviation, rotationDeviation);
        }

        protected Gravity_System CreateGravitySystem()
        {
            List<Texture2D> sprites = new List<Texture2D>();
            Texture2D simple = Content.Load<Texture2D>("Particles/simple");
            sprites.Add(simple);

            Vector2 startPosition = new Vector2(200, 200);
            Vector2 initialVelocity = new Vector2(0.5f, 1.5f);
            Vector2 velocityDeviation = new Vector2(0.2f, 0.3f);

            float initialGravity = 0.0005f;
            float gravityDeviation = 0.0001f;

            float initialTTL = 1000;
            float ttlDeviation = 300;

            byte initialOpacity = 255;
            byte opacityDeviation = 0;

            float initialScale = 1;
            float scaleDeviation = 0.8f;

            float initialRotation = 0;
            float rotationDeviation = 0;


            return new Gravity_System(sprites, startPosition,
                initialVelocity, initialTTL, initialOpacity, initialScale, initialRotation, initialGravity,
                velocityDeviation, ttlDeviation, opacityDeviation, scaleDeviation, rotationDeviation, gravityDeviation);
        }
    }
}
