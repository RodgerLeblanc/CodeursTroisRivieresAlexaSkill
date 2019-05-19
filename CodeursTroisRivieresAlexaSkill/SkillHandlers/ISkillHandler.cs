using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeursTroisRivieresAlexaSkill.SkillHandlers
{
    public interface ISkillHandler
    {
        Task<IActionResult> GetResultAsync();
    }
}
