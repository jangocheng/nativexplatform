﻿//-----------------------------------------------------------------------
// <copyright file="extractCLIProgram.cs" company="Akeeba Ltd">
// Copyright (c) 2006-2017  Nicholas K. Dionysopoulos / Akeeba Ltd
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
// Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using ExtractWizard.Helpers;

namespace ExtractWizard.Gateway
{
    /// <summary>
    /// Gateway to the MainForm View (form).
    /// 
    /// The gateway is used to abstract the View (form) interaction for the Controller. The Controller is only ever given
    /// a reference to the gateway. This allows us to create a test double which doesn't require implementing the real UI
    /// form. It even allows us to switch to a different UI library or form layout without affecting the way the Controller
    /// works.
    /// 
    /// See:
    /// http://martinfowler.com/eaaCatalog/gateway.html
    /// http://martinfowler.com/eaaDev/PassiveScreen.html
    /// </summary>
    class MainFormGateway : IMainFormGateway
    {
        /// <summary>
        /// The form object we're the gateway for
        /// </summary>
        private MainForm _myForm;

        /// <summary>
        /// Caches our Windows detection result for performance reasons
        /// </summary>
        private bool? _isWindows = null;

        /// <summary>
        /// Public constructor. This specialised gateway expects a MainForm object as input.
        /// </summary>
        /// <param name="myForm"></param>
        public MainFormGateway(MainForm myForm)
        {
            this._myForm = myForm;
        }

        /// <summary>
        /// Sets the title of the UI window
        /// </summary>
        /// <param name="title"></param>
        public void SetWindowTitle(string title)
        {
            _myForm.Text = title;
        }

        /// <summary>
        /// Handles the translation of all labels, buttons etc given a ResourceManager holding all the language strings
        /// </summary>
        public void TranslateInterface(ResourceManager text)
        {
            // Groups
            _myForm.groupOptions.Text = text.GetString((string)_myForm.groupOptions.Tag);
            _myForm.groupProgress.Text = text.GetString((string)_myForm.groupProgress.Tag);

            // Labels
            _myForm.lblBackupArchive.Text = text.GetString((string) _myForm.lblBackupArchive.Tag);
            _myForm.lblExtractToFolder.Text = text.GetString((string)_myForm.lblExtractToFolder.Tag);
            _myForm.lblPassword.Text = text.GetString((string)_myForm.lblPassword.Tag);

            // Checkboxes
            _myForm.chkDryRun.Text = text.GetString((string)_myForm.chkDryRun.Tag);
            _myForm.chkIgnoreErrors.Text = text.GetString((string)_myForm.chkIgnoreErrors.Tag);

            // Buttons
            _myForm.btnBrowseArchive.Text = text.GetString((string)_myForm.btnBrowseArchive.Tag);
            _myForm.btnExtractToFolder.Text = text.GetString((string)_myForm.btnExtractToFolder.Tag);
            _myForm.btnHelp.Text = text.GetString((string)_myForm.btnHelp.Tag);
        }

        /// <summary>
        /// Sets the text of the Backup Archive UI element
        /// </summary>
        /// <param name="BackupArchivePath"></param>
        public void SetBackupArchivePath(string backupArchivePath)
        {
            ThreadHelper.SetText(_myForm, _myForm.editBackupArchive, backupArchivePath);
        }

        /// <summary>
        /// Gets the text of the Backup Archive UI element
        /// </summary>
        /// <returns></returns>
        public string GetBackupArchivePath()
        {
            return _myForm.editBackupArchive.Text;
        }

        /// <summary>
        /// Sets the text for the Output Directory UI element
        /// </summary>
        /// <param name="outputFolderPath"></param>
        public void SetOutputFolderPath(string outputFolderPath)
        {
            ThreadHelper.SetText(_myForm, _myForm.editExtractToFolder, outputFolderPath);
        }

        /// <summary>
        /// Gets the text for the Output Directory UI element
        /// </summary>
        /// <returns></returns>
        public string GetOutputFolderPath()
        {
            return _myForm.editExtractToFolder.Text;
        }

        /// <summary>
        /// Gets the text of the JPS Password UI element
        /// </summary>
        /// <returns></returns>
        public void SetPassword(string password)
        {
            ThreadHelper.SetText(_myForm, _myForm.editPassword, password);
        }

        /// <summary>
        /// Gets the text of the JPS Password UI element
        /// </summary>
        /// <returns></returns>
        public string GetPassword()
        {
            return _myForm.editPassword.Text;
        }

        /// <summary>
        /// Gets the value of the Ignore File Write Errors UI element
        /// </summary>
        /// <returns></returns>
        public bool GetIgnoreFileWriteErrors()
        {
            return _myForm.chkIgnoreErrors.Checked;
        }

        /// <summary>
        /// Sets the value of the Ignore File Write Errors UI element
        /// </summary>
        /// <returns></returns>
        public void SetIgnoreFileWriteErrors(bool isChecked)
        {
            ThreadHelper.SetCheckboxEnabled(_myForm, _myForm.chkIgnoreErrors, isChecked);
        }

        /// <summary>
        /// Gets the value of the Dry Run UI element
        /// </summary>
        /// <returns></returns>
        public bool GetDryRun()
        {
            return _myForm.chkDryRun.Checked;
        }

        /// <summary>
        /// Sets the value of the Dry Run UI element
        /// </summary>
        /// <returns></returns>
        public void SetDryRun(bool isChecked)
        {
            ThreadHelper.SetCheckboxEnabled(_myForm, _myForm.chkDryRun, isChecked);
        }

        /// <summary>
        /// Sets the state (enabled / disabled) for all the user-interactive extraction option controls
        /// </summary>
        /// <param name="enabled"></param>
        public void SetExtractionOptionsState(bool enabled)
        {
            // Set state of edit box labels
            ThreadHelper.SetEnabled(_myForm, _myForm.lblBackupArchive, enabled);
            ThreadHelper.SetEnabled(_myForm, _myForm.lblExtractToFolder, enabled);
            ThreadHelper.SetEnabled(_myForm, _myForm.lblPassword, enabled);

            // Set state of edit boxes
            ThreadHelper.SetEnabled(_myForm, _myForm.editBackupArchive, enabled);
            ThreadHelper.SetEnabled(_myForm, _myForm.editExtractToFolder, enabled);
            ThreadHelper.SetEnabled(_myForm, _myForm.editPassword, enabled);

            // Set state of check boxes
            ThreadHelper.SetEnabled(_myForm, _myForm.chkDryRun, enabled);
            ThreadHelper.SetEnabled(_myForm, _myForm.chkIgnoreErrors, enabled);

            // Set state of buttons
            ThreadHelper.SetEnabled(_myForm, _myForm.btnBrowseArchive, enabled);
            ThreadHelper.SetEnabled(_myForm, _myForm.btnExtractToFolder, enabled);
            ThreadHelper.SetEnabled(_myForm, _myForm.btnHelp, enabled);
        }

        /// <summary>
        /// Set the label of the Extract UI button
        /// </summary>
		/// <param name="text">Resource manager handling the translations</param>
        /// <param name="label">The label's translation key</param>
		public void SetExtractButtonText(ResourceManager text, string label)
        {
			ThreadHelper.SetText(_myForm, _myForm.btnExtract, text.GetString(label));
        }

        /// <summary>
        /// Set the percentage of the extraction which is already complete
        /// </summary>
        /// <param name="percent"></param>
        public void SetExtractionProgress(int percent)
        {
            // Squash percentage between 0 - 100
            percent = Math.Max(0, percent);
            percent = Math.Min(100, percent);

            // Set the progress bar value
            ThreadHelper.SetProgressValue(_myForm, _myForm.progressBarExtract, percent);
        }

        /// <summary>
        /// Set the label text for the extracted file name
        /// </summary>
        /// <param name="fileName"></param>
        public void SetExtractedFileName(string fileName)
        {
            ThreadHelper.SetText(_myForm, _myForm.lblExtractedFile, fileName);
        }

        /// <summary>
        /// Set the taskbar progress bar's state. Has an effect only on Windows 7+.
        /// </summary>
        /// <param name="state"></param>
        public void SetTaskbarProgressState(TaskBarProgress.TaskbarStates state)
        {
            // Make sure we're on Windows
            if (!IsWindows())
            {
                return;
            }

            // Use the helper to set the state
            ThreadHelper.SetTaskbarProgressState(_myForm, state);
        }

        /// <summary>
        /// Set the taskbar progress bar's value (whole percentage points, 0-100). Has an effect only on Windows 7+.
        /// </summary>
        /// <param name="state"></param>
        public void SetTaskbarProgressValue(int value)
        {
            // Make sure we're on Windows
            if (!IsWindows())
            {
                return;
            }

            // Use the helper to set the value
            ThreadHelper.SetTaskbarProgressPercent(_myForm, value);
        }

        /// <summary>
        /// Are we running on Windows?
        /// 
        /// The first time called it runs a detection and caches the rsult. Subsequent calls use the
        /// cached result for performance reasons.
        /// </summary>
        /// <returns></returns>
        private bool IsWindows()
        {
            // Do I need to update the cache?
            if (_isWindows == null)
            {
                _isWindows = true;

                OperatingSystem os = Environment.OSVersion;
                PlatformID[] windowsOS = { PlatformID.Win32NT, PlatformID.Win32S, PlatformID.Win32Windows };

                if (Array.IndexOf(windowsOS, os.Platform) == -1)
                {
                    _isWindows = false;
                }
            }

            // Return the cached value
            return (bool)_isWindows;
        }

		/// <summary>
		/// Shows an error message dialog in a GUI framework appropriate way.
		/// </summary>
		/// <param name="title">The title of the message dalog.</param>
		/// <param name="message">The error message to present to the user.</param>
		public void showErrorMessage(string title, string message)
		{
			MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		/// <summary>
		/// Shows an information message dialog in a GUI framework appropriate way.
		/// </summary>
		/// <param name="title">The title of the message dalog.</param>
		/// <param name="message">The message to present to the user.</param>
		public void showInfoMessage(string title, string message)
		{
			MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// Picks an archive file for opening
		/// </summary>
		/// <returns>The path to the file.</returns>
		/// <param name="title">The title of the dialog.</param>
		/// <param name="fileName">The pre-selected file name.</param>
		/// <param name="patterns">File filter patters as an array of {patternName, pattern}.</param>
		/// <param name="OKLabel">The label for the OK button (where supported)</param>
		/// <param name="CancelLabel">The label for the Cancel button (where supported)</param>
		public string pickFile(string title, string fileName, string[,] patterns, string OKLabel, string CancelLabel)
		{
			using (OpenFileDialog fileDialog = new OpenFileDialog())
			{
				StringBuilder sb = new StringBuilder();

				for (int i = 0; i < patterns.Length / 2; i++)
				{
					sb.Append(patterns[i, 0]);
					sb.Append("|");
					sb.Append(patterns[i, 1]);

					if (i < ((patterns.Length / 2) - 1))
					{
						sb.Append("|");
					}
				}

				// Set up the dialog
				fileDialog.FileName = fileName;
				fileDialog.AddExtension = true;
				fileDialog.AutoUpgradeEnabled = true;
				fileDialog.CheckFileExists = true;
				fileDialog.CheckPathExists = true;
				fileDialog.DefaultExt = patterns[0, 1].Substring(2);
				fileDialog.DereferenceLinks = true;
				fileDialog.Filter = sb.ToString();
				fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				fileDialog.Multiselect = false;
				fileDialog.SupportMultiDottedExtensions = true;
				fileDialog.Title = title;

				// If we have a file we will open that folder instead of My Documents
				if (fileName != "")
				{
					fileDialog.InitialDirectory = Path.GetDirectoryName(fileName);
				}

				// Show the dialog
				DialogResult dialogResult = fileDialog.ShowDialog();

				// Did the user cancel the dialog?
				if (dialogResult != DialogResult.OK)
				{
					return "";
				}

				fileName = fileDialog.FileName;
			}

			return fileName;
		}

		/// <summary>
		/// Picks a folder for opening
		/// </summary>
		/// <returns>The path to the filder.</returns>
		/// <param name="title">The title of the dialog.</param>
		/// <param name="folderName">The pre-selected folder name.</param>
		/// <param name="OKLabel">The label for the OK button (where supported)</param>
		/// <param name="CancelLabel">The label for the Cancel button (where supported)</param>
		public string pickFolder(string title, string folderName, string OKLabel, string CancelLabel)
		{
			using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
			{
				// Set up the dialog
				folderDialog.SelectedPath = folderName;
				folderDialog.ShowNewFolderButton = true;

				// Show the dialog
				DialogResult dialogResult = folderDialog.ShowDialog();

				// Did the user cancel the dialog?
				if (dialogResult != DialogResult.OK)
				{
					return "";
				}

				folderName = folderDialog.SelectedPath;
			}

			return folderName;
		}
    }
}
