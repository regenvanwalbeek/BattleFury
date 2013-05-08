using BattleFury.EntitySystem;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BattleFury.Entities.Items;

namespace BattleFury.Components.Animated
{
    public class IceCreamRenderComponent : BasicModelComponent
    {
        public Matrix Transform;

        private BepuPhysicsComponent physicsComponent;

        public IceCreamRenderComponent(Entity parent, Matrix transform)
            : base(parent, ContentLoader.IceCream)
        {
            this.Transform = transform;
        }

        protected override Matrix GetWorld()
        {
            return Matrix.CreateScale(.12f) * Matrix.CreateTranslation(new Vector3(0, .5f, 0)) * Transform * physicsComponent.Box.WorldTransform;
        }

        public override void Start()
        {
            base.Start();
 
            // Get a reference to the transform component
            this.physicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
        }
    }
}
