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
	[HeaderAttribute("Level Button to Focus")]
	public GameObject Level1Button;

	/* Main Menu Buttons */
	public void LevelSelect(){
		LevelSelect_Panel.SetActive(true);
		EventSystem.current.SetSelectedGameObject(Level1Button);
	}
	public void Controls(){
		Controls_Panel.SetActive(true);
	}
	public void Credits(){
		Credits_Panel.SetActive(true);
	}
	public void Quit(){
		Application.Quit();
	}

	// Level Select Buttons
	/* Create all the level button functions here
	SceneManager.LoadScene(name of scene or int in build index) */

	/* End of Game Menu Buttons */
	public void PlayAgain(){
		// Reloads the current scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void ExittoMain(){
		// Loads Main Menu
		SceneManager.LoadScene(0);
	}
}
