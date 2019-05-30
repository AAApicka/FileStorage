using System;
using Elinkx.FileStorage.Contracts;
using Elinkx.FileStorage.DataLayer.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;

namespace Elinkx.FileStorage.DataLayer
{
    public class DataLayer : IDataLayer, IDisposable
    {
        readonly DataContext _context;
        Metadata metadata;
        FileVersion fileVersion;
        FileContent fileContent;

        public DataLayer(DataContext context)
        {
            _context = context;
        }
         
        public InsertResult Insert(InsertRequest insertRequest)
        {
            metadata = new Metadata
            {
                ContentType = insertRequest.ContentType,
                SubjectId = insertRequest.SubjectId,
                Name = insertRequest.Name,
                Description = insertRequest.Description,
                DocumentId = insertRequest.DocumentId,
                TypeId = insertRequest.TypeId,
                SubtypeId = insertRequest.SubtypeId,
                Reject = false,
                Created = DateTime.Now,
                CreatedBy = insertRequest.UserCode,
                Changed = DateTime.Now,
                ChangedBy = insertRequest.UserCode
            };
            _context.Add(metadata);
            fileContent = new FileContent
            {
                Content = insertRequest.Content
            };
            _context.Add(fileContent);
            fileVersion = new FileVersion
            {
                Metadata = metadata,
                FileContent = fileContent,
                Signed = insertRequest.Signed,
                Changed = metadata.Changed,
                ChangedBy = metadata.ChangedBy,
                Size = fileContent.Content.Length
            };
            _context.Add(fileVersion);
            _context.SaveChanges();
            return new InsertResult()
            {
                FileId = metadata.FileId,
                Changed = metadata.Changed,
                ChangedBy = metadata.ChangedBy,
                ResultType = ResultTypes.Inserted,
                Message = ResultTypes.Inserted.ToString()
            };
        }
        public InsertVersionResult InsertVersion(InsertVersionRequest insertVersionRequest)
        {
            metadata = _context.Metadata.Find(insertVersionRequest.FileId);
            metadata.Changed = DateTime.Now;
            metadata.ChangedBy = insertVersionRequest.UserCode;
            fileContent = new FileContent
            {
                Content = insertVersionRequest.Content
            };
            _context.Add(fileContent);
            fileVersion = new FileVersion
            {
                Metadata = metadata,
                FileContent = fileContent,
                Changed = metadata.Changed,
                ChangedBy = metadata.ChangedBy,
                Size = fileContent.Content.Length,
                Signed = insertVersionRequest.Signed
            };
            _context.Add(fileVersion);
            _context.SaveChanges();
            return new InsertVersionResult()
            {
                RowId = fileContent.RowId,
                Changed = metadata.Changed,
                ChangedBy = metadata.ChangedBy,
                Signed = fileVersion.Signed,
                ResultType = ResultTypes.Inserted,
                Message = ResultTypes.Inserted.ToString()
            };
        }
        public UpdateResult Update(UpdateRequest updateRequest)
        {
            metadata = _context.Metadata.Find(updateRequest.FileId);
            metadata.ContentType = !string.IsNullOrEmpty(updateRequest.ContentType) ? updateRequest.ContentType : metadata.ContentType;
            metadata.SubjectId = !(updateRequest.SubjectId == 0) ? updateRequest.SubjectId : metadata.SubjectId;
            metadata.Name = !string.IsNullOrEmpty(updateRequest.Name) ? updateRequest.Name : metadata.Name;
            metadata.Description = !string.IsNullOrEmpty(updateRequest.Description) ? updateRequest.Description : metadata.Description;
            metadata.DocumentId = !(updateRequest.DocumentId == 0) ? updateRequest.DocumentId : metadata.DocumentId;
            metadata.TypeId = !string.IsNullOrEmpty(updateRequest.TypeId) ? updateRequest.TypeId : metadata.TypeId;
            metadata.SubtypeId = !string.IsNullOrEmpty(updateRequest.SubtypeId) ? updateRequest.SubtypeId : metadata.SubtypeId;
            metadata.Changed = DateTime.Now;
            metadata.ChangedBy = updateRequest.UserCode;
            _context.SaveChanges();
            return new UpdateResult()
            {
                FileId = metadata.FileId,
                Changed = metadata.Changed,
                ChangedBy = metadata.ChangedBy,
                ResultType = ResultTypes.Updated,
                Message = ResultTypes.Updated.ToString()
            };
        }
        public DeleteResult Delete(DeleteRequest deleteRequest)
        {
            metadata = _context.Metadata.Find(deleteRequest.FileId);
            metadata.Reject = true;
            _context.SaveChanges();
            return new DeleteResult()
            {
                FileId = metadata.FileId,
                ResultType = ResultTypes.Deleted,
                Message = ResultTypes.Deleted.ToString()
            };
        }
        public IEnumerable<GetMetadataResult> GetMetadata(GetMetadataRequest getMetadataRequest)
        {
            if (string.IsNullOrEmpty(getMetadataRequest.SubtypeId) && (getMetadataRequest.SubjectId == 0))
            {
                List<Metadata> metadata = QueryByDocAndTypeId(getMetadataRequest);
                return MapToResultList(metadata);
            }
            else if (getMetadataRequest.SubjectId != 0)
            {
                List<Metadata> metadata = QueryBySubjectId(getMetadataRequest);
                return MapToResultList(metadata);
            }
            else
            {
                List<Metadata> metadata = QueryByDocTypeAndSubtypeId(getMetadataRequest);
                return MapToResultList(metadata);
            }

        }
        public GetFileResult GetFile(GetFileRequest getFileRequest)
        {
            GetFileResult result = new GetFileResult();
            var content = _context.FileContent.Single(v => v.FileVersion.FileContent.RowId == getFileRequest.RowId);
            result.Content = content.Content;
            result.ResultType = ResultTypes.GetFileSuccess;
            result.Message = ResultTypes.GetFileSuccess.ToString();
            return result;
        }

        private List<Metadata> QueryByDocAndTypeId(GetMetadataRequest getMetadataRequest)
        {
            return _context.Metadata.Where(dbEntry =>
                (dbEntry.DocumentId == getMetadataRequest.DocumentId) &&
                (dbEntry.TypeId == getMetadataRequest.TypeId)).ToList();
        }
        private List<Metadata> QueryByDocTypeAndSubtypeId(GetMetadataRequest getMetadataRequest)
        {
            return _context.Metadata.Where(dbEntry =>
                (dbEntry.DocumentId == getMetadataRequest.DocumentId) &&
                (dbEntry.TypeId == getMetadataRequest.TypeId) &&
                (dbEntry.SubtypeId == getMetadataRequest.SubtypeId)).ToList();
        }
        private List<Metadata> QueryBySubjectId(GetMetadataRequest getMetadataRequest)
        {
            return _context.Metadata.Where(dbEntry =>
                (dbEntry.SubjectId == getMetadataRequest.SubjectId)).ToList();
        }
        private IEnumerable<GetMetadataResult> MapToResultList(List<Metadata> metadata)
        {
            List<GetMetadataResult> result = new List<GetMetadataResult>();
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
                    SubjectId = item.SubjectId,
                    SubtypeId = item.SubtypeId,
                    TypeId = item.TypeId,
                    AllVersions = GetVersionsFromMetadata(item).ToList(),
                    ResultType = ResultTypes.GetMetadataSuccess,
                    Message = ResultTypes.GetMetadataSuccess.ToString()
                });
            }
            return result;

        }
        private IEnumerable<FileVersionResult> GetVersionsFromMetadata(Metadata md)
        {
            List<FileVersionResult> versions = new List<FileVersionResult>();
            return _context.FileVersion.Where(c => c.Metadata.FileId == md.FileId)
                            .Select(c => new FileVersionResult() {
                                Changed = c.Changed,
                                ChangedBy = c.ChangedBy,
                                Size = c.Size,
                                Signed = c.Signed,
                                RowId = c.FileContent.RowId
                            });

        }

        //helper methods
        public bool FileIdExists(int fileId)
        {
            if (_context.Metadata.Find(fileId) != null)
            {
                return true;
            }
            return false;
        }
        public bool RowIdExists(int rowId)
        {
            if (_context.FileContent.Single(c => c.RowId == rowId) != null)
            {
                return true;
            }
            return false;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

