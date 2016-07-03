using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Particles_The_Next_Generation
{
    public partial class Physics_System
    {
        protected void HandleInput(bool takeInput, float dt, bool doExplosions)
        {
            if (takeInput)
            {
                #region Plosions
                if (doExplosions)
                {
                    if (Input.LMB_Clicked)
                    {
                        m_Explosions.AddExplosion(Input.MousePosition, 10, 0.5f, 0.2f, 0, 800);
                    }

                    float implodeTimer = 2f;
                    Vector2 expPosition = Input.MousePosition;
                    float expMaxStrength = 100;
                    float expMinStrength = 5;
                    float expTtl = 0.2f;
                    float expMinRadius = 0;
                    float expMaxRadius = 200;
                    Vector2 impPosition = Input.MousePosition;
                    float impMaxStrength = 4;
                    float impMinStrength = 2;
                    float impTtl = implodeTimer;
                    float impMinRadius = 0;
                    float impMaxRadius = 500;

                    if (Input.RMB_Clicked)
                    {
                        m_Explosions.AddSingularity(implodeTimer,
                            expPosition, expMaxStrength, expMinStrength, expTtl, expMinRadius, expMaxRadius,
                            impPosition, impMaxStrength, impMinStrength, impTtl, impMinRadius, impMaxRadius);
                    }
                }
                #endregion

                #region Toggling Thangs
                if (Input.KeyPressed(Keys.T))
                    m_GridCollisionHandler.CollisionsEnabled = !m_GridCollisionHandler.CollisionsEnabled;

                //if (Input.KeyPressed(Keys.S))
                   //m_CurrentParticleType = m_AllParticleTypes[m_CurrentParticleTypeIndex++ % m_AllParticleTypes.Count()];

                #endregion

                #region Doodats and Spawnsettings
                if (Input.KeyPressed(Keys.C))
                { Clear(); }

                if (Input.KeyPressed(Keys.Q))
                {
                    m_GravWellManager.AddGravWell(Input.MousePosition);
                }

                if (Input.KeyPressed(Keys.V))
                {
                    m_GravWellManager.AddVortex(Input.MousePosition);
                }

                if (Input.KeyPressed(Keys.Right))
                {
                    m_MaxParticles += 200;
                    Clear();
                }

                if (Input.KeyPressed(Keys.Left))
                {
                    m_MaxParticles = Math.Max(0, m_MaxParticles - 200);
                    Clear();
                }

                if (Input.KeyPressed(Keys.Down))
                {
                    m_SpawnDelay = Math.Max(0, m_SpawnDelay - m_SpawnDelayInc);
                }

                if (Input.KeyPressed(Keys.Up))
                {
                    m_SpawnDelay += m_SpawnDelayInc;
                }
                #endregion

                if (Input.KeyPressed(Keys.Enter))
                {
                    autoGeneration = !autoGeneration;
                }
            }
        }
    }
}
