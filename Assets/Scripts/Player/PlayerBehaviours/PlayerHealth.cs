using System.Diagnostics;

public class PlayerHealth
{
    public delegate void HealthUpdateEvent(Player player);
    public event HealthUpdateEvent OnHealthUpdate;

    private float _maxHealth;
    private float _health;
    private Player _thisPlayer;
    public float Health 
    {
        get
        {
            return _health;
        } 
        set 
        { 
            _health = value;
            OnHealthUpdate?.Invoke(_thisPlayer); //Currently not in use. Included so that the healthbar can update during a players turn
                                                 //Incase we want to enable something like friendly fire
            if (_health <= 0) { _thisPlayer.Die(); } 
            if (_health > _maxHealth) { Health = _maxHealth; }
        } 
    }

    public PlayerHealth(Player player, float health)
    {
        _maxHealth = health;
        Health = health;
        _thisPlayer = player;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
