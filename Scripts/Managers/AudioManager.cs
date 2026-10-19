using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        public AudioMixerGroup musicGroup;
        public AudioMixerGroup sfxGroup;
        
        public List<GameObject> audioSourceLoopList = new List<GameObject>();
        public AudioSource oneShotSource;
        
        public AudioClip enemyDamageSound;
        public AudioClip playerDamageSound;
        public AudioClip playerDeathSound;
        public AudioClip errorSound;
        public AudioClip correctSound;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlayOneShot(AudioClip clip)
        {
            if (!clip) return;
            oneShotSource.PlayOneShot(clip);
        }

        public void PlayAtObject(GameObject playObject, AudioClip clip)
        {
            var sourceObject = Instantiate(new GameObject(), playObject.transform);
            var audioSource = sourceObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.outputAudioMixerGroup = sfxGroup;
            audioSource.Play();
            
            Destroy(sourceObject, clip.length);
        }

        public void PlayLoop(AudioClip clip)
        {
            StopLoop(clip);

            var newSource = new GameObject("AudioLoop_" + clip.name);
            newSource.transform.SetParent(transform);

            var audioSource = newSource.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.outputAudioMixerGroup = musicGroup;
            audioSource.Play();

            audioSourceLoopList.Add(newSource);
        }


        public void StopLoop(AudioClip clip)
        {
            for (int i = audioSourceLoopList.Count - 1; i >= 0; i--)
            {
                var a = audioSourceLoopList[i];
                var audioSource = a.GetComponent<AudioSource>();
                if (audioSource.clip == clip)
                {
                    audioSource.Stop();
                    Destroy(a);
                    audioSourceLoopList.RemoveAt(i);
                }
            }
        }
        
        public void StopAllLoops()
        {
            for (int i = audioSourceLoopList.Count - 1; i >= 0; i--)
            {
                var a = audioSourceLoopList[i];
                var audioSource = a.GetComponent<AudioSource>();
                audioSource.Stop();
                Destroy(a);
                audioSourceLoopList.RemoveAt(i);
            }
        }
        
    }
}
