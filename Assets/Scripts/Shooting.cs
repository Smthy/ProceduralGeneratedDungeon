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

    private void Start()
    {
        currentAmmo = maxAmmo;
        ammoCount.text = (currentAmmo.ToString() + "/" + maxAmmo.ToString());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !reload)
        {

            if (currentAmmo <= 0)
            {
                Debug.Log("Empty");
            }
            else
            {
                currentAmmo--;
                ammoCount.text = (currentAmmo.ToString() + "/" + maxAmmo.ToString());
                Shoot();
            }

            if (Input.GetKey(KeyCode.R))
            {
                reload = true;
                StartCoroutine(Reloading());
            }

            
        }
    }
    void Shoot()
    {
        int layer_mask = LayerMask.GetMask("Enemy");

        muzzle.Play();

        RaycastHit hit;
        if (Physics.Raycast(FPS.transform.position, FPS.transform.forward, out hit, range, layer_mask))
        {
            Debug.Log(hit.transform.name);
            /*
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            */
        }
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(0.5f);
        currentAmmo = maxAmmo;
        ammoCount.text = (currentAmmo.ToString() + "/" + maxAmmo.ToString());
        reload = false;
    }

}
