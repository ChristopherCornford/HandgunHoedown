using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

	[Header("Player 1 UI")]
	public GameObject[] P1_Stars;
	public GameObject P1_BulletContainer;
	public Image[] P1_Bullets;

	[Header("Player 2 UI")]
	public GameObject[] P2_Stars;
	public GameObject P2_BulletContainer;
	public Image[] P2_Bullets;

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
		Color none = new Color32(0,0,0,0);
		switch (playerindex){
			case 1:
				P1_Bullets[bulletCount].color = none;
				break;
			case 2:
				P2_Bullets[bulletCount].color = none;
				break;
		}
	}
}
