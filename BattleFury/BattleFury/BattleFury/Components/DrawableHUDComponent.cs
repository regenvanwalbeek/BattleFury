using System.Collections.Generic;
using BattleFury.Entities.Characters;
using BattleFury.EntitySystem;
using BattleFury.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BattleFury.Components.Characters;

namespace BattleFury.Components
{
    public class DrawableHUDComponent : DrawableComponent
    {

        private List<VitalityComponent> vitalityComponents;

        private SpriteFont rageFont;

        private SpriteFont livesFont;

        private List<Character> characters;

        public DrawableHUDComponent(Entity parent, List<Character> characters)
            : base(parent, "DrawableHUDComponent")
        {
            this.rageFont = ContentLoader.HUDRageFont;
            this.livesFont = ContentLoader.HUDLivesFont;
            this.characters = characters;

            vitalityComponents = new List<VitalityComponent>();
            for (int i = 0; i < characters.Count; i++)
            {
                VitalityComponent component = (VitalityComponent) characters[i].GetComponent("VitalityComponent");
                vitalityComponents.Add(component);
            }
            this.characters = characters;
        }

        public override void Draw2D(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < vitalityComponents.Count; i++)
            {
                if (vitalityComponents[i].IsAlive || vitalityComponents[i].IsKO)
                {
                    // Draw the rage percentage
                    string rageString = (int)vitalityComponents[i].RageMeter + "%";
                    float xPosRage = ((i + 1) * (GameSettings.WindowWidth / (vitalityComponents.Count + 1))) - (rageFont.MeasureString(rageString).X / 2);
                    float yPosRage = (GameSettings.WindowHeight * .9f) - (rageFont.MeasureString(rageString).Y / 2);
                    Vector2 drawPositionRage = new Vector2(xPosRage, yPosRage);
                    spriteBatch.DrawString(rageFont, rageString, drawPositionRage, characters[i].getColor());

                    // Draw the number of lives left.
                    string livesString;
                    if (!vitalityComponents[i].IsKO)
                    {
                        livesString = ":) x" + vitalityComponents[i].LivesLeft;
                    }
                    else
                    {
                        livesString = ":(";
                    }
                    float xPosLives = ((i + 1) * (GameSettings.WindowWidth / (vitalityComponents.Count + 1))) - (livesFont.MeasureString(livesString).X / 2);
                    float yPosLives = yPosRage + (rageFont.MeasureString(rageString).Y / 2) + (livesFont.MeasureString(livesString).Y / 2);
                    Vector2 drawPositionLives = new Vector2(xPosLives, yPosLives);
                    spriteBatch.DrawString(livesFont, livesString, drawPositionLives, characters[i].getColor());
                }
                else
                {
                    string drawString = ":(";
                    float xPosRage = ((i + 1) * (GameSettings.WindowWidth / (vitalityComponents.Count + 1))) - (rageFont.MeasureString(drawString).X / 2);
                    float yPosRage = (GameSettings.WindowHeight * .9f) - (rageFont.MeasureString(drawString).Y / 2);
                    Vector2 drawPositionRage = new Vector2(xPosRage, yPosRage);
                    spriteBatch.DrawString(rageFont, drawString, drawPositionRage, characters[i].getColor());
                }
            }

         
        }

        #region Other Component Methods
        public override void Initialize()
        {
        }

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void LoadContent()
        {
        }

        public override void UnloadContent()
        {
        }

       
        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            // Do nothing
        }

        #endregion
    }
}
