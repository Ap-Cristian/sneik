using Logic.Models;
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
        event CollisionEventHandler CollisionHandler;

        bool CheckCollision(ICollidable collidable);

    }
}
