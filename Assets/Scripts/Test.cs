using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [VectorRangeAttribute]
    public Vector2 vector;

    void OnPlay() {
        Debug.Log("Play");
    }

    void OnUse() {
        Debug.Log("Use");
    }
}
