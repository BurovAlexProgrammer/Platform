using System;
using ScriptableObjects.Destruction;
using UnityEngine;

namespace Bricks
{
    public class Brick : MonoBehaviour
    {
        private const string BrickSequenceName = "Brick Sequence";
        [SerializeField] private DestructibleSequence destructibleSequence;
        private GameObject _brickSequence;

        //Temp data
        private float health;
        private bool mustBeDestroyed;

        private void Start()
        {
            _brickSequence = transform.Find(BrickSequenceName)?.gameObject;
            if (_brickSequence == null) RefreshSequence();
            health = destructibleSequence.Health;
        }

        public void RefreshSequence()
        {
            if (destructibleSequence == null || destructibleSequence.CurrentState == null)
                throw new Exception("DestructibleSequence cannot be empty");
            _brickSequence = transform.Find(BrickSequenceName)?.gameObject;
            if (_brickSequence == null)
            {
                _brickSequence = new GameObject();
                _brickSequence.name = BrickSequenceName;
                _brickSequence.transform.SetParent(transform);
                _brickSequence.transform.localPosition = Vector3.zero;
                _brickSequence.transform.localScale = Vector3.one;
            }

            _brickSequence.Replace(destructibleSequence.CurrentState);
        }

        private void Update()
        {
            health = destructibleSequence.Health;
            mustBeDestroyed = destructibleSequence.MustBeDestroyed;
        }

        public void MakeDamage(float damage)
        {
            destructibleSequence.MakeDamage(damage);
            if (destructibleSequence.MustBeDestroyed)
            {
                //_brickSequence.gameObject.SetActive(false);
                Instantiate(destructibleSequence.destroyedPrefab,
                    transform.position, transform.rotation, transform.parent);
                Destroy(gameObject);
            }
            else
            {
                //If current state prefab changed
                if (!ReferenceEquals(_brickSequence.transform.GetChild(0).gameObject,
                    destructibleSequence.CurrentState))
                {
                    _brickSequence = destructibleSequence.CurrentState;
                    RefreshSequence();
                }
            }
        }
    }
}