namespace WebAPI.Model
{
    public static class ReturnCode
    {
        /// <summary>
        /// 0000 交易成功
        /// </summary>
        public static MobileBankError Success => new() { Code = "0000", Description = "交易成功" };

        /// <summary>
        /// 201 查無資料
        /// </summary>
        public static MobileBankError NoData => new() { Code = "201", Description = "查無資料" };

        /// <summary>
        /// 9999 預設錯誤
        /// </summary>
        public static MobileBankError InternalSystemError => new() { Code = "9999", Description = "系統發生錯誤，請洽系統管理員。" };
    }

    public class MobileBankError
    {
        public string Code { get; set; }

        public string Description { get; set; }
    }
}