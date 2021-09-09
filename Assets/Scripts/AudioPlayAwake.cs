using UnityEngine;
using System.Collections;

using Extensione.Audio;

public class AudioPlayAwake : MonoBehaviour
{
    public AudioClip clip;

    void Start()
    {
        if (AudioManager.Instance)
        {
            AudioManager.Instance.ChangeMusicSilent(clip);
        }
    }
}
