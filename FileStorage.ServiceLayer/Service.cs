using System;
using System.Collections.Generic;
using Elinkx.FileStorage.Contracts;
using Elinkx.FileStorage.DataLayer;

namespace Elinkx.FileStorage.ServiceLayer
{
    public class Service
    {
        private readonly Data dataLayer;

        public Service()
        {
            dataLayer = new Data("server=localhost\\SQLEXPRESS;database=FileStorage;Trusted_Connection=true");
        }
        /// <summary>
        /// Saves File and its Metadata into database. It will create new version for existing files
        /// </summary>
        /// <returns>Returns metadata</returns>
        public SetFileResult SetFile(SetFileRequest fileMetadata)
        {
            return dataLayer.SetFile(fileMetadata);
        }
        /// <summary>
        /// Sets Reject value under provided FileId
        /// </summary>
        /// <param name="setRejectRequest"></param>
        /// <returns>Returns metadata with new Reject value</returns>
        public SetRejectResult SetReject(SetRejectRequest setRejectRequest)
        {
            return dataLayer.SetReject(setRejectRequest);
        }
        /// <summary>
        /// Gets List of Metadata 
        /// </summary>
        /// <param name="getMetadataByDateRequest"></param>
        /// <returns></returns>
        public List<GetMetadataResult> GetMetadataByDate(GetMetadataByDateRequest getMetadataByDateRequest)
        {
            return dataLayer.GetMetadataByDate(getMetadataByDateRequest);
        }
        /// <summary>
        /// Get File by File ID
        /// </summary>
        /// <param name="getFileRequest"></param>
        /// <returns></returns>
        public GetFileResult GetFile(GetFileRequest getFileRequest)
        {
            return dataLayer.GetFile(getFileRequest);  }
    }
}
