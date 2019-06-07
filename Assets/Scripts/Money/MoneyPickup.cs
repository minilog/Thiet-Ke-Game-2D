using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{


    [SerializeField] Object floatingMoneyPrefab;
    [SerializeField] Rigidbody2D rb2D;
    [Space]
    public float Number;
    public bool StartMoving;
    public bool ExistOnlyOne;
    public float FloatID;

    private float MinY = 3f;
    private float MaxY = 4f;
    private float MinX = -1.5f;
    private float MaxX = 1.5f;

    private void Start()
    {
        if (StartMoving)
        {
            rb2D.velocity = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
        }

        if (ExistOnlyOne)
        {
            foreach (float floatId in ObjectsInGame.WasTakenFloatIDs)
            {
                if (FloatID == floatId)
                    Destroy(gameObject);
            }
        }
    }

    private void OnValidate()
    {
        floatingMoneyPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Money/Floating Money.prefab", typeof(GameObject));
        rb2D = GetComponent<Rigidbody2D>();
        //FloatID = Random.Range(-999f, 999f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ObjectsInGame.CanvasController.Money += Number;

            GameObject floating = Instantiate(floatingMoneyPrefab, transform.position + new Vector3(0, 0.66f, 0), Quaternion.identity) as GameObject;
            floating.GetComponent<FloatingMoney>().Number = Number;

            if (ExistOnlyOne)
                ObjectsInGame.WasTakenFloatIDs.Add(FloatID);

            Destroy(gameObject);
        }
    }
}
