using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField]
    float seconds = 1;

    void Start()
    {
        Invoke("WaitAndDestroy", seconds);
    }

    private void WaitAndDestroy()
    {
        Destroy(gameObject);
    }
}
