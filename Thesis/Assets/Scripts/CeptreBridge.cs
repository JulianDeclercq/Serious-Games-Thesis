using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.IO;

public class CeptreBridge : MonoBehaviour
{
    public Text testText;
    public GameObject testObj;

    private void Awake()
    {
        // Copy all files from the streaming assets to the persistent data path
        var streamingAssets = new DirectoryInfo(Application.streamingAssetsPath);
        var persistentDataPath = new DirectoryInfo(Application.persistentDataPath);

        CopyFilesRecursively(streamingAssets, persistentDataPath);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            StartCeptre("hello-world.cep");

        if (Input.GetKeyDown(KeyCode.J))
            StartCeptre("numbers.cep");
    }

    /// <summary>
    /// Generates a graph from the last Ceptre output and puts it as texture
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateTexture()
    {
        // Wait for the graph generation to finish
        yield return StartCoroutine(GraphLoader.GenerateGraph("graph"));

        // Update the texture
        testObj.GetComponent<Renderer>().material.mainTexture = GraphLoader.LoadPNG(string.Format("{0}/graph.png", Application.streamingAssetsPath));
    }

    private void StartCeptre(string ceptreFile)
    {
        // Append the ceptre extension if it wasn't present in the passed filename
        string ceptreExtension = ".cep";
        if (!ceptreFile.EndsWith(ceptreExtension))
            ceptreFile += ceptreExtension;

        // Specify the process
        //string ceptrePath = @"..\..\Ceptre";
        string ceptrePath = string.Format("{0}/Ceptre", Application.persistentDataPath);
        string ceptreFolder = "files";

        Process process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                Verb = "runas", // run as administrator
                FileName = string.Format(@"{0}\ceptre.exe", ceptrePath),
                //Arguments = string.Format(@"{0}\{1}\{2}", ceptrePath, ceptreFolder, ceptreFile),
                Arguments = string.Format(@"{0}\{1}", /*ceptrePath,*/ ceptreFolder, ceptreFile),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = ceptrePath  // When UseShellExecute is false, gets or sets the working directory FOR the process to be started.
                                               // When UseShellExecute is true, gets or sets the directory that CONTAINS the process to be started.
            }
        };

        // Start the process
        process.Start();

        // Print all program output
        Console.WriteLine(process.StandardOutput.ReadToEnd());

        /*// Read the output from the process
        string output = "";

        // Read until the end of the stream (== end of Ceptre process)
        while (!ceptreProcess.StandardOutput.EndOfStream)
        {
            // For formatting purposes
            bool providedInput = false;

            // Peek for a question mark. If a question mark is provided in the Ceptre output, input is expected
            int peek = ceptreProcess.StandardOutput.Peek();
            if ((char)peek == '?')
            {
                // Input choice one
                string choice = "1";
                ceptreProcess.StandardInput.WriteLine(choice);

                // Toggle bool
                providedInput = true;
            }

            // Read the line from the program
            string line = ceptreProcess.StandardOutput.ReadLine();

            // Add endlines for formatting (add one extra if input has been provided to match the original ceptre from the console)
            line += (providedInput) ? "\n\n" : "\n";

            // Add the line to the output, print choice if it is not empty
            output += line;
        }

        if (ceptreProcess.HasExited)
            print("Ceptre finished running.");

        // Update the text box
        //testText.text = output;

        // Print the output
        print(output);*/
    }

    private void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
    {
        foreach (DirectoryInfo dir in source.GetDirectories())
            CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));

        // TODO: Fix overwrite by doing a check to see if it exist and dont copy if it exists
        foreach (FileInfo file in source.GetFiles())
            file.CopyTo(Path.Combine(target.FullName, file.Name), true);
    }
}