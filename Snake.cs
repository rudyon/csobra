using Raylib_cs;

namespace csobra
{
	internal class Snake
	{
		internal int x, y;
		public int direction;
		public int fitness;
		public List<Tail> tails = new List<Tail>();
		public Fruit fruit;
		NeuralNetwork neural_network = new NeuralNetwork(8, 6, 4);

		public Snake(Fruit fruit)
		{
			this.x = 15;
			this.y = 15;
			this.direction = 0;
			this.fruit = fruit;

			tails.Add(new Tail(x - 1, y));
		}

		internal void Draw()
		{
			Raylib.DrawRectangle(x * 15 + 2, y * 15 + 2, 11, 11, Color.YELLOW);

			for (int i = 0; i < tails.Count(); i++)
			{
				tails[i].Draw();
			}
		}

		internal bool Update()
		{
			fitness++;

			double[] inputs = {Input1(), Input2(), Input3(), Input4(), Input5(), Input6(), Input7(), Input8()};
			double output = neural_network.Run(inputs);

			if (output == 0 && direction != 2)
			{
				direction = 0;
			}
			else if (output == 1 && direction != 3)
			{
				direction = 1;
			}
			else if (output == 2 && direction != 0)
			{
				direction = 2;
			}
			else if (output == 3 && direction != 1)
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
					return true;
				}
			}

			tails.RemoveAt(0);

			if (x == fruit.x && y == fruit.y)
			{
				fruit.Move();
				fitness += 60;

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

			return false;
		}

		public void Mutate()
		{
			this.x = 15;
			this.y = 15;
			this.direction = 0;
			tails = new List<Tail>();

			tails.Add(new Tail(x - 1, y));
			neural_network.Mutate();
		}

		public void DrawNeuralNetowk()
		{

		}

		// Wall inputs
		internal double Input1()
		{
			for (int i = 0; i < 5; i++)
			{
				if (x + i > 39)
				{
					return 1;
				}
			}

			return 0;
		}

		internal double Input2()
		{
			for (int i = 0; i < 5; i++)
			{
				if (y + i > 39)
				{
					return 1;
				}
			}

			return 0;
		}

		internal double Input3()
		{
			for (int i = 0; i < 5; i++)
			{
				if (x - i < 0)
				{
					return 1;
				}
			}

			return 0;
		}

		internal double Input4()
		{
			for (int i = 0; i < 5; i++)
			{
				if (y - i < 0)
				{
					return 1;
				}
			}

			return 0;
		}

		// Food inputs
		internal double Input5()
		{
			for (int i = 0; i < 5; i++)
			{
				if (x + i == fruit.x)
				{
					return 1;
				}
			}

			return 0;
		}

		internal double Input6()
		{
			for (int i = 0; i < 5; i++)
			{
				if (y + i > fruit.y)
				{
					return 1;
				}
			}

			return 0;
		}

		internal double Input7()
		{
			for (int i = 0; i < 5; i++)
			{
				if (x - i < fruit.x)
				{
					return 1;
				}
			}

			return 0;
		}

		internal double Input8()
		{
			for (int i = 0; i < 5; i++)
			{
				if (y - i < fruit.y)
				{
					return 1;
				}
			}

			return 0;
		}
	}
}