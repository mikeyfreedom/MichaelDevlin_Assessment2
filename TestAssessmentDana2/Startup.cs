using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestAssessmentDana2.Startup))]
namespace TestAssessmentDana2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
