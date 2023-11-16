using System;
using _App.Scripts.Window;
using UnityEngine;
using UnityEngine.UI;

namespace _App.Scripts.Core
{
    public class MenuController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CameraController _cameraController;
        
        [Header("UI")]
        [SerializeField] private Canvas _menuCanvas;
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private TutorialWindow tutorialWindow;
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _exit;

        private void OnEnable()
        {
            _startGame.onClick.AddListener(OnStartGameButtonPress);
            _exit.onClick.AddListener(OnExitButtonPress);

            GameStateMachine.OnGameStateChange += OnGameStateChange;
        }

        private void OnDisable()
        {
            _startGame.onClick.RemoveAllListeners();
            _exit.onClick.RemoveAllListeners();
            
            GameStateMachine.OnGameStateChange -= OnGameStateChange;
        }

        private void OnStartGameButtonPress()
        {
            _menuCanvas.gameObject.SetActive(false);
            _cameraController.MoveToBoardPosition();
        }
        
        private void OnGameStateChange(GameState newState)
        {
            switch (newState)
            {
                case GameState.Menu:
                    _menuCanvas.gameObject.SetActive(true);
                    _gameCanvas.gameObject.SetActive(false);
                    break;
                case GameState.StartGame:
                    _gameCanvas.gameObject.SetActive(true);
                    tutorialWindow.Open();
                    break;
                case GameState.EndGame:
                    break;
                case GameState.ToMenuTransition:
                    _gameCanvas.gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void OnExitButtonPress()
        {
            Application.Quit();
        }
    }
}
