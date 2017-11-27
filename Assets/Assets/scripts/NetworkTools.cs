using System;

public static class NetworkTools
{
    private static Random random = new Random();

    public static double[] createRandomDoubleArray(int size, double minValue, double maxValue)
    {
        if(size <= 0) return null;

        double[] array = new double[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.NextDouble() * (maxValue - minValue) + minValue;
        }

        return array;
    }
}