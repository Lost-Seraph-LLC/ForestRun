using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class GameControls : MonoBehaviour {

    public List<AudioClip> audioSources;
    private int clipIndex = 0;

    private AudioSource source;
	// Use this for initialization
	void Start () {
        this.source = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(!source.isPlaying)
        {
            source.clip = audioSources[clipIndex];
            clipIndex = (clipIndex + 1) % audioSources.Count;
            source.Play();
        }
	}
}
