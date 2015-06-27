using UnityEngine;

namespace BB
{
	public class InputManager : MonoBehaviour
	{
		public static InputManager _ { get; private set; }

		public bool IsRightPressed { get; private set; }
		public bool IsLeftPressed { get; private set; }

		void Start()
		{
			_ = this;
		}

		void Update()
		{
			IsRightPressed = Input.GetKey(KeyCode.RightArrow);
			IsLeftPressed = Input.GetKey(KeyCode.LeftArrow);
		}
	}
}