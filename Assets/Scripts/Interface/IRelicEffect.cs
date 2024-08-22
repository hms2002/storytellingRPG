public interface IRelicEffect
{
    void ApplyEffect(Actor player);
    void OnTurnStart(Actor player);
    void OnTurnEnd(Actor player);
    void OnBattleStart(Actor player);
    void OnBattleEnd(Actor player);
}
