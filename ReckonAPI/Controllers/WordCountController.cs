using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReckonAPI.Service.Interfaces;
using ReckonAPI.Shared.Entities;
using Microsoft.Extensions.Logging;

namespace ReckonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountController : ControllerBase
    {

        private readonly ILogger<WordCountController> _logger;
        private readonly IWordCountService _wordCountService;
        private readonly IReckonTextService _reckonTextService;
        public WordCountController(ILogger<WordCountController> logger, IWordCountService wordCountService, IReckonTextService textService)
        {
            _logger = logger;
            _wordCountService = wordCountService;
            _reckonTextService = textService;            
        }

        [HttpGet]
        public async Task<WordCountOutput> Get()
        {
            WordCountOutput result = new WordCountOutput();
            try
            {
                var textToSearch = await _reckonTextService.GetTextToSearchAsync();
                var subTexts = await _reckonTextService.GetSubTextsAsync();

                result = _wordCountService.GetWordCount(textToSearch, subTexts.ToArray());

                await _reckonTextService.PostResultAsync(result);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }
    }
}
