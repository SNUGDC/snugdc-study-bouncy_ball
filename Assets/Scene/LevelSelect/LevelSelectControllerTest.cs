using UnityEngine;

namespace BB
{
	public class LevelSelectControllerTest : MonoBehaviour
	{
		public LevelSelectController Target;
		public int World;

		private void Start()
		{
			Target.SetWorld((WorldType) World);
		}
	}
}