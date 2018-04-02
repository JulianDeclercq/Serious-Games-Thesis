using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class ProjectBuilder
{
    [MenuItem("MyTools/Windows Build With Postprocess")]
    public static void BuildGame()
    {
        // Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
        string[] levels = new string[] { "Assets/Scene1.unity", "Assets/Scene2.unity" };

        // Build player.
        BuildPipeline.BuildPlayer(levels, path + "/BuiltGame.exe", BuildTarget.StandaloneWindows, BuildOptions.Development);

        // Copy a file from the project folder to the build folder, alongside the built game.
        //FileUtil.CopyFileOrDirectory("Assets/Templates/Readme.txt", path + "Readme.txt");
        var streamingAssets = new DirectoryInfo(Application.streamingAssetsPath);
        var persistentDataPath = new DirectoryInfo(Application.persistentDataPath);

        CopyFilesRecursively(streamingAssets, persistentDataPath);

        // run test
        File.WriteAllText(Application.persistentDataPath + "/Ceptrebridge.txt", "kankerjoden");

        // Run the game (Process class from System.Diagnostics).
        Process process = new Process();
        process.StartInfo.FileName = path + "/BuiltGame.exe";
        process.Start();
    }

    private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
    {
        foreach (DirectoryInfo dir in source.GetDirectories())
            CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));

        // TODO: Fix overwrite by doing a check to see if it exist and dont copy if it exists
        foreach (FileInfo file in source.GetFiles())
            file.CopyTo(Path.Combine(target.FullName, file.Name), true);
    }
}