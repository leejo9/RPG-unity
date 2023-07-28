using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "New State", menuName = "RPG/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        public AnimationCurve SpeedGraph;
        public bool constant;
        public float Speed;
        public float BlockDistance;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.Jump)
            {
                animator.SetBool(TransitionParamter.Jump.ToString(), true);
            }
            if (constant)
            {
                ConstantMove(control, animator, stateInfo);
            }
            else
            {
                ControlledMove(control, animator, stateInfo);
            }
            
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        private void ConstantMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!CheckFront(control))
            {
                control.MoveFront(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }
        private void ControlledMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.MoveRight && control.MoveLeft && control.MoveForward && control.MoveBackward)
            {
                animator.SetBool(TransitionParamter.Move.ToString(), false);
                return;
            }
            if (!control.MoveRight && !control.MoveLeft && !control.MoveForward && !control.MoveBackward)
            {
                animator.SetBool(TransitionParamter.Move.ToString(), false);
                return;
            }
            if (control.MoveRight)
            {

                if (!CheckFront(control))
                {
                    control.MoveFront(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }

            }
            if (control.MoveLeft)
            {

                if (!CheckFront(control))
                {
                    control.MoveFront(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }
            if (control.MoveForward)
            {

                if (!CheckFront(control))
                {
                    control.MoveFront(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }

        }
        bool CheckFront(CharacterControl control)
        {
            
            foreach (GameObject o in control.FrontSpheres)
            {
                Debug.DrawRay(o.transform.position, control.transform.forward * 0.3f, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, BlockDistance))
                {
                    if (!control.RagdollParts.Contains(hit.collider))
                    {
                        if (!IsBodyPart(hit.collider))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        bool IsBodyPart(Collider c)
        {
            CharacterControl control = c.transform.root.GetComponent<CharacterControl>();
            if(control == null)
            {
                return false;
            }
            if(control.gameObject == c.gameObject)
            {
                return false;
            }
            if (control.RagdollParts.Contains(c))
            {
                return true;
            }
            return false;
        }
    }
}
