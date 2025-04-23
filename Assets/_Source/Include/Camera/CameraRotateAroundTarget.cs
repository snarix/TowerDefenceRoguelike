using UnityEngine;

namespace Include
{
    public class CameraRotateAroundTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float rotationSpeed = 50f;
        
        private void Update() => transform.RotateAround(target.position, Vector3.down, rotationSpeed * Time.deltaTime);
    }
}