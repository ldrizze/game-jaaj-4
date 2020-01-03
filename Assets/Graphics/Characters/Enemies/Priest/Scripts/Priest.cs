using System.Collections;
using UnityEngine;

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

    public bool playerControllable = false;

    Vector2 move = Vector2.zero;
    Rigidbody2D m_rb = null;
    SpriteRenderer m_sp = null;
    Animator m_an = null;
    Animator m_pan = null;

    float lasth = 1f;
    float lastv = -1f;

    // CLP Update!
    public float chaseSpeed = 5f;
    public float fieldOfView = 1f;
    Transform target = null;
    float countTime = 0f;

    public float possessTime = 10f;
    float tempPossess = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_sp = GetComponent<SpriteRenderer>();
        m_an = GetComponent<Animator>();

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
        if (move.x != 0f)
            lasth = move.x;

        if (move.y != 0f)
            lastv = move.y;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(playerControllable)
            move = new Vector2(h * speed, v * speed);

        if (playerControllable && m_sp)
            m_sp.flipX = lasth < 0f;

        if(playerControllable)
        {
            if (Input.GetButtonDown("Fire1") && punch)
                m_pan.SetTrigger("Punch");

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

        // CLP Update!
        // SE o jogador estiver controlando o Priest...
        if (playerControllable)
        {
            // Ele se torna o alvo.
            gameObject.tag = "Target";
            PriestManager.Target = true;

            // Inicia contagem regressiva.
            tempPossess += Time.deltaTime;

            if (tempPossess > possessTime)
            {
                tempPossess = 0;

                target = null;

                playerControllable = false;

                m_an.SetBool("Walking", false);

                m_rb.velocity = Vector2.zero;

                PlayerManager.isEnabled = true;

                Player.zeroPower = true;

                // Recolocar na mesma posição.
                //PlayerManager.priestPosition = transform.position;
                //PlayerManager.resetPosition = true;
            }
        }
        else
        {
            gameObject.tag = "Untagged";
        }

        // SE há um alvo definido e não sou "eu"...
        if (!playerControllable && PriestManager.Target)
        {
            // Definir o alvo.
            target = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>();

            // Workaround...
            if (transform.position.x > target.position.x)
            {
                punch.rotation = Quaternion.Euler(Vector3.forward * 180);
            }
            else
            {
                punch.rotation = Quaternion.Euler(Vector3.forward * 0);
            }

            // SE o alvo estiver dentro do "campo de visão".
            if (Vector2.Distance(transform.position, target.position) < fieldOfView)
            {
                // Carregando o punch!
                countTime += Time.deltaTime;

                // Ataques:
                // Campo de visão maior, demora para atacar.
                // Campo de visão menor, rápido para atacar.
                if(countTime > fieldOfView)
                {
                    countTime = 0; // Inicia novo ciclo.
                    m_pan.SetTrigger("Punch"); // Executa ataque.
                }

                // Flip Character.
                m_sp.flipX = transform.position.x > target.position.x;

                // Translate position.
                transform.position = Vector2.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
            }
            else
            {
                countTime = 0; // Inicia novo ciclo.
            }
        }
        else
        {
            target = null;
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
            PlayerManager.isEnabled = false;
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
