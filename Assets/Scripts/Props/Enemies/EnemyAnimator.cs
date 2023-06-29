using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props.Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int AttackIndex = Animator.StringToHash("attack");
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Attack()
        {
            animator.SetTrigger(AttackIndex);
        }
    }
}
