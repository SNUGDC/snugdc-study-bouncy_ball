using System;
using UnityEngine;

namespace BB
{
	public static class UserLevelClear
	{
		private const string UnlockedKey = "LevelUnlocked";

		public struct WorldAndLevel
		{
			public WorldType World;
			public Level Level;

			public WorldAndLevel(WorldType world, Level level)
			{
				World = world;
				Level = level;
			}

			public WorldAndLevel? Next()
			{
				if (Level != Const.LevelMax)
					return new WorldAndLevel(World, Level + 1);
				if (World != Const.WorldMax)
					return new WorldAndLevel(World + 1, (Level)1);
				return null;
			}
		}

		public static WorldAndLevel? Unlocked
		{
			get
			{
				if (!PlayerPrefs.HasKey(UnlockedKey))
				{
					var clearState = Get((WorldType) 1, (Level) 1);
					if (clearState.HasValue)
						return null;

					var ret = new WorldAndLevel((WorldType) 1, (Level) 1);
					Unlocked = ret;
					return ret;
				}

				var unlockedStr = PlayerPrefs.GetString(UnlockedKey);
				var levelUnlocked = unlockedStr.Split('_');
				var world = (WorldType)int.Parse(levelUnlocked[0]);
				var level = (Level)int.Parse(levelUnlocked[1]);
				return new WorldAndLevel(world, level);
			}

			set
			{
				if (value == null)
				{
					PlayerPrefs.DeleteKey(UnlockedKey);
					return;
				}

				var worldAndLevel = value.Value;
				var worldAndLevelStr = worldAndLevel.World.ToNumber() + "_" + worldAndLevel.Level.ToNumber();
				PlayerPrefs.SetString(UnlockedKey, worldAndLevelStr);
			}
		}

		private static string MakeKey(WorldType world, Level level)
		{
			return "LevelClear." + world.ToNumber() + "_" + level.ToNumber();
		}

		public static LevelClearState? Get(WorldType world, Level level)
		{
			var key = MakeKey(world, level);
			if (!PlayerPrefs.HasKey(key))
				return null;
			return (LevelClearState)Enum.Parse(typeof (LevelClearState), PlayerPrefs.GetString(key));
		}

		public static void Set(WorldType world, Level level, LevelClearState state)
		{
			PlayerPrefs.SetString(MakeKey(world, level), state.ToString());
		}

		public static void Win(LevelDef levelDef)
		{
			var oldClearState = Get(levelDef.World, levelDef.Level);
			var newClearState = levelDef.Difficulty == Difficulty.Easy ? LevelClearState.Easy : LevelClearState.Hard;

			if (oldClearState.HasValue)
			{
				var oldClearStateValue = oldClearState.Value;
				if (oldClearStateValue == LevelClearState.Hard && levelDef.Difficulty == Difficulty.Hard)
				{
					newClearState = LevelClearState.Perfect;
				}
				else if (levelDef.Difficulty == Difficulty.Easy)
				{
					return;
				}
			}

			Set(levelDef.World, levelDef.Level, newClearState);
			Unlocked = new WorldAndLevel(levelDef.World, levelDef.Level).Next();
		}
	}
}