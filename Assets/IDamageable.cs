public interface IDamageable
{
    public bool IsAlly(EntityType type);
    public void TakeDamage(DamageData damageData);
    public bool IsInvunerable();
    public bool IsDead();
    public void Death();
}
