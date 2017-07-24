using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour {

	[Header("Player 1 UI")]
	public Image[] P1_Stars;
	public Image[] P1_Bullets;

	[Header("Player 2 UI")]
	public Image[] P2_Stars;
	public Image[] P2_Bullets;
	
	[HeaderAttribute("Countdown Images")]
	public Image[] Countdown;
	
	[HeaderAttribute("PlayCanvas UI Panels")]
	public GameObject EndGameMenu;
	public GameObject PauseGameMenu;

	/* Private */
	private Color none = new Color32(0,0,0,0);

	private IEnumerator timerUtil(float timerLength){
		yield return new WaitForSeconds(timerLength);
		yield return null;
	}

	public IEnumerator RoundStartCountdown(){
		int countDown = 3;
		StartCoroutine(UI_Manager.Message("3", 0f));
		yield return new WaitForSeconds(1.0f);
			countDown -= 1;
		StartCoroutine(UI_Manager.Message("2", 0f));
		yield return new WaitForSeconds(1.0f);
			countDown -= 1;
		StartCoroutine(UI_Manager.Message("1", 0f));
		yield return new WaitForSeconds(1.0f);
			countDown -= 1;
		StartCoroutine(UI_Manager.Message("HOEDOWN!", 1f));
		yield return null;
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

	public IEnumerator Message(string in_string, float wait){
		/* message_text.text = in_string;
		message_panel.SetActive(true); */
		if (wait > 0.1f){
			yield return StartCoroutine(timerUtil(wait));
			message_panel.SetActive(false);
		}
		// if you put in 0f as "wait", it'll just stay up - helpful for countdowns
		yield return null;
	}

	public void PauseMenu(){
		PauseGameMenu.SetActive(true);
		
	}
}
