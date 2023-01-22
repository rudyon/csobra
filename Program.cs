using Raylib_cs;
using Newtonsoft.Json;

namespace csobra
{
	class Program
	{
		public static void Main()
		{
			int width, height, gen_size, gen, current, record, gen_divider;
			width = 800;
			height = 800;
			gen_size = 100;
			gen = 0;
			gen_divider = 10;
			current = 0;
			record = 0;
			List<Tuple<int, Snake>> pops = new List<Tuple<int, Snake>>();
			List<Snake> snakes = new List<Snake>();

			Raylib.InitWindow(width, height, "csobra");
			Raylib.SetTargetFPS(9999);

			for (int i = 0; i < gen_size + 1; i++)			
			{
				snakes.Add(new Snake(new Fruit()));
			}

			Snake snake = snakes[current];
			
			while (!Raylib.WindowShouldClose())
			{
				if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
				{
					Raylib.SetTargetFPS(30);
				}
				else if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE))
				{
					Raylib.SetTargetFPS(9999);
				}

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

						record = pops[0].Item1;

						List<Tuple<int, Snake>> new_pops = pops.Take(gen_size / gen_divider).ToList();
						pops = new List<Tuple<int, Snake>>();
						snakes = new List<Snake>();

						for (int i = 0; i < gen_size + 1; i++)
						{
							Snake new_snek = DeepClone(new_pops[Raylib.GetRandomValue(0, new_pops.Count() - 1)].Item2);
							new_snek.Mutate();
							snakes.Add(new_snek);
						}

						gen++;
						current = 0;
					}

					snake = snakes[current];
				}

				Raylib.BeginDrawing();
				Raylib.ClearBackground(Color.BLACK);

				snake.Draw();
				snake.fruit.Draw();

				Raylib.DrawRectangleLines(0, 0, 600, 600, Color.WHITE);

				foreach (var item in snakes)
				{
					Raylib.DrawLine(600, 0 + item.fitness, 800, item.fitness, Color.GRAY);
				}
				
				Raylib.DrawLine(600, 0 + record, 800, record, Color.YELLOW);
				Raylib.DrawLine(600, 0 + snake.fitness, 800, snake.fitness, Color.RED);

				Raylib.DrawText("fitness " + snake.fitness, 20, 600 + 20, 20, Color.WHITE);
				Raylib.DrawText("gen " + gen, 20, 600 + 20*2, 20, Color.WHITE);
				Raylib.DrawText("specimen " + current, 20, 600 + 20*3, 20, Color.WHITE);
				Raylib.DrawText("record " + record, 20, 600 + 20*4, 20, Color.WHITE);

				Raylib.EndDrawing();
			}

			Raylib.CloseWindow();
		}

		public static T DeepClone<T>(T obj)
		{
			// Serialize the object to a JSON string
			string json = JsonConvert.SerializeObject(obj);

			// Deserialize the JSON string to a new object of the same type
			T clone = JsonConvert.DeserializeObject<T>(json);

			return clone;
		}
	}
}