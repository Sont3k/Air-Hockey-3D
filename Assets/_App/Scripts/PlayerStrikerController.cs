using UnityEngine;

namespace _App.Scripts
{
    public class PlayerStrikerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera _camera;
        [SerializeField] private Rigidbody _rb;

        private void Update()
        {
            if (!Input.GetMouseButton(1)) return;

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hitInfo, 5))
            {
                _rb.position = hitInfo.point;
            }
        }
    }
}