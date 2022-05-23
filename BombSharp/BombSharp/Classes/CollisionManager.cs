﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombSharp.Classes
{
    public class CollisionManager
    {
        public List<Player> PlayerList = new List<Player>();
        public List<Entity> Entities { get; set; } = new List<Entity>();

        public void HandleCollision()
        {
            foreach (var entity in Entities)
            {
                foreach (var player in PlayerList)
                {
                    player.CheckCollision(entity);
                }
            }
        }
    }
}