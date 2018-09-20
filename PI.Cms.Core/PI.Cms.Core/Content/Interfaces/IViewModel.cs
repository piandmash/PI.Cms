using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PI.Cms.Core.Content.Interfaces
{
    /// <summary>
    /// Interface for an IViewModel used to store the content structure
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// The id of the item, defaulted to empty
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// The id of the parent of the item, defaults to root
        /// </summary>
        string ParentId { get; set; }

        /// <summary>
        /// A user friendly name for the item
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// A value advising of the target within the view to render to, defaulted to empty
        /// </summary>
        string Target { get; set; }

        /// <summary>
        /// A value representing the view to render, defaulted to empty
        /// </summary>
        string View { get; set; }

        /// <summary>
        /// The Json structure of the model saved as a string, defaulted to an empty Json object
        /// </summary>
        string ModelData { get; set; }

        /// <summary>
        /// A JObject of the deserialized ModelData 
        /// </summary>
        JObject Model { get; }

        /// <summary>
        /// A list of all the Children
        /// </summary>
        List<IViewModel> Children { get; set; }

        /// <summary>
        /// Find a child view based on the target name
        /// </summary>
        /// <param name="target">The name of the target</param>
        /// <param name="nested">Optional flag to advise if to search through all children in the tree, defaulted to false</param>
        /// <returns>A matching IViewModel if found</returns>
        IViewModel FindByTarget(string target, bool nested = false);

        /// <summary>
        /// Find a child view based on the id
        /// </summary>
        /// <param name="id">The id of the target</param>
        /// <param name="nested">Optional flag to advise if to search through all children in the tree, defaulted to true</param>
        /// <returns>A matching IViewModel if found</returns>
        IViewModel FindById(string id, bool nested = false);
    }
}
