using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace OTBG.Audio
{

    public class AudioPlayer : MonoBehaviour
    {

        public AudioClipContainerSO clipContainer;
        public List<AudioController> allSoundEffectAudioSources = new List<AudioController>();

        private void Awake()
        {
            GetReferences();
        }

        [Button]
        public void GetReferences()
        {
            allSoundEffectAudioSources = GetComponentsInChildren<AudioController>().ToList();

        }

        public void PlayAudioClip(string audioClipID)
        {
            AudioClipSO clipSO = clipContainer.GetAudioClip(audioClipID);

            AudioController source = GetFreeSource();
            if (source == null)
                return;

            source.PlayClip(clipSO);
        }

        public void PlayAudioClipOneShot(string audioClipID)
        {
            AudioClipSO clipSO = clipContainer.GetAudioClip(audioClipID);

            AudioController source = allSoundEffectAudioSources[0];

            source.PlayClipOneShot(clipSO);
            
        }

        public AudioController GetFreeSource()
        {
            AudioController freeSource = allSoundEffectAudioSources.FirstOrDefault(a => !a.IsPlaying());

            if (freeSource == null)
            {
                Debug.LogError($"No free AudioSource available");
                //If there aren't any. Create a new one.
                return null;
            }

            return freeSource;
        }
        public void StopAllControllers()
        {
            allSoundEffectAudioSources.ForEach(a => a.StopClip());
        }

        public void StopAudioClip(string id)
        {
            AudioController source = allSoundEffectAudioSources.FirstOrDefault(a => a.IsPlaying() && a.currentAudioClip.id == id);
            if (source == null)
                return;
            source.StopClip();
        }

    }
}