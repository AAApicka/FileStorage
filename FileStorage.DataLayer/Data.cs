﻿using System;
using System.Linq;
using Elinkx.FileStorage.Contracts;
using Elinkx.FileStorage.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace Elinkx.FileStorage.DataLayer
{
    // Dodelat Interface pro Data a pro Service.
    public class Data
    {
        string connectionString;

        public Data(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Set File - new or new version
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
                        // rozhodit do dvou metod
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

                            fileContent.Content = setFileRequest.Content;
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
                        System.Diagnostics.Debug.WriteLine(e.Message + e.StackTrace);
                        result.FileId = 0;
                        result.Changed = new DateTime();
                        result.ChangedBy = "NULL";
                    }
                    return result;
                }
            }
        }

        //Set Reject by File ID
        public SetRejectResult SetReject(SetRejectRequest setRejectRequest)
        {

            using (var _context = new DataContext(connectionString))
            {
                Metadata metadata = new Metadata();
                SetRejectResult result = new SetRejectResult();

                using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        if (setRejectRequest.FileId == 0)
                        {
                            //whatif
                        }
                        else if (setRejectRequest.FileId > 0)
                        {
                            //get entity by FileID and fill changes
                            metadata = _context.Metadata.SingleOrDefault(dbEntry => dbEntry.FileId == setRejectRequest.FileId);
                            metadata.Reject = setRejectRequest.Reject;
                            metadata.Changed = DateTime.Now;
                            metadata.ChangedBy = setRejectRequest.UserCode;
                            _context.SaveChanges();
                            //set result
                            result.FileId = metadata.FileId;
                            result.Reject = metadata.Reject;
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

        //Get Metadata by Date
        public List<GetMetadataResult> GetMetadataByDate(GetMetadataByDateRequest getMetadataByDateRequest)
        {
            using (var _context = new DataContext(connectionString))
            {
                List<GetMetadataResult> result = new List<GetMetadataResult>();

                using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        var adjustedCreatedTo = getMetadataByDateRequest.CreatedTo.AddHours(23).AddMinutes(59).AddSeconds(59);
                        if (string.IsNullOrEmpty(getMetadataByDateRequest.TypeId))
                        {
                            var metadata = 
                           _context.Metadata.Where(dbEntry =>
                           (dbEntry.Created >= getMetadataByDateRequest.CreatedFrom) &&
                           (dbEntry.Created <= adjustedCreatedTo)).ToList();
                            foreach (var item in metadata)
                            {
                                result.Add(new GetMetadataResult()
                                {
                                    Changed = item.Changed,
                                    ChangedBy = item.ChangedBy,
                                    ContentType = item.ContentType,
                                    Created = item.Created,
                                    CreatedBy = item.CreatedBy,
                                    Description = item.Description,
                                    DocumentId = item.DocumentId,
                                    FileId = item.FileId,
                                    Name = item.Name,
                                    Signed = item.Signed,
                                    SubjectId = item.SubjectId,
                                    SubtypeId = item.SubtypeId,
                                    TypeId = item.TypeId
                                });
                            }
                        }
                        else
                        {
                            var metadata =
                                _context.Metadata.Where(dbEntry =>
                                (dbEntry.Created >= getMetadataByDateRequest.CreatedFrom) &&
                                (dbEntry.Created <= adjustedCreatedTo) && (dbEntry.TypeId == getMetadataByDateRequest.TypeId)).ToList();
                            foreach (var item in metadata)
                            {
                                result.Add(new GetMetadataResult()
                                {
                                    Changed = item.Changed,
                                    ChangedBy = item.ChangedBy,
                                    ContentType = item.ContentType,
                                    Created = item.Created,
                                    CreatedBy = item.CreatedBy,
                                    Description = item.Description,
                                    DocumentId = item.DocumentId,
                                    FileId = item.FileId,
                                    Name = item.Name,
                                    Signed = item.Signed,
                                    SubjectId = item.SubjectId,
                                    SubtypeId = item.SubtypeId,
                                    TypeId = item.TypeId
                                });
                            }
                        }
                        transaction.Commit();

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine(e.Message + e.StackTrace);
                    }
                    return result;
                }
            }

        }

        //Get File by File Id or Document Id
        public GetFileResult GetFile(GetFileRequest getFileRequest)
        {
            GetFileResult result = new GetFileResult();

            using (var _context = new DataContext(connectionString))
            {
                using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        if (getFileRequest.FileId > 0 && getFileRequest.DocumentID == 0)
                        {
                            var lastVersion = (from c in _context.FileVersion
                                               where c.FileId == getFileRequest.FileId
                                               select c).Max(c => c.RowId);

                            var lastFile = (from c in _context.FileContent
                                            where c.RowId == lastVersion
                                            select c).Single();

                            result.ChangedBy = _context.FileVersion.Where(c => c.RowId == lastVersion).SingleOrDefault().ChangedBy;
                            result.Content = lastFile.Content;
                            transaction.Commit();

                            return result;
                        }
                        else if (getFileRequest.FileId == 0 && getFileRequest.DocumentID > 0)
                        {
                            var fileIdquery = (from c in _context.Metadata
                                               where c.DocumentId == getFileRequest.DocumentID
                                               select c).Single().FileId;
                            var lastVersion = (from c in _context.FileVersion
                                               where c.FileId == fileIdquery
                                               select c).Max(c => c.RowId);
                            var lastFile = (from c in _context.FileContent
                                            where c.RowId == lastVersion
                                            select c).Single();

                            result.ChangedBy = _context.FileVersion.Where(c => c.RowId == lastVersion).SingleOrDefault().ChangedBy;
                            result.Content = lastFile.Content;
                            transaction.Commit();

                            return result;
                        }
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine(e.Message + e.StackTrace);
                    }
                    return result;
                }
            }
        }

    }
}

