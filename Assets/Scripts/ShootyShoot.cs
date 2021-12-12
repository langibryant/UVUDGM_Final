using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyShoot : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bulletPrefab;
    // public GameObject muzzleFlash;
    public int maxAmmo = 10;
    public int currentAmmo = 10;
    public float reloadTime = 1.5f;
    public float shootTime = 0.35f;
    private Animator anim;


    private float bulletForce = 100.0f;
    public bool isReloading = false;
    public bool isShooting = false;

    void Start(){
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !isShooting && !isReloading) {
            StartCoroutine(Shoot());
        }
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo) {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload() {
        isReloading = true;
        anim.SetBool("isReloading", true);
        
        yield return new WaitForSeconds(reloadTime - 0.25f);
        anim.SetBool("isReloading", false);

        yield return new WaitForSeconds(.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    IEnumerator Shoot() {
        if(currentAmmo > 0) {
            anim.SetBool("isShooting", true);
            isShooting = true;
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(spawnPoint.up * bulletForce, ForceMode2D.Impulse);
            currentAmmo -= 1;
            yield return new WaitForSeconds(shootTime - .05f);
            anim.SetBool("isShooting", false);

            yield return new WaitForSeconds(.05f);
            isShooting = false;
        }
        else {
            StartCoroutine(Reload());
        }
        
        // GameObject effect = Instantiate(muzzleFlash, spawnPoint.position, spawnPoint.rotation);
        // Destroy(effect, 14f);
    }
}
