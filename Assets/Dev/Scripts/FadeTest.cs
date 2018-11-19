using UnityEngine;

public class SteamVrFadeTest : MonoBehaviour {
	private float _fadeDuration = 2f;

	private void Start() {
		FadeToWhite();
		Invoke("FadeFromWhite", _fadeDuration);
	}
	private void FadeToWhite() {
		//set start color clear
		SteamVR_Fade.Start(Color.clear, 0f);
		//set and start fade to white
		SteamVR_Fade.Start(Color.white, _fadeDuration);
	}
	private void FadeFromWhite() {
		//set start color white
		SteamVR_Fade.Start(Color.white, 0f);
		//set and start fade to clear
		SteamVR_Fade.Start(Color.clear, _fadeDuration);
	}
}
