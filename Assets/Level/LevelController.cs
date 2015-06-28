using System.Collections.Generic;
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

		private HashSet<Star> _starsLeft;

		private bool _getStarRegistered;

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

		void OnDestroy()
		{
			UnregisterGetStar();
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
				OnEscapeKeyDown();

			if (_ball && _ball.transform.localPosition.y < 0)
				Lose();
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
			_starsLeft = new HashSet<Star>(_map.Stars);
			Star.OnGetStar += OnGetStar;
			_getStarRegistered = true;

			_ball = MapFactory.InstantiateBall(_map.StartPoisition);
			_ball.transform.SetParent(transform, false);
			var ballController = _ball.gameObject.AddComponent<BallController>();
			ballController.Ball = _ball;
		}

		private void Win()
		{
			Debug.Assert(_starsLeft.Count == 0);

			UnregisterGetStar();

			UserLevelClear.Win(_levelDef);

			var nextLevel = _levelDef.Next();
			if (nextLevel.HasValue)
				TransitionManager.TransferToLevel(nextLevel.Value);
			else
				TransitionManager.TransferToLevelSelect(new LevelSelectController.TransitionData(_levelDef.World));
		}

		private void Lose()
		{
			UnregisterGetStar();
			TransitionManager.TransferToLevel(_levelDef);
		}

		private void UnregisterGetStar()
		{
			if (_getStarRegistered)
			{
				Star.OnGetStar -= OnGetStar;
				_getStarRegistered = false;
			}
		}

		private void OnGetStar(Star star)
		{
			if (!_starsLeft.Remove(star))
				Debug.LogError("get unmanaged star.");

			if (_starsLeft.Count == 0)
				Win();
		}
	}
}