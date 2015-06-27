using UnityEditor;
using UnityEngine;

namespace BB
{
	[CustomEditor(typeof(LevelController))]
	public class LevelControllerEditor : Editor
	{
		private WorldType _world = (WorldType)1;
		private Level _level = (Level)1;
		private Difficulty _difficulty = Difficulty.Easy;

		public LevelController Target { get { return (LevelController) target; } }
		public LevelDef LevelDef { get { return new LevelDef(_world, _level, _difficulty); } }

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!Application.isPlaying)
				return;

			_world = (WorldType)EditorGUILayout.IntField("world", (int)_world);
			_level = (Level)EditorGUILayout.IntField("level", (int)_level);

			if (GUILayout.Button(_difficulty.ToString()))
			{
				_difficulty = _difficulty == Difficulty.Easy 
					? Difficulty.Hard : Difficulty.Easy;
			}

			if (GUILayout.Button("generate"))
				Target.Load(LevelDef);

			if (GUILayout.Button("reload"))
				TransitionManager.TransferToLevel(LevelDef);
		}
	}
}