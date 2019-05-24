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

    bool playerInRange = false;

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(OpenDoorKeyCode))
            {
                //ObjectsInGame.ChangeSceneManager.ChangeToNextScene(NewSceneName);
                StartCoroutine(ChangeToNextScene(NewSceneName, TimeForNewScene));
                animator.SetTrigger("Open");
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
    }
}