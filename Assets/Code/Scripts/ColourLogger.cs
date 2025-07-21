using System.Collections.Generic;
using UnityEngine;


public static class ColourLogger
{
    private static Dictionary<Object, string> _registeredClasses = new Dictionary<Object, string>();

    private static readonly string warningColour = "#F1C40F";
    private static readonly string errorColour = "#C0392B";

    public static void RegisterColour(Object a_className, string a_assignedColour)
    {
#if UNITY_EDITOR
        if (_registeredClasses.ContainsKey(a_className))
        {
            return;
        }

        _registeredClasses.Add(a_className, a_assignedColour);
#endif
    }

    public static void Log(Object a_className, string a_message)
    {

#if UNITY_EDITOR
        string colour = GetColour(a_className);
        Debug.Log($"<color={colour}>[{a_className.name}] {a_message}</color>");
#endif
    }

    public static void LogWarning(Object a_className, string a_message)
    {
#if UNITY_EDITOR
        string colour = GetColour(a_className);
        Debug.LogWarning($"<color={colour}>[{a_className.name}]</color> <color={warningColour}>{a_message}</color>");
#endif
    }

    public static void LogError(Object a_className, string a_message)
    {
#if UNITY_EDITOR
        string colour = GetColour(a_className);
        Debug.LogError($"<color={colour}>[{a_className.name}]</color> <color={errorColour}>{a_message}</color>");
#endif
    }

    private static string GetColour(Object a_className)
    {
        return _registeredClasses.ContainsKey(a_className) ? _registeredClasses[a_className] : "white";
    }
}

