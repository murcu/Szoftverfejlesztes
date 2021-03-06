﻿using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName = "Data/Character")]
public class CharacterData : ScriptableObject {

	public string characterName;
	public GameObject characterPrefab;

	public int maxHealth;
	public int currHealth;

	public int points;
}
