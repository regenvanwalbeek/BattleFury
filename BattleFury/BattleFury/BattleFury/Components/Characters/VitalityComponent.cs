using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using BattleFury.Input;

namespace BattleFury.Components.Characters
{
    /// <summary>
    /// A class which tracks vitality information for a character.
    /// </summary>
    public class VitalityComponent : Component
    {
        /// <summary>
        /// Number of lives left for the character
        /// </summary>
        public int LivesLeft { get; private set; }

        /// <summary>
        /// The character's current rage meter
        /// </summary>
        public float RageMeter { get; private set; }

        /// <summary>
        /// True if the entity is still alive (that is, the entity still has lives left)
        /// </summary>
        public bool IsAlive { get; private set; }

        /// <summary>
        /// True if the entity is knocked out of the arena
        /// </summary>
        public bool IsKO { get; private set; }

        private const int RUMBLE_TIME = 100;
        private int[] timeTillRumbleOff = new int[4];

        private CharacterInformationComponent characterInformationComponent;

        public VitalityComponent(Entity parent, int lives) : 
            base(parent, "VitalityComponent")
        {
            this.IsAlive = true;
            this.IsKO = false;
            this.LivesLeft = lives - 1;
            this.RageMeter = 100.0f;
            for (int i = 0; i < 4; i++)
            {
                timeTillRumbleOff[i] = 0;
            }
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            characterInformationComponent = (CharacterInformationComponent) Parent.GetComponent("CharacterInformationComponent");
        }

        public override void Update(GameTime gameTime)
        {
            if (IsKO || !IsAlive)
            {
                RageMeter = 0;
            }

            for (int i = 0; i < 4; i++)
            {
                if (timeTillRumbleOff[i] >= 0)
                {
                    timeTillRumbleOff[i] -= gameTime.ElapsedGameTime.Milliseconds;
                    if (timeTillRumbleOff[i] < 0)
                    {
                       InputState.Rumble(characterInformationComponent.PlayerIndex, 0, 0);
                    }
                }
            }
        }

        /// <summary>
        /// Damages the entity's rage meter
        /// </summary>
        /// <param name="damageAmount">The amount to damage the entity</param>
        public void Damage(float damageAmount)
        {
            if (characterInformationComponent != null)
            {
                bool rumbled = InputState.Rumble(characterInformationComponent.PlayerIndex, 1, 1);
                timeTillRumbleOff[(int) characterInformationComponent.PlayerIndex] = RUMBLE_TIME;
            }
            
            RageMeter -= damageAmount;
            if (RageMeter < 0)
            {
                RageMeter = 0;
            }
        }

        public void Knockout()
        {
            if (LivesLeft > 0)
            {
                LivesLeft--;
            }
            else
            {
                IsAlive = false;
            }
            IsKO = true;
            
        }

        public void Respawn()
        {
            IsKO = false;
            RageMeter = 100.0f;
        }

        public void Heal(float amount)
        {
            RageMeter += amount;
            if (RageMeter > 100.0f)
            {
                RageMeter = 100.0f;
            }
        }

    }
}
