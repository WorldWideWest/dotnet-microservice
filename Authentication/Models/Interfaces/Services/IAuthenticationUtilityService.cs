using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Models.DTOs.General;

namespace Models.Interfaces.Services
{
    public interface IAuthenticationUtilityService
    {
        void SendActionEmail(ActionEmailContext actionEmailContext);
    }
}
