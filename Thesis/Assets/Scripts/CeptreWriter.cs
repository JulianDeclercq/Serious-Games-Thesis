using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Using type aliases for readability
using Rule = System.String;

using Predicate = System.String;
using Type = System.String;
using Instance = System.String;

// Todo: remove monobehaviour when done testing in awake
public class CeptreWriter : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// A list containing all rules.
    /// Example rule: "compliment: $at C L * $at C' L * $likes C C' -o likes C' C.
    /// </summary>
    private List<Rule> _rules = new List<Rule>();

    /// <summary>
    /// A list containing all types.
    /// Example type: "character"
    /// </summary>
    private List<Type> _types = new List<Type>();

    /// <summary>
    /// A list containing all predicates.
    /// Example predicate: "at character location"
    /// </summary>
    private List<Predicate> _predicates = new List<Predicate>();

    /// <summary>
    /// A dictionary where every value is a list of instances of the type of its key.
    /// Example element: _typeInstances["character"] = {"Romeo", "Juliet", "Julian"};
    /// </summary>
    private Dictionary<Type, List<Instance>> _typeInstances = new Dictionary<string, List<string>>();

    #endregion

    private void Awake()
    {
        WriteCeptreFile("testfilebitch");
    }

    private void WriteCeptreFile(string fileName)
    {
        string ceptreDirectory = "Ceptre";
        string outputDirectory = "generatedFiles";
        string path = string.Format(@"{0}\{1}\{2}\{3}.cep", Application.persistentDataPath, ceptreDirectory, outputDirectory, fileName);

        // Create a streamwriter for the file to be written
        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            streamWriter.WriteLine("writing in text file");
        }
    }

    #region Helper methods

    public void AddRule(Rule rule)
    {
        _rules.Add(rule);
    }

    public void AddType(Type type)
    {
        _types.Add(type);
    }

    public void AddPredicate(Predicate predicate)
    {
        _predicates.Add(string.Format("{0} : pred.", predicate));
    }

    public void AddInstance(Type instanceType, Instance instance)
    {
        _typeInstances[instanceType].Add(instance);
    }

    private void WriteRule(string ruleName, string lhs, string rhs, bool addToRules = true)
    {
        string rule = string.Format("{0}: {1} -o {2}.", ruleName, lhs, rhs);
    }

    #endregion
}