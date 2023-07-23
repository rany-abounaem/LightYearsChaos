using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


namespace LightYearsChaos
{
    public class AudioManager : MonoBehaviour
    {

        public static AudioManager instance;
        [System.Serializable]
        public class Sound
        {
            public string name;
            public AudioClip audio;
            public float volume;
            [HideInInspector]
            public AudioSource source;

        }
        public List<Sound> sounds;
        // Start is called before the first frame update

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.volume = s.volume;
                s.source.clip = s.audio;
            }
            Play("MainMenu");
        }
        public void Play(string name)
        {
            Sound s = sounds.Find(sound => sound.name == name);
            s.source.Play();
            if (name.Equals("MainMenu") || name.Equals("Background"))
                s.source.loop = true;

        }
        public void Stop(string name)
        {
            Sound s = sounds.Find(sound => sound.name == name);
            s.source.Stop();

        }
    }
}

