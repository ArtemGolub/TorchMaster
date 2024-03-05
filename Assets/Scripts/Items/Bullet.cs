using System;
using UnityEngine;

public class Bullet : ABullet, IBullet
{
    public float speed;
    public float offSet;
    public Transform target { get; set; }
    public Transform bulletTransform { get; set; }
    private IBullet currentEnchantment;

    public void Hit()
    {
        if (currentEnchantment != null)
        {
            currentEnchantment.Hit();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Seek(Transform Target)
    {
        target = Target;
        bulletTransform = transform;
    }
    public void SetEnchant(EnchantType enchantType)
    { 
        currentEnchantment= EnchantFabric.CreateEnchant(this, enchantType);
    }

    private void FixedUpdate()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = new Vector3(target.position.x, target.position.y, target.position.z) - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        
        //TargetLook.LookAtTarget(this.transform, target);

        if (dir.magnitude <= distanceThisFrame + offSet)
        {
            Hit();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }




}
