using System.Collections.Generic;
using UnityEngine;

namespace BB
{
	public static class MapHelper
	{
		public static BlockType? MapGidToBlockType(int gid)
		{
			switch (gid)
			{
				case 0:
					return null;

				case 1:
					return BlockType.Simple;

				default:
					Debug.LogError(gid);
					return null;
			}
		}
	}

	public class Map : MonoBehaviour
	{
		private const int MapHeight = 14;

		private const int StartGid = 76;
		private const int StarGid = 77;

		public Coor StartPoisition { get; private set; }

		public readonly List<Star> Stars = new List<Star>();

		public void Load(TiledSharp.Map map)
		{
			foreach (var tile in map.Layers[0].Tiles)
			{
				var coor = new Coor(tile.X, MapHeight - tile.Y);
				GameObject go = null;

				switch (tile.Gid)
				{
					case StartGid:
						StartPoisition = coor;
						break;

					case StarGid:
					{
						var star = MapFactory.InstantiateStar(coor);
						Stars.Add(star);
						go = star.gameObject;
						break;
					}

					default:
					{
						var gid = MapHelper.MapGidToBlockType(tile.Gid);
						if (gid.HasValue)
						{
							var block = MapFactory.Instantiate(gid.Value, coor, transform);
							go = block.gameObject;
						}
						break;
					}
				}

				if (go != null)
				{
					go.transform.localPosition = coor.ToVector2();
				}
			}
		}
	}
}