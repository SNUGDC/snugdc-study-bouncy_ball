using UnityEngine;

namespace BB
{
	public class InputManager : MonoBehaviour
	{
		private const float TouchCriteria = 400;

		public static InputManager _ { get; private set; }

		public bool IsRightPressed { get; private set; }
		public bool IsLeftPressed { get; private set; }

		void Start()
		{
			_ = this;
		}

		void Update()
		{
			if (Input.touchSupported)
			{
				if (Input.touchCount == 0)
				{
					IsRightPressed = false;
					IsLeftPressed = false;
				}
				else
				{
					var x = Input.GetTouch(0).position.x;
					IsRightPressed = x > TouchCriteria;
					IsLeftPressed = x < TouchCriteria;
				}
			}
			else
			{
				IsRightPressed = Input.GetKey(KeyCode.RightArrow);
				IsLeftPressed = Input.GetKey(KeyCode.LeftArrow);
			}
		}
	}
}