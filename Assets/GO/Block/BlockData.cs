using UnityEngine;

namespace BB
{
	public class BlockData : ScriptableObject
	{
		[SerializeField]
		private BlockType _type;
		public BlockType Type { get { return _type; } }

		[SerializeField]
		private Block _prefab;
		public Block Prefab { get { return _prefab; } }
	}
}