using System;
using System.IO;
using System.Threading.Tasks;
using PI.Cms.Core.Resources.Models;
using PI.Cms.Core.Resources.Interfaces;

namespace PI.Cms.Core.Resources.FileResource
{
    /// <summary>
    /// Manager for the Resources
    /// </summary>
    public class ResourceManager : IResourceManager, IDisposable
        {
            #region Properties

            /// <markdown>
            /// ### private string Root
            /// </markdown>
            /// <summary>
            /// Stores the physical directory root for the FileInfoView Manager
            /// </summary>
            private string _Root;

            /// <markdown>
            /// ### public string Root { get; }
            /// </markdown>
            /// <summary>
            /// Returns the current FileInfoView Managers root directory
            /// </summary>
            public string Root
            {
                get
                {
                    string[] parts = _Root.Split('\\');
                    return parts[parts.Length - 1];
                }
            }

            /// <markdown>
            /// ### public string FullRoot { get; }
            /// </markdown>
            /// <summary>
            /// Returns the current FileInfoView Managers root directory full pyhsical path
            /// </summary>
            public string FullRoot
            {
                get { return _Root.Replace("/", "\\"); }
            }

            #endregion

            #region Constructors

            /// <markdown>
            /// ###public ResourceManager(string root)
            /// </markdown>
            /// <summary>
            /// Constructor for the FileInfoView Manager
            /// </summary>
            /// <param name="root">The physical directory for the root of all the resources</param>
            public ResourceManager(string root)
            {
                _Root = root;
            }

        #endregion

        #region Methods


        /// <summary>
        /// Creates a new FileInfoView based on a FileInfo object
        /// </summary>
        /// <param name="fileInfo">The FileInfo object to create this object from</param>
        private FileInfoView CreateFileInfoView(FileInfo fileInfo)
        {
            return new FileInfoView() {
                DirectoryName = fileInfo.DirectoryName,
                IsReadOnly = fileInfo.IsReadOnly,
                Length = fileInfo.Length,
                Name = fileInfo.Name,
                Attributes = fileInfo.Attributes,
                CreationTime = fileInfo.CreationTime,
                CreationTimeUtc = fileInfo.CreationTimeUtc,
                Extension = fileInfo.Extension,
                FullName = fileInfo.FullName,
                LastAccessTime = fileInfo.LastAccessTime,
                LastAccessTimeUtc = fileInfo.LastAccessTimeUtc,
                LastWriteTime = fileInfo.LastWriteTime,
                LastWriteTimeUtc = fileInfo.LastWriteTimeUtc
            };
        }

        /// <summary>
        /// Creates a new DirectoryInfoView based on a DirectoryInfo object
        /// </summary>
        /// <param name="directoryInfo">The DirectoryInfo object to create this object from</param>
        private DirectoryInfoView CreateDirectoryInfoView(DirectoryInfo directoryInfo)
        {
            return new DirectoryInfoView()
            {
                Name = directoryInfo.Name,
                Attributes = directoryInfo.Attributes,
                CreationTime = directoryInfo.CreationTime,
                CreationTimeUtc = directoryInfo.CreationTimeUtc,
                Extension = directoryInfo.Extension,
                FullName = directoryInfo.FullName,
                LastAccessTime = directoryInfo.LastAccessTime,
                LastAccessTimeUtc = directoryInfo.LastAccessTimeUtc,
                LastWriteTime = directoryInfo.LastWriteTime,
                LastWriteTimeUtc = directoryInfo.LastWriteTimeUtc
            };
        }

        /// <markdown>
        /// ### public DirectoryInfoView GetDirectoryInfoView(string relativePath = "", bool populateChildren = true, bool recursive = true)
        /// </markdown>
        /// <summary>
        /// Returns a DirectoryInfoView for the relative path from the root, with optionally appended children and recursive children
        /// </summary>
        /// <param name="relativePath">Optional relative path from root</param>
        /// <param name="populateChildren">Set to true to populate child directories and files, defaults to true</param>
        /// <param name="recursive">Set to true to recursively construct all child directories</param>
        /// <returns>The matching directory info view</returns>
        public DirectoryInfoView GetDirectoryInfoView(string relativePath = "", bool populateChildren = true, bool recursive = true)
            {
                //rebuild reltative path
                relativePath = RebuildRelativePath(relativePath);
                string fullPath = FullRoot + relativePath + @"\";
                string path = Root + relativePath;
                //throw directory doesn't exist method
                if (!Directory.Exists(fullPath)) throw new DirectoryNotFoundException("Directory '" + path + "' does not exist");
                DirectoryInfo di = new DirectoryInfo(fullPath);
                DirectoryInfoView div = CreateDirectoryInfoView(di);
                //populate children if requested
                if (populateChildren)
                {
                    //add child files
                    foreach (var file in di.GetFiles()) div.ChildFiles.Add(CreateFileInfoView(file));
                    //get child directories
                    DirectoryInfo[] childDirectories = di.GetDirectories();
                    foreach (var i in childDirectories)
                    {
                        //if recursive then get child directories
                        if (recursive)
                        {
                            string rp = relativePath + @"\" + i.Name;
                            div.ChildDirectories.Add(GetDirectoryInfoView(rp, populateChildren, recursive));
                        }
                        else
                        {
                            DirectoryInfoView div2 = CreateDirectoryInfoView(i);
                            div2 = CleanDirectoryView(div2);
                            div.ChildDirectories.Add(div2);
                        }
                    }
                }
                //clean children
                foreach (var c in div.ChildFiles)
                {
                    CleanFileView(c);
                }
                //clean directory view
                div = CleanDirectoryView(div);
                return div;
            }

            ///// <markdown>
            ///// ### public async Task[FileInfoView] Save(HttpContent content, string relativePath = "")
            ///// </markdown>
            ///// <summary>
            ///// Saves a file to the path specified
            ///// </summary>
            ///// <param name="content">The HttpContent that contains the upload form data</param>
            ///// <param name="relativePath">The relative path to save the file to</param>
            ///// <returns>The new FileInfoView</returns>
            //public async Task<FileInfoView> Save(HttpContent content, string relativePath = "")
            //{
            //    //rebuild reltative path
            //    relativePath = RebuildRelativePath(relativePath);
            //    string fullPath = FullRoot + relativePath + @"\";
            //    string path = Root + relativePath;
            //    //throw directory doesn't exist method
            //    if (!Directory.Exists(fullPath)) throw new DirectoryNotFoundException("Directory '" + path + "' does not exist");

            //    MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(fullPath);
            //    var result = await content.ReadAsMultipartAsync(provider);
            //    var originalFileName = GetDeserializedFileName(result.FileData.First());
            //    var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
            //    //remove old file
            //    if (File.Exists(fullPath + originalFileName)) File.Delete(fullPath + originalFileName);
            //    //add new file
            //    File.Move(fullPath + uploadedFileInfo.Name, fullPath + originalFileName);
            //    //get file info on new file
            //    FileInfo file = new FileInfo(fullPath + originalFileName);
            //    FileInfoView resource = new FileInfoView();
            //    //map to resource model
            //    Mapper.Map(file, resource);
            //    //return model
            //    return CleanFileView(resource);
            //}

            /// <markdown>
            /// ### public FileInfoView GetFileInfoView(string path = "")
            /// </markdown>
            /// <summary>
            /// Saves a file to the path specified
            /// </summary>
            /// <param name="path">The relative path to the file</param>
            /// <returns>The new FileInfoView</returns>
            public FileInfoView GetFileInfoView(string path = "")
            {
                //get file info on new file
                FileInfo file = new FileInfo(path);
                FileInfoView resource = CreateFileInfoView(file);
                //return model
                return CleanFileView(resource);
            }

            /// <markdown>
            /// ### public void RemoveOldFile(string path = "")
            /// </markdown>
            /// <summary>
            /// Saves a file to the path specified
            /// </summary>
            /// <param name="path">The path to the file to remove</param>
            /// <returns>The new FileInfoView</returns>
            public void RemoveOldFile(string path = "")
            {
                //remove old file
                if (File.Exists(path)) File.Delete(path);
            }

            /// <markdown>
            /// ### public string GetFullPath(string relativePath = "")
            /// </markdown>
            /// <summary>
            /// Gets the full path from the relative path
            /// </summary>
            /// <param name="relativePath">The relative path to save the file to</param>
            /// <returns>The new FileInfoView</returns>
            public string GetFullPath(string relativePath = "")
            {
                //rebuild reltative path
                relativePath = RebuildRelativePath(relativePath);
                string fullPath = FullRoot + relativePath + @"\";
                //throw directory doesn't exist method
                if (!Directory.Exists(fullPath)) throw new DirectoryNotFoundException("Directory '" + relativePath + "' does not exist");
                return fullPath;
            }

            /// <markdown>
            /// ### public void Delete(string path)
            /// </markdown>
            /// <summary>
            /// Deletes the file at the path specified
            /// </summary>
            /// <param name="path">The path of the file to delete</param>
            public void Delete(string path)
            {
                string fullPath = FullRoot + path;
                //throw directory doesn't exist method
                if (!File.Exists(fullPath)) throw new FileNotFoundException("File '" + path + "' does not exist");
                File.Delete(fullPath);
            }

            /// <markdown>
            /// ### public void DirectoryRename(string path)
            /// </markdown>
            /// <summary>
            /// Renames a directory at the path specified
            /// </summary>
            /// <param name="path">The path of the directory to rename</param>
            /// <param name="name">The new name of the directory</param>
            /// <returns>The renamed directory view</returns>
            public DirectoryInfoView DirectoryRename(string path, string name)
            {
                string fullPath = FullRoot + path;
                //throw directory doesn't exist method
                if (!Directory.Exists(fullPath)) throw new DirectoryNotFoundException("Directory '" + path + "' does not exist");
                //get new path
                DirectoryInfo diP = new DirectoryInfo(fullPath);
                string newPath = diP.Parent.FullName + @"\" + name;
                //throw exception if move directory exists
                if (Directory.Exists(newPath)) throw new Exception("Directory '" + path + "' already exists");
                //move
                Directory.Move(fullPath, newPath);
                DirectoryInfo di = new DirectoryInfo(newPath);
                DirectoryInfoView div = CreateDirectoryInfoView(di);
                //return model
                return CleanDirectoryView(div);
            }

            /// <markdown>
            /// ### public void DirectoryCopy(string path, string name)
            /// </markdown>
            /// <summary>
            /// Copies a directory at the path specified with the new name at the same level
            /// </summary>
            /// <param name="path">The path of the directory to rename</param>
            /// <param name="name">The new name of the directory</param>
            /// <returns>The renamed directory view</returns>
            public DirectoryInfoView DirectoryCopy(string path, string name)
            {
                string fullPath = FullRoot + path;
                //throw directory doesn't exist method
                if (!Directory.Exists(fullPath)) throw new DirectoryNotFoundException("Directory '" + path + "' does not exist");
                //get new path
                DirectoryInfo diP = new DirectoryInfo(fullPath);
                string newPath = diP.Parent.FullName + @"\" + name;
                //throw exception if move directory exists
                if (Directory.Exists(newPath)) throw new Exception("Directory '" + path + "' already exists");

                // Copy from the current directory, include subdirectories.
                DirectoryCopy(fullPath, newPath, true);

                DirectoryInfo di = new DirectoryInfo(newPath);
                DirectoryInfoView div = CreateDirectoryInfoView(di);
                //return model
                return CleanDirectoryView(div);
            }

            private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
            {
                // Get the subdirectories for the specified directory.
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);

                if (!dir.Exists)
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + sourceDirName);
                }

                DirectoryInfo[] dirs = dir.GetDirectories();
                // If the destination directory doesn't exist, create it.
                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                }

                // Get the files in the directory and copy them to the new location.
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, false);
                }

                // If copying subdirectories, copy them and their contents to new location.
                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                    }
                }
            }

            /// <markdown>
            /// ### public void DirectoryDelete(string path)
            /// </markdown>
            /// <summary>
            /// Deletes the directory and all it's child files and directories at the path specified
            /// </summary>
            /// <param name="path">The path of the directory to delete</param>
            public void DirectoryDelete(string path)
            {
                string fullPath = FullRoot + path;
                //throw directory doesn't exist method
                if (!Directory.Exists(fullPath)) throw new DirectoryNotFoundException("Directory '" + path + "' does not exist");
                Directory.Delete(fullPath, true);
            }

            /// <markdown>
            /// ### public void DirectoryCreate(string path)
            /// </markdown>
            /// <summary>
            /// Creates a new directory at the path specified
            /// </summary>
            /// <param name="path">The path of the directory to create</param>
            /// <returns>The newly created directory view</returns>
            public DirectoryInfoView DirectoryCreate(string path, bool throwOnExist = true)
            {
                string fullPath = FullRoot + path;
                //throw directory exist method
                if (Directory.Exists(fullPath))
                {
                    if (throwOnExist) throw new Exception("Directory '" + path + "' already exists");
                }
                else
                {
                    Directory.CreateDirectory(fullPath);
                }
                DirectoryInfo di = new DirectoryInfo(fullPath);
                DirectoryInfoView div = CreateDirectoryInfoView(di);
                //return model
                return CleanDirectoryView(div);
            }

            /// <markdown>
            /// ### public FileInfoView CleanFileView(FileInfoView file)
            /// </markdown>
            /// <summary>
            /// Cleans a file removing full paths and leaving it with relative paths only
            /// </summary>
            /// <param name="file">The file to clean</param>
            /// <returns>The cleaned file</returns>
            public FileInfoView CleanFileView(FileInfoView file)
            {
                file.DirectoryName = file.DirectoryName.Replace(FullRoot, "");
                file.FullName = file.FullName.Replace(FullRoot, "");
                return file;
            }

            /// <markdown>
            /// ### public FileInfoView CleanDirectoryView(FileInfoView directory)
            /// </markdown>
            /// <summary>
            /// Cleans a resource directory full paths and leaving it with relative paths only
            /// </summary>
            /// <param name="directory">The directory to clean</param>
            /// <returns>The cleaned directory</returns>
            public DirectoryInfoView CleanDirectoryView(DirectoryInfoView directory)
            {
                directory.FullName = directory.FullName.Replace(FullRoot, "");
                return directory;
            }

            /// <markdown>
            /// ### public string RebuildRelativePath(string relativePath)
            /// </markdown>
            /// <summary>
            /// Takes a relative path and rebuilds it so that it has the correct \ and starting \
            /// </summary>
            /// <param name="relativePath">The relative path to rebuild</param>
            /// <returns>A rebuilt relative path string</returns>
            public string RebuildRelativePath(string relativePath)
            {
                //rebuild reltative path
                if (relativePath.Length > 0)
                {
                    relativePath = relativePath.Replace("/", @"\");
                    if (!relativePath.StartsWith(@"\")) relativePath = @"\" + relativePath;
                }
                return relativePath;
            }

            ///// <markdown>
            ///// ### public string GetDeserializedFileName(MultipartFileData fileData)
            ///// </markdown>
            ///// <summary>
            ///// Takes the multipart file data information and gets the deserialized file name
            ///// </summary>
            ///// <param name="fileData">The file data to collect the name from</param>
            ///// <returns>A the file name as a string</returns>
            //public string GetDeserializedFileName(MultipartFileData fileData)
            //{
            //    return JsonConvert.DeserializeObject(fileData.Headers.ContentDisposition.FileName).ToString();
            //}

            #endregion

            #region Dispose Methods

            /// <markdown>
            /// ### public void Dispose()
            /// </markdown>
            /// <summary>
            /// Defines a method to release allocated resources.
            /// </summary>
            public void Dispose()
            {
                //if (Context != null) Context.Dispose();
            }

            #endregion
        }
    
}
