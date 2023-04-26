using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace OTBG.Audio
{

    [CreateAssetMenu(fileName = "Audio_", menuName = "OTBG/Audio/Create Audio File")]
    public class AudioClipSO : ScriptableObject
    {
        public string id;
        public string constName;
        public AudioClip clip;
        public bool isLooping;
        public bool isRandomPitch;
        [ShowIf("isRandomPitch")]
        public float pitchVariance;
        [Range(0f, 1f)]
        public float volume;
    }

}