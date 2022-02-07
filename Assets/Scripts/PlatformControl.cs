using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public void OnMove(InputAction.CallbackContext context) {
        inputMove = context.ReadValue<Vector2>();
    }

    public void MoveTarget() {
        if (inputMove.sqrMagnitude < 0.01f)
            return;
        targetX += inputMove.x * Time.deltaTime * sensetivity;
        targetX = Mathf.Clamp(targetX, -range, range);
    }

    public void MovePlatform() {
        var newX = Mathf.Lerp(platformX, targetX, 1f);
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, new Vector3(targetX,0), 0.3f);
    }

    private void Update() {
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
