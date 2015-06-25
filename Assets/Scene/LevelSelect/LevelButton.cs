using System;
using UnityEngine;
using UnityEngine.UI;

namespace BB
{
	public class LevelButton : MonoBehaviour
	{
		public Level Level { get; private set; }

		[SerializeField]
		private Text _levelText;
		[SerializeField]
		private Text _clearText;

		public bool IsLocked
		{
			get { return _lock.gameObject.activeSelf; }
		}

		[SerializeField]
		private RawImage _lock;

		[SerializeField]
		private Animator _animator;

		public Action<LevelButton> OnSelectedCallback;

		private bool mIsFocused;

		public void SetLevel(Level val)
		{
			Level = val;
			_levelText.text = val.ToString();
		}

		public void SetAsNotCleared()
		{
			_clearText.text = "";
		}

		public void SetAsClear(Difficulty difficulty)
		{
			_clearText.text = difficulty.ToString();
		}

		public void SetLock(bool val)
		{
			_levelText.gameObject.SetActive(false);
			_clearText.gameObject.SetActive(false);
			_lock.gameObject.SetActive(val);
		}

		public void Focus(bool val)
		{
			if (mIsFocused == val)
				return;

			if (IsLocked)
			{
				Debug.LogError("locked.");
				return;
			}

			mIsFocused = val;
			_animator.SetTrigger(val ? "Focus" : "Defocus");
		}

		public void OnSelected()
		{
			if (mIsFocused || IsLocked)
				return;

			if (OnSelectedCallback == null)
				return;

			OnSelectedCallback(this);
		}
	}
}