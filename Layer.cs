using Raylib_cs;

namespace csobra
{
	internal class Layer
	{
		private int nodes_in, nodes_out;
		double[,] weights;
		double[] biases;

		public Layer(int nodes_in, int nodes_out)
		{
			this.nodes_in = nodes_in;
			this.nodes_out = nodes_out;

			weights = new double[nodes_in, nodes_out];

			for (int i = 0; i < weights.GetLength(0); i++)
			{
				for (int j = 0; j < weights.GetLength(1); j++)
				{
					weights[i, j] = Raylib.GetRandomValue(-100, 100);
				}
			}

			biases = new double[nodes_out];

			for (int i = 0; i < biases.Length; i++)
			{
				biases[i] = Raylib.GetRandomValue(-100, 100);
			}
		}

		public double[] Outputs(double[] inputs)
		{
			double[] weighted_inputs = new double[nodes_out];

			for (int i = 0; i < nodes_out; i++)
			{
				double weighted_input = biases[i];

				for (int j = 0; j < nodes_in; j++)
				{
					weighted_input += inputs[j] * weights[j, i];
				}

				weighted_inputs[i] = Activation(weighted_input);
			}

			return weighted_inputs;
		}

		double Activation(double weighted_input)
		{
			return 1 / (1 + Math.Exp(-weighted_input));
		}

		internal void Mutate(int mutation_rate)
		{
			for (int i = 0; i < weights.GetLength(0); i++)
			{
				for (int j = 0; j < weights.GetLength(1); j++)
				{
					weights[i, j] += Raylib.GetRandomValue(-mutation_rate, mutation_rate);
				}
			}

			for (int i = 0; i < biases.Length; i++)
			{
				biases[i] += Raylib.GetRandomValue(-mutation_rate, mutation_rate);
			}
		}
	}
}