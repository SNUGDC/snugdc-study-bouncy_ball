using UnityEngine;

namespace BB
{
	public class CollisionJump : MonoBehaviour
	{
		void OnTriggerStay2D(Collider2D collider)
		{
			if (collider.tag != Tag.Ball)
				return;

			collider.gameObject.GetComponent<Ball>().Jump();
		}
	}
}