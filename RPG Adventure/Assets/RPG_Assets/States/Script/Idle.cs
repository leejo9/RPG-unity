using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG {
    [CreateAssetMenu(fileName = "New State", menuName = "RPG/AbilityData/Idle")]

    public class Idle : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParamter.Jump.ToString(), false);
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (control.Attack)
            {
                animator.SetBool(TransitionParamter.Attack.ToString(), true);
            }
            if (control.Jump)
            {
                animator.SetBool(TransitionParamter.Jump.ToString(), true);
            }
            if (control.MoveRight)
            {

                animator.SetBool(TransitionParamter.Move.ToString(), true);
            }
            if (control.MoveLeft)
            {

                animator.SetBool(TransitionParamter.Move.ToString(), true);
            }
            if (control.MoveForward)
            {

                animator.SetBool(TransitionParamter.Move.ToString(), true);
            }

        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
    }
}
