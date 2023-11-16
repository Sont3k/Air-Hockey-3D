﻿using System;
using UnityEngine;

namespace _App.Scripts.Core
{
    public enum GameState
    {
        Menu,
        StartGame,
        EndGame,
        ToMenuTransition
    }
    
    public class GameStateMachine : MonoBehaviour
    {
        public GameState CurrentState { get; set; }
        public static event Action<GameState> OnGameStateChange;

        public static GameStateMachine Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            CameraController.OnStartMoveCompleted += OnStartMoveCompleted;
            CameraController.OnBoardMoveCompleted += OnBoardMoveCompleted;
        }

        private void OnDisable()
        {
            CameraController.OnStartMoveCompleted -= OnStartMoveCompleted;
            CameraController.OnBoardMoveCompleted -= OnBoardMoveCompleted;
        }

        private void OnBoardMoveCompleted()
        {
            SetState(GameState.StartGame);
        }

        private void OnStartMoveCompleted()
        {
            SetState(GameState.Menu);
        }
        
        private void Start()
        {
            SetState(GameState.Menu);
        }

        public void SetState(GameState newState)
        {
            CurrentState = newState;
            OnGameStateChange?.Invoke(newState);
        }
    }
}