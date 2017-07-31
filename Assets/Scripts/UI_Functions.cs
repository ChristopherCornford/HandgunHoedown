using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_Functions : MonoBehaviour {

	[HeaderAttribute("MAIN MENU UI Panels")]
	public GameObject LevelSelect_Panel;
	public GameObject Controls_Panel;
	public GameObject Credits_Panel;
	
	[HeaderAttribute("Control Select Button to Focus")]
	public GameObject Control_Button;
	
	[HeaderAttribute("Control Schemes")]
	public GameObject Keyboard_Controls;
	public GameObject Controller_Controls;
	
	[HeaderAttribute("Level Select Button to Focus")]
	public GameObject Canyon_Button;
	
	[HeaderAttribute("Levels")]
	public GameObject[] Levels;
	private int currentLevel;
	
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
		EventSystem.current.SetSelectedGameObject(Canyon_Button);
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
