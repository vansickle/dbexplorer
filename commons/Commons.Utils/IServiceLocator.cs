namespace Commons.Utils
{
    public interface IServiceLocator
    {
        T Get<T>(string key);
        T Get<T>();
    }
}
