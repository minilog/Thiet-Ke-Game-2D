using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewScene : MonoBehaviour
{
    public string SceneName;
    [Space]
    public int PlayerApearPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ObjectsInGame.ChangeSceneManager.ChangeToNextScene(SceneName);

            Vector3 offset = ObjectsInGame.PlayerController.transform.position - transform.position;
            PositionInNewScene.SetValueForNewScene(PlayerApearPoint, offset);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ObjectsInGame.ChangeSceneManager.ChangeToNextScene(SceneName);

            Vector3 offset = ObjectsInGame.PlayerController.transform.position - transform.position;
            PositionInNewScene.SetValueForNewScene(PlayerApearPoint, offset);
        }
    }
}
