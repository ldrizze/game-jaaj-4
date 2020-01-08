using UnityEngine;

namespace Nav2D
{
	public class Agent : MonoBehaviour
	{

		Navigation navigation = null;
		Vector3[] path;

		public Vector3 destination;

		void Start()
		{
			navigation = Navigation.Instance;
		}

		void Update()
		{

		}

		public void CalculatePath()
		{
			if (navigation)
				path = navigation.CalculatePath(transform.position, destination);
		}

		public Vector3[] Path
		{
			get
			{
				return this.path;
			}
		}
	}
}
