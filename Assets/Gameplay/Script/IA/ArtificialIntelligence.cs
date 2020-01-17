using UnityEngine;
using Nav2D;

namespace AI
{
    public class ArtificialIntelligence : MonoBehaviour
    {
        [SerializeField]
        float time = 1;

        [SerializeField]
        float fieldOfView = 1f;

        Priest priest = null;
        Agent m_ag = null;
        float elapsedTime = 0;

        private void Start()
        {
            priest = GetComponent<Priest>();
            m_ag = GetComponent<Agent>();
        }

        private void Update()
        {
            AttackLoop();
        }

        void AttackLoop()
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > time)
            {
                elapsedTime = 0;
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

