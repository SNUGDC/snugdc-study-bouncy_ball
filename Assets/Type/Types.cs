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
	}
}
