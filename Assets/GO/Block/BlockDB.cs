using System.Collections.Generic;
using UnityEngine;

namespace BB
{
	public class BlockDB : MonoBehaviour
	{
		public static BlockDB _ { get; private set; }

		[SerializeField]
		private List<BlockData> _dataList;
		private readonly Dictionary<BlockType, BlockData> _dataDic = new Dictionary<BlockType, BlockData>();

		public static void Init(BlockDB obj)
		{
			if (_ != null)
			{
				Debug.Assert(false, "trying to Init again.");
				return;
			}

			_ = obj;
			_.Init();
		}

		private void Init()
		{
			foreach (var data in _dataList)
				_dataDic.Add(data.Type, data);
		}

		public BlockData Find(BlockType type)
		{
			BlockData ret;
			if (!_dataDic.TryGetValue(type, out ret))
			{
				Debug.LogError("key " + type + " not exists.");
				return null;
			}
			return ret;
		}
	}
}