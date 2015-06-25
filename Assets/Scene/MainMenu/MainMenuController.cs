using System;
using UnityEngine;

namespace BB
{
	public class MainMenuController : MonoBehaviour
	{
		[Serializable]
		public enum State
		{
			MainMenu,
			Play,
			Option,
			Save,
		};

		private State _state;

		[SerializeField] private Animator _animator;

		public class TransitionData
		{
			public State State = State.MainMenu;
		}

		public static TransitionData TransitionData_;

		private void Start()
		{
			if (TransitionData_ != null)
			{
				if (TransitionData_.State == State.Play)
					TransferToPlay();
				TransitionData_ = null;
			}
		}

		private bool CheckTransfer(State state)
		{
			if (_state != State.MainMenu)
			{
				Debug.LogWarning("Cannot transfer from except MainMenu.");
				return false;
			}

			return true;
		}

		public void TransferToPlay()
		{
			if (!CheckTransfer(State.Play))
				return;

			_state = State.Play;
			_animator.SetTrigger(State.Play.ToString());
		}

		public void TransferToPlaySmooth()
		{
			if (!CheckTransfer(State.Play))
				return;

			_state = State.Play;
			_animator.SetTrigger(_state + "Smooth");
		}

		public void TransferToWorldSelect()
		{
			TransitionManager.TransferToWorldSelect(new WorldSelectController.TransitionData());
		}

		public void Back()
		{
			if (_state != State.Play)
			{
				Debug.LogWarning("Cannot back from except Play.");
				return;
			}

			_state = State.MainMenu;
			_animator.SetTrigger("Back");
		}

	}
}