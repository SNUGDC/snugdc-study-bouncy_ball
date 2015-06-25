using UnityEngine;

namespace BB
{
	public class App : MonoBehaviour
	{
		[SerializeField]
		private DB _db;

		private void Awake()
		{
			_db.Initialize();
			DB._ = _db;
		}
	}
}