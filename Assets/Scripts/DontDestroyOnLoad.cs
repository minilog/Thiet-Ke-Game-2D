using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static bool alreadyExist;

    [SerializeField] GameObject player;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject camera;

    private void Start()
    {
        if (!alreadyExist)
        {
            alreadyExist = true;
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(camera);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(player);
            Destroy(canvas);
            Destroy(camera);
            Destroy(gameObject);
        }
    }
}
