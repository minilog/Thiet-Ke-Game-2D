using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration2 : MonoBehaviour
{
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] List<Sprite> sprites;
    public float minTime = 3f;
    public float maxTime = 5f;
    
    float count;
    int currentSpriteIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        ChangeSprite();

        count = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        count -= Time.deltaTime;
        if (count <= 0)
        {
            ChangeSprite();

            count = Random.Range(minTime, maxTime);
        }
    }

    private void ChangeSprite()
    {
        int ran = Random.Range(0, sprites.Count);
        while (currentSpriteIndex == ran)
        {
            ran = Random.Range(0, sprites.Count);
        }

        currentSpriteIndex = ran;

        renderer.sprite = sprites[currentSpriteIndex];
    }
}
