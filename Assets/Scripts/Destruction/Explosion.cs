using UnityEngine;

namespace Destruction
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] [Range(0, 100)] public float radius = 5f;
        [SerializeField] [Range(0, 1000)] public float force = 10;
        [SerializeField] [Range(0, 100)] public float liftForce = 1;
        [SerializeField] public ForceMode forceMode = ForceMode.Force;

        void Start()
        {
            foreach (var affectedCollider in Physics.OverlapSphere(transform.position, radius))
            {
                if (affectedCollider.IsComponentExist<Rigidbody>())
                {
                    affectedCollider.GetComponent<Rigidbody>()
                        .AddExplosionForce(force, transform.position, radius, liftForce, forceMode);
                }
            }

            Destroy(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0.56f, 0.09f, 0.73f);
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}