using UnityEngine;
using System.Collections;
using System.IO;
using System.Diagnostics;

public class GraphLoader
{
    public static Texture2D LoadPNG(string filePath)
    {
        Texture2D texture = null;

        if (!File.Exists(filePath))
        {
            UnityEngine.Debug.LogWarning(string.Format("File on path {0} didn't exist.", filePath));
            return null;
        }

        // Load the data from the path
        byte[] fileData = File.ReadAllBytes(filePath);

        // Create a new texture2D
        texture = new Texture2D(2, 2);

        // Load the image to the texture, this automatically resizes the texture
        texture.LoadImage(fileData);

        return texture;
    }

    /// <summary>
    /// Generates a graph from the ceptre log and saves it in the streamingassets folder under given fileName.
    /// </summary>
    /// <param name="fileName">Name of the graph to write</param>
    public static IEnumerator GenerateGraph(string fileName)
    {
        // Remove the extension, as it is added in formatting anyways
        if (fileName.EndsWith(".png"))
            fileName = fileName.Substring(0, fileName.Length - 4);

        // Specify path names
        string folder = "GraphViz";
        string executableName = "dot.exe";

        // Specify the process
        Process graphVizProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = string.Format("{0}/{1}/{2}", Application.persistentDataPath, folder, executableName),
                //Arguments = string.Format("-Tpng {0}/Ceptre/trace.dot -o {0}/{1}.png", Application.persistentDataPath, fileName),
                Arguments = string.Format("-Tpng Ceptre/trace.dot -o {0}.png", fileName),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = Application.persistentDataPath
            }
        };

        // Start the process
        graphVizProcess.Start();

        // Wait for the process to finish
        while (!graphVizProcess.HasExited)
            yield return new WaitForEndOfFrame();

        // Print success!
        UnityEngine.Debug.Log(string.Format("Successfully generated graph with name: {0}.png", fileName));
    }
}