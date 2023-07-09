using System;
using UnityEngine;

namespace ProjectW.NPCs.Baker
{
    public class Baker : MonoBehaviour
    {
        private BakerAnimator _bakerAnimator;
        private BakerMovement _bakerMovement;

        #region Health

        public Health Health;
        public Healthbar HealthBar;

        #endregion
        
        private void Start()
        {
            _bakerMovement = GetComponent<BakerMovement>();
            _bakerAnimator = GetComponent<BakerAnimator>();
            _bakerMovement.SetMoving(true);
            Health = GetComponent<Health>();
            HealthBar.SetMaxHealth(Health.HealthPoints);
        }

        [ContextMenu("Death")]
        public void Die()
        {
            _bakerMovement.SetMoving(false);
            _bakerAnimator.Die();
            Health.enabled = false;
        }
        
        private void OnEnable()
        {
            Health.OnDeath += Die;
            Health.OnTakeDamage += SetUIHealth;
        }

        private void OnDisable()
        {
            Health.OnDeath -= Die;
            Health.OnTakeDamage -= SetUIHealth;
        }
        
        private void SetUIHealth(int currentHealth)
        {
            HealthBar.SetHealth(currentHealth);
        }
        
        
    }
}