using System;
using System.Collections.Generic;
using System.Text;
using ReckonAPI.Service.Interfaces;
using ReckonAPI.Shared.Entities;

namespace ReckonAPI.Service.Services
{
    public class WordCountService : IWordCountService
    {
        public WordCountOutput GetWordCount(string stringToCheck, IEnumerable<string> stringsToSearch)
        {
            var result = new WordCountOutput() { Candidate = "Jon Bulalaque", Text = stringToCheck };
            var subTextResults = new List<SubTextIndexResult>();

            foreach(var search in stringsToSearch)
            {
                var subTextResult = new SubTextIndexResult() { Subtext = search };
                var indexes = FindIndexes(stringToCheck, search);
                subTextResult.Result = indexes.Count > 0 ? string.Join(", ", indexes) : "<No Output>";
                subTextResults.Add(subTextResult);
            }

            result.Results = subTextResults;

            return result;
        }

        private List<int> FindIndexes(string mainString, string subText)
        {
            var indexes = new List<int>();

            for (var index = 0; index < mainString.Length; index++)
            {
                var combinedIndex = index + subText.Length;
                if (combinedIndex < mainString.Length)
                {
                    StringBuilder textToSearch = new StringBuilder();
                    int startIndex = index;
                    while (startIndex < combinedIndex)
                    {
                        textToSearch.Append(mainString[startIndex]);
                        startIndex++;
                    }

                    if (textToSearch.ToString().ToLower() == subText.ToLower())
                    {
                        indexes.Add(index + 1);
                    }
                }
            }

            return indexes;
        }
    }
}
