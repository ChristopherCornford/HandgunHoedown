using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UI_Manager : MonoBehaviour {
	[Header("SoundManager")]
	public SoundManager SoundManager;

	[Header("Player 1 UI")]
	public Image[] P1_Stars;
	public Image[] P1_Bullets;

	[Header("Player 2 UI")]
	public Image[] P2_Stars;
	public Image[] P2_Bullets;
	
	[Header("Countdown Images")]
	public GameObject[] Countdown;
	
	[Header("Game Over Menu")]
	public GameObject EndGameMenu;
	public GameObject PlayAgain;
	
	[Header("Pause Menu")]
	public GameObject PauseGameMenu;
	public GameObject Resume;

	[Header("Player Winner Screens")]
	public GameObject Player1Win;
	public GameObject Player2Win;

	/* Private */
	private Color none = new Color32(0,0,0,0);

	public IEnumerator timerUtil(float timerLength){
		yield return new WaitForSeconds(timerLength);
	}

	public IEnumerator RoundStartCountdown(){
		//TODO: FIX COUNTDOWN TO GET RID OF FLASHING
		int countDown = 3;
		SoundManager.CountdownSound(countDown);
		yield return StartCoroutine(Message(Countdown[countDown], 1f));
			countDown -= 1;
		SoundManager.CountdownSound(countDown);	
		yield return StartCoroutine(Message(Countdown[countDown], 1f));
			countDown -= 1;
		SoundManager.CountdownSound(countDown);
		yield return StartCoroutine(Message(Countdown[countDown], 1f));
			countDown -= 1;
		SoundManager.CountdownSound(countDown);
		yield return StartCoroutine(Message(Countdown[countDown], 1f));
	}

	public void giveBullets(int playerindex){
		switch (playerindex){
			case 1:
				for (int i = 0; i < P1_Bullets.Length; i++){
					P1_Bullets[i].color = Color.white;
				}
				break;
			case 2:
				for (int i = 0; i < P1_Bullets.Length; i++){
					P2_Bullets[i].color = Color.white;	
				}
				break;
		}
	}
	
	public void removeBullets(int playerindex, int bulletCount){
		switch (playerindex){
			case 1:
				P1_Bullets[bulletCount].color = none;
				break;
			case 2:
				P2_Bullets[bulletCount].color = none;
				break;
			case 3:
				for (int i = 0; i < P1_Bullets.Length; i++){
					P1_Bullets[i].color = none;
				}
				for (int i = 0; i < P1_Bullets.Length; i++){
					P2_Bullets[i].color = none;	
				}
				break;
		}
	}

	public void giveStars(int playerindex, int playerScore){
		switch (playerindex){
			case 1:
				P1_Stars[playerScore - 1].color = Color.white;
				break;
			case 2:
				P2_Stars[playerScore - 1].color = Color.white;
				break;
		}
	}

	 public IEnumerator Message(GameObject gameObject, float wait){
		gameObject.SetActive(true);
		if (wait > 0.1f){
			yield return new WaitForSeconds (wait);
			gameObject.SetActive(false);
		}	
		yield return null;
	} 

	public void PauseMenu(){
		PauseGameMenu.SetActive(true);
		GameObject pause_button = GameObject.Find("Resume_B");
		EventSystem.current.SetSelectedGameObject(pause_button);
	}
}
