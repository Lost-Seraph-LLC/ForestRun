using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameControls : MonoBehaviour {

    public List<AudioClip> audioSources;
    private int clipIndex = 0;
    public EnemySpawner spawner;
    public EnemySpawner collectables;
    public PlayerController player;
    public Text timerText;
    public Text TitleText;
    public Text GameOver;
    private bool gameStarted;

    private AudioSource source;
	// Use this for initialization
	void Start () {
        this.source = this.GetComponent<AudioSource>();
        spawner.Disable();
        collectables.Disable();
	}

    private void SetTitle(bool status) {
        TitleText.gameObject.SetActive(status);
    }

    private void SetGameOver(bool status) {
        GameOver.gameObject.SetActive(status);
    }
	
	// Update is called once per frame
	void Update () {
	    if(!source.isPlaying)
        {
            source.clip = audioSources[clipIndex];
            clipIndex = (clipIndex + 1) % audioSources.Count;
            source.Play();
        }

        if((Input.GetButtonDown("Jump") ||  Input.GetMouseButtonDown(0)) && !gameStarted) {
            SetTitle(false);
            spawner.Enable();
            collectables.Enable();
            player.SetTimer();
            gameStarted = true;
        }
	}

    void OnGUI() {
        if(gameStarted) {
            timerText.text = player.GetTimeLeft().ToString("000");
        }

        if(gameStarted && player.GetTimeLeft() == 0) {
            SetGameOver(true);

            spawner.Disable();
            collectables.Disable();
        }
    }
}
