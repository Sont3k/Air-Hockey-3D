using DG.Tweening;
using UnityEngine;

namespace _App.Scripts
{
    public class PlayerStrikerController : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _mouseSyncTime;
        
        [Header("Borders")]
        [SerializeField] private Transform _topLimit;
        [SerializeField] private Transform _bottomLimit;
        [SerializeField] private Transform _leftLimit;
        [SerializeField] private Transform _rightLimit;

        [Header("References")]
        [SerializeField] private Camera _camera;
        [SerializeField] private Rigidbody _rb;

        private void Update()
        {
            if (!Input.GetMouseButton(1)) return;

            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (UnityEngine.Physics.Raycast(ray, out var hitInfo))
            {
                UpdateStrikerPosition(hitInfo);
            }
        }

        private void UpdateStrikerPosition(RaycastHit hitInfo)
        {
            if (hitInfo.point.z > _topLimit.position.z) return;
            if (hitInfo.point.z < _bottomLimit.position.z) return;
            if (hitInfo.point.x < _leftLimit.position.x) return;
            if (hitInfo.point.x > _rightLimit.position.x) return;

            var destination = new Vector3(hitInfo.point.x, _rb.position.y, hitInfo.point.z);
            _rb.DOMove(destination, _mouseSyncTime);
        }
    }
}