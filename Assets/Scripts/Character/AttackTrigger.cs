using UnityEngine;

namespace Assets.Scripts.Character
{
    public class AttackTrigger : MonoBehaviour
    {
        // Private refs
        private FightController fightController;

        private void Awake()
        {
            fightController = GetComponentInParent<FightController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (fightController.fighterType == FightController.FighterType.Player)
                if (other.CompareTag("Enemy"))
                {
                    IDamageable damageable = other.GetComponent<IDamageable>();

                    if (damageable != null)
                    {
                        damageable.Damage(fightController.punchPower / 2);
                    }
                }

            if (fightController.fighterType == FightController.FighterType.Boss)
                if (other.CompareTag("Player"))
                {
                    IDamageable damageable = other.GetComponent<IDamageable>();

                    if (damageable != null)
                    {
                        damageable.Damage(fightController.punchPower / 2);
                    }
                }
        }
    }
}