using UnityEngine;
using UnityEngine.UI;

namespace Window
{
    public class TutorialWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _uiHolder;
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
            _uiHolder.SetActive(true);
            Time.timeScale = 0;
        }
        
        private void OnCloseButton()
        {
            _uiHolder.SetActive(false);
            Time.timeScale = 1;
        }
    }
}