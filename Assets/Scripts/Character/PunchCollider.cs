using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PunchCollider : MonoBehaviour
    {
        [SerializeField]
        private CharacterType characterType;

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                switch (characterType)
                {
                    case CharacterType.Player:
                        damageable.Damage(PlayerController.instance.powerCount);
                        break;
                    case CharacterType.Boss:
                        damageable.Damage(BossController.instance.powerCount);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public enum CharacterType
    {
        Player,
        Boss
    }
}