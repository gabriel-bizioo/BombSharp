using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombSharp.Classes
{
    public class CollisionManager
    {
        public List<Entity> Entities { get; set; } = new List<Entity>();

        public void HandleCollision()
        {
            for(int i = 0; i < Entities.Count; i++)
            {
                for(int j = i; j < Entities.Count; j++)
                {
                    Entities[i].CheckCollision(Entities[j]);
                }
            }
        }
    }
}
