using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechBuzzApi.Models;

namespace TechBuzzApi.Controllers
{
    public class QuizController : ApiController
    {

        [HttpGet]
        [Route("api/Questions")]
        public HttpResponseMessage GetQuestions()
        {
        
            using (Techbuzz_testEntities db = new Techbuzz_testEntities())
            {
                var Qns = db.Questions
                    .Select(x => new { QnID = x.QnID, Qn = x.Qn, ImageName = x.ImageName, x.Option1, x.Option2, x.Option3, x.Option4, QuesType = x.Ques_type, Answer = x.Answer })
                    //.OrderBy(y => Guid.NewGuid())
                    .Take(10)
                    .ToArray();
                var updated = Qns.AsEnumerable()
                    .Select(x => new
                    {
                        QnID = x.QnID,
                        Qn = x.Qn,
                        ImageName = x.ImageName,
                        Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 },
                        QuesType = x.QuesType,
                        Answer = x.Answer
                    }).ToList();
                return this.Request.CreateResponse(HttpStatusCode.OK, updated);
            }
        }

        [HttpGet]
        [Route("api/Jobs")]
        public HttpResponseMessage GetJobs()
        {
            using (Techbuzz_testEntities db = new Techbuzz_testEntities())
            {
                var Jobs = db.JOB_POSTINGS
                    .Select(x => new { JobId = x.JOB_ID, JobTitle = x.JOB_TITLE, JobDesc = x.JOB_DESCRIPTION, Company = x.COMPANY, Experience = x.experience, PostedBy = x.posted_by, Category =x.category, Location = x.Location })
                    .OrderBy(y => Guid.NewGuid())
                    .Take(10)
                    .ToArray();
                var updated = Jobs.AsEnumerable()
                    .Select(x => new
                    {
                        JobId = x.JobId,
                        JobTitle = x.JobTitle,
                        JobDesc = x.JobDesc,
                        Company = x.Company,
                        Experience = x.Experience,
                        PostedBy = x.PostedBy,
                        Category = x.Category,
                        Location = x.Location
                    }).ToList();
                return this.Request.CreateResponse(HttpStatusCode.OK, updated);
            }
        }


        [HttpPost]
        [Route("api/Answers")]
        public HttpResponseMessage GetAnswers(int[] qIDs)
        {
            using (Techbuzz_testEntities db = new Techbuzz_testEntities())
            {
                var result = db.Questions
                     .AsEnumerable()
                     .Where(y => qIDs.Contains(y.QnID))
                     //.OrderBy(x => { return Array.IndexOf(qIDs, x.QnID); })
                     .Select(z => z.Answer)
                     .ToArray();
                return this.Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        [HttpPost]
        public HttpResponseMessage PostQuery(USER_QUERY uq)
        {
            using ( Techbuzz_testEntities db = new Techbuzz_testEntities())
            {
                db.USER_QUERY.Add(new USER_QUERY() {
                    FULL_NAME = uq.FULL_NAME,
                EMAIL_ADDRESS = uq.EMAIL_ADDRESS,
                USER_DESC = uq.USER_DESC
                }) ;

                db.SaveChanges();
            }
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
