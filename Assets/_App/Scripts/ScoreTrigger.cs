using System;
using Core;
using Cysharp.Threading.Tasks;
using UnityEngine;

public enum ScoreTriggerType
{
    Player,
    Computer
}
    
public class ScoreTrigger : MonoBehaviour
{
    [SerializeField] private ScoreTriggerType _type;
        
    private readonly float _delay = 2f;
    private bool _inProgress;
    
    public static event Action<ScoreTriggerType> OnScoreTriggeredStatic;

    private async void OnTriggerEnter(Collider other)
    {
        if (_inProgress) return;
        if (GameStateMachine.Instance.CurrentState != GameState.StartGame) return;
        if (!other.gameObject.CompareTag("Puck")) return;

        await HandleScore();
    }

    private async UniTask HandleScore()
    {
        _inProgress = true;
        await UniTask.Delay(TimeSpan.FromSeconds(_delay));
        OnScoreTriggeredStatic?.Invoke(_type);
        _inProgress = false;
    }
}