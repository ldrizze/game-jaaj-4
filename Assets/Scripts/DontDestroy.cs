using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
}
