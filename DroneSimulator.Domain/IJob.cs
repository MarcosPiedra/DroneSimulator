using System.Threading.Tasks;

namespace DroneSimulator.Domain
{
    public interface IJob
    {
        void Init();
        void Cancel();
        Task ExecuteAsync();
        bool IsRunning { get; }
    }
}