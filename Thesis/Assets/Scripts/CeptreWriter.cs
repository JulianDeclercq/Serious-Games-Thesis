using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CeptreWriter : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void WriteCeptreFile(string fileName)
    {
        // TODO: correct path :p
        using (StreamWriter streamWriter = new StreamWriter(string.Format("{0}.cep", fileName)))
        {
            streamWriter.WriteLine("writing in text file");
        }
    }
}