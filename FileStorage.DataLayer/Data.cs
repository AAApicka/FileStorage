using System;
using System.Text;
using System.Linq;
using Elinkx.FileStorage.Contracts;
using Elinkx.FileStorage.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace Elinkx.FileStorage.DataLayer
{
    public class Data
    {
        string connectionString;
       

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
                        // case1 INSERT - FileId je 0 - tedy novy zaznam v db
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

                            fileContent.Content = setFileRequest.Content;
                            //Encoding.ASCII.GetBytes("some string to test storage in db");
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
                            //case2- UPDATE do something to write new version when FileId is positive nonzero

                            //get entity by FileID
                            metadata = _context.Metadata.SingleOrDefault(dbEntry => dbEntry.FileId == setFileRequest.FileId);
                            metadata.Changed = DateTime.Now;
                            metadata.ChangedBy = setFileRequest.UserCode;
                            _context.SaveChanges();

                            fileContent.Content = Encoding.ASCII.GetBytes("some string to test storage in db ..2ndVersion");
                            _context.Add(fileContent);
                            _context.SaveChanges();

                            fileVersion.FileId = metadata.FileId;
                            fileVersion.RowId = fileContent.RowId;
                            fileVersion.Changed = metadata.Changed;
                            fileVersion.ChangedBy = metadata.ChangedBy;
                            fileVersion.Size = fileContent.Content.Length;
                            _context.Add(fileVersion);
                            _context.SaveChanges();

                            result.FileId = metadata.FileId;
                            result.Changed = DateTime.Now;
                            result.ChangedBy = metadata.ChangedBy;

                            transaction.Commit();


                        }
                    }
                    catch (Exception e)
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
        public RejectResult SetReject(SetRejectRequest setRejectRequest)
        {
            using (var _context = new DataContext(connectionString))
            {
                Metadata metadata = new Metadata();
                RejectResult result = new RejectResult();

                using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        if (setRejectRequest.FileId > 0)
                        {
                            metadata =
                                  (from c in _context.Metadata
                                   where c.FileId == setRejectRequest.FileId
                                   select c).SingleOrDefault();
                            //metadata = _context.Metadata.SingleOrDefault(dbEntry => dbEntry.FileId == setRejectRequest.FileId);
                            //mapovani v modelu??? zde jen volani fce z modelu
                            metadata.Reject = setRejectRequest.Rejected;
                            metadata.Changed = DateTime.Now;
                            metadata.ChangedBy = setRejectRequest.UserCode;
                            _context.SaveChanges();

                            //throw new Exception();

                            result.Reject = true;
                            result.Changed = DateTime.Now;
                            result.ChangedBy = metadata.ChangedBy;

                            transaction.Commit();
                        }



                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine(e.Message + e.StackTrace);
                        result.Reject = false;

                    }

                    return result;

                }
            }
        }
        public List<GetMetadataResult> GetMetadata(GetMetadata readMetadata)
        {
            using (var _context = new DataContext(connectionString))
            {

                List<GetMetadataResult> result = new List<GetMetadataResult>();

                using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // search by FileId

                        if (readMetadata.FileId > 0)
                        {
                            var metadata =
                                   (from c in _context.Metadata
                                    where c.FileId == readMetadata.FileId
                                    select c);
                            //metadata = _context.Metadata.SingleOrDefault(dbEntry => dbEntry.FileId == setRejectRequest.FileId);
                            //mapovani v modelu??? zde jen volani fce z modelu
                            foreach (var item in metadata)
                            {
                                var resultMd = new GetMetadataResult()
                                {
                                    Name = item.Name,
                                    FileId = item.FileId,
                                    ContentType = item.ContentType,
                                    SubjectId = item.SubjectId,
                                    Description = item.Description,
                                    DocumentId = item.DocumentId,
                                    TypeId = item.TypeId,
                                    SubtypeId = item.SubtypeId,
                                    Signed = item.Signed,
                                    Created = item.Created,
                                    Changed = item.Changed,
                                    ChangedBy = item.ChangedBy,
                                    CreatedBy = item.CreatedBy
                                };
                                result.Add(resultMd);
                            }
                            return result;
                        }
                        else if (readMetadata.FileId == 0)
                        {
                            DateTime DtCreated2 = readMetadata.Created2.AddHours(23).AddMinutes(59).AddSeconds(59);
                            var metadata =
                                    (from c in _context.Metadata
                                     where (c.Created >= readMetadata.Created ) && (c.Created <= DtCreated2)
                                     select c);
                        
                            foreach (var item in metadata)
                            {
                                var resultMd = new GetMetadataResult()
                                {
                                    Name = item.Name,
                                    FileId = item.FileId,
                                    ContentType = item.ContentType,
                                    SubjectId = item.SubjectId,
                                    Description = item.Description,
                                    DocumentId = item.DocumentId,
                                    TypeId = item.TypeId,
                                    SubtypeId = item.SubtypeId,
                                    Signed = item.Signed,
                                    Created = item.Created,
                                    Changed = item.Changed,
                                    ChangedBy = item.ChangedBy,
                                    CreatedBy = item.CreatedBy
                                };
                                result.Add(resultMd);
                            }
                            return result;
                        }


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message + e.StackTrace);
                    }

                    return result;


                }
            }
        }
        public GetContentResult GetContent(GetContentRequest getContent)
        {
            using (var _context = new DataContext(connectionString))
            {
                Metadata metadata = new Metadata();
                FileVersion fileVersion = new FileVersion();
                FileContent fileContent = new FileContent();
                GetContentResult result = new GetContentResult();

                using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message + e.StackTrace);
                    }
                    return result;
                }
            }
        }
    }
}
