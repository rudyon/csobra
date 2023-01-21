using Raylib_cs;

namespace csobra
{
	class Program
	{
		public static void Main()
		{
			int width, height;
			width = 600;
			height = 600;

			Raylib.InitWindow(width, height, "csobra");
			Raylib.SetTargetFPS(15);

			Fruit fruit = new Fruit();
			Snake snake = new Snake(fruit);
			
			while (!Raylib.WindowShouldClose())
			{
				snake.Update();

				Raylib.BeginDrawing();
				Raylib.ClearBackground(Color.BLACK);

				snake.Draw();
				fruit.Draw();

				Raylib.EndDrawing();
			}

			Raylib.CloseWindow();
		}
	}
}