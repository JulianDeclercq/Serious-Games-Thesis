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
            StartCeptre("hello-world.cep");
    }

    private void StartCeptre(string ceptreFile)
    {
        // Append the ceptre extension if it wasn't present in the passed filename
        string ceptreExtension = ".cep";
        if (!ceptreFile.EndsWith(ceptreExtension))
            ceptreFile += ceptreExtension;

        // Specify path names
        string ceptreFolder = "Ceptre";
        string ceptreFilesFolder = "files";
        //string executableName = "ExeSimulator.exe";
        string executableName = "ceptre.exe";

        // Specify the process
        Process ceptreProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = string.Format("{0}/{1}/{2}", Application.streamingAssetsPath, ceptreFolder, executableName),
                Arguments = string.Format("{0}/{1}/{2}/{3}", Application.streamingAssetsPath, ceptreFolder, ceptreFilesFolder, ceptreFile),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
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
        string output = "";
        while (!ceptreProcess.StandardOutput.EndOfStream)
        {
            // Read the line from the program
            output += ceptreProcess.StandardOutput.ReadLine() + '\n';
        }

        // Update the text box
        testText.text = output;

        // Print the output
        print(output);
    }
}