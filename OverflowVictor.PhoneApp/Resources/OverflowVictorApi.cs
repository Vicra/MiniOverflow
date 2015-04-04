using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Portable;
using RestSharp.Portable.Deserializers;

namespace OverflowVictor.PhoneApp.Resources
{
    public class OverflowVictorApi
    {
        private RestClient cliente;
        public OverflowVictorApi()
        {
            cliente = new RestClient()
            {
               BaseUrl = new Uri("http://localhost:55102/")
            };
        }

        public IEnumerable<QuestionListModel> GetQuestionListModels()
        {
            RestRequest request = new RestRequest{Resource = "api/QuestionApi"};
            var result = cliente.Execute(request);
            RestSharp.Portable.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
            var list = deserial.Deserialize<IEnumerable<QuestionListModel>>(result.Result);
            return list;
        }
    }
}
