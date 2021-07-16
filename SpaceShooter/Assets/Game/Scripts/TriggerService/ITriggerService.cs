public interface ITriggerService
{
    void AddListener(TriggerType type, System.Action<TriggerType, object> call);
    void RemoveListener(TriggerType type, System.Action<TriggerType, object> call);
    void ClearListeners();
    void FireEvent(TriggerType type, object param);
}
