using UnityEngine;
public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 1000f;

    public Camera fpsCam;

    public ParticleSystem muzzleflash;
    public ParticleSystem lazer;

    public float fireDelay = 0.1f;
    private float lastfired = 0f;

    public float recoilamtV = 1f;
    public float recoilamtH = 0.3f;


    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (Time.time - lastfired > fireDelay)
            {
                ShootFirst();
                lastfired = Time.time; 
            }
        }
    }

    void ShootFirst()
    {
        muzzleflash.Play();
        lazer.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            target targ = hit.transform.GetComponent<target>();
            if (targ != null)
            {
                targ.TakeDamage(damage);
            }
        }
        MouseLook look = fpsCam.transform.GetComponent<MouseLook>();
        look.Recoil(recoilamtV, recoilamtH);
    }
}
