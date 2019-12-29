using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;
    Vector2 move = Vector2.zero;
    Rigidbody2D m_rb = null;
    SpriteRenderer m_sp = null;

    float lasth = 0f;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move.x != 0f)
            lasth = move.x;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        move = new Vector2(h * speed, v * speed);

        if (m_sp)
            m_sp.flipX = lasth < 0f;
    }

    void FixedUpdate()
    {
        if (m_rb)
            m_rb.velocity = move;
    }
}
