using System;
using UnityEngine;

namespace BB
{
	public enum X { }
	public enum Y { }

	[Serializable]
	public struct Coor
	{
		[SerializeField]
		private int _x;
		public X X
		{
			get { return (X)_x; }
			set { _x = (int)value; }
		}

		[SerializeField]
		private int _y;
		public Y Y
		{
			get { return (Y)_y; }
			set { _y = (int)value; }
		}

		public Coor(int x, int y)
		{
			_x = x;
			_y = y;
		}

		public Vector2 ToVector2()
		{
			return new Vector2(_x / 2f + 0.25f, _y / 2f + 0.25f);
		}

		public override int GetHashCode()
		{
			var hash = -1047578147;
			hash = (hash * -1521134295) + _x;
			hash = (hash * -1521134295) + _y;
			return hash;
		}
	}
}