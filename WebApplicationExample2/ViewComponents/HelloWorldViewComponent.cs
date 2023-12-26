using Microsoft.AspNetCore.Mvc;

namespace WebApplicationExample2.ViewComponents
{
    public class HelloWorldViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // You can perform any logic here if needed
            // For simplicity, let's just return a simple message
            return Content("Hello, World!");
        }
    }
}
