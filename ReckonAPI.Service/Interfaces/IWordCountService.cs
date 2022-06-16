using System;
using System.Collections.Generic;
using System.Text;
using ReckonAPI.Shared.Entities;
namespace ReckonAPI.Service.Interfaces
{
    public interface IWordCountService
    {
        WordCountOutput GetWordCount(string stringToCheck, IEnumerable<string> stringsToSearch);
    }
}
