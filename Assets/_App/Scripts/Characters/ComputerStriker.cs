using System;
using _App.Scripts.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _App.Scripts.Characters
{
    public class ComputerStriker : MonoBehaviour
    {
        [SerializeField] private Transform _puck; 
        [SerializeField] private float _paddleSpeed = 5f; 
        [SerializeField] private float _reactionTime = 0.5f;

        private bool _isEnabled;
        
        private void OnEnable()
        {
            GameStateMachine.OnGameStateChange += OnGameStateChange;
        }

        private void OnDisable()
        {
            GameStateMachine.OnGameStateChange -= OnGameStateChange;
        }

        private void Update()
        {
            if (!_isEnabled) return;
            Move().AttachExternalCancellation(destroyCancellationToken);
        }

        private async UniTask Move()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_reactionTime));
            MovePaddleTowardsPuck();
        }

        private void MovePaddleTowardsPuck()
        {
            var directionToPuck = _puck.position - transform.position;
            directionToPuck.y = 0f;
            directionToPuck.Normalize();

            transform.Translate(directionToPuck * (_paddleSpeed * Time.deltaTime));
            MakeDecision();
        }

        private void MakeDecision()
        {
            // Add decision-making logic here, such as changing tactics based on game state
            // For example, the AI might decide to switch between defense and offense strategies
        }
        
        private void OnGameStateChange(GameState newState)
        {
            switch (newState)
            {
                case GameState.Menu:
                    break;
                case GameState.StartGame:
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