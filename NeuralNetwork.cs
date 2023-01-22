using System.Linq;

namespace csobra
{
	internal class NeuralNetwork
	{	
		Layer[] layers;

		public NeuralNetwork(params int[] layerSizes)
		{
			layers = new Layer[layerSizes.Length - 1];
			
			for (int i = 0; i < layers.Length; i++)
			{
				layers[i] = new Layer(layerSizes[i], layerSizes[i + 1]);
			}
		}

		double[] Outputs(double[] inputs)
		{
			foreach (Layer layer in layers)
			{
				inputs = layer.Outputs(inputs);
			}

			return inputs;
		}

		public int Run(double[] inputs)
		{
			double[] outputs = Outputs(inputs);
			
 			return outputs.ToList().IndexOf(outputs.Max());
		}

		public void Mutate()
		{
			for (int i = 0; i < layers.Length; i++)
			{
				layers[i].Mutate();
			}
		}
	}
}