using System;
using UnityEngine;

namespace Core
{
    public enum GameState
    {
        Menu,
        ToGameTransition,
        StartGame,
        EndGame,
        ToMenuTransition
    }
    
    public class GameStateMachine : MonoBehaviour
    {
        public GameState CurrentState { get; private set; }
        public static event Action<GameState> OnGameStateChange;

        public static GameStateMachine Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
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