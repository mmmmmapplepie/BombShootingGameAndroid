public interface IDamageable {
  public float currentLife { get; set; }
  public float maxLife { get; set; }
  public int Shield { get; set; }
  public int MaxShield { get; set; }
  void takeTrueDamage(float Damage);
  void takeDamage(float Damage);
  void AoeHit(float Damage);
  void ChainExplosion();
}
