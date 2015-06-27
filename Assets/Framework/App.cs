using UnityEngine;

namespace BB
{
	public class App : MonoBehaviour
	{
		private static bool _init;

		[SerializeField]
		private DB _db;

		[SerializeField] 
		private BlockDB _blockDB;

		void Awake()
		{
			Init();
		}

		void Init()
		{
			if (_init)
				return;

			_init = true;
			DB.Init(_db);
			BlockDB.Init(_blockDB);
		}
	}
}