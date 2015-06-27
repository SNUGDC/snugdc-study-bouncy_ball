using System.IO;
using UnityEngine;

namespace BB
{
	public static class MapGenerator
	{
		private static TiledSharp.Map Load(LevelDef def)
		{
			var path = "Level/episode" + def.World.ToNumber() + "/" + def.Level.ToNumber();
			if (def.Difficulty == Difficulty.Easy) path += "e";

			var tmxContent = Resources.Load<TextAsset>(path);
			if (tmxContent == null)
			{
				Debug.LogError("map is not exists: " + path);
				return null;
			}

			var tmxReader = new StringReader(tmxContent.text);
			var tmx = new TiledSharp.Map(tmxReader, pathToLoad =>
			{
				Debug.Log(pathToLoad);
				return null;
			});

			return tmx;
		}

		public static Map Generate(LevelDef def)
		{
			return Generate(Load(def));
		}

		public static Map Generate(TiledSharp.Map map)
		{
			var go = new GameObject();
			go.name = "Level";

			var ret = go.AddComponent<Map>();
			ret.Load(map);
			return ret;
		}
	}
}