using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARShooting : MonoBehaviour
{
    public float damage;
    public float range;
    public float fireRate;

    public Camera FPS;
    public ParticleSystem muzzle;

    public int maxAmmo = 30;
    private int currentAmmo;

    public static bool reload;
    public float reloadSpeed;

    public Text ammoCount;
    private float next = 0f;

    public GameObject impact;

    public AudioSource reloading, shooting;

    private void Start()
    {
        currentAmmo = maxAmmo;
        ammoCount.text = (currentAmmo.ToString() + "/" + maxAmmo.ToString());
    }

    private void Update()
    {
        ammoCount.text = (currentAmmo.ToString() + "/" + maxAmmo.ToString());

        if (Input.GetButton("Fire1") && !reload && Time.time >= next)
        {
           currentAmmo--;
           ammoCount.text = (currentAmmo.ToString() + "/" + maxAmmo.ToString());
           next = Time.time + 1f / fireRate;
           Shoot();
        }

        if (Input.GetKey(KeyCode.R))
        {
            reload = true;
            //reloading.Play();
            StartCoroutine("Reloading");
        }

        if (currentAmmo == 0)
        {
            Debug.Log("Reloading");
            reload = true;
            //reloading.Play();
            StartCoroutine("Reloading");
        }
    }
    void Shoot()
    {
        int layer_mask = LayerMask.GetMask("Enemy");

        muzzle.Play();
        shooting.Play();

        RaycastHit hit;
        if (Physics.Raycast(FPS.transform.position, FPS.transform.forward, out hit, range, layer_mask))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }            
        }

        Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
    }

    IEnumerator Reloading()
    {        
        yield return new WaitForSeconds(3f);
        currentAmmo = maxAmmo;
        ammoCount.text = (currentAmmo.ToString() + "/" + maxAmmo.ToString());
        reload = false;
    }

}
