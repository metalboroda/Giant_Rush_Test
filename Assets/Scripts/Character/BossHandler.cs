using UnityEngine;

namespace Assets.Scripts.Character
{
    public class BossHandler : MonoBehaviour
    {
        [SerializeField]
        private int _level;
        [SerializeField]
        private int _health;

        private int health
        {
            get { return _health; }
            set
            {
                _health = value;

                if (_health <= 0)
                {
                    print("Boss Death");
                }
            }
        }
    }
}