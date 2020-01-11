using UnityEngine;
using Nav2D;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Priest : MonoBehaviour
{

    [SerializeField]
    Transform punch = null;

    [SerializeField]
    float speed = 10f;

    [SerializeField]
    PriestType type = PriestType.Neutral;

    [SerializeField]
    Transform target = null;

    public bool playerControllable = false;

    Vector2 move = Vector2.zero;
    Rigidbody2D m_rb = null;
    SpriteRenderer m_sp = null;
    Animator m_an = null;
    Animator m_pan = null;
    Agent m_ag = null;

    float lasth = 1f;
    float lastv = -1f;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_sp = GetComponent<SpriteRenderer>();
        m_an = GetComponent<Animator>();
        m_ag = GetComponent<Agent>();

        if (m_ag)
            m_ag.speed = speed;

        if (punch)
            m_pan = punch.GetComponent<Animator>();

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
        // disable agent when priests is player controllable
        m_ag.enabled = !playerControllable;

        if (move.x != 0f)
            lasth = move.x;

        if (move.y != 0f)
            lastv = move.y;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (playerControllable)
            move = new Vector2(h * speed, v * speed);
        else
            move = m_ag.Velocity;

        if (m_sp)
            m_sp.flipX = lasth < 0f;

        m_an.SetBool("FacingDown", lastv < 0f);

        if (playerControllable)
        {
            if (Input.GetButtonDown("Fire1") && punch)
                m_pan.SetTrigger("Punch");
        }

        // if character is player movable uses rigidbody velocity
        Vector2 vel = m_rb.velocity;

        if (!playerControllable) // if not uses agent velocity
            vel = m_ag.Velocity;

        if (vel.x != 0f)
        {
            m_an.SetBool("Walking", true);
        }
        else
        {
            if (vel.y != 0f)
            {
                m_an.SetBool("Walking", true);
            }
            else
            {
                m_an.SetBool("Walking", false);
            }
        }

        if (move.x > 0 && move.x > move.y) // moving right
        {
            punch.rotation = Quaternion.Euler(Vector3.forward * 0);
        }
        else if (move.x < 0 && move.x < move.y) // moving left
        {
            punch.rotation = Quaternion.Euler(Vector3.forward * 180);
        }
        else if (move.y > 0 && move.y > move.x) // moving up
        {
            punch.rotation = Quaternion.Euler(Vector3.forward * 90);
        }
        else if (move.y < 0 && move.y < move.x) // moving down
        {
            punch.rotation = Quaternion.Euler(Vector3.forward * 270);
        }

        if (playerControllable)
            m_rb.mass = 0f;
        else
            m_rb.mass = 1000f;
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

        if (p.CanPossess)
        {
            p.gameObject.SetActive(false);
            playerControllable = true;
        }
    }

    public void SetMove(Vector2 move)
    {
        this.move = move;
    }

    public void GotoTarget()
    {
        if (playerControllable || !m_ag || !target)
            return;

        m_ag.SetDestination(target.position);
    }

    private void OnDrawGizmos()
    {
        if (!m_ag || !target)
            return;

        if (m_ag.Path == null)
            return;

        Vector3 prev = m_ag.Path[0];
        foreach (Vector3 waypoint in m_ag.Path)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(prev, waypoint);
            prev = waypoint;
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
