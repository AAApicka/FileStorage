using System;
using System.Collections.Generic;
using Elinkx.FileStorage.Contracts;
using Elinkx.FileStorage.DataLayer;

namespace Elinkx.FileStorage.ServiceLayer
{
    public class ServiceLayer: IServiceLayer
    {
        private readonly IDataLayer _dataLayer;

        //1. Metodu SetReject prejmenovat na Delete a funkce pouze nastaveni na true at programator webklienta vi co to dela
        public ServiceLayer(IDataLayer datalayer)
        {
            _dataLayer = datalayer;
        }

        public InsertResult Insert(InsertRequest insertRequest)
        {
            try
            {
                return _dataLayer.Insert(insertRequest);
            }
            catch (Exception)
            {
                _dataLayer.RollBack();
                return new InsertResult()
                {
                    ResultType = ResultTypes.NotInserted
                };

            }

        }

        public UpdateResult Update(UpdateRequest updateRequest)
        {
            try
            {
                if (_dataLayer.FileIdExists(updateRequest.FileId))
                {
                    return _dataLayer.Update(updateRequest);
                }
                else
                {
                    return new UpdateResult()
                    {
                        ResultType = ResultTypes.NotUpdated
                    };
                }
            }
            catch (Exception)
            {
                _dataLayer.RollBack();
                return new UpdateResult()
                {
                    ResultType = ResultTypes.NotUpdated
                };

            }

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
        /// Get File by File ID od DocumentID
        /// </summary>
        /// <param name="getFileRequest"></param>
        /// <returns></returns>
        public GetFileResult GetFile(GetFileRequest getFileRequest){
            if (getFileRequest.FileId != 0){
                try{
                    if (_dataLayer.FileIdExists(getFileRequest.FileId)){
                        return _dataLayer.GetFile(getFileRequest);
                    }
                    else{
                        return new GetFileResult(){
                            ResultType = ResultTypes.NotReceived
                        };
                    }
                }
                catch (Exception){
                    return new GetFileResult(){
                        ResultType = ResultTypes.NotReceived
                    };
                }
            }
            else 
                try {
                    if (_dataLayer.DocumentIdExists(getFileRequest.DocumentID)){
                        return _dataLayer.GetFileByDId(getFileRequest);
                    }
                    else{
                        return new GetFileResult(){
                            ResultType = ResultTypes.NotReceived
                        };
                    }
                }
                catch (Exception){
                    return new GetFileResult(){
                        ResultType = ResultTypes.NotReceived
                    };
                }
        }
    }
}
