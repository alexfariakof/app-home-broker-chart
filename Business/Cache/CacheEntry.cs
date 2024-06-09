namespace HomeBroker.Business.Cache;
public class CacheEntry<T>
{
    public T Data { get; }
    public DateTime Timestamp { get; }
    public DateTime ExpirationTime { get; set; }

    public CacheEntry(T data, DateTime timestamp, TimeSpan expirationTime)
    {
        Data = data;
        Timestamp = timestamp;
        ExpirationTime = timestamp.Add(expirationTime);
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow > ExpirationTime;
    }
}