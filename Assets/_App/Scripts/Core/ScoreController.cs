using System;
using TMPro;
using UnityEngine;

namespace _App.Scripts.Core
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        private int _playerScore;
        private int _computerScore;

        public static event Action<int, int> OnGameFinished;
        
        private void OnEnable()
        {
            ScoreTrigger.OnScoreTriggeredStatic += OnScoreTriggered;
            GameStateMachine.OnGameStateChange += OnGameStateChange;
        }

        private void OnDisable()
        {
            ScoreTrigger.OnScoreTriggeredStatic -= OnScoreTriggered;
            GameStateMachine.OnGameStateChange -= OnGameStateChange;
        }

        private void OnScoreTriggered(ScoreTriggerType type)
        {
            switch (type)
            {
                case ScoreTriggerType.Player:
                    IncreaseComputerScore();
                    break;
                case ScoreTriggerType.Computer:
                    IncreasePlayerScore();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void IncreasePlayerScore()
        {
            _playerScore++;
            UpdateUI();
            CheckEndGame();
        }

        private void IncreaseComputerScore()
        {
            _computerScore++;
            UpdateUI();
            CheckEndGame();
        }

        private void UpdateUI()
        {
            _scoreText.text = $"Score: {_playerScore} x {_computerScore}";
        }
        
        private void CheckEndGame()
        {
            if (_playerScore == 5 || _computerScore == 5)
            {
                GameStateMachine.Instance.SetState(GameState.EndGame);
            }
        }
        
        private void OnGameStateChange(GameState newState)
        {
            switch (newState)
            {
                case GameState.Menu:
                    break;
                case GameState.StartGame:
                    _playerScore = 0;
                    _computerScore = 0;
                    UpdateUI();
                    break;
                case GameState.EndGame:
                    OnGameFinished?.Invoke(_playerScore, _computerScore);
                    break;
                case GameState.ToMenuTransition:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }
    }
}