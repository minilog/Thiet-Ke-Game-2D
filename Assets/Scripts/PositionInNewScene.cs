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
            ObjectsInGame.PlayerController.transform.position = transform.position + PositionOffset;
    }

    static public void SetValueForNewScene(int point, Vector3 offset)
    {
        PlayerAppearPoint = point;
        PositionOffset = offset;
    }
}
