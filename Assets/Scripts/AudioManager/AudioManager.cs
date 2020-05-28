using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public bool StartMusic;
	public bool Randomizer;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	private void Start()
	{
		if(StartMusic)
		{
			Play("Music");
			Play("Ambiance");
		}
	}

	public void SoundRandomize()
	{
		if(Randomizer)
		{
			int newIndex = Random.Range(0, sounds.Length);

			for (int i = 0; i < sounds.Length; i++)
			{
				if (i == newIndex)
					Play(sounds[i].name);
			}
		}
	}

	public void Play(string sound)
	{
		Sound s = System.Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}
}
