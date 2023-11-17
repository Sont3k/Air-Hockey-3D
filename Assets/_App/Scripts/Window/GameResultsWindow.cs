using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Window
{
    public class GameResultsWindow : MonoBehaviour
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private GameObject _uiHolder;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _winnerText;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButton);
            ScoreController.OnGameFinished += Init;
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveAllListeners();
            ScoreController.OnGameFinished -= Init;
        }

        private void Init(int playerScore, int computerScore)
        {
            _winnerText.text = playerScore > computerScore
                ? $"Player Won!\n{playerScore} x {computerScore}"
                : $"Computer Won!\n{playerScore} x {computerScore}";
            _uiHolder.gameObject.SetActive(true);
        }

        private void OnCloseButton()
        {
            _uiHolder.SetActive(false);
            _cameraController.MoveToStartPosition();
            GameStateMachine.Instance.SetState(GameState.ToMenuTransition);
        }
    }
}