using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MuteButton : MonoBehaviour {
    public AudioSource audioSource;
    public Sprite OffImage;
    public Sprite OnImage;

	private Image myImage;

    // Use this for initialization
    void Start () {
        this.myImage = this.GetComponent<Image>();
    }
	
	public void ToggleMute() {
		if(audioSource != null) {
            audioSource.mute = !audioSource.mute;
			this.myImage.sprite = audioSource.mute ? OffImage : OnImage;
        }
	}

	// Update is called once per frame
	void Update () {
	
	}
}
