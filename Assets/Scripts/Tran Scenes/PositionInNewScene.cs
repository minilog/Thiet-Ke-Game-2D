using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInNewScene : MonoBehaviour
{
    static public int PlayerAppearPoint;
    static public Vector3 PositionOffset;

    public int Point;

    void Start()
    {
        if (Point == PlayerAppearPoint)
        {
            ObjectsInGame.PlayerController.transform.position = transform.position + PositionOffset;

            Vector3 playerPos = ObjectsInGame.PlayerController.transform.position;
            Camera.main.transform.position = new Vector3(playerPos.x, playerPos.y, Camera.main.transform.position.z);
        }
    }

    static public void SetValueForNewScene(int point, Vector3 offset)
    {
        PlayerAppearPoint = point;
        PositionOffset = offset;
    }
}
