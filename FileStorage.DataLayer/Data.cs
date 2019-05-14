using System;
using Elinkx.FileStorage.Contracts;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Elinkx.FileStorage.DataLayer {
    public class Data {
        string connection;
        DataContext _context;

        public Data() {
            _context = new DataContext("server=localhost\\SQLEXPRESS;database=FileStorage;Trusted_Connection=true");
        }
        public void SetFileContent(FileMetadata fileMetadata) {
            //Todo:
        }
        public SetFileResult SetFileMetadata(FileMetadata fileMetadata) {
            DbMetadata dbmd = new DbMetadata();
            SetFileResult sfResult = new SetFileResult();
            dbmd.Name = fileMetadata.Name;
            dbmd.FileId = 1;
            sfResult.FileId = dbmd.FileId;
            _context.dbMetadata.Add(dbmd);
            _context.SaveChanges();

            return sfResult;
        }




        //Todo
        public FileMetadata GetFileMetadata() {
            return new FileMetadata();
        }
        
    }
}
