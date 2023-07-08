using System;
using UnityEngine;

namespace ProjectW.NPCs.Baker
{
    public class BakerAnimator : MonoBehaviour
    {
        public Animator BakerAnimationController;
        [NonSerialized]public float Speed;

        private void Update()
        {
            BakerAnimationController.SetFloat("MovementSpeed", Speed);
        }

        [ContextMenu("DieAnimation")]
        public void Die()
        {
            BakerAnimationController.SetTrigger("Death");
        }
    }
}