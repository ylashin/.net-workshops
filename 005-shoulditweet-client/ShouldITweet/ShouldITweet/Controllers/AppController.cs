using Serilog;
using ShouldITweetClient.Data;
using ShouldITweetClient.Filters;
using ShouldITweetClient.Logic;
using ShouldITweetClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace ShouldITweetClient.Controllers
{
    public class AppController : ApiController
    {
        private IVerbotenChecker VerbotenChecker;
        private IRepository<VerbotenPhrase> Repository;

        
        public AppController(IVerbotenChecker verbotenChecker, IRepository<VerbotenPhrase> repository)
        {
            VerbotenChecker = verbotenChecker;
            Repository = repository;

        }

        [HttpGet]
        [Route("api/app/phrases/getall")]
        public IEnumerable<VerbotenPhraseDto> GetAllPhrases()
        {
            return Repository.GetAll().Select(a=>a.MapToDto());
        }

        [HttpGet]
        [Route("api/app/phrases/{id}")]
        public VerbotenPhraseDto GetPhrase(Guid id)
        {
            return Repository.GetById(id).MapToDto();
        }




        [HttpPost]
        [Route("api/app/tweet/check")]
        [ValidateModel]
        public Tweet Post([FromBody]Tweet tweet)
        {
            if (tweet == null || string.IsNullOrWhiteSpace(tweet.Text))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var checkResponse = VerbotenChecker.ValidateText(tweet.Text);

            tweet.VerbotenCheckPassed = checkResponse.IsSafeText;
            tweet.Violations = checkResponse.Violations;

            return tweet;

        }

        [HttpPost]
        [Route("api/app/phrases/add")]
        public VerbotenPhraseDto Create([FromBody] VerbotenPhraseDto verbotenPhraseDto)
        {

            if (verbotenPhraseDto == null || string.IsNullOrWhiteSpace(verbotenPhraseDto.Phrase))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var verbotenPhrase = VerbotenPhrase.Create(verbotenPhraseDto.Phrase);
            Repository.AddOrUpdate(verbotenPhrase);

            Log.Information("Created verboten phrase {PhraseText}", verbotenPhrase.Phrase);

            return verbotenPhrase.MapToDto();
        }


        [HttpPut]
        [Route("api/app/phrases/update")]
        public VerbotenPhraseDto Update([FromBody] VerbotenPhraseDto verbotenPhraseDto)
        {
            if (verbotenPhraseDto == null || string.IsNullOrWhiteSpace(verbotenPhraseDto.Phrase) ||
                verbotenPhraseDto.Id == Guid.Empty)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            VerbotenPhrase verbotenPhrase = Repository.GetById(verbotenPhraseDto.Id);

            if (verbotenPhrase == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            verbotenPhrase.UpdatePhrase(verbotenPhraseDto.Phrase);

            Repository.AddOrUpdate(verbotenPhrase);

            Log.Information("Updated verboten phrase {PhraseID}", verbotenPhrase.Id);

            return verbotenPhrase.MapToDto();
        }


        [HttpDelete]
        [Route("api/app/phrases/delete/{id}")]
        public void Delete(Guid id)
        {
            VerbotenPhrase verbotenPhrase = Repository.GetById(id);
            if (verbotenPhrase == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Repository.Delete(verbotenPhrase);
            Log.Information("Deleted verboten phrase {PhraseID}", id);            
        }


    }
}