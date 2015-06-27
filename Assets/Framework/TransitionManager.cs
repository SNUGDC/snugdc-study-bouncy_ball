using UnityEngine;

namespace BB
{
	public static class TransitionManager
	{
		public static void TransferToMainMenu(MainMenuController.TransitionData data)
		{
			MainMenuController.TransitionData_ = data;
			Application.LoadLevel("MainMenu");
		}

		public static void TransferToWorldSelect(WorldSelectController.TransitionData data)
		{
			WorldSelectController.TransitionData_ = data;
			Application.LoadLevel("WorldSelect");
		}

		public static void TransferToLevelSelect(LevelSelectController.TransitionData data)
		{
			LevelSelectController.TransitionData_ = data;
			Application.LoadLevel("LevelSelect");
		}

		public static void TransferToLevel(LevelDef def)
		{
			LevelController.LevelDef = def;
			Application.LoadLevel("Level");
		}
	}
}