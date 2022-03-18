using System;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptableObjects.Destruction
{
    [CreateAssetMenu(menuName = "Custom/Destruction/Destructible Sequence")]
    public class DestructibleSequence : DestructibleObject
    {
        [Tooltip("In a head must be default prefab")] [SerializeField]
        private GameObject[] statePrefabs = new GameObject[0];

        [SerializeField] public float initHealth = 100f;
        [SerializeField] public AudioEvent damageSound;
        [SerializeField] public GameObject damageEffect;
        [SerializeField] public GameObject destroyedPrefab;
        private float _health;
        private int _stateCount;
        private GameObject _currentState;
        private int _currentStateNumber;
        [UsedImplicitly] public float Health => _health;
        [UsedImplicitly] public GameObject CurrentState => _currentState;
        [UsedImplicitly] public bool MustBeDestroyed => GetHealth() <= 0f;

        private void OnEnable()
        {
            _health = initHealth;
        }

        private void OnValidate()
        {
            _stateCount = statePrefabs.Length;
            if (_stateCount == 0) throw new Exception("statePrefabs array cannot be empty.");
            _currentStateNumber = 0;
            OnStateChanged();
        }

        public override void MakeDamage(float damage)
        {
            if (MustBeDestroyed) return;
            _health -= damage;
            OnMakeDamage();
            //TODO play sound
            //TODO play effects
        }

        private void OnMakeDamage()
        {
            var newStateNumber =
                Mathf.FloorToInt(
                    // 45/100 0.45 = 2-nd state     0.51 = still 1-st state   1/0.51 = 1.96 => 1
                    1f / (_health / initHealth)) - 1;
            if (newStateNumber != _currentStateNumber)
            {
                _currentStateNumber = newStateNumber;
                OnStateChanged();
            }
        }

        void OnStateChanged()
        {
            if (MustBeDestroyed) return;
            _currentState = statePrefabs[_currentStateNumber];
        }

        /// <summary>
        /// Returns float value of health (Range [0..1]) currentHealth/maxHealth
        /// </summary>
        [UsedImplicitly]
        public float GetHealth() => _health / initHealth;
    }
}