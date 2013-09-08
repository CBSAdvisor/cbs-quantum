// ***********************************************************************
// Assembly         : VfSpeaker
// Author           : Cameleer
// Created          : 12-14-2012
//
// Last Modified By : Cameleer
// Last Modified On : 12-16-2012
// ***********************************************************************
// <copyright file="VfApplication.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using VfSpeaker.Data.Repositories;
using VfSpeaker.Data.Models;
using VfSpeaker.Views;
using VfSpeaker.Extentions;
using VfSpeaker.Services;
using NAudio.Wave;

namespace VfSpeaker
{
    /// <summary>
    /// Class VfApplication
    /// </summary>
    public class VfApplication : ApplicationContext
    {
        /// <summary>
        /// The _current context
        /// </summary>
        private static VfApplication _currentContext = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.ApplicationContext" /> class with the specified <see cref="T:System.Windows.Forms.Form" />.
        /// </summary>
        /// <param name="mainForm">The main <see cref="T:System.Windows.Forms.Form" /> of the application to use for context.</param>
        public VfApplication(Form mainForm)
            : this()
        {
            MainForm = mainForm;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VfApplication" /> class.
        /// </summary>
        public VfApplication()
            : base()
        {
            _currentContext = this;

            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            if (MainForm == null)
            {
                MainForm = new MainForm();
            }
            
            MainForm.FormClosing += new FormClosingEventHandler(OnFormClosing);
            MainForm.FormClosed += new FormClosedEventHandler(OnFormClosed);
        }

        /// <summary>
        /// Gets the document.
        /// </summary>
        /// <param name="documentId">The document id.</param>
        /// <returns>IVfDocumentModel.</returns>
        public IVfDocumentModel GetDocument(Guid documentId) 
        { 
            return Repository.GetDocumentById(documentId); 
        }

        public List<IVfPartModel> GetParts(Guid documentId)
        {
            return Repository.GetPartsByDocumentId(documentId);
        }

        /// <summary>
        /// News the document.
        /// </summary>
        public void NewDocument()
        {
            NewDocumentForm ndf = new NewDocumentForm("Book Title", VfRepository.DefaultDataDirectory);

            if (ndf.ShowDialog(MainForm) == DialogResult.OK)
            {
                Repository = new VfRepository();
                Repository.DataDirectory = ndf.Directory;
                Repository.DataFilePath = Path.Combine(new string[] { ndf.Directory, String.Format("{0}.xml", ndf.Title) });
                Repository.Initialize();

                IVfDocumentModel document = new VfDocumentModel()
                {
                    Title = ndf.Title
                };

                Guid docId = Repository.AddDocument(document);

                document = GetDocument(docId);
                ((MainForm)MainForm).DisplayView(new VfDocumentView(document.Id));
            }
        }

        /// <summary>
        /// Opens the document.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void OpenDocument(string fileName)
        {
            Repository = new VfRepository();
            Repository.DataDirectory = Path.GetDirectoryName(fileName);
            Repository.DataFilePath = fileName;
            Repository.Initialize();

            var listDoc = Repository.GetAllDocuments();

            IVfDocumentModel document = GetDocument(listDoc.FirstOrDefault().Id);

            ((MainForm)MainForm).DisplayView(new VfDocumentView(document.Id));
        }

        /// <summary>
        /// Reads the text file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>System.String.</returns>
        public static string ReadTextFile(string fileName)
        {
            string result = string.Empty;

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] buffer = new byte[fs.Length];
                    br.Read(buffer, 0, buffer.Length);
                    result = Encoding.UTF8.GetString(buffer);
                    br.Close();
                }
                fs.Close();
            }

            return result;
        }

        /// <summary>
        /// Saves the document.
        /// </summary>
        /// <param name="document">The document.</param>
        public void SaveDocument(IVfDocumentModel document)
        {
            Repository.UpdateDocument(document);
        }

        /// <summary>
        /// Lists the parts.
        /// </summary>
        /// <param name="document">The document.</param>
        public void ListParts(IVfDocumentModel document)
        {
        }

        /// <summary>
        /// Adds the part.
        /// </summary>
        /// <param name="part">The part.</param>
        public void AddPart(IVfPartModel part)
        {
            Repository.AddPart(part);
        }

        public void AddText(Guid documentId, string text)
        {
            ProgressForm prForm = new ProgressForm();

            ThreadPool.QueueUserWorkItem(new WaitCallback(DoAddText), new AddTextInfo(documentId, text, prForm));

            prForm.ShowDialog(MainForm);
        }

        private void DoAddText(object parameters) 
        {

            Guid documentId = ((IAddTextInfo)parameters).DocumentId;
            string text = ((IAddTextInfo)parameters).Text;
            IProgressCallback progressCallback = ((IAddTextInfo)parameters).ProgressCallback;

            progressCallback.Begin();
            progressCallback.StepTo(0);
            progressCallback.SetComment("Splitting text...");

            List<String> parts = text.Split(500);

            progressCallback.SetRange(0, parts.Count);

            parts.ForEach((item) =>
            {
                if (progressCallback.IsAborting)
                {
                    return;
                }

                progressCallback.SetComment(item);

                AddPart(new VfPartModel()
                    {
                        Text = item,
                        DocumentId = documentId
                    });
                progressCallback.Increment(1);
            });

            progressCallback.End();
        }

        public void TextToSpeech(Guid documentId)
        {
            ProgressForm prForm = new ProgressForm();

            ThreadPool.QueueUserWorkItem(new WaitCallback(DoTextToSpeech), new TextToSpeechInfo(documentId, prForm));

            prForm.ShowDialog(MainForm);
        }

        private void DoTextToSpeech(object parameters)
        {
            Guid documentId = ((ITextToSpeechInfo)parameters).DocumentId;
            IProgressCallback progressCallback = ((ITextToSpeechInfo)parameters).ProgressCallback;

            progressCallback.Begin();
            progressCallback.StepTo(0);
            progressCallback.SetComment("Loading document...");

            IVfDocumentModel document = GetDocument(documentId);
            VfWebServer webClient = new VfWebServer();

            List<IVfPartModel> parts = Repository.GetPartsByDocumentId(documentId, false);

            progressCallback.SetRange(0, parts.Count);

            FileStream fsFullOut = null;
            BinaryWriter bwFull = null;
            string currentFileName = String.Empty;

            int writedParts = 0;
            //int fileIndex = 0;
            Id3v2Tag id3v2tag = null;

            for (int i = 0; i < parts.Count; i++)
            {
                progressCallback.Increment(1);
                progressCallback.SetComment(parts[i].Text);

                byte[] audioData = null;

                string tempId = webClient.GetTempId();
                Thread.Sleep(1000);
                string cardLink = webClient.GetCardLink(tempId, parts[i].Text);

                byte[] previewPage = webClient.PostPreview(tempId, parts[i].Text/*"У Кремля нет повода менять взгляд на отношения с Минском"*/);

                while (true)
                {
                    if (progressCallback.IsAborting)
                    {
                        Log4.DeveloperLog.Info("Job canceled");
                        break;
                    }

                    Thread.Sleep(2000);
                    audioData = webClient.GetSound(tempId);

                    if ((audioData != null) && ((audioData.Length > 100000) || (i == parts.Count - 1)))
                    {
                        break;
                    }
                    Log4.DeveloperLog.InfoFormat("*** Try get sound again. Bytes: {0} tempid: {1}", audioData.Length, tempId);
                    audioData = null;
                }

                if (audioData == null)
                {
                    Log4.DeveloperLog.Info("Job canceled");
                    break;
                }

                if (writedParts == 0)
                {
                    document.FileIndex++;
                    SaveDocument(document);

                    string bookName = document.Title;
                    string author = document.Author;
                    string group = document.Group;
                    string year = document.Year.ToString();
                    string comments = document.Comments;
                    string genre = document.Genre;

                    string fName = String.Format(@"{1:0000}-{0}", bookName, document.FileIndex);
                    string fDir = String.Format(@"{0}\{1}", document.AudioDirectory, bookName);

                    if (!Directory.Exists(fDir))
                    {
                        Directory.CreateDirectory(fDir);
                    }

                    currentFileName = String.Format(@"{0}\{1}.mp3", fDir, fName);
                    Log4.DeveloperLog.InfoFormat("Current file name: {0}", currentFileName);

                    ///////////////////////////////////////////////////////////
                    // http://www.id3.org/id3v2.3.0
                    ///////////////////////////////////////////////////////////
                    Dictionary<string, string> tags = new Dictionary<string, string>
                        {
                            { "TCON", String.Format("{0}", genre) }, /* Genre */
                            { "TALB", String.Format("{0}", bookName) }, /* Album name */
                            { "TRCK", String.Format("{0}", document.FileIndex) }, /* Track No */
                            { "TIT2", String.Format("{0}", fName) }, /* Name */
                            { "TPE1", String.Format("{0}", author) }, /* Artist */
                            { "TPE2", String.Format("{0}", group) }, /* Group name */
                            { "TYER", String.Format("{0}", year) }, /* year of the song */
                            { "COMM", String.Format("{0}", comments) }
                        };
                    id3v2tag = Id3v2Tag.Create(tags);
                    //id3v2tag = Id3v2xTag.Create();
                }

                fsFullOut = new FileStream(currentFileName, FileMode.OpenOrCreate);
                bwFull = new BinaryWriter(fsFullOut);

                using (MemoryStream ms = new MemoryStream(audioData))
                {
                    using (Mp3FileReader mp3Reader = new Mp3FileReader(ms))
                    {
                        bwFull.Seek(0, SeekOrigin.End);

                        Mp3Frame frame;
                        mp3Reader.CurrentTime = TimeSpan.FromSeconds(1.8);
                        while ((frame = mp3Reader.ReadNextFrame()) != null)
                        {
                            if ((mp3Reader.TotalTime.TotalSeconds - mp3Reader.CurrentTime.TotalSeconds) <= 4)
                            {
                                break;
                            }

                            if (id3v2tag != null)
                            {
                                bwFull.Write(id3v2tag.RawData, 0, id3v2tag.RawData.Length);
                                bwFull.Flush();
                                id3v2tag = null;

                                //Mp3Stream mp3Strm = new Mp3Stream(fsFullOut, Mp3Permissions.ReadWrite);
                            }

                            bwFull.Write(frame.RawData, 0, frame.RawData.Length);
                            bwFull.Flush();
                        }
                        mp3Reader.Close();
                    }
                    ms.Close();
                }

                bwFull.Close();
                fsFullOut.Close();

                parts[i].Processed = true;
                Repository.UpdatePart(parts[i]);
                writedParts = (writedParts == 15) ? 0 : writedParts + 1;

                Log4.DeveloperLog.InfoFormat("Writed Parts: {0}", writedParts);
            }

            progressCallback.End();
            ((MainForm)MainForm).RefreshView();
        }

        /// <summary>
        /// Called when [application exit].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void OnApplicationExit(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Called when [form closing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
        private void OnFormClosing(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// Called when [form closed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void OnFormClosed(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>The repository.</value>
        public IVfRepository Repository { get; private set; }

        /// <summary>
        /// Gets the current context.
        /// </summary>
        /// <value>The current context.</value>
        public static VfApplication CurrentContext
        {
            get {
                if (_currentContext == null)
                {
                    _currentContext = new VfApplication();
                }
                return _currentContext;
            }
        }
    }
}
