using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Penta : MonoBehaviour
{
    CircleCollider2D m_col = null;
    // Start is called before the first frame update
    void Start()
    {
        m_col = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("Player"))
        {
            go.SendMessage("AddPower", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
