using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManager : MonoBehaviour
{
    [SerializeField] CanvasController canvasController;

    private string sceneName;

    private float changeSceneTime = 0.5f;

    bool removingOldScene = false;
    bool goingToNewScene = false;
    float chagingSceneCounter;

    private void Awake()
    {
        ObjectsInGame.ChangeSceneManager = this;
    }

    private void Update()
    {
        if (removingOldScene)
        {
            chagingSceneCounter -= Time.unscaledDeltaTime;
            if (chagingSceneCounter <= 0)
            {
                removingOldScene = false;
                NewScene();
            }
        }
        
        if (goingToNewScene)
        {
            chagingSceneCounter -= Time.unscaledDeltaTime;
            if (chagingSceneCounter <= 0.5f)
            {
                goingToNewScene = false;
                Time.timeScale = 1;
            }
        }
    }

    public void ChangeToNextScene(string sceneName)
    {
        this.sceneName = sceneName;
        Time.timeScale = 0;

        canvasController.ImageBlacking(changeSceneTime);

        removingOldScene = true;
        chagingSceneCounter = changeSceneTime;

        // Change Musuc
        BackgroundMusic BMusic = ObjectsInGame.SoundManager.GetComponentInChildren<BackgroundMusic>();
        if (SceneManager.GetActiveScene().name == "Final Level" ||
            sceneName == "Final Level")
        BMusic.ChangeMusic();
    }

    void NewScene()
    {
        SceneManager.LoadScene(sceneName);

        canvasController.ImageWhiting(changeSceneTime);

        goingToNewScene = true;
        chagingSceneCounter = changeSceneTime;

        ObjectsInGame.PlayerHealth.CheckSpawnPlayer();
    }
}
