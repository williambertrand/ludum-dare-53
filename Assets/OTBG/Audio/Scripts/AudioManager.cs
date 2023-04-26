using OTBG.Utility;
using UnityEngine.Audio;

namespace OTBG.Audio
{

    public class AudioManager : Singleton<AudioManager>
    {
        public const string BGM_VOLUME = "BGMVolume";
        public const string SFX_VOLUME = "SFXVolume";

        public AudioPlayer soundEffectsManager;
        public AudioPlayer backgroundMusicManager;
        public AudioMixer mixer;

        private void Start()
        {
            Initialise();

            mixer.SetFloat(BGM_VOLUME, SaveData.Load<float>("BGMVolume"));
            mixer.SetFloat(SFX_VOLUME, SaveData.Load<float>("SFXVolume"));
        }

        public void Initialise()
        {
            if (!SaveData.DoesKeyExist(BGM_VOLUME))
                SaveData.Save(BGM_VOLUME, 0f);
            if(!SaveData.DoesKeyExist(SFX_VOLUME))
                SaveData.Save(SFX_VOLUME, 0f);
        }

        public void PlayMusicTrack(string id)
        {
            backgroundMusicManager.PlayAudioClip(id);
        }

        public void StopMusicTrack(string id)
        {
            backgroundMusicManager.StopAudioClip(id);
        }

        public void StopAllMusicTracks()
        {
            backgroundMusicManager.StopAllControllers();
        }

        public void PlaySoundEffect(string id, bool isOneShot)
        {
            if (isOneShot)
                soundEffectsManager.PlayAudioClipOneShot(id);
            else soundEffectsManager.PlayAudioClip(id);
        }
    }
}

