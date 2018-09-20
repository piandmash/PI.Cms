using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using PI.Cms.Core.Content.Interfaces;
using PI.Cms.Core.Content.Models;
using PI.Cms.Examples.MvcNetCore.Models;


namespace PI.Cms.Examples.MvcNetCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleExample()
        {
            //create a simple model structure

            //create the root as a row_columns_3
            IViewModel row0 = new StandardViewModel()
            {
                Id = "R0",
                Name = "Layout",
                ParentId = "",
                Target = "",
                View = "row_columns_3",
                ModelData = "{ Columns: [ { CssClass: 'left' }, { CssClass:'center' }, { CssClass: 'right' } ] }",
                Children = new List<IViewModel>()
            };
            //add a cell_container for cell 0
            row0.Children.Add(new StandardViewModel()
            {
                Id = "abc",
                Name = "R0C0",
                ParentId = "Layout",
                Target = "column_0",
                View = "container",
                ModelData = "{ CssClass: 'cell_container', Heading: { Type: 'h1', Text: 'Container 1!'}  }",
                Children = new List<IViewModel>()
            });
            //add a cell_container for cell 1
            row0.Children.Add(new StandardViewModel()
            {
                Id = "abc1",
                Name = "R0C1",
                ParentId = "Layout",
                Target = "column_1",
                View = "container",
                ModelData = "{ CssClass: 'cell_container', Heading: { Type: 'h1', Text: 'Container 2'}  }",
                Children = new List<IViewModel>()
            });
            //add a cell_container for cell 2
            row0.Children.Add(new StandardViewModel()
            {
                Id = "abc2",
                Name = "R0C2",
                ParentId = "Layout",
                Target = "column_2",
                View = "container",
                ModelData = "{ CssClass: 'cell_container', Heading: { Type: 'h1', Text: 'Container 3'}  }",
                Children = new List<IViewModel>()
            });

            //add some content to cell 0
            row0.Children[0].Children.Add(new StandardViewModel()
            {
                Id = "abc3",
                Name = "Html",
                ParentId = "R0C1",
                Target = "",
                View = "html",
                ModelData = @"{ ""Copy_1"":{ ""Copy"":""<p>Nested Copy</p>""}, ""Copy"":""<p>Lorem ipsum dolor sit amet consectetur adipiscing elit penatibus tincidunt, non tempus mi euismod lobortis aenean erat. Curabitur tellus sociis viverra leo purus, sapien eget cursus tempor posuere molestie, faucibus ut ridiculus laoreet. Eu eleifend sodales scelerisque felis dictumst blandit hendrerit, integer elementum eget per rhoncus velit aliquet, ligula facilisis nisl nam aptent porta.</p>""}",
                Children = new List<IViewModel>()
            });
            //add some more content to cell 0
            row0.Children[0].Children.Add(new StandardViewModel()
            {
                Id = "abc4",
                Name = "Html",
                ParentId = "R0C1",
                Target = "",
                View = "html",
                ModelData = @"{ ""Copy"":""<h2>A Second Item</h2><p>This is another para from a seperate child with a heading!</p>""}",
                Children = new List<IViewModel>()
            });
            //add some more content to cell 1
            row0.Children[1].Children.Add(new StandardViewModel()
            {
                Id = "abc5",
                Name = "Html",
                ParentId = "R0C2",
                Target = "",
                View = "html",
                ModelData = @"{ ""Copy"":""<p>Gravida condimentum blandit hendrerit bibendum per ante vitae venenatis taciti, molestie varius pharetra urna vestibulum est interdum risus cubilia, velit pellentesque pulvinar mollis fames nunc rhoncus duis. Habitant tempus pretium viverra suspendisse vivamus nibh leo himenaeos, accumsan metus suscipit sociosqu porttitor lacinia neque sed primis, curae porta sem aenean fringilla mattis fermentum. Et nunc varius placerat ullamcorper dis imperdiet proin posuere, enim hac lectus lacinia turpis montes scelerisque nulla tempus, fusce duis libero ridiculus metus congue dictumst.</p>""}",
                Children = new List<IViewModel>()
            });
            //add an image to cell 2
            row0.Children[2].Children.Add(new StandardViewModel()
            {
                Id = "abc6",
                Name = "Image",
                ParentId = "R0C3",
                Target = "",
                View = "html",
                ModelData = @"{ ""Copy"":""<p>Gravida condimentum blandit hendrerit bibendum per ante vitae venenatis taciti, molestie varius pharetra urna vestibulum est interdum risus cubilia, velit pellentesque pulvinar mollis fames nunc rhoncus duis. Habitant tempus pretium viverra suspendisse vivamus nibh leo himenaeos, accumsan metus suscipit sociosqu porttitor lacinia neque sed primis, curae porta sem aenean fringilla mattis fermentum. Et nunc varius placerat ullamcorper dis imperdiet proin posuere, enim hac lectus lacinia turpis montes scelerisque nulla tempus, fusce duis libero ridiculus metus congue dictumst.</p>""}",
                Children = new List<IViewModel>()
            });

            //send the model to the standard view
            return View(row0);
        }

        public IActionResult ComplexModelData()
        {
            var model = new StandardViewModel()
            {
                Id = "complex",
                Name = "Complex",
                ParentId = "root",
                Target = "",
                View = "complex",
                ModelData = @"{ ""Item1"":{ ""Copy"":""<p>Item 1 copy</p>""}, ""Copy"":""<p>Standard copy</p>""}",
                Children = new List<IViewModel>()
            };
            //send the model to the standard view
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
