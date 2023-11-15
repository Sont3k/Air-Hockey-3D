using System;
using UnityEngine;

namespace _App.Scripts
{
    public enum ScoreTriggerType
    {
        Player,
        Computer
    }
    
    public class ScoreTrigger : MonoBehaviour
    {
        [SerializeField] private ScoreTriggerType _type;
        public static event Action<ScoreTriggerType> OnScoreTriggeredStatic;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Puck")) return;
            OnScoreTriggeredStatic?.Invoke(_type);
        }
    }
}
