using Raylib_cs;

namespace csobra
{
	class Program
	{
		public static void Main()
		{
			int width, height, gen_size, gen, current, record;
			width = 800;
			height = 800;
			gen_size = 10;
			gen = 0;
			current = 0;
			record = 0;
			List<Tuple<int, Snake>> pops = new List<Tuple<int, Snake>>();
			List<Snake> snakes = new List<Snake>();

			Raylib.InitWindow(width, height, "csobra");
			Raylib.SetTargetFPS(30);

			for (int i = 0; i < gen_size + 1; i++)			
			{
				snakes.Add(new Snake(new Fruit()));
			}

			Snake snake = snakes[current];
			
			while (!Raylib.WindowShouldClose())
			{
				if (snake.Update())
				{
					if (current < gen_size)
					{
						pops.Add(new Tuple<int, Snake>(snake.fitness, snake));
						current++;
					}
					else
					{
						pops = pops.OrderByDescending(i => i.Item1).ToList();

						if (pops[0].Item1 > record)
						{
							record = pops[0].Item1;
						}

						List<Tuple<int, Snake>> new_pops = pops.Take(gen_size / 3).ToList();
						pops = new List<Tuple<int, Snake>>();
						snakes = new List<Snake>();

						foreach (var item in new_pops)
						{
							item.Item2.Mutate();
							snakes.Add(item.Item2);
						}

						for (int i = 0; i < (gen_size + 1 - gen_size / 3); i++)
						{
							snakes.Add(new Snake(new Fruit()));
						}

						gen++;
						current = 0;
					}

					Console.WriteLine(snake.fitness);
					snake = snakes[current];
				}

				Raylib.BeginDrawing();
				Raylib.ClearBackground(Color.BLACK);

				snake.Draw();
				snake.fruit.Draw();

				Raylib.DrawRectangleLines(0, 0, 600, 600, Color.WHITE);
				Raylib.DrawText("fitness " + snake.fitness, 20, 600 + 20, 20, Color.WHITE);
				Raylib.DrawText("gen " + gen, 20, 600 + 20*2, 20, Color.WHITE);
				Raylib.DrawText("specimen " + current, 20, 600 + 20*3, 20, Color.WHITE);
				Raylib.DrawText("record " + record, 20, 600 + 20*4, 20, Color.WHITE);

				Raylib.EndDrawing();
			}

			Raylib.CloseWindow();
		}
	}
}