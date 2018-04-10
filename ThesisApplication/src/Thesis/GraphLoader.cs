using System;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public class GraphLoader
{
    private static string _resourcesFolder = Path.GetFullPath(@"..\..\resources");

    /// <summary>
    /// Generates a graph from the ceptre log and saves it in the streamingassets folder under given fileName.
    /// </summary>
    /// <param name="outputFileName">Name of the graph to write</param>
    public static void GenerateGraph(string outputFileName)
    {
        // Remove the extension, as it is added in formatting anyways
        if (outputFileName.EndsWith(".png"))
            outputFileName = outputFileName.Substring(0, outputFileName.Length - 4);

        // Specify names and path names
        string executableName = "dot.exe";
        string folder = "GraphViz";

        // Specify the process
        Process graphVizProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = $"{_resourcesFolder}/{folder}/{executableName}",
                Arguments = $"-Tpng Ceptre/trace.dot -o {outputFileName}.png",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = _resourcesFolder
            }
        };

        // Start the process
        graphVizProcess.Start();

        // Print success!
        Console.WriteLine($"Successfully generated graph with name: {outputFileName}.png");
    }

    public static string GenerationPath()
    {
        return _resourcesFolder;
    }
}