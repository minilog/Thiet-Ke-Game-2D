using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootSpore : MonoBehaviour
{
    public GameObject theProjectile;
    public float shootTime;
    [Range(0, 100)]
    public int chanceShoot;
    public Transform shootFrom;

    float nextShootTime;
    Animator cannonAnim;

    // add by myself
    bool playerInRange;


    // Start is called before the first frame update
    void Start()
    {
        cannonAnim = GetComponentInChildren<Animator>();
        nextShootTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && nextShootTime < Time.time)
        {
            nextShootTime = Time.time + shootTime;
            if (Random.Range(0, 100) < chanceShoot)
            {
                Instantiate(theProjectile, shootFrom.position, Quaternion.identity);
                cannonAnim.SetTrigger("cannonShoot");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerInRange = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerInRange = true;


    }
}
