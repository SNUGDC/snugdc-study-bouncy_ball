using System;
using System.Collections.Generic;
using UnityEngine;

namespace BB
{
	public class DB : MonoBehaviour
	{
		public static DB _;

		[Serializable]
		public struct SE_
		{
			public AudioClip Button;
			public AudioClip Die;
			public AudioClip Fanfare;
			public AudioClip Jump;
			public AudioClip NormalCollision;
			public AudioClip Star;
		}

		[Serializable]
		public class World_
		{
			[HideInInspector]
			public WorldType Type;
			public string Name;

			public Sprite LoadSprite()
			{
				return Resources.Load<Sprite>("Worlds/world" + Type.ToNumber());
			}
		}

		public SE_ SE;

		public List<World_> World;

		public void Initialize()
		{
			int i = 0;
			foreach (var world in World)
				world.Type = TypeHelper.MakeWorldTypeFromIndex(i++);
		}

		public World_ GetWorld(WorldType type)
		{
			var typeInt = (int)type - 1;
			if (typeInt < 0 || typeInt >= World.Count)
			{
				Debug.LogError("Trying to access World of index: " + typeInt);
				return World[0];
			}

			return World[(int)type - 1];
		}
	}
}