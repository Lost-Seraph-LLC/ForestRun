using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameControls : MonoBehaviour {

    public List<AudioClip> audioSources;
    public EnemySpawner spawner;
    public EnemySpawner collectables;
    public PlayerController player;
    public Text counter;
    public Text TitleText;
    public Text GameOver;
    public Text results;
    public Text tapRestart;
    public float restartDelay = 10f;

    private bool gameStarted;
    private int clipIndex = 0;
    private AudioSource source;
    private float restartTimer = 0;

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
    private void SetCounter(bool status) {
        counter.gameObject.SetActive(status);
    }
	private void SetResults() {
        results.text = "Score: " + (player.GetTimeElapsed() * 100).ToString("0");

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
            SetCounter(true);
            spawner.Enable();
            collectables.Enable();
            player.SetTimer();
            gameStarted = true;
        }
	}

    void OnGUI() {
        if(gameStarted) {
            counter.text = player.GetTimeLeft().ToString("0");
        }

        if(restartTimer > 0 && Time.time >= restartTimer) {
            
            tapRestart.gameObject.SetActive(true);

            if(Input.GetButtonDown("Jump") ||  Input.GetMouseButtonDown(0)) {
                gameStarted = true;
                SceneManager.LoadScene(0);
            }
        }


        if(gameStarted && player.GetTimeLeft() == 0) {
            if(restartTimer == 0) {
                restartTimer = Time.time + restartDelay;
                SetResults();
            }

            SetGameOver(true);
            SetCounter(false);

            spawner.Disable();
            collectables.Disable();
        }
    }
}
