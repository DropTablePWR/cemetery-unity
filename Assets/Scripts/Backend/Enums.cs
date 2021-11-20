namespace Backend
{
    public class Enums
    {
        public enum RequestType
        {
            GET = 0,
            POST = 1,
            PUT = 2,
            PATCH = 3,
            DELETE = 4
        }

        public enum ResultType
        {
            Text = 3,
            Object = 4,
            Array = 5
        }
        
        public enum ActionType
        {
            GetGrave = 0,
            GetCemetery = 1,
            
        }
    }
}