using System.Web.Http;

namespace OverflowVictor.Web.Controllers
{
    public class TestController : ApiController
    {
        //GET: api/test
        /*
        public string[] GetQuestions()
        {
            return new []{"test1","test2"};
        }
        */
        public TestModel[] GetQuestions2(int id)
        {
            return new []
            {
                new TestModel {Id=2,Nombre="test1"},
                new TestModel{Id=2,Nombre = "test2"}
            };
        }
         
         
        //POST: api/test
        public string [] PostNewQuestion([FromBody]TestModel model)
        {
            return null;
        }

    }

    public class TestModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}