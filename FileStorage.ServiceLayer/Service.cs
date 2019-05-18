using System;
using System.Collections.Generic;
using System.IO;
using Elinkx.FileStorage.Contracts;
using Elinkx.FileStorage.DataLayer;

namespace Elinkx.FileStorage.ServiceLayer
{
    public class Service
    {
        private readonly Data dataLayer;

        public Service()
        {
            dataLayer = new Data();
        }
        /// <summary>
        /// Saves File and its Metadata into database. It will create new version for existing files
        /// </summary>
        /// <returns>Returns metadata with generated FileId</returns>
        public SetFileResult SetFile(SetFileRequest fileMetadata)
        {
            
            return dataLayer.SetFile(fileMetadata);
        }

        public RejectResult SetReject(SetRejectRequest fileMetadata)
        {
            
            return dataLayer.SetReject(fileMetadata);
        }

        public List<GetMetadataResult> ReadMetadata(GetMetadata readMetadata)
        {
            return dataLayer.GetMetadata(readMetadata);
        }
         
        public GetContentResult GetContent (GetContentRequest getFile)
        {
            return dataLayer.GetContent(getFile);
        }
        
    }

}
