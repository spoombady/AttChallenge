using ATTRepos.Issues.DataAccess.RepoIssues.Client;
using System.Web.Http;

namespace ATTRepos.Issues.WebServices.Controllers
{
    /// <summary>
    ///  API to get open issues for ATT/ast repository
    /// </summary>
    /// <remarks>API to get open issues for ATT/ast repository</remarks>

    public class IssuesController : ApiController
    {
        public IssuesController()
        {}

        [Route]
        public IHttpActionResult GetOpenIssues()
        {
            RepoClient rc = new RepoClient();
            return Ok(rc.GetRepoIssues());
        }
     
    }
}
