using System.Web.Mvc;

namespace AjModel.WebMvc.Areas.ModelArea
{
    public class ModelAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ModelArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ModelArea_default",
                "Model/{entity}/{action}/{id}",
                new { controller = "Model", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
