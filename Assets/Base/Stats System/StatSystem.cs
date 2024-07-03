using Hung.StatSystem;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Hung.StatSystem
{
    public static class StatSystem
    {
        public static int ID_COUNTER;

        public const float TICK_TIME = 0.25f;

        public static readonly WaitForSeconds DOT_DELAY = new WaitForSeconds(StatSystem.TICK_TIME);

        public const float CONSTANT_DURATION = 999f;
    }

    [Serializable]
    public class Critical : Stat
    {
        public const float DEFAULT_CRITICAL_DAMAGE = 1.4f;
        public override void Refresh()
        {
            floor = DEFAULT_CRITICAL_DAMAGE;
            base.Refresh();
        }
    }

    [Serializable]
    public class AreaEffect: Stat
    {
        public override void Refresh()
        {
            //floor = 0;
            base.Refresh();
        }
    }

    [Serializable]
    public class Radius : Stat
    {
        public float percent => current / max;
        
        public void SetCurrentPercent(float percent)
        {
            current = max * percent;
        }
    }

    [Serializable]
    public class AttackSpeed: Stat
    {

    }

    [Serializable]
    public class AttackDamage: Stat
    {

    }

    [Serializable]
    public class HealthPoint: Stat
    {

    }

    [Serializable]
    public class MovementSpeed: Stat
    {
        public static MovementSpeed operator +(MovementSpeed ms, StatChangerSource<MovementSpeed> statChanger)
        {
            ms.Change(statChanger.GetDirectValue(ms));
            return ms;
        }

        //public static MovementSpeed operator *(MovementSpeed ms, StatChangerSource<MovementSpeed> statChanger)
        //{
        //    ms.Modify(statChanger.GetDirectValue(ms));
        //    return ms;
        //}

        public void AddModify(StatChangerSource<MovementSpeed> statChanger)
        {
            AddModify_Core(statChanger);
        }

        public void RemoveModify(StatChangerSource<MovementSpeed> statChanger)
        {
            RemoveModify_Core(statChanger);
        }
    }

    [Serializable]
    public class Stat
    {
        [field: SerializeField] public float floor { get; protected set; }
        [field: SerializeField] public float current { get; protected set; }
        [field: SerializeField] public float max { get; protected set; }

        [field: SerializeField] public Amount amount { get; protected set; }

        public bool isOverkill;

        public bool isFull => current >= max;

        public float GetScaled(float scale)
        {
            return current * scale;
        }

        public void SetValue(float value)
        {
            floor = value;
            Refresh();
        }

        public virtual void Refresh()
        {
            current = floor;
            max = floor;
            amount = new Amount();
        }

        public float Change(float signedValue)
        {
            float _pre = current;
            current = Mathf.Clamp(current + signedValue, 0, max);
            //current = Mathf.Min(current, max + signedValue);
            if (isOverkill && current == 0) return signedValue;
            return current - _pre;
        }

        public float AttempChange(float signedValue)
        {
            float _attemp = Mathf.Clamp(current + signedValue, 0, max);
            if (isOverkill && _attemp == 0) return signedValue;
            return _attemp - current;
        }

        //public float Modify(float signedValue)
        //{
        //    float _pre = max;
        //    max += signedValue;
        //    if (max < 0) max = 0;

        //    float _change = max - _pre;

        //    current += _change;

        //    return _change;
        //}

        protected void AddModify_Core<T>(StatChangerSource<T> statChanger) where T: Stat
        {
            amount.Add(statChanger);
        } 

        protected void RemoveModify_Core<T>(StatChangerSource<T> statChanger) where T: Stat
        {
            amount.Minus(statChanger);
        }

        public void UpdateValues()
        {
            float premax = max;
            max = floor * amount;
            if (max < 0) max = 0;

            current = Mathf.Clamp(0, current + max - premax, max);
        }
    }

    [Serializable]
    public record Stacking
    {
        public int stack;
        public float refreshTime;

        public Stacking(float refreshTime)
        {
            this.stack = 0;
            this.refreshTime = refreshTime;
        }

        public Stacking(int stack, float refreshTime)
        {
            this.stack = stack;
            this.refreshTime = refreshTime;
        }
    }

    [Serializable]
    public record Amount
    {
        [field: SerializeField] public float plus { get; private set; }
        [field: SerializeField] public float multiply { get; private set; }

        public Amount()
        {
            plus = 0;
            multiply = 1;
        }


        public static Amount operator +(Amount amount, float plusValue)
        {
            amount.plus += plusValue;
            return amount;
        }

        public static Amount operator *(Amount amount, float multiplyValue)
        {
            amount.multiply += multiplyValue;
            return amount;
        }

        public void Add<T>(StatChangerSource<T> statChanger) where T : Stat
        {
            if (statChanger.calculator == Calculator.Direct)
            {
                plus += statChanger.value * statChanger.sign;
            }
            else
            {
                multiply += statChanger.value * statChanger.sign;
            }
        }

        public void Minus<T>(StatChangerSource<T> statChanger) where T: Stat
        {
            if (statChanger.calculator == Calculator.Direct)
            {
                plus -= statChanger.value * statChanger.sign;
            }
            else
            {
                multiply -= statChanger.value * statChanger.sign;
            }
        }

        public static float operator *(float value, Amount amount)
        {
            return (value + amount.plus) * amount.multiply;
        }      
    }
}

public interface IHealthScaleable: IEffectable, IHP, IModifyStat<HealthUp, HealthPoint>
{

}

public interface IMoveable: IEffectable, IMovementSpeed, IModifyStat<Slow, MovementSpeed>, IModifyStat<Haste, MovementSpeed>
{

}

public interface IDamageable: IEffectable, IHP, IChangeStat<Damage, HealthPoint>
{
    Transform Head { get; }
}

public interface IRegenation: IEffectable, IHP, IHPRegen, IChangeStat<Healing, HealthPoint>
{
    void Regenerate();
}

public interface IHealable: IEffectable, IHP, IChangeStat<Healing, HealthPoint>, IModifyStat<Healing, HealthPoint>
{
     
}

public interface IAttackable: IEffectable, IATK, IATKSpeed, IModifyStat<Windfury, AttackSpeed>, IModifyStat<Cripple, AttackSpeed>, IModifyStat<ATKInscrease, AttackDamage>
{

}

public interface IHaveCooldown: ICooldown, IModifyStat<CooldownReduction, Stat>
{

}

public interface IEffectable: IMono
{
    
}

public interface IModifyStat<T, S> where T: StatChangerSource<S> where S : Stat
{
    void TakeModify(T statChanger, Action onModifyEnd = null);

    void RemoveModify(T statChanger);
}

public struct DamageDealtInfo
{
    public float actual;
    public float expected;
}

public interface IChangeStat<T, S> where T: StatChangerSource<S> where S : Stat
{
    DamageDealtInfo TakeChange(T statChanger);
}

public interface IAOE: IUnitStat
{
    AreaEffect AOE{ get; }
}

public interface ICooldown: IUnitStat
{
    Stat Cooldown { get; }
}

public interface IHP: IUnitStat
{
    HealthPoint HP { get; }
}

public interface IHPRegen: IUnitStat
{
    float HPRegen { get; set; }
}

public interface IATK: IUnitStat
{
    AttackDamage ATK { get; }
}

public interface ICritical: IUnitStat
{
    Critical CriticalDMG { get; }
}

public interface IArmor: IUnitStat
{
    float Armor { get; set; }
}

public interface IATKRange: IUnitStat
{
    Radius ATKRange { get; }
}

public interface IATKSpeed: IUnitStat
{
    AttackSpeed ATKSpeed { get; }
}

public interface IMovementSpeed: IUnitStat
{
    MovementSpeed MS { get; }
}

public interface IProjectileSpeed: IUnitStat
{
    float ProjectileSpeed { get; set; }
}

public interface IATKScale: IAbilityStat
{
    float ATKScale { get; }
}

public interface IATKScale2: IAbilityStat
{
    float ATKScale2 { get; }
}

public interface IMaxHealthScale: IAbilityStat
{
    float MaxHealthScale { get; }
}

public interface ISlowScale : IAbilityStat
{
    float SlowPercent { get; }
}

public interface ISkillRadius: IAbilityStat
{
    Radius SkillRadius { get; }
}

public interface ISkillCone: ISkillRadius
{
    float ConeAngle { get; }
}

public interface ISkillQuantity
{
    int Quantity { get; }
}

public interface ISkillDuration
{
    float LastDuration { get; }
}

public interface IUnitStat: IStat
{
    
}

public interface IAbilityStat: IStat
{

}

public interface IStat
{

}
