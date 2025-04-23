using TowerDefenceRoguelike.Gameplay.Base.Abstractions;

public class AttackView : IAttackable
{
    private int _damage;

    public AttackView(int damage)
    {
        _damage = damage;
    }
    
    public void Attack(IDamageable target)
    {
        target.TakeDamage(_damage);
    }
}