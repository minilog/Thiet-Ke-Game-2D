using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenTheDoor : MonoBehaviour
{
    [SerializeField] Animator animator;
    public string NewSceneName;
    public KeyCode OpenDoorKeyCode;
    public float TimeForNewScene;
    [Space]
    public int PlayerApearPoint;

    bool playerInRange = false;

    [Space]
    public bool RequireKey = false;

    private void Update()
    {
        if (playerInRange)
        {
            if (PlayerController.IsInteract)
            {
                if (!RequireKey)
                {
                    //ObjectsInGame.ChangeSceneManager.ChangeToNextScene(NewSceneName);
                    StartCoroutine(ChangeToNextScene(NewSceneName, TimeForNewScene));

                    animator.SetTrigger("Open");
                }
                else
                {
                    if (ObjectsInGame.CanvasController.HaveKey)
                    {
                        StartCoroutine(ChangeToNextScene(NewSceneName, TimeForNewScene));

                        animator.SetTrigger("Open");
                    }
                }
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

    IEnumerator ChangeToNextScene(string name, float delay)
    {
        yield return new WaitForSeconds(delay);

        ObjectsInGame.ChangeSceneManager.ChangeToNextScene(name);


        Vector3 offset = ObjectsInGame.PlayerController.transform.position - transform.position;
        PositionInNewScene.SetValueForNewScene(PlayerApearPoint, offset);
    }
}