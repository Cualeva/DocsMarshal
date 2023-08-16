using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Orchestrator.Managers
{
    public class Common : IDisposable
    {
        private Manager Orchestrator;
        public Common(DocsMarshal.Connectors.Orchestrator.Manager manager)
        {
            Orchestrator = manager;
        }

        public async Task<Entities.BaseJsonResult<Guid?>> UploadTmpFile(Entities.FileValue file)
        {
            try
            {
                if (file == null || file.Content == null)
                    throw new ArgumentNullException(nameof(file));
                var url = Orchestrator.DocsMarshalUrl + "/Profile/UploadFile?sessionId=" + Orchestrator.SessionId;
                HttpContent bytesContent = new ByteArrayContent(file.Content);
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(60);
                    using (var formData = new MultipartFormDataContent())
                    {
                        formData.Add(bytesContent, "file", file.FileName);
                        var response = await client.PostAsync(url, formData);
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception(response.StatusCode.ToString());
                        }
                        var responseBodyString = await response.Content.ReadAsStringAsync();
                        var responseBodyObj = JsonConvert.DeserializeObject<Entities.BaseJsonResult<Guid?>>(responseBodyString);
                        return responseBodyObj;
                    }
                }
               
            } catch(Exception e)
            {
                return new Entities.BaseJsonResult<Guid?> { Error = true, ErrorDescription = e.Message };
            }
        }

        public async Task<Entities.BaseJsonResult<bool>> DeleteUploadedTmpFile(Guid tempId)
        {
            return await Orchestrator.PostAsync<Entities.BaseJsonResult<bool>>("/Profile/DeleteUploadedFile", new { sessionId = Orchestrator.SessionId, tempId = tempId });
        }

        public void Dispose()
        {
        }
    }
}