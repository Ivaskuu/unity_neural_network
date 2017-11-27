using System; // Math functions
using UnityEngine; // Print function

public class Network : MonoBehaviour
{
    public readonly int[] NETWORK_LAYER_SIZES; // The number of neurons, by each layer
    public readonly int INPUT_SIZE; // The number of input neurons
    public readonly int OUTPUT_SIZE; // The number of output neurons
    public readonly int NETWORK_SIZE; // The total number of layers

    private double[][] output; // [Layer][Neuron], aka Neuron
    private double[][][] weights; // [Layer][Neuron][Previous neuron]
    private double[][] bias; // [Layer][Neuron]

    public Network(int[] NETWORK_LAYER_SIZES)
    {
        this.NETWORK_LAYER_SIZES = NETWORK_LAYER_SIZES;
        this.INPUT_SIZE = NETWORK_LAYER_SIZES[0];
        this.NETWORK_SIZE = NETWORK_LAYER_SIZES.Length;
        this.OUTPUT_SIZE = NETWORK_LAYER_SIZES[NETWORK_SIZE - 1]; // -1 becuase arrays start at 0

        this.output = new double[NETWORK_SIZE][];
        this.weights = new double[NETWORK_SIZE][][];
        this.bias = new double[NETWORK_SIZE][];

        // Init the neurons, weights and biases arrays
        for(int i = 0; i < NETWORK_SIZE; i++)
        {
            this.output[i] = new double[NETWORK_LAYER_SIZES[i]];
            this.bias[i] = NetworkTools.createRandomDoubleArray(size: NETWORK_SIZE, minValue: 1, maxValue: 1);

            if(i > 0) // After the input layer
            {
                weights[i] = new double[NETWORK_LAYER_SIZES[i]][];
                for(int j = 0; j < weights[i].Length; j++) // For each neuron in this layer
                {
                    // Assign random values to the weights from the previous layer neuron to this one
                    weights[i][j] = NetworkTools.createRandomDoubleArray(size: NETWORK_LAYER_SIZES[i-1], minValue: -1, maxValue: 1);
                }
            }
        }
    }

    public double[] calculate(double[] input)
    {
        if(input.Length != INPUT_SIZE) return null;
        
        this.output[0] = input;
        for (int layer = 1; layer < NETWORK_SIZE; layer++) // For each layer except the input one 
        {
            // For each neuron in this layer
            for (int neuron = 0; neuron < NETWORK_LAYER_SIZES[layer]; neuron++)
            {
                // [1] Calculate the sum
                double sum = bias[layer][neuron];
                for (int prevNeuron = 0; prevNeuron < NETWORK_LAYER_SIZES[layer-1]; prevNeuron++)
                {
                    double prevNeuronOutput = output[layer-1][prevNeuron];
                    double prevNeuronWeight = weights[layer][neuron][prevNeuron];

                    sum += prevNeuronOutput * prevNeuronWeight;
                }

                // [2] Calculate the activation
                output[layer][neuron] = sigmoid(neuronOutput: sum);
            }
        }
        
        return output[NETWORK_SIZE - 1]; // Arrays start at 0
    }

    // Activation function
    private double sigmoid(double neuronOutput)
    {
        return 1d / (1 + Math.Exp(-neuronOutput));
    }

    public void printInfos()
    {
        print("Num of layers: " + NETWORK_SIZE);
        print("Num of input neurons: " + INPUT_SIZE);
        print("Num of output neurons: " + OUTPUT_SIZE);

        for(int i = 0; i < NETWORK_SIZE; i++)
        {
            print("Layer " + i);

            for(int j = 0; j < NETWORK_LAYER_SIZES[i]; j++)
            {
                print("Neuron " + i + "/" + j);
            }
        }

        print("Weights [1][0][0] : " + weights.Length + " layers/" + weights[1].Length + "neurons in the 1st layer/" + weights[1][0].Length + "neurons in the 0th layer");
    }
}