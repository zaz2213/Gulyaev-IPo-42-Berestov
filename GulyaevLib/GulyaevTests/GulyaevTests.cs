using Microsoft.EntityFrameworkCore;
using GulyaevLib;

namespace GulyaevTests
{
    [TestClass]
    [DoNotParallelize]
    public sealed class GulyaevServiceTests
    {
        private static ApplicationContext CreateContext()
        {
            return new ApplicationContext();
        }

        [TestMethod]
        public void AddPartner_AddsPartnerToDatabase()
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();
            var service = new Service();
            var partner = new Partner
            {
                PartnerName = "Test Partner",
                PartnerType = "TP",
                PartnerDirector = "Director",
                PartnerEmail = "test@example.com",
                PartnerNumber = "1234567890",
                PartnerAddress = "Test Address",
                PartnerInn = "1234567890",
                PartnerRating = 1
            };

            service.AddPartner(context, partner);

            var exists = context.Partners.Any(p => p.Id == partner.Id);
            Assert.IsTrue(exists);
            transaction.Rollback();
        }

        [TestMethod]
        public void DeletePartner_RemovesPartnerFromDatabase()
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();
            var service = new Service();
            var partner = new Partner
            {
                PartnerName = "Partner To Delete",
                PartnerType = "PD"
            };
            context.Partners.Add(partner);
            context.SaveChanges();

            service.DeletePartner(context, partner);

            var exists = context.Partners.Any(p => p.Id == partner.Id);
            Assert.IsFalse(exists);
            transaction.Rollback();
        }

        [TestMethod]
        public void UpdatePartner_UpdatesPartnerInDatabase()
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();
            var service = new Service();
            var partner = new Partner
            {
                PartnerName = "Old Name",
                PartnerType = "UP"
            };
            context.Partners.Add(partner);
            context.SaveChanges();

            partner.PartnerName = "New Name";
            service.UpdatePartner(context, partner);

            var updatedName = context.Partners.Where(p => p.Id == partner.Id).Select(p => p.PartnerName).FirstOrDefault();
            Assert.AreEqual("New Name", updatedName);
            transaction.Rollback();
        }

        [TestMethod]
        public void SaveChanges_PersistsPendingChanges()
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();
            var service = new Service();
            var partner = new Partner
            {
                PartnerName = "SaveChanges Partner",
                PartnerType = "SC"
            };
            context.Partners.Add(partner);

            service.SaveChanges(context);

            var exists = context.Partners.Any(p => p.Id == partner.Id);
            Assert.IsTrue(exists);
            transaction.Rollback();
        }

        [TestMethod]
        public void GetAllPartnerInfo_ReturnsInsertedPartner()
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();
            var service = new Service();
            var partner = new Partner
            {
                PartnerName = "View Partner",
                PartnerType = "VP"
            };
            context.Partners.Add(partner);
            context.SaveChanges();

            var result = service.GetAllPartnerInfo(context);

            Assert.IsTrue(result.Any(p => p.Id == partner.Id));
            transaction.Rollback();
        }

        [TestMethod]
        public void GetAllSalesInfo_ReturnsSalesForPartner()
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();
            var service = new Service();
            var partner = new Partner
            {
                PartnerName = "Sales Partner",
                PartnerType = "SP"
            };
            context.Partners.Add(partner);
            context.SaveChanges();

            var product = new Product
            {
                ProductName = "Sales Product"
            };
            context.Products.Add(product);
            context.SaveChanges();

            var sale = new PartnerProduct
            {
                Partner = partner.Id,
                Product = product.Id,
                PartnerCount = 10,
                SaleDate = DateOnly.FromDateTime(DateTime.Today)
            };
            context.PartnerProducts.Add(sale);
            context.SaveChanges();

            var result = service.GetAllSalesInfo(context, partner.Id);

            Assert.IsTrue(result.Any(s => s.Id == sale.Id));
            transaction.Rollback();
        }

        [TestMethod]
        public void GetPartnerInfo_ReturnsCorrectPartner()
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();
            var service = new Service();
            var partner = new Partner
            {
                PartnerName = "Single Partner",
                PartnerType = "GP"
            };
            context.Partners.Add(partner);
            context.SaveChanges();

            var result = service.GetPartnerInfo(context, partner.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(partner.Id, result!.Id);
            transaction.Rollback();
        }

        [TestMethod]
        public void GetProductInfo_ReturnsProductNames()
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();
            var service = new Service();
            var product = new Product
            {
                ProductName = "Info Product"
            };
            context.Products.Add(product);
            context.SaveChanges();

            var result = service.GetProductInfo(context);

            Assert.IsTrue(result.Contains(product.ProductName!));
            transaction.Rollback();
        }

        [TestMethod]
        public void GetDiscount_ReturnsExpectedValues()
        {
            var discountLow = Service.GetDiscount(10000);
            var discountMid = Service.GetDiscount(20000);
            var discountHigh = Service.GetDiscount(40000);

            Assert.AreEqual(0, discountLow);
            Assert.AreEqual(5, discountMid);
            Assert.AreEqual(15, discountHigh);
        }

        [TestMethod]
        public void AddSale_AddsPartnerProduct()
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();
            var service = new Service();
            var partner = new Partner
            {
                PartnerName = "Sale Partner",
                PartnerType = "AS"
            };
            context.Partners.Add(partner);
            context.SaveChanges();

            var product = new Product
            {
                ProductName = "Sale Product"
            };
            context.Products.Add(product);
            context.SaveChanges();

            var sale = new PartnerProduct
            {
                Partner = partner.Id,
                Product = product.Id,
                PartnerCount = 5,
                SaleDate = DateOnly.FromDateTime(DateTime.Today)
            };

            service.AddSale(context, sale);

            var exists = context.PartnerProducts.Any(s => s.Id == sale.Id);
            Assert.IsTrue(exists);
            transaction.Rollback();
        }

        [TestMethod]
        public void UpdateSale_UpdatesPartnerProduct()
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();
            var service = new Service();
            var partner = new Partner
            {
                PartnerName = "Update Sale Partner",
                PartnerType = "US"
            };
            context.Partners.Add(partner);
            context.SaveChanges();

            var product = new Product
            {
                ProductName = "Update Sale Product"
            };
            context.Products.Add(product);
            context.SaveChanges();

            var sale = new PartnerProduct
            {
                Partner = partner.Id,
                Product = product.Id,
                PartnerCount = 5,
                SaleDate = DateOnly.FromDateTime(DateTime.Today)
            };
            context.PartnerProducts.Add(sale);
            context.SaveChanges();

            sale.PartnerCount = 20;
            service.UpdateSale(context, sale);

            var updatedCount = context.PartnerProducts.Where(s => s.Id == sale.Id).Select(s => s.PartnerCount).FirstOrDefault();
            Assert.AreEqual(20, updatedCount);
            transaction.Rollback();
        }

        [TestMethod]
        public void DeleteSale_RemovesPartnerProduct()
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();
            var service = new Service();
            var partner = new Partner
            {
                PartnerName = "Delete Sale Partner",
                PartnerType = "DS"
            };
            context.Partners.Add(partner);
            context.SaveChanges();

            var product = new Product
            {
                ProductName = "Delete Sale Product"
            };
            context.Products.Add(product);
            context.SaveChanges();

            var sale = new PartnerProduct
            {
                Partner = partner.Id,
                Product = product.Id,
                PartnerCount = 5,
                SaleDate = DateOnly.FromDateTime(DateTime.Today)
            };
            context.PartnerProducts.Add(sale);
            context.SaveChanges();

            service.DeleteSale(context, sale);

            var exists = context.PartnerProducts.Any(s => s.Id == sale.Id);
            Assert.IsFalse(exists);
            transaction.Rollback();
        }
    }
}
