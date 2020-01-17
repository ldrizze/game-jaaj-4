using UnityEngine;

namespace Nav2D
{
	public class Agent : MonoBehaviour
	{
		/// <summary>
		/// The avoidance radius for this agent.
		/// This is the agent's "personal space" within which obstacles and other agents should not pass.
		/// </summary>
		[SerializeField]
		[Tooltip("The avoidance radius for this agent. \n This is the agent's \"personal space\" within which obstacles and other agents should not pass.")]
		float radius = .25f;

		Navigation navigation = null;
		Vector3[] path = null;
		int m_stepIdx = 0;
		Vector3? m_step = null;
		Vector3 m_previousPosition;

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

		void Update()
		{
			if (destination == null)
				return;

			if (Mathf.Abs(Vector3.Distance(transform.position, (Vector3)destination)) > radius * 2)
				Move();
			else
			{
				destination = null;
				path = null;
				velocity = Vector3.zero;
			}
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
			if (m_stepIdx < path.Length - 1)
				if (Mathf.Abs(Vector3.Distance(transform.position, (Vector3)path[m_stepIdx])) <= radius)
					return path[++m_stepIdx];

			return path[m_stepIdx];
		}

		public void Move()
		{
			if (destination == null)
				return;

			if (transform.position == destination)
				return;

			// finds new waypoint in path array
			m_step = NextStep();

			if (!m_rb)
				return;

			Vector3 step = (Vector3)m_step;

			// before moving stores old position
			m_previousPosition = transform.position;

			Vector3 adjustPosition = step;
			
			// move object
			transform.position = Vector3.MoveTowards(transform.position, adjustPosition, Time.deltaTime * speed);

			float deltaX = transform.position.x - m_previousPosition.x,
				  deltaY = transform.position.y - m_previousPosition.y;

			// stores calculated speed based on movement
			velocity = new Vector3(deltaX, deltaY, 0) / Time.deltaTime;
		}
	}
}
