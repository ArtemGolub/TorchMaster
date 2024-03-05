using System;

public class RangeObserver : AObserver<IAttackStrategy>
{
    public static RangeObserver current;

    private void Awake()
    {
        current = this;
    }

    private void FixedUpdate()
    {
        TryAttack();
    }

    private void TryAttack()
    {
        foreach (var observer in observers)
        {
            if (observer.IsInRange())
            {
                observer.Attack();
            }
        }
    }
}
