using UnityEditor;
using UnityEngine;

namespace BB
{
	[CustomEditor(typeof(LevelController))]
	public class LevelControllerEditor : Editor
	{
		private WorldType _world;
		private Level _level;
		private Difficulty _difficulty = Difficulty.Easy;

		public LevelController Target { get { return (LevelController) target; } }

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
				Target.Load(new LevelDef(_world, _level, _difficulty));
		}
	}
}