namespace ProjectTest.Common
{
    public static class ResUnAuthorized
    {
        public static ResultModel Unauthor()
        {
            var detailUs = new ResultModel()
            {
                Message = "Unauthorized",
                Code = 401,
            };
            return detailUs;
        }
    }
    
}
