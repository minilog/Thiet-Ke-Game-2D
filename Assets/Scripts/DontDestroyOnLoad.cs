using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static bool alreadyExist;

    [SerializeField] GameObject[] gameObjects;

    private void Start()
    {
        if (!alreadyExist)
        {
            alreadyExist = true;
            foreach (GameObject gameObject in gameObjects)
            {
                DontDestroyOnLoad(gameObject);
            }
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            foreach(GameObject gameObject in gameObjects)
            {
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
