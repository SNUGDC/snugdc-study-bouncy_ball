using UnityEngine;

namespace BB
{
	public static class BlockFactory 
	{
		public static Block Instantiate(BlockType type, Coor position)
		{
			var data = BlockDB._.Find(type);
			var obj = (GameObject)GameObject.Instantiate(data.Prefab.gameObject, position.ToVector2(), Quaternion.identity);
			return obj.GetComponent<Block>();
		}
	}
}