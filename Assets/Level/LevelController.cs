using UnityEngine;

namespace BB
{
	public class LevelController : MonoBehaviour
	{
		public class TransitionData
		{
			public WorldType World;
			public Level Level;
			public Difficulty Difficulty;

			public TransitionData(WorldType world, Level level, Difficulty difficulty)
			{
				World = world;
				Level = level;
				Difficulty = difficulty;
			}
		}

		private WorldType _world;

		public static TransitionData TransitionData_;

		void Start()
		{
			if (TransitionData_ != null)
			{
				Debug.Log("World: " + TransitionData_.World + ", Level: " + TransitionData_.Level);
				_world = TransitionData_.World;
				TransitionData_ = null;
			}
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
				OnEscapeKeyDown();

		}

		void OnEscapeKeyDown()
		{
			var transition = new LevelSelectController.TransitionData(_world);
			TransitionManager.TransferToLevelSelect(transition);
		}
	}
}