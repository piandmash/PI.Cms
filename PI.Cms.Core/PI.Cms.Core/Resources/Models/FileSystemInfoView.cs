using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PI.Cms.Core.Resources.Models
{
    public abstract class FileSystemInfoView
    {
        #region Properties
        
        /// <markdown>
        /// ### public string Name { get; set; }
        /// </markdown>
        /// <summary>
        /// The Name
        /// </summary>
        public string Name { get; set; }

        /// <markdown>
        /// ### public FileAttributes Attributes { get; set; }
        /// </markdown>
        /// <summary>
        /// The Attributes
        /// </summary>
        public FileAttributes Attributes { get; set; }

        /// <markdown>
        /// ### public DateTime CreationTime { get; set; }
        /// </markdown>
        /// <summary>
        /// The CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <markdown>
        /// ### public DateTime CreationTimeUtc { get; set; }
        /// </markdown>
        /// <summary>
        /// The CreationTimeUtc
        /// </summary>
        public DateTime CreationTimeUtc { get; set; }

        /// <markdown>
        /// ### public string Extension { get; set; }
        /// </markdown>
        /// <summary>
        /// The Extension
        /// </summary>
        public string Extension { get; set; }

        /// <markdown>
        /// ### public string FullName { get; set; }
        /// </markdown>
        /// <summary>
        /// The FullName
        /// </summary>
        public string FullName { get; set; }

        /// <markdown>
        /// ### public string FullNameUri { get; set; }
        /// </markdown>
        /// <summary>
        /// The FullNameUri
        /// </summary>
        public string FullNameUri
        {
            get { return FullName.Replace(@"\", "/"); }
            set { FullName = value.Replace("/", @"\"); }
        }

        /// <markdown>
        /// ### public DateTime LastAccessTime { get; set; }
        /// </markdown>
        /// <summary>
        /// The LastAccessTime
        /// </summary>
        public DateTime LastAccessTime { get; set; }

        /// <markdown>
        /// ### public DateTime LastAccessTimeUtc { get; set; }
        /// </markdown>
        /// <summary>
        /// The LastAccessTimeUtc
        /// </summary>
        public DateTime LastAccessTimeUtc { get; set; }

        /// <markdown>
        /// ### public DateTime LastWriteTime { get; set; }
        /// </markdown>
        /// <summary>
        /// The LastWriteTime
        /// </summary>
        public DateTime LastWriteTime { get; set; }

        /// <markdown>
        /// ### public DateTime LastWriteTimeUtc { get; set; }
        /// </markdown>
        /// <summary>
        /// The LastWriteTimeUtc
        /// </summary>
        public DateTime LastWriteTimeUtc { get; set; }


        #endregion
    }
}
