using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName = "Data/DungeonData")]
public class DungeonData : ScriptableObject {
	public string dungeonName = "myDungeon";
	public float lat = 0f;
	public float lon = 0f;
	public int difficulty = 1;
	public bool completed = false;
}
