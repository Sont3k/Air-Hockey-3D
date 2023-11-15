using UnityEngine;

namespace _App.Scripts.Physics
{
    public class FeedbackForce : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _pushForce;

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Wall") && !other.gameObject.CompareTag("Striker")) return;
            
            var pushDirection = (transform.position - other.contacts[0].point).normalized;
            _rb.AddForce(pushDirection * _pushForce, ForceMode.Impulse);
        }
    }
}