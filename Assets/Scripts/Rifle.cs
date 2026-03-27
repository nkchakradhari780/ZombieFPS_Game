using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Settings")]
    public float damage = 10f;
    public float range = 100f;
    public Camera cam;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hitInfo;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);
        }
    }
}
