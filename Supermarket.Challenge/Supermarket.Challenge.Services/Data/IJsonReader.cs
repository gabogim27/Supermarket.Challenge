using Supermarket.Challenge.Domain.Config;
using Supermarket.Challenge.Domain.Entities;

namespace Supermarket.Challenge.Services.Data
{
    public interface IJsonReader
    {
        void ReadDataFromJson(string path);
    }
}
