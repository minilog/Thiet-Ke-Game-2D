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
                Invoke("NewScene", TimeForNewScene);
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

    void NewScene()
    {
        SceneManager.LoadScene(NewSceneName);
    }
}