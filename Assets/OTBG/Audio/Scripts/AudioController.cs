using OTBG.Audio;
using UnityEngine;

namespace OTBG.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClipSO currentAudioClip;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayClip(AudioClipSO clipSO)
        {
            currentAudioClip = clipSO;
            if(clipSO.isRandomPitch)
                audioSource.pitch = 1f + Random.Range(-clipSO.pitchVariance, clipSO.pitchVariance);
            else audioSource.pitch = 1f;
            audioSource.loop = clipSO.isLooping;
            audioSource.volume = clipSO.volume;
            audioSource.clip = clipSO.clip;
            audioSource.Play();
        }

        public void PlayClipOneShot(AudioClipSO clipSO)
        {
            if (clipSO.isRandomPitch)
                audioSource.pitch = 1f + Random.Range(-clipSO.pitchVariance, clipSO.pitchVariance);
            else audioSource.pitch = 1f;

            audioSource.PlayOneShot(clipSO.clip, clipSO.volume);

        }

        public bool IsPlaying() => audioSource.isPlaying;

        public void StopClip()
        {
            audioSource.Stop();

            currentAudioClip = null;
            audioSource.clip = null;
        }

        public void OnValidate()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
                audioSource.playOnAwake = false;
            }
        }
    }
}
