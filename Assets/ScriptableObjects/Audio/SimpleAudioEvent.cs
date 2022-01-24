using UnityEngine;
using Global;

[CreateAssetMenu(menuName = "Custom/Audio Events/Simple")]
public class SimpleAudioEvent : AudioEvent
{
    public AudioClip[] audioClips;

    public RangedFloat volume;

    public override void Play(AudioSource audioSource) {
        
    }
}
