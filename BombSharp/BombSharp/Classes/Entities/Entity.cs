using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BombSharp.Classes
{
    public abstract class Entity
    {

        public Entity(HitBox hitbox)
        {
            this.HitBox = hitbox;
        }

        public HitBox HitBox { get; set; }
        public PointF Location { get; set; }

        public void CheckCollision(Entity entity)
        {
            List<CollisionInfo> infos = entity.HitBox.IsColliding(HitBox);
            foreach(CollisionInfo info in infos)
            {
                if (info.IsColliding)
                {
                    info.Entity = entity;
                    OnCollision(info);
                }
            }
                
        }

        public virtual void OnCollision(CollisionInfo info) { }
        public virtual void OnFrame() { }
        public abstract void Draw(Graphics g);
    }
}
