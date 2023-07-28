using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class KeyboardInput : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                VirtualInputManager.Instance.MoveForward = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveForward = false;
            }
            if (Input.GetKey(KeyCode.S))
            {
                VirtualInputManager.Instance.MoveBackward = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveBackward = false;
            }
            if (Input.GetKey(KeyCode.D))
            {
                VirtualInputManager.Instance.MoveRight = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveRight = false;
            }
            if (Input.GetKey(KeyCode.A))
            {
                VirtualInputManager.Instance.MoveLeft = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveLeft = false;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                VirtualInputManager.Instance.Attack = true;
            }
            else
            {
                VirtualInputManager.Instance.Attack = false;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                VirtualInputManager.Instance.Jump = true;
            }
            else
            {
                VirtualInputManager.Instance.Jump = false;
            }

        }
    }
}