using System;
using _App.Scripts.Core;
using DG.Tweening;
using UnityEngine;

namespace _App.Scripts.Characters
{
    public class ComputerStriker : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _moveSpeed;

        [SerializeField] private float _defenseDistance; // Distance to trigger defensive behavior
        [SerializeField] private float _switchToOffenseDistance; // Distance to switch from defense to offense
        [SerializeField] private float _resetDuration;

        [Header("References")]
        [SerializeField] private Transform _puck;

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

        private void Update()
        {
            if (!_isEnabled) return;
            MovePaddleTowardsPuck();
        }

        private void MovePaddleTowardsPuck()
        {
            var directionToTarget = _puck.position - transform.position;
            directionToTarget.Normalize();

            MakeDecision(directionToTarget);
        }

        private void MakeDecision(Vector3 directionToTarget)
        {
            var distanceToPuck = Vector3.Distance(transform.position, _puck.position);

            if (distanceToPuck > _switchToOffenseDistance)
            {
                // Move towards the puck aggressively (offensive behavior)
                transform.Translate(directionToTarget * (_moveSpeed * Time.deltaTime));
            }
            else if (distanceToPuck > _defenseDistance)
            {
                // Move towards the puck, but not too aggressively (balanced behavior)
                transform.Translate(directionToTarget * (_moveSpeed * 0.5f * Time.deltaTime));
            }
            else
            {
                // Move away from the puck defensively (defensive behavior)
                transform.Translate(-directionToTarget * (_moveSpeed * Time.deltaTime));
            }
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
                case GameState.ToGameTransition:
                    ResetPosition();
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