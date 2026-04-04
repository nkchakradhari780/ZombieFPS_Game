using System.Collections;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Settings")]
    public float damage = 10f;
    public float range = 100f;
    public Camera cam;
    public float fireCharge = 15;
    private float nextTimeToShoot = 0f;
    public playerScript player;
    public Transform hand;

    [Header("Rifle Ammunition and shooting")]
    private int magazineSize = 32;
    private int totalMagazines = 10;
    private int currentAmmo;
    public float reloadingTime = 1.3f;
    private bool setReloading = false;


    [Header("Rifle Effects")]
    public ParticleSystem muzzleSpark;
    public GameObject woodEffect;

    private void Awake()
    {
        transform.SetParent(hand);
        currentAmmo = magazineSize;
    }

    void Update()
    {
        if (setReloading) return;

        if ( currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
    }

    private void Shoot()
    {
        if ( totalMagazines == 0)
        {
            // show ammo out text 
            return;
        }

        currentAmmo--;

        if ( currentAmmo == 0)
        {
            totalMagazines--;
        }

        // Update the UI

        muzzleSpark.Play();     
        RaycastHit hitInfo;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit hit = hitInfo.transform.GetComponent<ObjectToHit>();

            if (hit != null)
            {
                hit.ObjectHitDamage(damage);
                GameObject hitWood = Instantiate(woodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hitWood, 1f);
            }
        }
    }

    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSprintSpeed = 0f;
        setReloading = true;
        Debug.Log("Reloading..........");
        // Play Animatoin
        // Play Reloading Sound 
        yield return new WaitForSeconds(reloadingTime);
        // play Animation
        currentAmmo = magazineSize;
        player.playerSpeed = 1.9f;
        player.playerSprintSpeed = 3f;
        setReloading = false;

    } 
}
