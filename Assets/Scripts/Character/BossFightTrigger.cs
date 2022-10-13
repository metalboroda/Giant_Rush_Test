using UnityEngine;

namespace Assets.Scripts.Character
{
    public class BossFightTrigger : MonoBehaviour
    {
        // Private refs
        private BossController _bossController;

        private void Awake()
        {
            _bossController = GetComponentInParent<BossController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _bossController.UpdateBossState(BossState.Fighting);
            }
        }
    }
}