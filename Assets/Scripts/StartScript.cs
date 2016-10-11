using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartScript : MonoBehaviour {
	public void startDemo(){	
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void exitProgram(){
		Application.Quit ();
	}
}
