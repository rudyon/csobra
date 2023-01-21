using Raylib_cs;

namespace csobra
{
	internal class Tail
	{
		public int x, y;

		public Tail(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		internal void Draw()
		{
			Raylib.DrawRectangle(x * 15 + 2, y * 15 + 2, 11, 11, Color.WHITE);
		}
	}
}