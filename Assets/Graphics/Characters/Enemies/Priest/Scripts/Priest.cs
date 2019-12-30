using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Priest : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    PriestType type = PriestType.Neutral;

    public bool playerControllable = false;

    Vector2 move = Vector2.zero;
    Rigidbody2D m_rb = null;
    SpriteRenderer m_sp = null;
    Animator m_an = null;

    float lasth = 1f;
    float lastv = -1f;
    int power = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_sp = GetComponent<SpriteRenderer>();
        m_an = GetComponent<Animator>();

        switch (type)
        {
            case PriestType.Purple:
                m_an.SetInteger("Color", 1);
                break;
            case PriestType.Blue:
                m_an.SetInteger("Color", 2);
                break;
            case PriestType.Green:
                m_an.SetInteger("Color", 3);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (move.x != 0f)
            lasth = move.x;

        if (move.y != 0f)
            lastv = move.y;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(playerControllable)
            move = new Vector2(h * speed, v * speed);

        if (m_sp && playerControllable)
            m_sp.flipX = lasth < 0f;

        if(playerControllable)
        {
            m_an.SetBool("FacingDown", lastv < 0f);

            if (m_rb.velocity.x != 0f)
            {
                m_an.SetBool("Walking", true);
            }
            else
            {
                if (m_rb.velocity.y != 0f)
                {
                    m_an.SetBool("Walking", true);
                }
                else
                {
                    m_an.SetBool("Walking", false);
                }
            }
        }
            
    }

    void FixedUpdate()
    {
        if (!playerControllable)
            return; 

        if (m_rb)
            m_rb.velocity = move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();

        if (!p)
            return;

        if(p.CanPossess)
        {
            p.gameObject.SetActive(false);
            playerControllable = true;
        }
    }
}

public enum PriestType
{
    Neutral, // 0
    Purple, // 1
    Blue, // 2
    Green // 3
}
