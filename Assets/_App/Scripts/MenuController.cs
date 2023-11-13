using UnityEngine;
using UnityEngine.UI;

namespace _App.Scripts
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _exit;

        private void OnEnable()
        {
            _startGame.onClick.AddListener(OnStartGameButtonPress);
            _exit.onClick.AddListener(OnExitButtonPress);
        }

        private void OnDisable()
        {
            _startGame.onClick.RemoveAllListeners();
            _exit.onClick.RemoveAllListeners();
        }
        
        private void OnStartGameButtonPress()
        {
            
        }

        private void OnExitButtonPress()
        {
            Application.Quit();
        }
    }
}
