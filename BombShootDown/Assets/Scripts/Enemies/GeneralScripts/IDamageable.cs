public interface IDamageable {
  Enemy data { get; }
  int Armor { get; set; }
  bool dead { get; set; }
  float currentLife { get; set; }
  float maxLife { get; set; }
  int Shield { get; set; }
  int MaxShield { get; set; }
  void takeTrueDamage(float Damage);
  void takeDamage(float Damage);
  void AoeHit(float Damage);
  void ChainExplosion();
}
