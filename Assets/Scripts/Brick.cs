using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private float health = 100f;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Ball")) return;
        var ball = other.transform.GetComponent<Ball>();
        GetDamage(ball.Damage);
    }

    private void GetDamage(float damage)
    {
        health -= damage;
        OnGetDamage();
    }

    private void OnGetDamage()
    {
        if (health <= 0f)
        {
            //Implement destruction as scriptable object
            Destroy(gameObject);
        }
    }
}