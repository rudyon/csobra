using Raylib_cs;

namespace csobra
{
	internal class Snake
	{
		internal int x, y;
		public int direction, direction_prev;
		List<Tail> tails = new List<Tail>();
		Fruit fruit;

		public Snake(Fruit fruit)
		{
			this.x = 15;
			this.y = 15;
			this.direction = 0;
			this.fruit = fruit;
		}

		internal void Draw()
		{
			Raylib.DrawRectangle(x * 15 + 2, y * 15 + 2, 11, 11, Color.YELLOW);

			for (int i = 0; i < tails.Count(); i++)
			{
				tails[i].Draw();
			}
		}

		internal void Update()
		{
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
			{
				direction = 0;
			}
			else if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
			{
				direction = 1;
			}
			else if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
			{
				direction = 2;
			}
			else if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
			{
				direction = 3;
			}

			if (direction == 0)
			{
				x += 1;
				tails.Add(new Tail(x - 1, y));
			}
			else if (direction == 1)
			{
				y += 1;
				tails.Add(new Tail(x, y - 1));
			}
			else if (direction == 2)
			{
				x -= 1;
				tails.Add(new Tail(x + 1, y));
			}
			else if (direction == 3)
			{
				y -= 1;
				tails.Add(new Tail(x, y + 1));
			}

			for (int i = 0; i < tails.Count(); i++)
			{
				if ((x == tails[i].x && y == tails[i].y) || (x > 39 || y > 39 || x < 0 || y < 0))
				{
					Raylib.CloseWindow();
				}
			}

			tails.RemoveAt(0);

			if (x == fruit.x && y == fruit.y)
			{
				fruit.Move();

				if (direction == 0)
				{
					x += 1;
					tails.Add(new Tail(x - 1, y));
				}
				else if (direction == 1)
				{
					y += 1;
					tails.Add(new Tail(x, y - 1));
				}
				else if (direction == 2)
				{
					x -= 1;
					tails.Add(new Tail(x + 1, y));
				}
				else if (direction == 3)
				{
					y -= 1;
					tails.Add(new Tail(x, y + 1));
				}
			}
		}
	}
}