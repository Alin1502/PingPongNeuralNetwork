using System.Collections.Generic;
using UnityEngine;

public class Neuron 
{
    public int inputsCount;
    public double bias;
    public double output;
    public double errorGradient;
    public List<double> inputs = new List<double>();
    public List<double> weights = new List<double>();

    public Neuron(int iCount)
    {
        float valueRange = (float)1.0 / (float) iCount;
        bias = UnityEngine.Random.Range(-valueRange, valueRange);
        inputsCount = iCount;

        for (int i = 0; i < inputsCount; i++) {
            weights.Add(UnityEngine.Random.Range(-valueRange, valueRange));
        }

    }
}
