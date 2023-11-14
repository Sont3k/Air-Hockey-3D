using TMPro;
using UnityEngine;

namespace _App.Scripts
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        private int _playerScore;
        private int _computerScore;

        public void IncreasePlayerScore()
        {
            _playerScore++;
            UpdateUI();
        }

        public void IncreaseComputerScore()
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