using UnityEngine;

public class Melee : MonoBehaviour
{
    Priest priest = null;
    bool active = false;

    private void Start()
    {
        Transform parent = transform.parent;

        if (!parent)
            return;

        priest = parent.GetComponent<Priest>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active)
            return;

        Priest other = collision.GetComponent<Priest>();

        if (!other)
            return;

        // if target has different tag than this object send damage
        if (!other.CompareTag(gameObject.tag))
            other.SendMessage("TakeDamage", priest.damage, SendMessageOptions.DontRequireReceiver);
    }

    void m_Active()
    {
        active = true;
    }

    void m_Inactive()
    {
        active = false;
    }
}
