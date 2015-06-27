using UnityEngine;

namespace BB
{
	public class LevelDB : MonoBehaviour
	{
		public static LevelDB _ { get; private set; }

		public Ball Ball;
		public Star Star;

		public static void Init(LevelDB obj)
		{
			_ = obj;
		}
	}
}