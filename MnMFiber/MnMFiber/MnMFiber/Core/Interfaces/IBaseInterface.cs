using Prism.Navigation;

namespace MnMFiber.Core.Interfaces
{
    public interface IBaseInterface : INavigatedAware
    {
        void InitiatePageAsync();
        void SaveToSession();
    }
}
