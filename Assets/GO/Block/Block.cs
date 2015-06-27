using UnityEngine;

namespace BB
{
	public class Block : MonoBehaviour
	{
		[SerializeField]
		private BlockData _data;
		public BlockData Data { get { return _data; } }
	}
}