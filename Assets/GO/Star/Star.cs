using System;
using UnityEngine;

namespace BB
{
	public class Star : MonoBehaviour
	{
		public static Action<Star> OnGetStar;

		void OnTriggerEnter2D(Collider2D collider)
		{
			if (OnGetStar != null)
				OnGetStar(this);
		}
	}
}