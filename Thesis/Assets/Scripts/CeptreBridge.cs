using System;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.IO;

public class CeptreBridge : MonoBehaviour
{
    public Text testText;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            StartCeptre();
    }

    private void StartCeptre()
    {
        // Create the path to the executable from the relative path and the executable name
        string path = Path.GetFullPath(@"..\Thesis\Assets\StreamingAssets\") + "ExeSimulator.exe";

        // Specify the process
        Process ceptreProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = path,
                Arguments = "Julian 125",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        // Start the process
        try
        {
            ceptreProcess.Start();
        }
        catch (Exception e)
        {
            print("Error: " + e.Message);
            return;
        }

        // Read the output from the process
        while (!ceptreProcess.StandardOutput.EndOfStream)
        {
            // Read the line from the program
            string line = ceptreProcess.StandardOutput.ReadLine() + '\n';

            // Update the text box
            testText.text += line;

            // Print the line
            print(line);
        }
    }
}