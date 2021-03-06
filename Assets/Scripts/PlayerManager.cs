﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerManager {
	public Color playerColor;
	public Transform spawnPoint;
	[HideInInspector] public GameManager manager;
	[HideInInspector] public int playerNumber;
	[HideInInspector] public GameObject instance;
	[HideInInspector] public int wins;

	private PlayerMovement movement;

	public void Setup() {
		
	
		movement = instance.GetComponent<PlayerMovement> ();
		//movement.fillColor = playerColor;

		movement.m_playerNumber = playerNumber;

	}
}