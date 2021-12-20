namespace Elselam.UnityRouter.Installers
{
    public interface IScreenFactory<T, T2>
    {
        T2 Create(T t);
    }
}