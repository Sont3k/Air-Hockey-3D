using System;
using DG.Tweening;
using UnityEngine;

namespace _App.Scripts
{
    [Serializable]
    public struct CameraTransform
    {
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _rotation;

        public Vector3 Position => _position;
        public Vector3 Rotation => _rotation;
    }

    public class CameraController : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private CameraTransform _startTransform;
        [SerializeField] private CameraTransform _boardTransform;
        [SerializeField] private float _transitionTime;

        [Header("References")]
        [SerializeField] private Camera _camera;

        public static event Action OnStartMoveCompleted;
        public static event Action OnBoardMoveCompleted;

        public void MoveToStartPosition()
        {
            _camera.transform.DOMove(_startTransform.Position, _transitionTime);
            _camera.transform.DORotate(_startTransform.Rotation, _transitionTime).OnComplete(() =>
            {
                OnStartMoveCompleted?.Invoke();
            });
        }

        public void MoveToBoardPosition()
        {
            _camera.transform.DOMove(_boardTransform.Position, _transitionTime);
            _camera.transform.DORotate(_boardTransform.Rotation, _transitionTime).OnComplete(() =>
            {
                OnBoardMoveCompleted?.Invoke();
            });
        }
    }
}