using UnityEditor;
using System.Reflection;
using System;

[CustomEditor(typeof(SFXPlayer))]
public class SFXPlayerEditor : Editor
{
    private string[] sfxNames;
    private int selectedSfxIndex;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        sfxNames = GetConstFieldNames(typeof(SFXIDs));

        selectedSfxIndex = EditorGUILayout.Popup("SFX Name", selectedSfxIndex, sfxNames);

        SFXPlayer sfxPlayer = (SFXPlayer)target;
        sfxPlayer.nameToPlay = sfxNames[selectedSfxIndex];
    }

    private static string[] GetConstFieldNames(Type type)
    {
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        string[] fieldNames = new string[fields.Length];

        for (int i = 0; i < fields.Length; i++)
        {
            fieldNames[i] = (string)fields[i].GetValue(null);
        }

        return fieldNames;
    }
}