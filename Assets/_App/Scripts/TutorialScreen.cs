using UnityEngine;
using UnityEngine.UI;

namespace _App.Scripts
{
    public class TutorialScreen : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButton);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveAllListeners();
        }
        
        public void Open()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        
        private void OnCloseButton()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}