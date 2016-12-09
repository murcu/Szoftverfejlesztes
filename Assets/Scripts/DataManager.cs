using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {
	public DungeonData[] dungeons;

	public CharacterData player;

	public CharacterData[] monsters;

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}
}
