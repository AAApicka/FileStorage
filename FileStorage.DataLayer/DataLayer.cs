using System;
using Elinkx.FileStorage.Contracts;
using Elinkx.FileStorage.DataLayer.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;

namespace Elinkx.FileStorage.DataLayer{
    public class DataLayer: IDataLayer{
        DataContext _context;
        IDbContextTransaction transaction;
        Metadata metadata;
        FileVersion fileVersion;
        FileContent fileContent;
        public DataLayer(DataContext context)
        {
            _context = context;
        }
        public InsertResult Insert(InsertRequest insertRequest)
        {
            metadata = new Metadata();
            InsertResult result = new InsertResult();
            transaction = _context.Database.BeginTransaction();
            metadata.ContentType = insertRequest.ContentType;
            metadata.SubjectId = insertRequest.SubjectId;
            metadata.Name = insertRequest.Name;
            metadata.Description = insertRequest.Description;
            metadata.DocumentId = insertRequest.DocumentId;
            metadata.TypeId = insertRequest.TypeId;
            metadata.SubtypeId = insertRequest.SubtypeId;
            metadata.Signed = insertRequest.Signed;
            metadata.Reject = false;
            metadata.Created = DateTime.Now;
            metadata.CreatedBy = insertRequest.UserCode;
            metadata.Changed = DateTime.Now;
            metadata.ChangedBy = insertRequest.UserCode;
            _context.Add(metadata);
            fileContent = new FileContent();
            fileContent.Content = insertRequest.Content;
            _context.Add(fileContent);
            fileVersion = new FileVersion();
            fileVersion.FileId = metadata.FileId;
            fileVersion.RowId = fileContent.RowId;
            fileVersion.Changed = metadata.Changed;
            fileVersion.ChangedBy = metadata.ChangedBy;
            fileVersion.Size = fileContent.Content.Length;
            _context.Add(fileVersion);
            _context.SaveChanges();
            result.FileId = metadata.FileId;
            result.Changed = metadata.Changed;
            result.ChangedBy = metadata.ChangedBy;
            result.ResultType = ResultTypes.Inserted;
            transaction.Commit();
            transaction.Dispose();
            _context.Dispose();
            return result;
        }
        public UpdateResult Update(UpdateRequest updateRequest)
        {
            //update only received fields from contract
            // only updates if request is non zero non null or non empty
            metadata = _context.Metadata.Find(updateRequest.FileId);
            UpdateResult result = new UpdateResult();
            transaction = _context.Database.BeginTransaction();
            metadata.ContentType = updateRequest.ContentType.Length > 0 ? updateRequest.ContentType : metadata.ContentType;
            metadata.SubjectId = updateRequest.SubjectId != 0 ? updateRequest.SubjectId : metadata.SubjectId;
            metadata.Name = updateRequest.Name.Length > 0 ? updateRequest.Name : metadata.Name;
            metadata.Description = updateRequest.Description;
            metadata.DocumentId = updateRequest.DocumentId;
            metadata.TypeId = updateRequest.TypeId;
            metadata.SubtypeId = updateRequest.SubtypeId;
            metadata.Signed = updateRequest.Signed;
            metadata.Reject = false;
            metadata.Changed = DateTime.Now;
            metadata.ChangedBy = updateRequest.UserCode;
            _context.SaveChanges();
            result.FileId = metadata.FileId;
            result.Changed = metadata.Changed;
            result.ChangedBy = metadata.ChangedBy;
            result.ResultType = ResultTypes.Updated;
            transaction.Commit();
            transaction.Dispose();
            _context.Dispose();
            return result;
        }
        public DeleteResult Delete(DeleteRequest deleteRequest)
        {
            metadata = _context.Metadata.Find(deleteRequest.FileId);
            DeleteResult result = new DeleteResult();
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            metadata.Reject = true;
            _context.Add(metadata);
            _context.SaveChanges();
            result.FileId = metadata.FileId;
            result.ResultType = ResultTypes.Deleted;
            transaction.Commit();
            transaction.Dispose();
            _context.Dispose();
            return result;
        }
        public IEnumerable<GetMetadataResult> GetMetadata(GetMetadataRequest getMetadataRequest)
        {
            throw new NotImplementedException();
        }
        public GetFileResult GetFile(GetFileRequest getFileRequest)
        {
            metadata = _context.Metadata.Find(getFileRequest.FileId);
            GetFileResult result = new GetFileResult();
            var content = _context.FileContent.Single(v => v.FileVersion.RowId == (from c in _context.FileVersion
                                                                                   where c.FileId == getFileRequest.FileId
                                                                                   select c).Max(c => c.RowId));
            result.Content = content.Content;
            result.ResultType = ResultTypes.Received;
            return result;
        }
        public GetFileResult GetFileByDId(GetFileRequest getFileRequest)
        {
            metadata = _context.Metadata.Find(getFileRequest.FileId);
            GetFileResult result = new GetFileResult();    
            {
                var content = _context.FileContent.Single(v => v.FileVersion.FileId == (from c in _context.Metadata
                                                                                        where c.DocumentId == getFileRequest.DocumentID
                                                                                        select c).Max(c=>c.FileId));
                result.Content = content.Content;
                result.ResultType = ResultTypes.Received;
            }
            return result;

        }

        public bool FileIdExists(int fileId){
            if (_context.Metadata.Find(fileId) != null){
                return true;
            }
            return false;
        }
        public void RollBack()
        {
            if (transaction != null)
            {
                transaction.Rollback();
            }
        }
        public bool DocumentIdExists(int fileId){
            if (_context.Metadata.Find(fileId) != null){
                return true;
            }
            return false;
        }

        // Old Query functions
        //    //Set Reject by File ID (SoftDelete)
        //    public DeleteResult Delete(DeleteRequest setRejectRequest)
        //    {

        //        using (var _context = new DataContext(connectionString))
        //        {
        //            Metadata metadata = new Metadata();
        //            DeleteResult result = new DeleteResult();

        //            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
        //            {
        //                try
        //                {
        //                    metadata = _context.Metadata.Find(setRejectRequest.FileId);
        //                    if (metadata == null)
        //                    {
        //                        throw new Exception("metadata is null, couldn't find data with that FileId");
        //                    }
        //                    else
        //                    {
        //                        SetReject(setRejectRequest, metadata);
        //                        _context.SaveChanges();
        //                        SetResult(metadata, result);
        //                        transaction.Commit();
        //                    }
        //                }
        //                catch (Exception e)
        //                {
        //                    transaction.Rollback();
        //                    System.Diagnostics.Debug.WriteLine(e.Message + e.StackTrace);
        //                }
        //                return result;
        //            }
        //        }

        //    }

        //    //Get Metadata by Date and optional TypeId
        //    public IEnumerable<GetMetadataResult> GetMetadataByDate(GetMetadataRequest getMetadataByDateRequest)
        //    {
        //        _context = new DataContext(connectionString);
        //        List<GetMetadataResult> result = new List<GetMetadataResult>();
        //        if (string.IsNullOrEmpty(getMetadataByDateRequest.TypeId))
        //        {
        //            List<Metadata> metadata = QueryByDate(getMetadataByDateRequest);
        //            MapToResultList(result, metadata);
        //        }
        //        else
        //        {
        //            List<Metadata> metadata = QueryByDateAndTypeId(getMetadataByDateRequest, _context);
        //            MapToResultList(result, metadata);
        //        }
        //        return result;
        //    }

        //    //Get File Content by File Id or Document Id
        //    public GetFileResult GetFile(GetFileRequest getFileRequest)
        //    {
        //        GetFileResult result = new GetFileResult();

        //        using (var _context = new DataContext(connectionString))
        //        {
        //            try
        //            {

        //                if (getFileRequest.FileId > 0 && getFileRequest.DocumentID == 0)
        //                {
        //                    QueryFileContentByFileId(getFileRequest, result, _context);
        //                    result.ResultType = ResultTypes.Received;
        //                    return result;
        //                }
        //                else if (getFileRequest.FileId == 0 && getFileRequest.DocumentID > 0)
        //                {
        //                    QueryFileContentByDocumentId(getFileRequest, result, _context);
        //                    result.ResultType = ResultTypes.Received;
        //                    return result;
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                System.Diagnostics.Debug.WriteLine(e.Message + e.StackTrace);
        //                result.ResultType = ResultTypes.NotReceived;
        //            }
        //            return result;

        //        }
        //    }


        //private void QueryFileContentByDocumentId(GetFileRequest getFileRequest, GetFileResult result, DataContext _context)
        //{
        //    var fileIdquery = (from c in _context.Metadata
        //                       where c.DocumentId == getFileRequest.DocumentID
        //                       select c).Single().FileId;
        //    var lastVersion = (from c in _context.FileVersion
        //                       where c.FileId == fileIdquery
        //                       select c).Max(c => c.RowId);
        //    var lastFile = (from c in _context.FileContent
        //                    where c.RowId == lastVersion
        //                    select c).Single();

        //    result.ChangedBy = _context.FileVersion.Where(c => c.RowId == lastVersion).Single().ChangedBy;
        //    result.Content = lastFile.Content;
        //}

        //private void QueryFileContentByFileId(GetFileRequest getFileRequest, GetFileResult result, DataContext _context)
        //{
        //    var lastVersion = (from c in _context.FileVersion
        //                       where c.FileId == getFileRequest.FileId
        //                       select c).Max(c => c.RowId);

        //    var lastFile = (from c in _context.FileContent
        //                    where c.RowId == lastVersion
        //                    select c).Single();

        //    result.ChangedBy = _context.FileVersion.Where(c => c.RowId == lastVersion).Single().ChangedBy;
        //    result.Content = lastFile.Content;
        //}
        //    private void EditMetadata(InsertRequest setFileRequest, Metadata metadata)
        //    {
        //        metadata.Changed = DateTime.Now;
        //        metadata.ChangedBy = setFileRequest.UserCode;
        //    }
        //    private void SetResult(Metadata metadata, InsertResult result)
        //    {
        //        result.FileId = metadata.FileId;
        //        result.Changed = DateTime.Now;
        //        result.ChangedBy = metadata.ChangedBy;
        //    }
        //    private void SetResult(Metadata metadata, DeleteResult result)
        //    {
        //        result.FileId = metadata.FileId;
        //result.Changed = metadata.Changed;
        //        result.ChangedBy = metadata.ChangedBy;
        //        result.Reject = metadata.Reject;
        //    }

        //    private void SetNewContent(InsertRequest setFileRequest, DataContext _context, FileContent fileContent)
        //    {


        //    }

        //    private void SetReject(DeleteRequest setRejectRequest, Metadata metadata)
        //    {
        //        metadata.Reject = setRejectRequest.Reject;
        //        metadata.Changed = DateTime.Now;
        //        metadata.ChangedBy = setRejectRequest.UserCode;
        //    }
        //    private List<Metadata> QueryByDateAndTypeId(GetMetadataRequest getMetadataByDateRequest, DataContext _context)
        //    {
        //        return _context.Metadata.Where(dbEntry =>
        //            (dbEntry.Created >= getMetadataByDateRequest.CreatedFrom) &&
        //            (dbEntry.Created <= getMetadataByDateRequest.CreatedTo) && (dbEntry.TypeId == getMetadataByDateRequest.TypeId)).ToList();
        //    }
        //    private void MapToResultList(List<GetMetadataResult> result, List<Metadata> metadata)
        //    {
        //        foreach (var item in metadata)
        //        {
        //            result.Add(new GetMetadataResult()
        //            {
        //                Changed = item.Changed,
        //                ChangedBy = item.ChangedBy,
        //                ContentType = item.ContentType,
        //                Created = item.Created,
        //                CreatedBy = item.CreatedBy,
        //                Description = item.Description,
        //                DocumentId = item.DocumentId,
        //                FileId = item.FileId,
        //                Name = item.Name,
        //                Signed = item.Signed,
        //                SubjectId = item.SubjectId,
        //                SubtypeId = item.SubtypeId,
        //                TypeId = item.TypeId
        //            });
        //        }
        //    }
        //    private List<Metadata> QueryByDate(GetMetadataRequest getMetadataByDateRequest, DataContext _context)
        //    {
        //        return _context.Metadata.Where(dbEntry =>
        //                                   (dbEntry.Created >= getMetadataByDateRequest.CreatedFrom) &&
        //                                   (dbEntry.Created <= getMetadataByDateRequest.CreatedTo)).ToList();
        //    }
    }
}

