﻿using System;
using System.Linq;
using Acuerdo.External.Uploader;
using Inscribe.Configuration;
using Inscribe.Plugin;
using Livet;

namespace Inscribe.ViewModels.Dialogs.SettingSub
{
    public class ExtServiceConfigViewModel : ViewModel, IApplyable
    {
        public ExtServiceConfigViewModel()
        {
            this.ImageUploaderCandidates = UploaderManager.Uploaders.ToArray();

            var su = UploaderManager.GetSuggestedUploader();
            this.ImageUploadCandidateIndex = -1;
            if (su != null)
            {
                var idx = this.ImageUploaderCandidates
                    .TakeWhile(u => u.ServiceName != Setting.Instance.ExternalServiceProperty.UploaderService)
                    .Count();
                if (idx < this.ImageUploaderCandidates.Length)
                {
                    this.ImageUploadCandidateIndex = idx;
                }
            }
            if (this.ImageUploadCandidateIndex == -1)
                this.ImageUploadCandidateIndex = 0;
        }

        public IUploader[] ImageUploaderCandidates { get; private set; }

        private int _imageUploadCandidateIndex;
        public int ImageUploadCandidateIndex
        {
            get { return this._imageUploadCandidateIndex; }
            set
            {
                this._imageUploadCandidateIndex = value;
                RaisePropertyChanged(() => ImageUploadCandidateIndex);
            }
        }

        public void Apply()
        {
            if (this.ImageUploadCandidateIndex < this.ImageUploaderCandidates.Count())
                Setting.Instance.ExternalServiceProperty.UploaderService =
                    this.ImageUploaderCandidates[this.ImageUploadCandidateIndex].ServiceName;
            else
                Setting.Instance.ExternalServiceProperty.UploaderService = String.Empty;
        }
    }
}
