using UnityEngine;

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
            this.bias[i] = new double[NETWORK_LAYER_SIZES[i]];

            if(i > 0) // After the input layer
            {
                weights[i] = new double[NETWORK_LAYER_SIZES[i]][];
                for(int j = 0; j < weights[i].Length; j++)
                {
                    weights[i][j] = new double[NETWORK_LAYER_SIZES[i-1]];
                }
            }
        }
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