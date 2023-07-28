using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class ManualInput : MonoBehaviour
    {
        private CharacterControl characterControl;

        private void Awake()
        {
            characterControl = this.gameObject.GetComponent<CharacterControl>();
        }
        void Update()
        {
            if (VirtualInputManager.Instance.MoveRight)
            {
                characterControl.MoveRight = true;
            }
            else
            {
                characterControl.MoveRight = false;
            }
            if (VirtualInputManager.Instance.MoveLeft)
            {
                characterControl.MoveLeft = true;
            }
            else
            {
                characterControl.MoveLeft = false;
            }
            if (VirtualInputManager.Instance.Jump)
            {
                characterControl.Jump = true;
            }
            else
            {
                characterControl.Jump = false;
            }
        
            if (VirtualInputManager.Instance.MoveForward)
            {
                characterControl.MoveForward = true;
            }
            else
            {
                characterControl.MoveForward = false;
            }
            if (VirtualInputManager.Instance.MoveBackward)
            {
                characterControl.MoveBackward = true;
            }
            else
            {
                characterControl.MoveBackward = false;
            }
            if (VirtualInputManager.Instance.Attack)
            {
                characterControl.Attack = true;
            }
            else
            {
                characterControl.Attack = false;
            }

        }
    }
}
