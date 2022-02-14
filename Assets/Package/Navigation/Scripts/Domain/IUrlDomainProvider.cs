namespace Elselam.UnityRouter.Domain
{
    public interface IUrlDomainProvider
    {
        /// <summary>
        /// Deep link address of the application
        /// </summary>
        string Url { get; }
    }
}