using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PI.Cms.Core.Resources.Models
{
    public class DirectoryInfoView : FileSystemInfoView
    {
        #region Properties

        private List<FileInfoView> _ChildFiles = new List<FileInfoView>();
        /// <markdown>
        /// ### public List[FileInfoView] ChildFiles { get; set; }
        /// </markdown>
        /// <summary>
        /// Gets/sets child files
        /// </summary>
        public List<FileInfoView> ChildFiles
        {
            get { return _ChildFiles; }
            set { _ChildFiles = value; }
        }

        
        private List<DirectoryInfoView> _ChildDirectories = new List<DirectoryInfoView>();
        /// <markdown>
        /// ### public List[DirectoryInfoView] ChildDirectories
        /// </markdown>
        /// <summary>
        /// Gets/sets child directories
        /// </summary>
        public List<DirectoryInfoView> ChildDirectories
        {
            get { return _ChildDirectories; }
            set { _ChildDirectories = value; }
        }

        #endregion
        
    }
}
