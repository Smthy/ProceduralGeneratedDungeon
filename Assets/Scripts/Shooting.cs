using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public float damage;
    public float range;

    public Camera FPS;

    public ParticleSystem muzzle;

    public int maxAmmo = 16;
    private int currentAmmo;

    public static bool reload;
    public float reloadSpeed;

    public Text ammoCount;

    public AudioSource reloading, shooting;

    private void Start()
    {
        currentAmmo = maxAmmo;
        ammoCount.text = (currentAmmo.ToString() + "/" + maxAmmo.ToString());
    }

    private void Update()
    {
        ammoCount.text = (currentAmmo.ToString() + "/" + maxAmmo.ToString());

        if (currentAmmo == 0)
        {
            //reloading.Play();
            Debug.Log("Reloading");
            reload = true;            
            StartCoroutine("Reloading");
        }

        if (Input.GetButtonDown("Fire1") && !reload)
        {     
            currentAmmo--;
            ammoCount.text = (currentAmmo.ToString() + "/" + maxAmmo.ToString());
            Shoot();
        }

        if (Input.GetKey(KeyCode.R))
        {
            //reloading.Play();
            reload = true;           
            StartCoroutine("Reloading");
        }


    }
    void Shoot()
    {
        int layer_mask = LayerMask.GetMask("Enemy");

        shooting.Play();

        muzzle.Play();

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
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(1f);
        currentAmmo = maxAmmo;
        ammoCount.text = (currentAmmo.ToString() + "/" + maxAmmo.ToString());
        reload = false;
    }

}
