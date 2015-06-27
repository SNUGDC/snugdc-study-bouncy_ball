using UnityEngine;

namespace BB
{
	public class LevelController : MonoBehaviour
	{
		public static LevelDef? LevelDef;

		public bool IsLoaded { get { return _map != null; } }

		private LevelDef _levelDef;

		[SerializeField]
		private Transform _origin;
		public Transform Origin { get { return _origin; } }

		private Map _map;

		private Ball _ball;

		void Start()
		{
			if (LevelDef != null)
			{
				var levelDef = LevelDef.Value;
				LevelDef = null;
				Debug.Log("World: " + levelDef.World + ", Level: " + levelDef.Level);
				Load(levelDef);
			}
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
				OnEscapeKeyDown();
		}

		void OnEscapeKeyDown()
		{
			var transition = new LevelSelectController.TransitionData(_levelDef.World);
			TransitionManager.TransferToLevelSelect(transition);
		}

		public void Load(LevelDef def)
		{
			if (IsLoaded)
			{
				Debug.LogError("trying to load again.");
				return;
			}

			_levelDef = def;
			_map = MapGenerator.Generate(def);
			_ball = MapFactory.InstantiateBall(_map.StartPoisition);
		}
	}
}