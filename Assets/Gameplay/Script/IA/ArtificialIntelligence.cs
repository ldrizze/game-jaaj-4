using UnityEngine;

namespace AI
{
    public class ArtificialIntelligence : MonoBehaviour
    {
        [SerializeField]
        float time = 1;

        Priest priest = null;
        float elapsedTime = 0;

        private void Start()
        {
            priest = GetComponent<Priest>();
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime > time)
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
    }
}

