﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Turn : GameState
{
    public Player1Turn (GameStateManager StateManager):base(StateManager)
    {
        this.StateName = "Player1 Turn";
        Debug.Log("Player1 Turn Start");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
