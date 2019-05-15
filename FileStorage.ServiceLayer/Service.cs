using System;
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
            // resit jako jedna transakce v DL.!
            return dataLayer.SetFile(fileMetadata);
        }


    }
}
