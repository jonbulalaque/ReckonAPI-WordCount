using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ReckonAPI.Shared.Entities;
namespace ReckonAPI.Service.Interfaces
{
    public interface IReckonTextService
    {
        Task<IEnumerable<string>> GetSubTextsAsync();
        Task<string> GetTextToSearchAsync();
        Task PostResultAsync(WordCountOutput postResult);
    }
}
