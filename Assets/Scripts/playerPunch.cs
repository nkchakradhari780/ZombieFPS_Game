using UnityEngine;

public class playerPunch : MonoBehaviour
{
    [Header("Player punch var")]
    public Camera cam;
    public float damageOf = 10f;
    public float punchingRange = 5f;

    [Header("Puching Effects")]
    public GameObject woodEffect;

    public void Punch()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, punchingRange))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit hit = hitInfo.transform.GetComponent<ObjectToHit>();

            if (hit != null)
            {
                hit.ObjectHitDamage(damageOf);
                GameObject hitWood = Instantiate(woodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hitWood, 1f);
            }
        }
    }
}
