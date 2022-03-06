using UnityEngine;

namespace Platform
{
    public class Platform : MonoBehaviour
    {
        //public SimpleAudioEvent simpleAudioEvent;
        public float power = 300f;
        public Transform forcePoint;
        public float forceRadius;

        void OnUse() {
            Debug.Log("Use");
            var audioSource = GetComponent<AudioSource>();
            //simpleAudioEvent.Play(audioSource);

        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.transform.CompareTag("Ball")) {
                collision.rigidbody.AddExplosionForce(power, forcePoint.position, forceRadius);
            }
        }


    }
}
