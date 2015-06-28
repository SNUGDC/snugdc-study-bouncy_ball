using System;

namespace BB
{
	public enum WorldType { }

	public enum Level { }

	[Serializable]
	public enum Difficulty
	{
		Easy, Hard,
	}

	[Serializable]
	public enum LevelClearState
	{
		Easy, Hard, Perfect,
	}

	public struct LevelDef
	{
		public WorldType World;
		public Level Level;
		public Difficulty Difficulty;

		public LevelDef(WorldType world, Level level, Difficulty difficulty)
		{
			World = world;
			Level = level;
			Difficulty = difficulty;
		}

		public LevelDef? Next()
		{
			if (Level < Const.LevelMax)
				return new LevelDef(World, Level + 1, Difficulty);

			if (World < Const.WorldMax)
				return new LevelDef(World + 1, (Level) 1, Difficulty);

			return null;
		}
	}

	public static class TypeHelper
	{
		public static WorldType MakeWorldTypeFromIndex(int idx)
		{
			return (WorldType)(idx + 1);
		}

		public static int ToIndex(this WorldType thiz)
		{
			return (int)thiz - 1;
		}

		public static int ToNumber(this WorldType thiz)
		{
			return (int)thiz;
		}

		public static Level MakeLevelFromIndex(int idx)
		{
			return (Level)(idx + 1);
		}

		public static int ToNumber(this Level thiz)
		{
			return (int)thiz;
		}
	}
}
