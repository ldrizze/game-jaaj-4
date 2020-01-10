using UnityEngine;

namespace Nav2D
{
	public class Agent : MonoBehaviour
	{
		[SerializeField]
		float precision = 0.25f;

		Navigation navigation = null;
		Vector3[] path = null;
		int m_stepIdx = 0;
		Vector3? m_step = null;

		Rigidbody2D m_rb = null;

		public Vector3? destination;
		public float speed = 5;

		void Start()
		{
			navigation = Navigation.Instance;
			m_rb = GetComponent<Rigidbody2D>();
		}

		void FixedUpdate()
		{
			if (destination != null)
				Move();
		}

		Vector3 NextStep()
		{
			if (Mathf.Abs(Vector3.Distance(transform.position, path[m_stepIdx])) < precision)
				if (m_stepIdx < path.Length - 1)
					return path[++m_stepIdx];

			return path[m_stepIdx];
		}

		public void CalculatePath()
		{
			// resets internal variables
			m_stepIdx = 0;
			m_step = null;

			if (navigation)
				path = navigation.CalculatePath(transform.position, (Vector3) destination);
		}

		public void SetDestination(Vector3 destination)
		{
			this.destination = destination;
			CalculatePath();
		}

		public Vector3[] Path
		{
			get
			{
				return path;
			}
		}

		public void Move()
		{
			if (destination == null)
				return;

			if (transform.position == destination)
				return;

			m_step = NextStep();

			if (!m_rb)
				return;

			
			// Calculates direction agent should move and sets it's rigidbody2D's velocity towards the calculated direction
			Vector3 step = (Vector3)m_step;
			Vector3 direction = (step - transform.position).normalized;

			if(m_stepIdx == path.Length - 1)
				m_rb.position = Vector3.Lerp(transform.position, step, Time.fixedDeltaTime * speed);
			else
				m_rb.position = Vector3.MoveTowards(transform.position, step, Time.fixedDeltaTime * speed);
		}
	}
}
