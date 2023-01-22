using Raylib_cs;

namespace csobra
{
	internal class Fruit
	{
		internal int x, y;

		public Fruit()
		{
			Move();
		}

		internal void Draw()
		{
			Raylib.DrawRectangle(x * 15 + 2, y * 15 + 2, 11, 11, Color.RED);
		}

		internal void Move()
		{
			x = Raylib.GetRandomValue(1, 38);
			y = Raylib.GetRandomValue(1, 38);
		}
	}
}