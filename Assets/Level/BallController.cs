using UnityEngine;

namespace BB
{
	public class BallController : MonoBehaviour
	{
		private const float Force = 5;
		private const float SpeedLimit = 1.5f;

		public Ball Ball { get; set; }

		void Start()
		{
			Debug.Assert(Ball);
		}

		void Update()
		{
			var velocityX = Ball.Rigidbody.velocity.x;

			if (InputManager._.IsRightPressed)
			{
				if (velocityX < SpeedLimit)
					Ball.Rigidbody.AddForce(new Vector2(Force, 0));
			}

			if (InputManager._.IsLeftPressed)
			{
				if (velocityX > -SpeedLimit)
					Ball.Rigidbody.AddForce(new Vector2(-Force, 0));
			}
		}
	}
}