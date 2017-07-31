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
	[HeaderAttribute("Level Select Button to Focus")]
	public GameObject Canyon_Button;

	/* Main Menu Buttons */
	public void LevelSelect(){
		LevelSelect_Panel.SetActive(true);
		EventSystem.current.SetSelectedGameObject(Canyon_Button);
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

	/* Level Select Buttons */
	public void LoadDesert(){SceneManager.LoadScene("Desert");}
	public void LoadTown(){SceneManager.LoadScene("Town");}
	public void LoadMine(){SceneManager.LoadScene("Mine");}
	// TODO: MAKE RANDOM FUNCTION
	public void LoadRandom(){SceneManager.LoadScene(Random.Range(1,3));}

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
