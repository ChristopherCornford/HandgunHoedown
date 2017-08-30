using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_Functions : MonoBehaviour {

	[Header("MAIN MENU UI Panels")]
	public GameObject LevelSelect_Panel;
	public GameObject Controls_Panel;
	public GameObject Credits_Panel;
	
	[Header("Control Select Button to Focus")]
	public GameObject Control_Button;
	
	[Header("Control Schemes")]
	public GameObject Keyboard_Controls;
	public GameObject Controller_Controls;
	
	[Header("Level Select Button to Focus")]
	public GameObject lvl_select_button;
	
	[Header("Levels")]
	public GameObject[] Levels;
	private int currentLevel;

	[Header("Pause Control Pane")]
	public GameObject pause_controls;
	
	// Put this before each button function to check what to do
	private void PanelCheck(){
		if(LevelSelect_Panel.activeInHierarchy == true){
			LevelSelect_Panel.SetActive(false);
		}
		if(Controls_Panel.activeInHierarchy == true){
			Controls_Panel.SetActive(false);
		}if(Credits_Panel.activeInHierarchy == true){
			Credits_Panel.SetActive(false);
		}
	}

	/* Main Menu Buttons */
	public void LevelSelect(){
		PanelCheck();
		LevelSelect_Panel.SetActive(true);
		EventSystem.current.SetSelectedGameObject(lvl_select_button);
	}
	public void Controls(){
		PanelCheck();
		Controls_Panel.SetActive(true);
		EventSystem.current.SetSelectedGameObject(Control_Button);
	}
	public void Credits(){
		PanelCheck();
		Credits_Panel.SetActive(true);
	}
	public void Quit(){
		Application.Quit();
	}

	/* Level Select Buttons */
	public void LoadCanyon(){SceneManager.LoadScene("Canyon");}
	public void LoadTown(){SceneManager.LoadScene("Town");}
	public void LoadMine(){SceneManager.LoadScene("Mine");}
	public void LoadRandom(){SceneManager.LoadScene(Random.Range(1,3));}

	/* Level Select Scrolling */
	public void LevelScrollRight(){
		Levels[currentLevel].SetActive(false);
		if (currentLevel == 3){
			currentLevel = 0;
			Levels[currentLevel].SetActive(true);
		}
		else{
			currentLevel += 1;
			Levels[currentLevel].SetActive(true);
		}
	}
	public void LevelScrollLeft(){
		Levels[currentLevel].SetActive(false);
		if (currentLevel == 0){
			currentLevel = 3;
			Levels[currentLevel].SetActive(true);
		}
		else{
			currentLevel -= 1;
			Levels[currentLevel].SetActive(true);
		}
	}

	/* Control Scrolling */
	public void ControlScrolling(){
		if(Controller_Controls.activeInHierarchy == true){
			Controller_Controls.SetActive(false);
			Keyboard_Controls.SetActive(true);
		}
		else{
			Keyboard_Controls.SetActive(false);
			Controller_Controls.SetActive(true);
		}
	}

	/* Pause Menu */
	public void Resume(){
		GameObject.Find("/Managers/GameManager").GetComponent<GameManager>().Resume();
	}
	public void Pause_Controls(){
		pause_controls.SetActive(true);
		EventSystem.current.SetSelectedGameObject(GameObject.Find("Controls_RightArrow"));
	}
	public void Pause_Back(){
		pause_controls.SetActive(false);
		EventSystem.current.SetSelectedGameObject(GameObject.Find("Resume_B"));
	}

	/* End of Game Menu Buttons */
	public void PlayAgain(){
		// Reloads the current scene
		int lvl = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(lvl);
	}
	public void ExittoMain(){
		// Loads Main Menu
		SceneManager.LoadScene(0);
	}
}
