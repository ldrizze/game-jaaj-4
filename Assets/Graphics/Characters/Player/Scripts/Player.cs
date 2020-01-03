using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    int powerAmountToPossess = 5;

    Vector2 move = Vector2.zero;
    Rigidbody2D m_rb = null;
    SpriteRenderer m_sp = null;
    Animator m_an = null;


    float lasth = 0f;
    int power = 0;

    // CLP Update!
    public static bool zeroPower = false;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_sp = GetComponent<SpriteRenderer>();
        m_an = GetComponent<Animator>();
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

        // CLP Update!
        if (zeroPower)
        {
            zeroPower = false;
            SubPower();
        }
    }

    void FixedUpdate()
    {
        if (m_rb)
            m_rb.velocity = move;
    }

    void AddPower()
    {
        Power++;
    }

    // CLP Update!
    void SubPower()
    {
        Power--;
    }

    int Power
    {
        get { return power; }
        set
        {
            power = value;
            if (power >= powerAmountToPossess)
                m_an.SetBool("Empowered", true);
        }
    }

    public bool CanPossess
    {
        get
        {
            return Power >= powerAmountToPossess;
        }
    }
}
