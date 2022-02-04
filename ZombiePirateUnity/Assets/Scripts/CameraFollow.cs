
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;



    private void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
