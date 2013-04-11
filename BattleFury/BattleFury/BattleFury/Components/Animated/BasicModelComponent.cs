using BattleFury.Components.CameraComponents;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BattleFury.Entities;

namespace BattleFury.Components.Animated
{
    /// <summary>
    /// A rendering component for a model.
    /// </summary>
    public class BasicModelComponent : DrawableComponent
    {
        /// <summary>
        /// Model to render
        /// </summary>
        protected Model model;

        public BasicModelComponent(Entity parent, Model model)
            : base(parent, "BasicModelComponent")
        {
            this.model = model;
        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.Projection = projection;
                    effect.View = view;
                    effect.World = GetWorld() * mesh.ParentBone.Transform;
                }
                mesh.Draw();
            }
        }

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Gets the model's world matrix
        /// </summary>
        /// <returns>The model's world</returns>
        protected virtual Matrix GetWorld()
        {
            return Matrix.Identity;
        }


        public override void LoadContent()
        {
        }

        public override void UnloadContent()
        {
        }

        public override void Initialize()
        {
        }
    }
}
