using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAI : MonoBehaviour
{
    private GameObject[] multipleEnemies;
    public Transform closestEnemy;
    public bool enemyContact;
    public float speed = 5f;

    // public Camera cam;
    Vector2 targetPos;
    public Rigidbody2D soldierRB;

    public float rr = 90;

    public Transform spawnPoint;
    public GameObject bulletPrefab;
    // public GameObject muzzleFlash;
    public int maxAmmo = 10;
    public int currentAmmo = 10;
    public float reloadTime = 1.5f;
    public float shootTime = 0.35f;
    private Animator anim;

    // shooting mechanics
    public float bulletForce = 2.0f;
    public bool isReloading = false;
    public bool isShooting = false;


    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.GetChild(0);
        closestEnemy = null;
        enemyContact = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        closestEnemy = getClosestEnemy();
        if(closestEnemy) {
            Vector2 soldierCords = new Vector2(transform.position.x, transform.position.y); 
            targetPos = closestEnemy.transform.position;
            if(Vector2.Distance(soldierCords,targetPos) < 15) {
                if(closestEnemy && !isShooting && !isReloading) {
                    StartCoroutine(Shoot());
                }
                if (closestEnemy && currentAmmo == 0) {
                    StartCoroutine(Reload());
                }
            }
        }
    }
    
    void FixedUpdate(){
        Vector2 lookDir = targetPos - soldierRB.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        soldierRB.rotation = angle;
    }

    public Transform getClosestEnemy() {
        multipleEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach (GameObject go in multipleEnemies) {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if(currentDistance < closestDistance) {
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
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
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.AddForce(spawnPoint.up * bulletForce, ForceMode2D.Impulse);
            currentAmmo -= 1;
            yield return new WaitForSeconds(shootTime - .05f);
            anim.SetBool("isShooting", false);

            yield return new WaitForSeconds(.05f);
            isShooting = false;
        }
        else {
            StartCoroutine(Reload());
        }
        
    }
}
