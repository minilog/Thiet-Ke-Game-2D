using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIblast : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        Invoke("DestroyGameObject", 3f);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Shootable") ||
            collision.gameObject.layer == LayerMask.NameToLayer("AI"))
            Destroy(gameObject);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
