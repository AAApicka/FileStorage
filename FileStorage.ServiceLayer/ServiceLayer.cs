using System;
using System.Collections.Generic;
using Elinkx.FileStorage.Contracts;
using Elinkx.FileStorage.DataLayer;

namespace Elinkx.FileStorage.ServiceLayer
{
    public class ServiceLayer : IServiceLayer
    {
        private readonly IDataLayer _dataLayer;

        //1. Metodu SetReject prejmenovat na Delete a funkce pouze nastaveni na true at programator webklienta vi co to dela
        public ServiceLayer(IDataLayer datalayer)
        {
            _dataLayer = datalayer;
        }

        public InsertResult Insert(InsertRequest insertRequest)
        {
            return _dataLayer.Insert(insertRequest);
        }

        public UpdateResult Update(UpdateRequest updateRequest)
        {
            throw new NotImplementedException();
        }

        public DeleteResult Delete(DeleteRequest deleteRequest)
        {
            return _dataLayer.Delete(deleteRequest);
        }

        public IEnumerable<GetMetadataResult> GetMetadata(GetMetadataRequest getMetadataRequest)
        {
            return _dataLayer.GetMetadata(getMetadataRequest);
        }

        /// <summary>
        /// Get File by File ID
        /// </summary>
        /// <param name="getFileRequest"></param>
        /// <returns></returns>
        public GetFileResult GetFile(GetFileRequest getFileRequest)
        {
            return _dataLayer.GetFile(getFileRequest);
        }
    }
}
