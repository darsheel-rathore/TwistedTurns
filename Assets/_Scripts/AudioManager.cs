using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource m_AudioSource;
    [SerializeField] private AudioClip[] clips;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
    }

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlayClip()
    {
        int index = UnityEngine.Random.Range(0, clips.Length);
        m_AudioSource.clip = clips[index];
        m_AudioSource.Play();
    }

}
