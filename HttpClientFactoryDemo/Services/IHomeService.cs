using System.Threading.Tasks;

namespace HttpClientFactoryDemo.Services
{
    public interface IHomeService
    {
        Task<string> GetPosts();
    }
}
