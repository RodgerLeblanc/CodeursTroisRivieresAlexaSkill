using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeursTroisRivieresAlexaSkill.RequestHandlers
{
    public interface IRequestHandler
    {
        Task<IActionResult> GetResultAsync();
    }
}
