using Hung.StatSystem;
using System;

[Serializable]
public record BonusCriticalDamage: StatChangerSource<Critical>
{
    public BonusCriticalDamage(float value = 0, float duration = 0, Calculator calculator = Calculator.Direct, int immuneStack = 1, float refreshTime = StatSystem.CONSTANT_DURATION) : base(value, duration, calculator, immuneStack, refreshTime)
    {
        sign = +1;
    }
}

[Serializable]
public record BonusAreaEffect: StatChangerSource<AreaEffect>
{
    public BonusAreaEffect(float value = 0, float duration = 0, Calculator calculator = Calculator.Direct, int immuneStack = 1, float refreshTime = StatSystem.CONSTANT_DURATION) : base(value, duration, calculator, immuneStack, refreshTime)
    {
        sign = +1;
    }
}


[Serializable]
public record HealthUp : StatChangerSource<HealthPoint>
{
    public HealthUp(float value = 0, float duration = 0, Calculator calculator = Calculator.MaxPercent, int immuneStack = 1, float refreshTime = StatSystem.CONSTANT_DURATION) : base(value, duration, calculator, immuneStack, refreshTime)
    {
        sign = +1;
    }
}

[Serializable]
public record ATKInscrease: StatChangerSource<AttackDamage>
{
    public ATKInscrease(float value = 0, float duration = 0, Calculator calculator = Calculator.FloorPercent, int immuneStack = 1, float refreshTime = StatSystem.CONSTANT_DURATION) : base(value, duration, calculator, immuneStack, refreshTime)
    {
        sign = +1;
    }
}

[Serializable]
public record SizeUp : StatChangerSource<Radius>
{
    public SizeUp(float value = 0, float duration = 0, Calculator calculator = Calculator.Direct, int immuneStack = 1, float refreshTime = StatSystem.CONSTANT_DURATION) : base(value, duration, calculator, immuneStack, refreshTime)
    {
        sign = +1;
    }
}

[Serializable]
public record Haste: StatChangerSource<MovementSpeed>
{
    public Haste(float value = 0, float duration = 0, Calculator calculator = Calculator.MaxPercent, int immuneStack = 1) : base(value, duration, calculator, immuneStack)
    {
        sign = +1;
    }
}

[Serializable]
public record Slow : StatChangerSource<MovementSpeed>
{
    public Slow(float value = 0, float duration = 0, Calculator calculator = Calculator.MaxPercent, int immuneStack = 1) : base(value, duration, calculator, immuneStack) 
    {
        sign = -1;
    }
}

[Serializable]
public record Windfury : StatChangerSource<AttackSpeed>
{
    public Windfury(float value = 0, float duration = 0, Calculator calculator = Calculator.FloorPercent, int immuneStack = 1, float refreshTime = StatSystem.CONSTANT_DURATION) : base(value, duration, calculator, immuneStack, refreshTime) 
    {
        sign = +1;
    }
}

[Serializable]
public record Cripple: StatChangerSource<AttackSpeed>
{
    public Cripple(float value = 0, float duration = 0, Calculator calculator = Calculator.MaxPercent, int immuneStack = 1, float refreshTime = StatSystem.CONSTANT_DURATION) : base(value, duration, calculator, immuneStack, refreshTime)
    {
        sign = -1;
    }
}

[Serializable]
public record Healing : StatChangerSource<HealthPoint>
{
    public Healing(float value = 0, float duration = 0, Calculator calculator = Calculator.Direct, int immuneStack = 1) : base(value, duration, calculator, immuneStack) 
    {
        sign = +1;
    }
}

[Serializable]
public record Damage : StatChangerSource<HealthPoint>
{
    public bool isCritical;
    public float criticalDamage;
    public float bonusAreaEffect;

    public Damage(float value = 0, float duration = 0, Calculator calculator = Calculator.Direct, int immuneStack = 1, float refreshTime = 5) : base(value, duration, calculator, immuneStack, refreshTime) 
    {
        sign = -1;
    }
}

[Serializable]
public record CooldownReduction : StatChangerSource<Stat>
{
    public CooldownReduction(float value = 0, float duration = 0, Calculator calculator = Calculator.Direct, int immuneStack = 1) :base (value, duration, calculator, immuneStack) 
    {
        sign = -1;
    }
}

[Serializable]
public abstract record StatChangerSource<T> where T : Stat
{
 
    public float value;
    public int sign;
    public float decayTime;
    public Calculator calculator;

    public int id;
    public int immuneStack;
    public float refreshTime;
    public int collideMask;

    public float GetDirectValue(T stat)
    {
        switch (calculator)
        {
            default: return value * sign;

            case Calculator.MaxPercent: return value * stat.max * sign;

            case Calculator.CurrentPercent : return value * stat.current * sign;

            case Calculator.FloorPercent: return value * stat.floor * sign;

            case Calculator.MissingPercent: return value * (stat.max - stat.current) * sign;
        }
    }

    public void Refesh(float value)
    {
        id = StatSystem.ID_COUNTER++;
        this.value = value;
    }

    public StatChangerSource()
    {
        this.id = StatSystem.ID_COUNTER++;
        this.immuneStack = 1;
        this.value = 1;
        this.decayTime = 0;
        this.calculator = Calculator.Direct;
        this.refreshTime = 5;
        this.collideMask = Layer.ENEMY_MASK;
    }

    public StatChangerSource(float value)
    {
        this.id = StatSystem.ID_COUNTER++;
        this.immuneStack = 1;
        this.value = value;
        this.decayTime = 0;
        this.calculator = Calculator.Direct;
        this.refreshTime = 5;
        this.collideMask = Layer.ENEMY_MASK;
    }

    public StatChangerSource(float value, float duration)
    {
        this.id = StatSystem.ID_COUNTER++;
        this.immuneStack = 1;
        this.value = value;
        this.decayTime = duration;
        this.calculator = Calculator.Direct;
        this.refreshTime = 5;
        this.collideMask = Layer.ENEMY_MASK;
    }

    public StatChangerSource(float value, float duration, Calculator calculator)
    {
        this.id = StatSystem.ID_COUNTER++;
        this.immuneStack = 1;
        this.value = value;
        this.decayTime = duration;
        this.calculator = calculator;
        this.refreshTime = 5;
        this.collideMask = Layer.ENEMY_MASK;
    }

    public StatChangerSource(float value = 0, float duration = 0, Calculator calculator = Calculator.Direct, int immuneStack = 1, float refreshTime = 5, int collideMask = Layer.ENEMY_MASK)
    {
        this.id = StatSystem.ID_COUNTER++;
        this.immuneStack = immuneStack;
        this.value = value;
        this.decayTime = duration;
        this.calculator = calculator;
        this.refreshTime = refreshTime;
        this.collideMask = collideMask;
    }
}

public enum DamageType
{
    Direct,
    DoT,
}

public enum Calculator
{
    Direct,
    MaxPercent,
    CurrentPercent,
    FloorPercent,
    MissingPercent
}