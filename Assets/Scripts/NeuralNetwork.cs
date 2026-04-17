using UnityEngine;
using System.Collections.Generic;
public class NeuralNetwork 
{
    public int inputsCount;
    public int outputsCount;
    public int hiddenLayersCount;
    public int neuronsPerHidden;
    public double LR; // learning rate
    List<Layer> layers = new List<Layer>();

    public NeuralNetwork(int nrI, int nrO, int nrH, int nrNPH, double lr)
    {
        inputsCount= nrI;
        outputsCount= nrO;
        hiddenLayersCount= nrH;
        neuronsPerHidden= nrNPH;
        LR = lr;

        if(hiddenLayersCount > 0)
        {
            layers.Add(new Layer(neuronsPerHidden, inputsCount));

            for(int i = 0; i < hiddenLayersCount-1; i++)
            {
                layers.Add(new Layer(neuronsPerHidden, neuronsPerHidden));
            }
            layers.Add(new Layer(outputsCount, neuronsPerHidden));
        }

        else
        {
            layers.Add(new Layer(outputsCount, inputsCount));
        }
    }
    public List<double> ForwardPropagation(List<double> inputValues, List<double> desiredOutput)
    {
        List<double> inputs = new List<double>();
        List<double> outputValues = new List<double>();
        int currentInput = 0;

        if(inputValues.Count != inputsCount)
        {
            Debug.Log("wrong number of inputs");
            return outputValues;
        }
        inputs = new List<double>(inputValues);

        for(int i = 0; i <hiddenLayersCount + 1; i++) // consider output layer aswell
        {
            if (i > 0)
            {
                inputs = new List<double>(outputValues);
            }
            outputValues.Clear();


            for(int j = 0; j < layers[i].neuronCount; j++)
            {
                double N = 0;
                layers[i].neurons[j].inputs.Clear();

                for(int k=0; k< layers[i].neurons[j].inputsCount; k++)
                {
                    layers[i].neurons[j].inputs.Add(inputs[currentInput]);
                    N += layers[i].neurons[j].weights[k] * inputs[currentInput];
                    currentInput++;
                }
                N += layers[i].neurons[j].bias;

                layers[i].neurons[j].output = ActivationFunction(N);

                outputValues.Add(layers[i].neurons[j].output);
                currentInput = 0;
            }
        }
        return outputValues;
    }


    void BackPropagation(List<double> outputs, List<double> desiredOutput)
    {
        double error;
        for(int i = hiddenLayersCount; i>=0; i--)
        {
            for(int j=0; j < layers[i].neuronCount; j++)
            {
                if(i == hiddenLayersCount)
                {
                    error = desiredOutput[j] - outputs[j];
                    layers[i].neurons[j].errorGradient = (1 - outputs[j] * outputs[j]) * error;
                }

                else
                {
                    layers[i].neurons[j].errorGradient =   (1 - layers[i].neurons[j].output * layers[i].neurons[j].output);
                    double gradientSum = 0;
                    for(int p = 0; p < layers[i+1].neuronCount; p++)
                    {
                        gradientSum += layers[i + 1].neurons[p].errorGradient * layers[i + 1].neurons[p].weights[j];
                    }
                    layers[i].neurons[j].errorGradient *= gradientSum;
                }
                for(int k=0; k < layers[i].neurons[j].inputsCount; k++)
                {
                    if(i == hiddenLayersCount)
                    {
                        error = desiredOutput[j] - outputs[j];
                        layers[i].neurons[j].weights[k] += LR * layers[i].neurons[j].inputs[k] * error;
                    }
                    else
                    {
                        layers[i].neurons[j].weights[k] += LR * layers[i].neurons[j].inputs[k] * layers[i].neurons[j].errorGradient;
                    }
                }
                layers[i].neurons[j].bias += LR * -1 * layers[i].neurons[j].errorGradient;

            }
        }
    }

    public List<double> Train(List<double> inputValues, List<double> desiredOutput)
    {
        List<double> outputValues = new List<double>();
        outputValues = ForwardPropagation(inputValues, desiredOutput);
        BackPropagation(outputValues, desiredOutput);
        return outputValues;
    }
    double ActivationFunction(double value)
    {
        return TanH(value);
    }
    double TanH(double value)
    {
        double k = (double)System.Math.Exp(-2 * value);
        return 2 / (1.0 + k) - 1;
    }


}
