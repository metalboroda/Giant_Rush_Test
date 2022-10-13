using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PunchCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
            }
        }
    }
}