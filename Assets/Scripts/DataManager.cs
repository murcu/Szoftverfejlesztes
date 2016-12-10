using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {
	//the current player
	public CharacterData player;

	//all dungeon data
	public DungeonData[] dungeons;

	//enemies
	public CharacterData[] monsters;
	public CharacterData[] bosses;

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}
}
