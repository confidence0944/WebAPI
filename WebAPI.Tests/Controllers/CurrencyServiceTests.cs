using Microsoft.EntityFrameworkCore;
using Web.Models.Repository;
using WebAPI.Entities;
using WebAPI.Model;
using WebAPI.Service;

namespace WebAPI.Controllers.Tests
{
    [TestClass()]
    public class CurrencyServiceTests
    {
        private ICurrencyService currencyService;

        [TestInitialize]
        public void SetUp()
        {           
            var options = new DbContextOptionsBuilder<PracticeContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var context = new PracticeContext(options);

            List<TbCurrency> mockData = new List<TbCurrency>()
            {
                new TbCurrency() { Currency ="EUR", CurrencyName ="歐元" },
                new TbCurrency() { Currency ="GBP", CurrencyName ="英鎊" },
                new TbCurrency() { Currency ="USD", CurrencyName ="美元" },
            };

            context.TbCurrencies.AddRange(mockData);
            context.SaveChanges();

            currencyService = new CurrencyService(new CurrencyRepository(context));
        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            //Arrange

            //Act
            var act = await currencyService.GetAll();
            
            Assert.IsNotNull(act);
            Assert.AreEqual(3, act.Count());
        }

        [TestMethod()]
        public async Task CreateTest()
        {
            //Arrange
            CurrencyModel mock = new CurrencyModel() { Currency = "JPY", CurrencyName = "日幣" };

            //Act
            await currencyService.Create(mock);
            var datas = await currencyService.GetAll();
            var act = datas.FirstOrDefault(x => x.Currency == mock.Currency);

            Assert.IsNotNull(act);
            Assert.AreEqual(mock.CurrencyName, act.CurrencyName);
        }

        [TestMethod()]
        public async Task UpdateTest()
        {
            //Arrange
            CurrencyModel mock = new CurrencyModel() { Currency = "EUR", CurrencyName = "歐元幣別" };
            var arrangeDatas = await currencyService.GetAll();
            var temp = arrangeDatas.FirstOrDefault(x => x.Currency == mock.Currency);
            temp.CurrencyName = mock.CurrencyName;

            //act
            await currencyService.Update(temp);
            var actDatas = await currencyService.GetAll();
            var act = actDatas.FirstOrDefault(x => x.Currency == mock.Currency);

            Assert.IsNotNull(act);
            Assert.AreEqual(mock.CurrencyName, act.CurrencyName);
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            //Arrange
            CurrencyModel mock = new CurrencyModel() { Currency = "EUR", CurrencyName = "" };

            //Act
            await currencyService.Delete(mock.Currency);
            var actDatas = await currencyService.GetAll();
            var act = actDatas.FirstOrDefault(x => x.Currency == mock.Currency);

            //Assert
            Assert.IsNull(act);
        }
    }
}