using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platform
{
    public class PlatformControl : MonoBehaviour {
        [SerializeField]
        private float range;
        [SerializeField]
        [Range(0.1f, 20f)]
        private float sensetivity = 1f;
        [SerializeField]
        private float speed;
        [SerializeField]
        private GameObject platform;

        private Vector2 inputMove;
        private float targetX = 0;
        private float platformX;
        private bool wait = true;
        private float startDelay = 0.3f;

        async void WaitOnStart() {
            while (startDelay > 0f) {
                await Task.Yield();
                startDelay -= Time.deltaTime;
            }
            wait = false;
        }

        public void Start() {
            WaitOnStart();
        }

        public void OnMove(InputAction.CallbackContext context) {
            inputMove = context.ReadValue<Vector2>();
        }

        private void MoveTarget() {
            if (inputMove.sqrMagnitude < 0.01f)
                return;
            targetX += inputMove.x * Time.deltaTime * sensetivity;
            targetX = Mathf.Clamp(targetX, -range, range);
        }

        private void MovePlatform() {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, new Vector3(targetX, 0), speed);
        }

        private void Update() {
            if (wait) return;
            platformX = platform.transform.position.x;
            MoveTarget();
            MovePlatform();
        }

        private void OnDrawGizmos() {
            //Range
            var leftMaxPosGizmos = transform.position + Vector3.left * range;
            var rightMaxPosGizmos = transform.position + Vector3.right * range;
            var gizmosSize = new Vector3(0.3f, 0.3f, 0.3f);
            Gizmos.color = Color.red;
            Gizmos.DrawCube(leftMaxPosGizmos, gizmosSize);
            Gizmos.color = Color.green;
            Gizmos.DrawCube(rightMaxPosGizmos, gizmosSize);
            //Target
            var targetGizmos = transform.position + Vector3.right * targetX + Vector3.down * 2;
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(targetGizmos, 0.3f);
        }
    }
}
