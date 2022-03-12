using UnityEngine;

namespace ScriptableObjects.Destruction
{
    public abstract class DestructibleObject : ScriptableObject
    {
        public abstract void MakeDamage(float damage);
    }
}