using UnityEngine;
using Nav2D;
using System;

namespace AI
{
    public class ArtificialIntelligence : MonoBehaviour
    {
        [SerializeField]
        float time = 1;

        [SerializeField]
        float speedLimit = .75f;

        public float fieldOfView = 1f;

        Priest priest = null;
        Agent m_ag = null;
        float elapsedTime = 0;
        Vector2 m_previousPosition;

        [SerializeField]
        GameObject target = null;

        [SerializeField]
        float avoidanceDistance = 2f;

        private void Start()
        {
            priest = GetComponent<Priest>();
            m_ag = GetComponent<Agent>();

            m_previousPosition = transform.position;
        }

        private void Update()
        {
            AttackLoop();
            FindEnemyLoop();
            ChaseLoop();
        }

        void ChaseLoop()
        {
            if (!target)
                return;

            m_previousPosition = transform.position;

            // while he is not close enough chase the target
            if(Vector2.Distance(transform.position, target.transform.position) > avoidanceDistance)
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * priest.speed * speedLimit);

            float deltaX = transform.position.x - m_previousPosition.x,
                  deltaY = transform.position.y - m_previousPosition.y;

            // stores calculated speed based on movement
            m_ag.Velocity = new Vector3(deltaX, deltaY, 0) / Time.deltaTime;
        }

        void FindEnemyLoop()
        {
            if (target)
                return;

            GameObject enemy = null;

            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, fieldOfView);

            if (cols.Length > 0)
            {
                foreach(Collider2D col in cols)
                {
                    if (!col.CompareTag("Player"))
                        continue;

                    if (col.GetComponent<Priest>())
                    {
                        enemy = col.gameObject;
                        break;
                    }
                }
            }

            target = enemy;
        }

        void AttackLoop()
        {
            elapsedTime += Time.deltaTime;

            if (!target)
                return;

            if (Vector2.Distance(transform.position, target.transform.position) > avoidanceDistance)
                return;

            if (elapsedTime > time)
            {
                elapsedTime -= time;
                Attack();
            }
        }

        public void Attack()
        {
            if (!priest)
                return;

            priest.Melee();

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, fieldOfView);
        }
    }
}

