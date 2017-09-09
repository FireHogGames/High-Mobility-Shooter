using UnityEngine;
using System.Collections;
using TMPro;

public class Weapon : MonoBehaviour {

    //publics
    public string targetTag = "Target";
    public float fireRate = 15f;
    public float damage = 5f;
    public float range = 100f;
    public LayerMask mask;
    public Camera playerCamera;
    public float standardbullets = 10;
    public ParticleSystem muzzleFlash;
    public float reloadTime;
    public Animator animator;

    private TMP_Text bulletText;
    private float bullets;
    private float nextTimeToFire = 0f;
    private bool canShoot;
    private bool isReloading;


    private void Start()
    {
        bulletText = GetComponent<PlayerSetup>().ui.bulletText;
        SetAmmo();
        canShoot = true;
        isReloading = false;
    }

    private void SetAmmo()
    {
        bullets = standardbullets;
    }

    //test
    private void Update()
    {
        bulletText.text = bullets.ToString();

        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if(bullets <= 0f)
        {
            CallReload();
            isReloading = true;
            canShoot = false;
        }

        //press r to reload
        if(bullets < standardbullets && Input.GetKeyDown(KeyCode.R))
        {
            CallReload();
            isReloading = true;
            canShoot = false;
        }
    }

    //test
    private void Shoot()
    {
        if (!canShoot)
            return;

        muzzleFlash.Play();

        bullets--;

        RaycastHit _hit;

        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out _hit, range, mask))
        {
            if(_hit.collider.tag == targetTag)
            {
                Target _target = _hit.collider.GetComponent<Target>();
                OnHitTarget(damage, _target);
            }
        }
    }

    private void OnHitTarget(float _damage, Target target)
    {
        if(target != null)
        {
            target.TakeDamage(_damage);
        }
    }

    private void CallReload()
    {
        if (isReloading)
            return;

        StartCoroutine(Reload());
    }

    //reload
    private IEnumerator Reload()
    {
        animator.SetBool("isReloading", true);

        yield return new WaitForSeconds(reloadTime - .45f);

        SetAmmo();
        animator.SetBool("isReloading", false);
        isReloading = false;

        yield return new WaitForSeconds(.45f);

        canShoot = true;
    }
}
