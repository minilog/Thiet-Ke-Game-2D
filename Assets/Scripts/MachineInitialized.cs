using UnityEngine;

public class MachineInitialized : MonoBehaviour
{
    public GameObject InitializationGO;

    public float InitTime = 0.2f;

    private float count;

    private void Update()
    {
        count += Time.deltaTime;
        if (count >= InitTime)
        {
            Instantiate(InitializationGO, gameObject.transform.position, Quaternion.identity);
            count = 0;
        }
    }
}
