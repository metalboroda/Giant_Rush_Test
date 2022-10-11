using UnityEngine;

namespace Assets.Scripts.Level
{
    public class SeaMovement : MonoBehaviour
    {
        private void Update()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        }
    }
}