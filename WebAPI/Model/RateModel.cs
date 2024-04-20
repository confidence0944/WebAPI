namespace WebAPI.Model
{
    public class RateModel
    {
        public RateModel()
        {
            this.RateList = new List<RateData> { };
        }

        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime UpdateTime { get; set; }

        public List<RateData> RateList { get; set; }

        public class RateData
        {
            /// <summary>
            /// 幣別
            /// </summary>
            public string Currency { get; set; }

            /// <summary>
            /// 幣別名稱(中文)
            /// </summary>
            public string CurrencyChineseName { get; set; }

            /// <summary>
            /// 匯率
            /// </summary>
            public double Rate { get; set; }
        }
    }
}
