using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PI.Cms.Core.Content.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PI.Cms.Core.Content.Models
{
    /// <summary>
    /// A StandardViewModel based on the the IViewModel interface, useful for most CMS requirements
    /// </summary>
    [Serializable]
    public class StandardViewModel: IViewModel
    {
        /// <summary>
        /// The id of the item, defaulted to empty
        /// </summary>
        public string Id { get; set; } = "";

        /// <summary>
        /// The id of the parent of the item, defaults to root
        /// </summary>
        public string ParentId { get; set; } = "root";

        /// <summary>
        /// A user friendly name for the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A value advising of the target within the view to render to, defaulted to empty
        /// </summary>
        public string Target { get; set; } = "";

        /// <summary>
        /// A value representing the view to render, defaulted to empty
        /// </summary>
        public string View { get; set; } = "";

        /// <summary>
        /// The Json structure of the model saved as a string, defaulted to an empty Json object
        /// </summary>
        public string ModelData { get; set; } = "{}";

        /// <summary>
        /// A JObject of the deserialized ModelData 
        /// </summary>
        public JObject Model
        {
            get
            {
                var o = (JObject)JsonConvert.DeserializeObject(ModelData);
                return o;
            }
        }

        private List<IViewModel> _Children = new List<IViewModel>();
        /// <summary>
        /// A list of all the Children
        /// </summary>
        public List<IViewModel> Children
        {
            get { return _Children; }
            set { _Children = value; }
        }

        /// <summary>
        /// Find a child view based on the target name
        /// </summary>
        /// <param name="target">The name of the target</param>
        /// <param name="nested">Optional flag to advise if to search through all children in the tree, defaulted to false</param>
        /// <returns>A matching IViewModel if found</returns>
        public IViewModel FindByTarget(string target, bool nested = false)
        {
            IViewModel vm = Children.Where(m => m.Target == target).FirstOrDefault();
            if (nested && Children.Count > 0)
            {
                foreach(var child in Children)
                {
                    var result = FindByTarget(target, nested);
                    if (result != null) return result;
                }
            }
            return vm;
        }

        /// <summary>
        /// Find a child view based on the id
        /// </summary>
        /// <param name="id">The id of the target</param>
        /// <param name="nested">Optional flag to advise if to search through all children in the tree, defaulted to true</param>
        /// <returns>A matching IViewModel if found</returns>
        public IViewModel FindById(string id, bool nested = true)
        {
            IViewModel vm = Children.Where(m => m.Id == id).FirstOrDefault();
            if (nested && Children.Count > 0)
            {
                foreach (var child in Children)
                {
                    var result = FindById(id, nested);
                    if (result != null) return result;
                }
            }
            return vm;
        }
    }
}
