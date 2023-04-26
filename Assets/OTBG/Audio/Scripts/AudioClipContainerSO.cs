using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Sirenix.OdinInspector;
using OTBG.Data;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace OTBG.Audio
{

    [CreateAssetMenu(fileName = "AudioContainer", menuName = "OTBG/Audio/Create Audio Container File")]
    public class AudioClipContainerSO : ScriptableObject
    {
        public string folderLocation;

        public List<AudioClipSO> allAudioClips = new List<AudioClipSO>();

        public AudioClipSO GetAudioClip(string id)
        {
            AudioClipSO clip = allAudioClips.FirstOrDefault(a => a.id == id);

            if (clip == null)
            {
                Debug.LogError($"Clip of ID: {id} not found in collection");
                return null;
            }

            return clip;
        }

#if UNITY_EDITOR
        [Button]
        public void GetAllFromFolder()
        {
            allAudioClips =  LoadAllFromFolder(folderLocation);
        }


        public List<CoupledNames> GetCoupledNames()
        {
            List<CoupledNames> coupledNames = new List<CoupledNames>();
            allAudioClips.ForEach(a =>
            {
                CoupledNames co = new CoupledNames()
                {
                    Name = a.constName,
                    ID = a.id
                };
                coupledNames.Add(co);   
            });

            return coupledNames;
        }

        public List<AudioClipSO> LoadAllFromFolder(string folderPath)
        {
            List<AudioClipSO> scriptableObjects = new List<AudioClipSO>();

            string[] assetPaths = Directory.GetFiles(folderPath, "*.asset", SearchOption.AllDirectories);

            foreach (string assetPath in assetPaths)
            {
                AudioClipSO scriptableObject = AssetDatabase.LoadAssetAtPath<AudioClipSO>(assetPath);

                if (scriptableObject != null)
                {
                    scriptableObjects.Add(scriptableObject);
                }
            }

            return scriptableObjects;
        }
#endif
    }
}