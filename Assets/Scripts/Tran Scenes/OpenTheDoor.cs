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
    [SerializeField] Vector3 positionInNewScene;

    bool playerInRange = false;

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(OpenDoorKeyCode))
            {
                Invoke("NewScene", TimeForNewScene);
                animator.SetTrigger("Open");
                Invoke("CanvasBlacking", TimeForNewScene / 2);
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
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        player.transform.position = positionInNewScene;

        CanvasController canvas = FindObjectOfType<CanvasController>();
        canvas.ImageWhiting();
    }

    void CanvasBlacking()
    {
        CanvasController canvas = FindObjectOfType<CanvasController>();
        canvas.ImageBlacking();
    }
}