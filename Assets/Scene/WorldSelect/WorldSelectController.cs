using UnityEngine;
using UnityEngine.UI;

namespace BB
{
	public class WorldSelectController : MonoBehaviour
	{
		private const int ButtonSize = 200;
		private const int ButtonPadding = 10;
		private const int ButtonInterval = ButtonSize + ButtonPadding * 2;

		private const int ScrollToSpeed = 20000;

		[SerializeField]
		private ScrollRect _worldScroll;
		[SerializeField]
		private RectTransform _worldContent;
		[SerializeField]
		private WorldButton _worldButtonPrf;

		public int ScrollOffset
		{
			get { return (int) _worldContent.localPosition.x; }
		}

		public class TransitionData
		{
		}

		public static TransitionData TransitionData_;

		private void Start()
		{
			int x = 0;
			x = ButtonInterval/2;

			foreach (var world in  DB._.World)
			{
				var button = SpawnWorldButton(world.Type);

				var buttonPos = button.transform.localPosition;
				buttonPos.x = x;
				button.transform.localPosition = buttonPos;

				button.OnSelectedCallback += OnWorldSelected;

				x += ButtonInterval;
			}

			var offsetX = _worldContent.offsetMax;
			offsetX.x = x - ButtonInterval/2;
			_worldContent.offsetMax = offsetX;

			var contentPos = _worldContent.localPosition;
			contentPos.x = SceneGlobalState.WorldScrollOffset;
			_worldContent.localPosition = contentPos;

			TransitionData_ = null;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
				OnEscapeKeyDown();
		}

		private WorldButton SpawnWorldButton(WorldType world)
		{
			var go = Instantiate(_worldButtonPrf.gameObject);
			go.transform.SetParent(_worldContent, false);

			var button = go.GetComponent<WorldButton>();
			button.SetWorld(world);

			return button;
		}

		public void ScrollToLeft()
		{
			_worldScroll.movementType = ScrollRect.MovementType.Clamped;
			_worldScroll.velocity = new Vector2(ScrollToSpeed, 0);
		}

		public void ScrollToRight()
		{
			_worldScroll.movementType = ScrollRect.MovementType.Clamped;
			_worldScroll.velocity = new Vector2(-ScrollToSpeed, 0);
		}

		private void OnWorldSelected(WorldButton button)
		{
			SceneGlobalState.WorldScrollOffset = ScrollOffset;
			var transition = new LevelSelectController.TransitionData(button.World);
			TransitionManager.TransferToLevelSelect(transition);
		}

		private static void OnEscapeKeyDown()
		{
			var transition = new MainMenuController.TransitionData() {State = MainMenuController.State.Play};
			TransitionManager.TransferToMainMenu(transition);
		}
	}
}