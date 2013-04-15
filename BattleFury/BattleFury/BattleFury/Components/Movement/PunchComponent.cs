
using BattleFury.EntitySystem;
using BattleFury.Entities;

namespace BattleFury.Components.Movement
{
    public class PunchComponent : Component
    {
        public PunchComponent(Entity parent, Environment environment)
            : base(parent, "PunchComponent")
        {
        
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            throw new System.NotImplementedException();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
