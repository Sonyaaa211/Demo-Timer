using Hung.Pooling;
using Hung.StatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IUnitFlow
{
    void OnFinishDead();
}


public abstract partial class Unit : MonoBehaviour, IDamageable, IFollowable, ITransformizable, IUnitFlow
{
    [field: Range(1, 10)][field: SerializeField] public float visualSize { get; private set; }

    [field: SerializeField] public bool isDead { get; protected set; }
    [field: SerializeField] public Transform FollowTarget { get; private set; }
    [field: SerializeField] public Transform Head { get; private set; }
    [field: SerializeField] public HealthPoint HP { get; private set; }

    public event Action<float> OnHealthChanged;

    [SerializeField] protected UIHealthBar m_healthBar;

    public float percentHP => HP.current / HP.max;

    public DamageDealtInfo TakeChange(Damage damage)
    {
        if (isDead) return new();
        //Debug.Log(GetType().ToString() + " is taking dmg");
        _dealInfo.expected = damage.GetDirectValue(HP);
        if (damage.isCritical)
        {
            Debug.Log("Critical " + damage.criticalDamage);
            //Debug.Log("Damage: " + changedValue);
            _dealInfo.expected *= damage.criticalDamage;
            //Debug.Log("To damage: " + changedValue); 
        }
        _dealInfo.actual = HP.AttempChange(_dealInfo.expected);
        
        if (HP.current + _dealInfo.actual <= 0)
        {
            m_healthBar?.Unfollow();
            Die();
        }
        else
        {
            Hit();
            if (m_healthBar == null)
            {
                StaticPool.Instance.Spawn(WorldUIType.HealthBar, out m_healthBar);

                m_healthBar.SetFollow(this);
            }

            OnHealthChanged?.Invoke(_dealInfo.actual / HP.max);
        }

        HP.Change(_dealInfo.actual);
        return _dealInfo;
    }

    DamageDealtInfo _dealInfo;

    public abstract void Die();

    public abstract void Hit();


    public virtual void OnFinishDead()
    {
        
    }
}
