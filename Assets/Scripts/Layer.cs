using UnityEngine;
using System.Collections.Generic;
public class Layer : MonoBehaviour
{
    public int neuronCount;
    public List<Neuron> neurons = new List<Neuron>();

    public Layer(int numberOfNeurons, int inputNumberOfNeurons)
    {
        neuronCount = numberOfNeurons;
        for(int i = 0; i < numberOfNeurons; i++)
        {
            neurons.Add(new Neuron(inputNumberOfNeurons));
        }
    }
}
