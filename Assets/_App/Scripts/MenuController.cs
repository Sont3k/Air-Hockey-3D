using UnityEngine;
using UnityEngine.UI;

namespace _App.Scripts
{
    public class MenuController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ScoreController _scoreController;
        [SerializeField] private CameraController _cameraController;
        
        [Header("UI")]
        [SerializeField] private Canvas _menuCanvas;
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private TutorialScreen _tutorialScreen;
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _exit;

        private void OnEnable()
        {
            _startGame.onClick.AddListener(OnStartGameButtonPress);
            _exit.onClick.AddListener(OnExitButtonPress);

            CameraController.OnStartMoveCompleted += OnStartMoveCompleted;
            CameraController.OnBoardMoveCompleted += OnBoardMoveCompleted;
        }

        private void OnDisable()
        {
            _startGame.onClick.RemoveAllListeners();
            _exit.onClick.RemoveAllListeners();
            
            CameraController.OnStartMoveCompleted -= OnStartMoveCompleted;
            CameraController.OnBoardMoveCompleted -= OnBoardMoveCompleted;
        }

        private void OnStartMoveCompleted()
        {
            _menuCanvas.gameObject.SetActive(true);
        }
        
        private void OnBoardMoveCompleted()
        {
            _gameCanvas.gameObject.SetActive(true);
            _tutorialScreen.Open();
        }

        private void OnStartGameButtonPress()
        {
            _menuCanvas.gameObject.SetActive(false);
            _cameraController.MoveToBoardPosition();
            _scoreController.Init();
        }

        private void OnExitButtonPress()
        {
            Application.Quit();
        }
    }
}
