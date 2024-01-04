﻿using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface ICollidable
    {
      
        Cell Cell { get; set; }
        event EventHandler CollisionHandler;

        bool CheckCollision(ICollidable collidable);

    }
}
