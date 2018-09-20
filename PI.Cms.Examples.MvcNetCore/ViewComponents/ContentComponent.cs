using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using PI.Cms.Core.Content.Interfaces;

namespace PI.Cms.Examples.MvcNetCore.ViewComponents
{
    public class ContentViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IViewModel contentModel)
        {
            if (contentModel == null) return View("empty");
            //using the IViewModel sent we call the view to render and send the content model
            return View(contentModel.View, contentModel);
        }
    }
}
