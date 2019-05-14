using System;
using Elinkx.FileStorage.Contracts;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Elinkx.FileStorage.DataLayer {
    public class Data {
        string connection;
        DataContext dbSession;

        public Data() {
            //dbSession = new DataContext();
        }
        public void SetFileContent(FileMetadata fileMetadata) {
            //Todo:LinQ query
        }
        public FileMetadata SetFileMetadata(FileMetadata fileMetadata) {
            //Todo:LinQ query
            return fileMetadata;
        }




        //Todo
        public FileMetadata GetFileMetadata() {
            return new FileMetadata();
        }
        public FileReject SetFileReject() {
            return new FileReject();
        }
        public FileReject GetFileReject() {
            return new FileReject();
        }
    }
}
