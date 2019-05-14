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
        public SetFileResult SetFile(FileMetadata fileMetadata) {
            // resit jako jedna transakce v DL.!
            dataLayer.SetFileContent(fileMetadata);
            return dataLayer.SetFileMetadata(fileMetadata);
        }





        public FileMetadata GetFileMetadata() {
            return dataLayer.GetFileMetadata();
        }
    

    }
}
