using UnityEngine;
using UnityEngine.UI;

namespace BB
{
	public class LevelSelectController : MonoBehaviour
	{
		private const int ButtonColumn = 7;

		private const int ButtonSize = 64;
		private const int ButtonPadding = 15;

		public WorldType World { get; private set; }

		[SerializeField] public Text _worldName;

		[SerializeField]
		private LevelButton _levelButtonPrf;
		[SerializeField]
		private RectTransform _levelsRoot;

		[SerializeField]
		private Button _easyButton;
		[SerializeField] 
		private Button _hardButton;

		[SerializeField] 
		private Animator _animator;

		private LevelButton _selectedButton;

		public class TransitionData
		{
			public TransitionData(WorldType world)
			{
				World = world;
			}

			public WorldType World;
		}

		public static TransitionData TransitionData_;

		private void Start()
		{
			if (TransitionData_ != null)
			{
				SetWorld(TransitionData_.World);
				TransitionData_ = null;
			}

			SpawnLevels();

			_easyButton.onClick.AddListener(() => OnDifficultySelected(Difficulty.Easy));
			_hardButton.onClick.AddListener(() => OnDifficultySelected(Difficulty.Hard));
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
				OnEscapeKeyDown();
		}

		private static Vector2 Locate(int i)
		{
			const float width = ButtonSize*ButtonColumn + 2*ButtonPadding*(ButtonColumn - 1);
			var x = i%ButtonColumn*(ButtonSize + ButtonPadding*2) + ButtonSize/2 - width/2;
			var y = -i/ButtonColumn*(ButtonSize + ButtonPadding*2) - ButtonSize/2;
			return new Vector2(x, y);
		}

		private void SpawnLevels()
		{
			for (int i = 0; i != (int)Const.LevelMax; ++i)
			{
				var go = Instantiate(_levelButtonPrf.gameObject);
				go.transform.SetParent(_levelsRoot, false);
				go.transform.localPosition = Locate(i);

				var button = go.GetComponent<LevelButton>();
				InitLevelButton(button, TypeHelper.MakeLevelFromIndex(i));
				button.OnSelectedCallback += OnLevelSelected;
			}
		}

		private void InitLevelButton(LevelButton button, Level level)
		{
			button.SetLevel(level);

			var clearState = UserLevelClear.Get(World, level);
			if (clearState.HasValue)
			{
				button.SetAsClear(clearState.Value);
			}
			else
			{
				var unlocked = UserLevelClear.Unlocked;
				var shouldLock = true;

				if (unlocked.HasValue)
				{
					var unlockedValue = unlocked.Value;
					var isCurrentUnlocked = unlockedValue.World == World && unlockedValue.Level == level;
					shouldLock = !isCurrentUnlocked;
				}

				button.SetLock(shouldLock);
				button.SetAsNotCleared();
			}
		}

		public void SetWorld(WorldType type)
		{
			World = type;

			var data = DB._.GetWorld(type);
			_worldName.text = data.Name;
		}

		public void OnEscapeKeyDown()
		{
			var data = new WorldSelectController.TransitionData();
			TransitionManager.TransferToWorldSelect(data);
		}

		public void OnLevelSelected(LevelButton button)
		{
			if (_selectedButton)
			{
				_selectedButton.Focus(false);
				_animator.SetTrigger("HideDifficulty");
			}

			_selectedButton = button;
			_selectedButton.Focus(true);

			_animator.SetTrigger("ShowDifficulty");
		}

		public void OnDifficultySelected(Difficulty difficulty)
		{
			if (!_selectedButton)
				return;

			var def = new LevelDef(World, _selectedButton.Level, difficulty);
			TransitionManager.TransferToLevel(def);
		}
	}
}