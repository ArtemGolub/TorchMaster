using FSM;
using UnityEngine;

public class Throw_State : State
{
    private Transform _myTransform;
    private IInventory _holder;
    private Transform _target;
    private Item _item;

    private float speed = 2f;
    private float offSet = 1f;

    public Throw_State(Item item, Transform myTransform)
    {
        _item = item;
        _myTransform = myTransform;
    }

    public override void Enter()
    {
        //_holder.RemoveItem(_myTransform, _item);
    }

    public void SetHolder(IInventory holder)
    {
        _holder = holder;
    }

public void SetTarget(Transform target)
    {
        _target = target;
    }

    public override void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        if (_target == null)
        {
            //Destroy(gameObject);
            return;
        }
        Vector3 dir = new Vector3(_target.position.x, _target.position.y, _target.position.z) - _myTransform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        
        //TargetLook.LookAtTarget(_myTransform, _target);

        if (dir.magnitude <= distanceThisFrame + offSet)
        {
            HitTarget();
            return;
        }
        _myTransform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    
    
    private void HitTarget()
    {
        // target.GetComponent<AEnemy>().ReciveDamage(Damage);
        // Destroy(gameObject);
    }
}