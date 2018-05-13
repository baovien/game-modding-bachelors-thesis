public interface IEnemy
{
    int AttackDamage { get; }
    float HitPoints { get; set; }
    float MoveSpeed { get; set; }

    void Start();
    void Update();

}
