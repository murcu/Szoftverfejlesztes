using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName = "Data/Character")]
public class CharacterData : ScriptableObject {

	public string characterName;
	public int maxHealth;
	public int currHealth;

	public int strenght;
	public int agility;
	public int intellect;
}
