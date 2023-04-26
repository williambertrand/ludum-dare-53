using OTBG.Audio;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using OTBG.Data;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using System.Linq;

namespace OTBG.Editor.Audio
{
    public enum AudioType
    {
        MusicTrackIDs,
        SFXIDs
    }
    public class AudioIDGenerator : OdinEditorWindow
    {
        public string fileLocation = "Assets/OTBG/Audio/Scripts/Consts";
        public AudioType scriptNames;
        [OnValueChanged("OnSOChanged")]
        public AudioClipContainerSO audioContainerSO;
        public List<string> allConsts = new List<string>();
        [MenuItem("OTBG/Audio/Audio Const ID Generator")]
        private static void OpenWindow()
        {
            AudioIDGenerator window = GetWindow<AudioIDGenerator>();
            window.Show();
        }

        [OnInspectorGUI]
        public void OnSOChanged()
        {
            if (audioContainerSO == null)
                return;

            allConsts = audioContainerSO.GetCoupledNames().Select(c => c.Name).ToList();
            
        }

        [Button("Generate Script")]
        private void GenerateScript()
        {
            string script = $"public static class {scriptNames}\n{{\n";

            audioContainerSO.GetAllFromFolder();

            List<CoupledNames> constsToCreate = audioContainerSO.GetCoupledNames();


            foreach (var value in constsToCreate)
            {
                script += $"\tpublic const string {value.Name} = \"{value.ID}\";\n";
            }

            script += "}";

            string filePath = Path.Combine(fileLocation, $"{scriptNames}.cs");

            string directoryPath = Path.GetDirectoryName(filePath);
            Directory.CreateDirectory(directoryPath);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllText(filePath, script);

            AssetDatabase.Refresh();
        }
    }
}