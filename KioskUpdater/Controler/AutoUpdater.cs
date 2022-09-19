using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Windows.Forms;

namespace KioskUpdater.Controler
{
    public partial class AutoUpdater : Component
    {
        public AutoUpdater()
        {
            InitializeComponent();
        }

        public AutoUpdater(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private DateTime _startedAt;
        private TransferingInfo _transferingInfo;
        private WebClient _webClient;
        private string _localRoot;

        public string RootUri { get; set; }
        public string UpdateListFileName { get; set; }
        public string LocalRoot
        {
            get { return _localRoot; }
            set
            {
                _localRoot = value;

                if (_localRoot.EndsWith(@"\") == false)
                    _localRoot += @"\";
            }
        }

        public void Run()
        {
            _startedAt = DateTime.Now;
            var updatableFiles = GetUpdatableFiles();
            OnUpdatableListFound(updatableFiles);

            LinkedList<RemoteFile> remoteFiles = new LinkedList<RemoteFile>(updatableFiles);

            _transferingInfo = new TransferingInfo();
            _transferingInfo.TotalFileCount = remoteFiles.Count;

            foreach (var remoteFile in remoteFiles)
                _transferingInfo.TotalLength += remoteFile.ContentLength;

            if (remoteFiles.Count == 0)
            {
                OnUpdateCompleted(_transferingInfo, LocalRoot);
                return;
            }

            _webClient = new WebClient();
            _webClient.Credentials = new NetworkCredential("devtm", "gaon!3@4");
            _webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            _webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

            var node = remoteFiles.First;
            DownloadFile(node);
        }

        private List<RemoteFile> GetUpdatableFiles()
        {
            if (RootUri.EndsWith(@"/") == false)
                RootUri += @"/";

            string listText = GetUpdatingText(RootUri + UpdateListFileName);

            UpdateListDataSet updateList = new UpdateListDataSet();
            StringReader reader = new StringReader(listText);
            updateList.ReadXml(reader);
            reader.Close();

            List<RemoteFile> remoteFiles = new List<RemoteFile>();

            foreach (UpdateListDataSet.FileRow file in updateList.File)
            {
                RemoteFile remoteFile = new RemoteFile();
                remoteFile.Uri = RootUri + file.Path.Replace('\\', '/');
                remoteFile.LocalPath = LocalRoot + file.Path;
                remoteFile.ContentLength = file.Length;
                remoteFile.LastModified = file.LastWriteTime;

                bool updatable = IsUpdatable(remoteFile);

                if (updatable)
                {
                    remoteFiles.Add(remoteFile);
                    OnUpdatableFileFound(remoteFile);
                }
            }

            return remoteFiles;
        }

        private bool IsUpdatable(RemoteFile remoteFile)
        {
            if (remoteFile.LocalPath.Equals(@"C:\temp\UpdateList.xml"))
                return false;

            if (File.Exists(remoteFile.LocalPath) == false)
                return true;

            if (File.GetLastWriteTime(remoteFile.LocalPath) != remoteFile.LastModified)
                return true;

            return false;
        }

        private string GetUpdatingText(string listFileUri)
        {
            WebClient webClient = new WebClient();
            webClient.Credentials = new NetworkCredential("devtm", "gaon!3@4");
            Stream stream = webClient.OpenRead(listFileUri);
            StreamReader reader = new StreamReader(stream);

            string text = reader.ReadToEnd();
            stream.Close();
            return text;
        }

        private void DownloadFile(LinkedListNode<RemoteFile> node)
        {
            OnFileTransfering(_transferingInfo, node.Value);

            string directory = Path.GetDirectoryName(node.Value.LocalPath);

            if (directory.Length != 0 && Directory.Exists(directory) == false)
                Directory.CreateDirectory(directory);

            lock (_transferingInfo)
            {
                _transferingInfo.CurrentFilePath = node.Value.LocalPath;
            }

            _webClient.DownloadFileAsync(new Uri(node.Value.Uri), node.Value.LocalPath, node);
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            LinkedListNode<RemoteFile> node = (LinkedListNode<RemoteFile>)e.UserState;

            //로컬 파일의 수정날짜를 서버 파일의 수정날짜로 변경한다.
            File.SetLastWriteTime(node.Value.LocalPath, node.Value.LastModified);

            lock (_transferingInfo)
            {
                _transferingInfo.TransferedFileCount++;
                _transferingInfo.TransferedLength += _transferingInfo.TransferingLength;
                _transferingInfo.TransferingLength = 0;
            }

            OnFileTransfered(_transferingInfo, node.Value);

            LinkedListNode<RemoteFile> nextNode = node.Next;
            if (nextNode != null)
            {
                DownloadFile(nextNode);
            }
            else
            {
                OnUpdateCompleted(_transferingInfo, LocalRoot);
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            lock (_transferingInfo)
            {
                _transferingInfo.TransferingLength = e.BytesReceived;

                double deltaSenconds = (DateTime.Now - _startedAt).TotalSeconds;
                double totalSeconds = deltaSenconds * 100 / _transferingInfo.LengthPercent;

                _transferingInfo.RemainingSeconds = totalSeconds - deltaSenconds;
            }

            OnUpdateProgressChanged(_transferingInfo);
        }

        #region events

        #region UpdateCompleted event things for C# 3.0
        /// <summary>
        /// 업데이트가 끝났음.
        /// </summary>
        [Description("업데이트가 끝났음.")]
        public event EventHandler<UpdateCompletedEventArgs> UpdateCompleted;

        protected virtual void OnUpdateCompleted(UpdateCompletedEventArgs e)
        {
            if (UpdateCompleted != null)
                UpdateCompleted(this, e);
        }

        protected virtual void OnUpdateCompleted(TransferingInfo transferingInfo, string localRoot)
        {
            if (UpdateCompleted != null)
                UpdateCompleted(this, new UpdateCompletedEventArgs(transferingInfo, localRoot));
        }

        public class UpdateCompletedEventArgs : EventArgs
        {

            public TransferingInfo TransferingInfo { get; set; }
            public string LocalRoot { get; set; }

            public UpdateCompletedEventArgs(TransferingInfo transferingInfo, string localRoot)
            {
                TransferingInfo = transferingInfo;
                LocalRoot = localRoot;
            }
        }
        #endregion

        #region UpdatableListFound event things for C# 3.0
        /// <summary>
        /// 업데이트 할 파일의 목록이 발견되었음.
        /// </summary>
        [Description("업데이트 할 파일의 목록이 발견되었음.")]
        public event EventHandler<UpdatableListFoundEventArgs> UpdatableListFound;

        protected virtual void OnUpdatableListFound(UpdatableListFoundEventArgs e)
        {
            if (UpdatableListFound != null)
                UpdatableListFound(this, e);
        }

        protected virtual void OnUpdatableListFound(List<RemoteFile> remoteFiles)
        {
            if (UpdatableListFound != null)
                UpdatableListFound(this, new UpdatableListFoundEventArgs(remoteFiles));
        }

        public class UpdatableListFoundEventArgs : EventArgs
        {
            public List<RemoteFile> RemoteFiles { get; set; }

            public UpdatableListFoundEventArgs(List<RemoteFile> remoteFiles)
            {
                RemoteFiles = remoteFiles;
            }
        }
        #endregion

        #region UpdatableFileFound event things for C# 3.0
        /// <summary>
        /// 업데이트 할 파일이 발견되었음.
        /// </summary>
        [Description("업데이트 할 파일이 발견되었음.")]
        public event EventHandler<UpdatableFileFoundEventArgs> UpdatableFileFound;

        protected virtual void OnUpdatableFileFound(UpdatableFileFoundEventArgs e)
        {
            if (UpdatableFileFound != null)
                UpdatableFileFound(this, e);
        }

        protected virtual void OnUpdatableFileFound(RemoteFile remoteFile)
        {
            if (UpdatableFileFound != null)
                UpdatableFileFound(this, new UpdatableFileFoundEventArgs(remoteFile));
        }

        public class UpdatableFileFoundEventArgs : EventArgs
        {
            public RemoteFile RemoteFile { get; set; }

            public UpdatableFileFoundEventArgs(RemoteFile remoteFile)
            {
                RemoteFile = remoteFile;
            }
        }
        #endregion

        #region FileTransfering event things for C# 3.0
        /// <summary>
        /// 개별 파일의 전송이 시작되려고 함.
        /// </summary>
        [Description("개별 파일의 전송이 시작되려고 함.")]
        public event EventHandler<FileTransferingEventArgs> FileTransfering;

        protected virtual void OnFileTransfering(FileTransferingEventArgs e)
        {
            if (FileTransfering != null)
                FileTransfering(this, e);
        }

        protected virtual void OnFileTransfering(TransferingInfo ransferingInfo, RemoteFile remoteFile)
        {
            if (FileTransfering != null)
                FileTransfering(this, new FileTransferingEventArgs(ransferingInfo, remoteFile));
        }

        public class FileTransferingEventArgs : EventArgs
        {
            /// <summary>
            /// 전송 정보
            /// </summary>
            public TransferingInfo TransferingInfo { get; set; }

            /// <summary>
            /// 업데이트 할 파일의 리스트
            /// </summary>
            public RemoteFile RemoteFile { get; set; }

            public FileTransferingEventArgs(TransferingInfo ransferingInfo, RemoteFile remoteFile)
            {
                TransferingInfo = ransferingInfo;
                RemoteFile = remoteFile;
            }
        }
        #endregion

        #region FileTransfered event things for C# 3.0
        /// <summary>
        /// 개별 파일의 전송이 완료되었음.
        /// </summary>
        [Description("개별 파일의 전송이 완료되었음.")]
        public event EventHandler<FileTransferedEventArgs> FileTransfered;

        protected virtual void OnFileTransfered(FileTransferedEventArgs e)
        {
            if (FileTransfered != null)
                FileTransfered(this, e);
        }

        protected virtual void OnFileTransfered(TransferingInfo transferingInfo, RemoteFile remoteFile)
        {
            if (FileTransfered != null)
                FileTransfered(this, new FileTransferedEventArgs(transferingInfo, remoteFile));
        }

        public class FileTransferedEventArgs : EventArgs
        {
            /// <summary>
            /// 전송 정보
            /// </summary>
            public TransferingInfo TransferingInfo { get; set; }

            /// <summary>
            /// 업데이트 할 파일의 리스트
            /// </summary>
            public RemoteFile RemoteFile { get; set; }

            public FileTransferedEventArgs(TransferingInfo transferingInfo, RemoteFile remoteFile)
            {
                TransferingInfo = transferingInfo;
                RemoteFile = remoteFile;
            }
        }
        #endregion

        #region UpdateProgressChanged event things for C# 3.0
        /// <summary>
        /// 업데이트 진행 상황이 변경되었음.
        /// </summary>
        [Description("업데이트 진행 상황이 변경되었음.")]
        public event EventHandler<UpdateProgressChangedEventArgs> UpdateProgressChanged;

        protected virtual void OnUpdateProgressChanged(UpdateProgressChangedEventArgs e)
        {
            if (UpdateProgressChanged != null)
                UpdateProgressChanged(this, e);
        }

        protected virtual void OnUpdateProgressChanged(TransferingInfo transferingInfo)
        {
            if (UpdateProgressChanged != null)
                UpdateProgressChanged(this, new UpdateProgressChangedEventArgs(transferingInfo));
        }

        public class UpdateProgressChangedEventArgs : EventArgs
        {
            /// <summary>
            /// 전송 정보
            /// </summary>
            public TransferingInfo TransferingInfo { get; set; }

            public UpdateProgressChangedEventArgs(TransferingInfo transferingInfo)
            {
                TransferingInfo = transferingInfo;
            }
        }
        #endregion
        #endregion
    }
}
