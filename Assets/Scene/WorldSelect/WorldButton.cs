using System;
using UnityEngine;
using UnityEngine.UI;

namespace BB
{
	public class WorldButton : MonoBehaviour
	{
		public WorldType World { get; private set; }

		[SerializeField]
		private Image _image;

		public Action<WorldButton> OnSelectedCallback;

		public void SetWorld(WorldType type)
		{
			World = type;

			var data = DB._.GetWorld(type);
			_image.sprite = data.LoadSprite();
		}

		public void OnSelected()
		{
			if (OnSelectedCallback == null)
				return;

			OnSelectedCallback(this);
		}
	}
}