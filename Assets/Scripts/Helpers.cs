using UnityEngine;

public class Helpers : MonoBehaviour
{

	public static AudioSource CreateAudioSource(GameObject go, AudioClip clip, float volume, bool loop)
	{
        AudioSource source = go.AddComponent<AudioSource>();
        source.loop = loop;
        source.volume = volume;
        source.clip = clip;
		return source;
	}

    
}
