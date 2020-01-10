using UnityEngine;

namespace Nav2D
{
	public class Agent : MonoBehaviour
	{
		[SerializeField]
		float precision = 0.15f;

		Navigation navigation = null;
		Vector3[] path = null;
		int m_stepIdx = 0;
		Vector3? m_step = null;
		Vector3 m_previousPosition;

		[SerializeField]
		Vector3 velocity = Vector3.zero;

		Rigidbody2D m_rb = null;

		public Vector3? destination;
		public float speed = 5;

		void Start()
		{
			m_previousPosition = transform.position;
			navigation = Navigation.Instance;
			m_rb = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			velocity = (transform.position - m_previousPosition) / Time.deltaTime;
			m_previousPosition = transform.position;
		}

		void FixedUpdate()
		{
			if (destination != null)
				Move();
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

		public Vector3 Velocity
		{
			get { return velocity;  }
		}

		public Vector3[] Path
		{
			get
			{
				return path;
			}
		}

		Vector3 NextStep()
		{
			if (Mathf.Abs(Vector3.Distance(transform.position, path[m_stepIdx])) < precision)
				if (m_stepIdx < path.Length - 1)
					return path[++m_stepIdx];

			return path[m_stepIdx];
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

			Vector3 step = (Vector3)m_step;

			m_rb.MovePosition(Vector3.MoveTowards(transform.position, step, Time.fixedDeltaTime * speed));

			Debug.Log(Time.fixedDeltaTime);
		}
	}
}
