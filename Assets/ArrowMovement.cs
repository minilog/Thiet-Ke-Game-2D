using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2D;
    public float Speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 Velocity = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));

        rb2D.velocity = Velocity * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Velocity = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));

        rb2D.velocity = Velocity * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" &&
            ObjectsInGame.PlayerHealth.CanTakeDamage())
        {
            Destroy(gameObject);
        }
    }
}
