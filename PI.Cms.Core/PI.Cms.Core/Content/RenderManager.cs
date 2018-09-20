using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PI.Cms.Core.Content.Interfaces;
using Newtonsoft.Json.Linq;

namespace PI.Cms.Core.Content
{
    public static class RenderManager
    {
        public static string Render(IViewModel model, string propertyPath)
        {
            //set the content
            string content = "";
            //split the property path up
            string[] properties = propertyPath.Split('.');
            //loop through the properties
            JToken jToken = null;
            bool first = true;
            bool matched = false;
            foreach (string property in properties)
            {
                if (first) {
                    jToken = model.Model[property];
                    first = false;
                } else
                {
                    if(jToken != null)
                    {
                        jToken = jToken.SelectToken(property);
                    }
                }
            }
            if (jToken != null) content = jToken.ToString();

            ////should add some code to do internal target replacement
            //Regex regex = new Regex("<strong>(.*)</strong>");
            //var v = regex.Match("Unneeded text <strong>Needed Text</strong> More unneeded text");
            ////look to see if there is nested targets in the content
            //string found = "[TARGET:";
            //string target = "target2";
            ////get child
            //var child = model.GetView(target);
            //string replace = "";
            //if (child != null)
            //{
            //    //load up child view
            //    //render view to string
            //    //replace in content
            //}
            ////replace
            //content = content.Replace(found, replace);
            ////should add some code to do internal target replacement


            //return
            return content;
        }
    }
}
