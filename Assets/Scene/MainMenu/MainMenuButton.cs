using UnityEngine;

namespace BB
{
	public class MainMenuButton : MonoBehaviour
	{
		[SerializeField]
		private AudioSource _seOnClick;

		public void OnPressDown()
		{
			if (_seOnClick)
				_seOnClick.Play();
		}
	}
}