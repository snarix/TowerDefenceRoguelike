using TowerDefenceRoguelike.Gameplay.Base.Abstractions;

public interface IAttackable
{
    public void Attack(IDamageable target);
}