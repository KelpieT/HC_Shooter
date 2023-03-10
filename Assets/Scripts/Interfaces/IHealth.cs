public interface IHealth : IDamageble
{
	float Health { get; }
	float MaxHealth { get; }
	void Dead();
}