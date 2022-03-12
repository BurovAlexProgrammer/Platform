using System;
using UnityEngine;
using Bricks;

public class Ball : MonoBehaviour
{
    [SerializeField] private float damage = 50f;
    public float Damage => damage;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTagWithParents("Brick")) return;
        var brick = other.transform.FindParentWithTag("Brick").GetComponent<Brick>();
        brick.MakeDamage(Damage);
    }
}