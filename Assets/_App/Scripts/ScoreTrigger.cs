using System;
using _App.Scripts.Core;
using Cysharp.Threading.Tasks;
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
        
        private readonly float _delay = 2f;
        public static event Action<ScoreTriggerType> OnScoreTriggeredStatic;

        private void OnTriggerEnter(Collider other)
        {
            if (GameStateMachine.Instance.CurrentState != GameState.StartGame) return;
            if (!other.gameObject.CompareTag("Puck")) return;
            
            HandleScore().AttachExternalCancellation(destroyCancellationToken);
        }

        private async UniTask HandleScore()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_delay));
            OnScoreTriggeredStatic?.Invoke(_type);
        }
    }
}
