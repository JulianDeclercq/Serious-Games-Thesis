using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// Using type aliases for readability
using Rule = System.String;
using Stage = System.Collections.Generic.List<string>; // string is a Rule in this case

using Predicate = System.String;
using Type = System.String;
using Instance = System.String;

// An initial state is a collection of ground predicates, this means predicates that are filled in
// Ground predicate example "at Romeo House", predicate example "at Character Location"
using Context = System.Collections.Generic.List<string>;

// Todo: remove monobehaviour when done testing in awake
public class CeptreWriter : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// A list containing all types.
    /// Example type: "character"
    /// </summary>
    private List<Type> _types = new List<Type>();

    /// <summary>
    /// A list containing all objects.
    /// Object example: "weapon"
    /// </summary>
    private List<string> _objects = new List<string>();

    /// <summary>
    /// A list containing all predicates.
    /// Example predicate: "at character location"
    /// </summary>
    private List<Predicate> _predicates = new List<Predicate>();

    /// <summary>
    /// A dictionary where every value is a list of rules that form the stage, the KEY is the NAME of the stage
    /// A stage contains rules and can be run.
    /// A stage is basically a list of rules.
    /// </summary>
    private Dictionary<string, Stage> _stages = new Dictionary<string, Stage>();

    /// <summary>
    /// A dictionary where every value is a list of instances of the type of its key.
    /// Example element: _typeInstances["character"] = {"Romeo", "Juliet", "Julian"};
    /// </summary>
    private Dictionary<Type, List<Instance>> _typeInstances = new Dictionary<string, List<string>>();

    /// <summary>
    /// A dictionary where every value is an initial state, the KEY is the NAME of the initial state
    /// </summary>
    private Dictionary<string, Context> _initialStates = new Dictionary<string, Context>();

    /// <summary>
    /// The trace to run this program.
    /// </summary>
    private string _trace = "";

    #endregion

    private void Awake()
    {
        HelloWorld();
        WriteCeptreFile("helloWorldGenerated");
    }

    private void WriteCeptreFile(string fileName)
    {
        string ceptreDirectory = "Ceptre";
        string outputDirectory = "generatedFiles";
        string path = string.Format(@"{0}\{1}\{2}\{3}.cep", Application.persistentDataPath, ceptreDirectory, outputDirectory, fileName);

        // Safety check, a trace is required to write a program
        if (string.IsNullOrEmpty(_trace))
        {
            UnityEngine.Debug.LogError("Can't write file, TRACE is not specified");
            return;
        }

        // Create a streamwriter for the file to be written
        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            // Write comment that this is a generated file
            streamWriter.WriteLine("File generated. Visit https://github.com/JulianDeclercq/Serious-Games-Thesis for source code on the generation.");

            // Readability
            streamWriter.WriteLine("");

            // Write all types
            foreach (Type type in _types)
                streamWriter.WriteLine("{0} : type.", type);

            // Readability
            if (_types.Any())
                streamWriter.WriteLine("");

            // Write all objects
            foreach (string obj in _objects)
                streamWriter.WriteLine("{0} : object.", obj);

            // Write all predicates
            foreach (Predicate predicate in _predicates)
                streamWriter.WriteLine("{0} : pred.", predicate);

            // Readability
            streamWriter.WriteLine("");

            // Write all stages
            foreach (var stage in _stages)
            {
                // Open the stage body
                streamWriter.WriteLine("stage {0} = {1}", stage.Key, "{"); // formatting has to be done like this because { can't be used or escaped in formatted string

                // Write all the rules, rules are already formatted correctly through the CreateRule() method
                foreach (Rule rule in stage.Value)
                    streamWriter.WriteLine(rule);

                // Close the stage body
                streamWriter.WriteLine("}");
            }

            // Readability
            streamWriter.WriteLine("");

            // Write all initial states (initial state and context are the same thing)
            foreach (var context in _initialStates)
            {
                // Open the context body
                streamWriter.WriteLine("context {0} = {1}", context.Key, "{"); // formatting has to be done like this because { can't be used or escaped in formatted string

                // Write all the (ground) predicates this context contains
                for (int i = 0; i < context.Value.Count; ++i)
                {
                    // Add a comma if it is not the last in the list, add newline after every 5 elements
                    string line = string.Format("{0}{1}{2}", context.Value[i],
                        (i != context.Value.Count - 1) ? "," : string.Empty,
                        (i != 0 && i % 5 == 0) ? Environment.NewLine : string.Empty);

                    streamWriter.Write(line);
                }

                // Close the context body
                streamWriter.WriteLine("}.");
            }

            // Readability
            streamWriter.WriteLine("");

            // Write the trace
            streamWriter.WriteLine(_trace);
        }

        print(string.Format("Done writing file {0}", path));
    }

    #region Helper methods

    public void AddInitialState(string name, Context initialState)
    {
        _initialStates.Add(name, initialState);
    }

    public void AddStage(string stageName, Stage stage)
    {
        _stages.Add(stageName, stage);
    }

    public void AddPredicate(Predicate predicate)
    {
        _predicates.Add(predicate);
    }

    /// <summary>
    /// Instance == ground predicate
    /// </summary>
    /// <param name="instanceType">The type of this instance.</param>
    /// <param name="instance">The name of this instance.</param>
    /// Example: instanceType = "Character", instance = "Romeo"
    public void AddInstance(Type instanceType, Instance instance)
    {
        _typeInstances[instanceType].Add(instance);
    }

    private Rule CreateRule(string ruleName, string lhs, string rhs)
    {
        return string.Format("{0}: {1} -o {2}.", ruleName, lhs, rhs);
    }

    private void CreateTrace(string stageName, string contextName)
    {
        if (!string.IsNullOrEmpty(_trace))
        {
            UnityEngine.Debug.LogWarning("Failed to create trace, trace already existed!");
            return;
        }

        // Format the trace
        _trace = string.Format("#trace _ {0} {1}.", stageName, contextName);
    }

    #endregion

    #region Program writers

    private void HelloWorld()
    {
        AddPredicate("a");
        AddPredicate("b");

        Rule rule = CreateRule("rule", "a", "b");
        AddStage("main", new List<Rule> { rule });

        Context init = new Context { "a", "a", "a" };
        AddInitialState("init", init);

        CreateTrace("main", "init");
    }

    #endregion
}