using DG.Tweening;
using UnityEngine;

namespace _App.Scripts
{
    public class PuckReset : MonoBehaviour
    {
        [SerializeField] private float _resetDuration;
        private Vector3 _startPosition;
        
        private void Start()
        {
            _startPosition = transform.position;
        }

        private void OnEnable()
        {
            ScoreTrigger.OnScoreTriggeredStatic += ResetPuck;
        }

        private void OnDisable()
        {
            ScoreTrigger.OnScoreTriggeredStatic -= ResetPuck;
        }

        private void ResetPuck(ScoreTriggerType type)
        {
            transform.DOMove(_startPosition, _resetDuration);
        }
    }
}