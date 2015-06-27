using UnityEngine;

namespace BB
{
	public class Ball : MonoBehaviour
	{
		private const float JumpDelay = 0.5f;
		private const float JumpVelocityY = 4;

		[SerializeField]
		private Rigidbody2D _rigidbody;
		public Rigidbody2D Rigidbody { get { return _rigidbody; } }

		private float _jumpElapsed = JumpDelay;
		public bool CanJump { get { return _jumpElapsed > JumpDelay; } }

		void Update()
		{
			_jumpElapsed += Time.deltaTime;
		}

		public void Jump()
		{
			if (!CanJump) return;
			_jumpElapsed = 0;
			_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpVelocityY);
		}
	}
}