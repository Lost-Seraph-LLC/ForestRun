using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MuteButton : MonoBehaviour {
    public AudioSource audioSource;
    public Sprite OffImage;
    public Sprite OnImage;

	private Image myImage;

    // Use this for initialization
    void Awake () {
        int muted = 0;
        this.myImage = this.GetComponent<Image>();
        
        Debug.Log("Muted: " + muted);
        if(PlayerPrefs.HasKey("Muted")) {
            muted = PlayerPrefs.GetInt("Muted", 0);
            SetMute(muted > 0 ? true : false);
        }
    }
	
	public void ToggleMute() {
		if(audioSource != null) {
            SetMute(!audioSource.mute);
        }
	}

    public void SetMute(bool isMuted) {
        audioSource.mute = isMuted;
        this.myImage.sprite = isMuted ? OffImage : OnImage;
        int muted = isMuted ? 1 : 0;
        PlayerPrefs.SetInt("Muted", muted);
        PlayerPrefs.Save();
    }
}
