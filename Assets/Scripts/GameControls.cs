using UnityEngine;
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
    private bool hasIncreased = true;

    // Use this for initialization
    void Start () {
        this.source = this.GetComponent<AudioSource>();
        spawner.Disable();
        collectables.Disable();
	}

    private void ToggleTitle(bool status) {
        TitleText.gameObject.SetActive(status);
    }

    private void ToggleGameOver(bool status) {
        GameOver.gameObject.SetActive(status);
    }
    private void ToggleCounter(bool status) {
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
            ToggleTitle(false);
            ToggleCounter(true);
            spawner.Enable();
            collectables.Enable();
            player.SetTimer();
            gameStarted = true;
        }
        
        if(player.IsPlaying()) {
            float looper = player.GetTimeElapsed() % 30;

            if(looper < 1 && !hasIncreased) {
                spawner.IncreaseDifficulty();
                hasIncreased = true;
            }

            if(looper > 1) {
                hasIncreased = false;
            }
        }
    }

    void OnGUI() {
        if(gameStarted) {
            counter.text = "til Death " + player.GetTimeLeft().ToString("0");
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

            ToggleGameOver(true);
            ToggleCounter(false);

            spawner.Disable();
            collectables.Disable();
        }
    }
}
