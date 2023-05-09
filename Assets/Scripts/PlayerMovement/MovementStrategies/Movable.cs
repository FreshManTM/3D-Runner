using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerMovement
{

    public abstract class Movable
    {
        public abstract void Move(CharacterController controller, Vector3 targetPosition);
    }
}