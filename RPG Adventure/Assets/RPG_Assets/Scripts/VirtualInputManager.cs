using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool MoveRight;
        public bool MoveLeft;
        public bool MoveForward;
        public bool MoveBackward;
        public bool Attack;
        public bool Jump;
    }
}
