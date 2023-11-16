using System;
using _App.Scripts.Core;
using UnityEngine;

namespace _App.Scripts.Characters
{
    public class ComputerStriker : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb; 
        [SerializeField] private Transform _puck; 
        [SerializeField] private float _moveForce = 5f; 
        [SerializeField] private float _reactionTime = 0.5f;

        private Vector3 _startPosition;
        private bool _isEnabled;
        
        private void OnEnable()
        {
            GameStateMachine.OnGameStateChange += OnGameStateChange;
            ScoreTrigger.OnScoreTriggeredStatic += ResetPosition;
        }
        
        private void Start()
        {
            _startPosition = transform.position;
        }

        private void OnDisable()
        {
            GameStateMachine.OnGameStateChange -= OnGameStateChange;
            ScoreTrigger.OnScoreTriggeredStatic -= ResetPosition;
        }

        private void FixedUpdate()
        {
            if (!_isEnabled) return;
            MovePaddleTowardsPuck();
        }

        private void MovePaddleTowardsPuck()
        {
            var directionToTarget = _puck.position - transform.position;
            directionToTarget.Normalize();
            _rb.AddForce(directionToTarget * _moveForce);
            
            MakeDecision();
        }

        private void MakeDecision()
        {
            // Add decision-making logic here, such as changing tactics based on game state
            // For example, the AI might decide to switch between defense and offense strategies
        }
        
        private void ResetPosition()
        {
            transform.position = _startPosition;
        }
        
        private void ResetPosition(ScoreTriggerType type)
        {
            transform.position = _startPosition;
        }
        
        private void OnGameStateChange(GameState newState)
        {
            switch (newState)
            {
                case GameState.Menu:
                    break;
                case GameState.StartGame:
                    ResetPosition();
                    _isEnabled = true;
                    break;
                case GameState.EndGame:
                    _isEnabled = false;
                    break;
                case GameState.ToMenuTransition:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }
    }
}