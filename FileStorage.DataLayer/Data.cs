using System;
using System.Text;
using Elinkx.FileStorage.Contracts;
using Elinkx.FileStorage.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Elinkx.FileStorage.DataLayer
{
    public class Data
    {
        string connectionString;
        DataContext _context;

        public Data()
        {
            connectionString = "server=localhost\\SQLEXPRESS;database=FileStorage;Trusted_Connection=true";
        }
        public SetFileResult SetFile(SetFileRequest setFileRequest)
        {
            using (var _context = new DataContext(connectionString))
            {
                Metadata metadata = new Metadata();
                FileVersion fileVersion = new FileVersion();
                FileContent fileContent = new FileContent();
                SetFileResult result = new SetFileResult();

                using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
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

                            fileContent.Content = Encoding.ASCII.GetBytes("some string to test storage in db");
                            _context.Add(fileContent);
                            _context.SaveChanges();

                            fileVersion.FileId = metadata.FileId;
                            fileVersion.RowId = fileContent.RowId;
                            fileVersion.Changed = metadata.Changed;
                            fileVersion.ChangedBy = metadata.ChangedBy;
                            fileVersion.Size = fileContent.Content.Length;
                            _context.Add(fileVersion);
                            _context.SaveChanges(); 

                            //throw new Exception();

                            result.FileId = metadata.FileId;
                            result.Changed = DateTime.Now;
                            result.ChangedBy = metadata.ChangedBy;

                            transaction.Commit();
                        }
                        else if (setFileRequest.FileId > 0)
                        {
                            //case2- do something to write new version when FileId is positive nonzero
                            
                        }
                    } catch (Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine(e.Message + e.StackTrace);
                        result.FileId = 0;
                        result.Changed = new DateTime();
                        result.ChangedBy = "NULL";
                    }
                    return result;
                }
            }

            
            
        }

    }
}
