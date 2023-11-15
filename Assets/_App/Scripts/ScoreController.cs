using System;
using TMPro;
using UnityEngine;

namespace _App.Scripts
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        private int _playerScore;
        private int _computerScore;

        private void OnEnable()
        {
            ScoreTrigger.OnScoreTriggeredStatic += OnScoreTriggered;
        }

        private void OnDisable()
        {
            ScoreTrigger.OnScoreTriggeredStatic -= OnScoreTriggered;
        }

        public void Init()
        {
            UpdateUI();
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
        }

        private void IncreaseComputerScore()
        {
            _computerScore++;
            UpdateUI();
        }

        private void UpdateUI()
        {
            _scoreText.text = $"Score: {_playerScore} x {_computerScore}";
        }
    }
}