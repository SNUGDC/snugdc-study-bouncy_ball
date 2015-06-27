using UnityEngine;

namespace BB
{
	public static class MapFactory 
	{
		public static Ball InstantiateBall(Coor position)
		{
			var go = (GameObject)GameObject.Instantiate(LevelDB._.Ball.gameObject, position.ToVector2(), Quaternion.identity);
			return go.GetComponent<Ball>();
		}

		public static Star InstantiateStar(Coor position)
		{
			var go = (GameObject)GameObject.Instantiate(LevelDB._.Star.gameObject, position.ToVector2(), Quaternion.identity);
			return go.GetComponent<Star>();
		}

		public static Block Instantiate(BlockType type, Coor position)
		{
			var data = BlockDB._.Find(type);
			if (data == null)
			{
				Debug.LogError("Block for type not exists: " + type);
				return null;
			}

			var obj = (GameObject)GameObject.Instantiate(data.Prefab.gameObject, position.ToVector2(), Quaternion.identity);
			return obj.GetComponent<Block>();
		}
	}
}