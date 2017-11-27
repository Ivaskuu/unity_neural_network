using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        print("Initing the NN...");

        Network nn = new Network(new int[] { 2, 3, 1 });
        //nn.printInfos();
        print(nn.calculate(input: new double[] {1, 0.5})[0]);
    }
}