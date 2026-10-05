using UnityEngine;
using UnityEngine.Events;

namespace Unity.FFS.Game
{
    public class Health : MonoBehaviour
    {
        [Tooltip("Maximum amount of health")] public float MaxHealth = 10f;

        public UnityAction<float, GameObject> OnDamaged;
        public UnityAction<float> OnHealed;
        public UnityAction OnDie;

        public float CurrentHealth { get; set; }
        public bool Invincible { get; set; }

        void Start()
        {
            CurrentHealth = MaxHealth;
        }

        void Update()
        {
            
        }
    }
}
