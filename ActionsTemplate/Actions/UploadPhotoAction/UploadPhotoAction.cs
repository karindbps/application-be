using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Amazon.Lambda.APIGatewayEvents;

namespace ActionsTemplate
{
    class UploadPhotoAction : IAction
    {
        public UploadPhotoAction() { 
        }
                public bool AppliesTo(string action)
        {
            string name = MyActions.Upload_Photo;
            return string.Equals(name, action, StringComparison.OrdinalIgnoreCase);
        }

        public async Task<string> DoAction(APIGatewayProxyRequest request)
        {
            //var resp = "photo has been uploaded";
            //UploadPhoto
            //Notify me that there is a new photo
            throw new NotImplementedException();
        }

    }
}
