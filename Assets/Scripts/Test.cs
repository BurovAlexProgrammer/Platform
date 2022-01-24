using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class Test : MonoBehaviour
{
    public Recipe ingredient;
    [Space(20)]
    public Recipe[] ingrArray;

    void OnPlay() {
        Debug.Log("Play");
        
    }

    void OnUse() {
        Debug.Log("Use");
    }
}
