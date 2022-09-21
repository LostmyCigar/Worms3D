public class PlayerHealth
{
    public delegate void HealthUpdateEvent(Player player);
    public delegate void PlayerDeathEvent(Player player);

    public event HealthUpdateEvent OnHealthUpdate;
    public event PlayerDeathEvent OnPlayerDeath;

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
            OnHealthUpdate?.Invoke(_thisPlayer);
            if (_health <= 0)
            {
                _thisPlayer.Die();
            }
        } 
    }

    public PlayerHealth(Player player , float health)
    {
        Health = health;
        _thisPlayer = player;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
