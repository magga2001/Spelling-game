public interface IObserver<T>
{
    public void OnNotify(T data);
}