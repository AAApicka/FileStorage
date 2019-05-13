using System;
using System.IO;
using Elinkx.FileStorage.Contracts;
using Elinkx.FileStorage.DataLayer;

namespace Elinkx.FileStorage.ServiceLayer {
    public class Service {
        private readonly Data dataLayer;

        public Service() {
            dataLayer = new Data();
        }
        // Sets File metadata and content into database and returns saved metadata
        // with model generated info
        public FileMetadata SetFile(FileMetadata fileMetadata) {
            FileMetadata retFm;
            dataLayer.SetFileContent(fileMetadata);
            return fileMetadata;
        }





        public FileMetadata GetFileMetadata() {
            return dataLayer.GetFileMetadata();
        }
        public FileReject SetFileReject() {
            return dataLayer.SetFileReject();
        }
        public FileReject GetFileReject() {
            return dataLayer.SetFileReject();
        }
       

    }
}
