using System;
using Elinkx.FileStorage.Contracts;
using Elinkx.FileStorage.Models;

namespace Elinkx.FileStorage.DataLayer
{
    public class Data
    {
        string connection;
        DataContext _context;

        public Data()
        {
            _context = new DataContext("server=localhost\\SQLEXPRESS;database=FileStorage;Trusted_Connection=true");
        }
        public SetFileResult SetFile(SetFileRequest setFileRequest)
        {
            Metadata metadata = new Metadata();
            FileVersion fileVersion = new FileVersion();
            FileContent fileContent = new FileContent();
            SetFileResult result = new SetFileResult();

            // case1 FileId je 0 - tedy novy zaznam v db
            if (setFileRequest.FileId == 0)
            {
                metadata.ContentType = setFileRequest.ContentType;
                metadata.SubjectId = setFileRequest.SubjectId;
                metadata.Name = setFileRequest.Name;
                metadata.Description = setFileRequest.Description;
                metadata.DocumentId = setFileRequest.DocumentId;
                metadata.TypeId = setFileRequest.TypeId;
                metadata.SubtypeId = setFileRequest.SubtypeId;
                metadata.Signed = setFileRequest.Signed;
                metadata.Reject = false;
                metadata.Created = DateTime.Now;
                metadata.CreatedBy = setFileRequest.UserCode;
                metadata.Changed = DateTime.Now;
                metadata.ChangedBy = setFileRequest.UserCode;
                _context.Add(metadata);
                _context.SaveChanges();
                result.UserCode = "done";
                result.FileId = 1;
                result.Created = DateTime.Now;
                result.Changed = DateTime.Now;
            }

            return result;
        }

    }
}
