namespace DStutz.Apps.Controllers.API
{
    //public class APIResponse200<T> : APIResponse200
    //{
    //    public T Data { get; set; }

    //    public APIResponse200(
    //        T data)
    //    {
    //        Data = data;
    //    }
    //}

    public class APIResponse200Many<T> : APIResponse200
    {
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }

        public APIResponse200Many(
            IEnumerable<T> data)
        {
            Count = data.Count();
            Data = data;
        }
    }
}
