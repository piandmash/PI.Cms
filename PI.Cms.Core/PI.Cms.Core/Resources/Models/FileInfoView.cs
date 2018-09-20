using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PI.Cms.Core.Resources.Models
{
    public class FileInfoView : FileSystemInfoView
    {
        #region Properties

        /// <markdown>
        /// ### public string DirectoryName { get; set; }
        /// </markdown>
        /// <summary>
        /// The DirectoryName
        /// </summary>
        public string DirectoryName { get; set; }

        /// <markdown>
        /// ### public string DirectoryNameUri { get; set; }
        /// </markdown>
        /// <summary>
        /// The DirectoryNameUri
        /// </summary>
        public string DirectoryNameUri
        {
            get { return DirectoryName.Replace(@"\", "/"); }
            set { DirectoryName = value.Replace("/", @"\"); }
        }

        /// <markdown>
        /// ### public bool IsReadOnly { get; set; }
        /// </markdown>
        /// <summary>
        /// The IsReadOnly
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <markdown>
        /// ### public long Length { get; set; }
        /// </markdown>
        /// <summary>
        /// The Length
        /// </summary>
        public long Length { get; set;  }

        #endregion

    }
}
