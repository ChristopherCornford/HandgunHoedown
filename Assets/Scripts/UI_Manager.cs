using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

	[Header("Player 1 UI")]
	public Image[] P1_Stars;
	public GameObject P1_BulletContainer;
	public Image[] P1_Bullets;

	[Header("Player 2 UI")]
	public Image[] P2_Stars;
	public GameObject P2_BulletContainer;
	public Image[] P2_Bullets;

	/* Private */
	private Color none = new Color32(0,0,0,0);

	public void GiveBullets(int playerindex){
		switch (playerindex){
			case 1:
				P1_BulletContainer.SetActive(true);
				break;
			case 2:
				P2_BulletContainer.SetActive(true);
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
		}
	}

	public void GiveStars(int playerindex, int playerScore){
		switch (playerindex){
			case 1:
				P1_Stars[playerScore - 1].color = Color.white;
				break;
			case 2:
				P2_Stars[playerScore - 1].color = Color.white;
				break;
		}
	}
}
