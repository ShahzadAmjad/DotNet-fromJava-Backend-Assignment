namespace Backend_Assignment.Models
{
    public class ResponseModel
    {
        public bool IsSuccess
        {
            get;
            set;
        }
        public string Messsage
        {
            get;
            set;
        }
        public object Data
        {
            get;
            set;
        }
    }
}
