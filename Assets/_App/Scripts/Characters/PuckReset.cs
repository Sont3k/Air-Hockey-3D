using System;
using _App.Scripts.Core;
using DG.Tweening;
using UnityEngine;

namespace _App.Scripts.Characters
{
    public class PuckReset : MonoBehaviour
    {
        [SerializeField] private float _resetDuration;
        private Vector3 _startPosition;
        
        private void Start()
        {
            _startPosition = transform.position;
        }

        private void OnEnable()
        {
            ScoreTrigger.OnScoreTriggeredStatic += ResetPosition;
            GameStateMachine.OnGameStateChange += OnGameStateChange;
        }

        private void OnDisable()
        {
            ScoreTrigger.OnScoreTriggeredStatic -= ResetPosition;
            GameStateMachine.OnGameStateChange -= OnGameStateChange;
        }

        private void ResetPosition()
        {
            transform.DOMove(_startPosition, _resetDuration);
        }
        
        private void ResetPosition(ScoreTriggerType type)
        {
            transform.DOMove(_startPosition, _resetDuration);
        }
        
        private void OnGameStateChange(GameState newState)
        {
            switch (newState)
            {
                case GameState.Menu:
                    break;
                case GameState.StartGame:
                    ResetPosition();
                    break;
                case GameState.EndGame:
                    break;
                case GameState.ToMenuTransition:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }
    }
}