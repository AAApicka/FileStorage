using System;
using System.Collections.Generic;
using Elinkx.FileStorage.Contracts;
using Elinkx.FileStorage.DataLayer;

namespace Elinkx.FileStorage.ServiceLayer
{
    public class ServiceLayer: IServiceLayer
    {
        private readonly IDataLayer _dataLayer;

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
            catch (Exception e) //melo by se logovat zvlast, nyni OK.
            {

                return new InsertResult()
                {
                    ResultType = ResultTypes.NotInserted,
                    Message = e.GetRootMessage()
                };

            }

        }

        public InsertVersionResult InsertVersion(InsertVersionRequest insertVersionRequest)
        {
            try
            {
                return _dataLayer.InsertVersion(insertVersionRequest);
            }
            catch (Exception e) //melo by se logovat zvlast, nyni OK.
            {

                return new InsertVersionResult()
                {
                    ResultType = ResultTypes.NotInserted,
                    Message = e.GetRootMessage()
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
                        ResultType = ResultTypes.NotUpdated,
                        Message = "FileId does not exist!"
                    };
                }
            }
            catch (Exception e)
            {
               
                return new UpdateResult()
                {
                    ResultType = ResultTypes.NotUpdated,
                    Message = e.Message
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
        /// Get File Content by rowId
        /// </summary>
        /// <param name="getFileRequest"></param>
        /// <returns></returns>
        public GetFileResult GetFile(GetFileRequest getFileRequest){
                try {
                    if (_dataLayer.RowIdExists(getFileRequest.RowId)){
                        return _dataLayer.GetFile(getFileRequest);
                    }
                    else{
                        return new GetFileResult(){
                            ResultType = ResultTypes.DataMissing
                        };
                    }
                }
                catch (Exception e){
                return new GetFileResult() {
                    ResultType = ResultTypes.NotReceived,
                    Message = e.Message    
                    };
                }
        }
    }
}
