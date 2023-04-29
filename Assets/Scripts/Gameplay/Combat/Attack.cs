using System.Collections.Generic;

public enum AttackType
{
    Basic
}

struct AttackInput
{
    AttackType type;
    float time;

    public AttackInput(AttackType type, float time)
    {
        this.type = type;
        this.time = time;
    }

    public float Time
    {
        get { return time; }
        set { time = value; }
    }

    public AttackType Type
    {
        get { return type; }
        set { type = value; }
    }

    public override string ToString()
    {
        return type.ToString();
    }
}

[System.Serializable]
public class Attack
{
    public AttackType type;
    public string anim;
    public float duration;
}

[System.Serializable]
public class Combo
{
    public List<AttackType> attackTypes;
    public int damage;
}