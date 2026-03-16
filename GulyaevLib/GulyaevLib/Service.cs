using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GulyaevLib
{

    public class Service
    {
        public void AddPartner(ApplicationContext _context, Partner partner)
        {
            _context.Partners.Add(partner);
            _context.SaveChanges();
        }
        public void DeletePartner(ApplicationContext _context, Partner partner)
        {
            _context.PartnerProducts.RemoveRange(_context.PartnerProducts.Where(p => p.Partner == partner.Id));
            _context.Partners.Remove(partner);
            _context.SaveChanges();
        }
        public void UpdatePartner(ApplicationContext _context, Partner partner)
        {
            _context.Partners.Update(partner);
            _context.SaveChanges();
        }
        public void SaveChanges(ApplicationContext _context)
        {
            _context.SaveChanges();
        }
        public List<PartnerView> GetAllPartnerInfo(ApplicationContext _context)
        {
            return _context.Partners
                .Select(p => new PartnerView
                {
                    Id = p.Id,
                    PartnerAddress = p.PartnerAddress,
                    PartnerName = p.PartnerName,
                    PartnerType = p.PartnerType,
                    PartnerDirector = p.PartnerDirector,
                    PartnerNumber = p.PartnerNumber,
                    PartnerEmail = p.PartnerEmail,
                    PartnerInn = p.PartnerInn,
                    PartnerRating = p.PartnerRating,
                    PartnerTypeName = p.PartnerType + " | " + p.PartnerName,
                    PartnerDiscount = GetDiscount(_context.PartnerProducts.Where(q => q.Partner == p.Id).Sum(q => q.PartnerCount) ?? 0)
                }).ToList();
              
        }

        public List<SalesView> GetAllSalesInfo(ApplicationContext _context, int partner_id)
        {
            return _context.PartnerProducts
                .Where(p => p.Partner == partner_id)
                .Select(p => new SalesView
                {
                    Id = p.Id,
                    Partner_Id = p.Partner,
                    PartnerCount = p.PartnerCount,
                    Product_name = _context.Products.Where(q => q.Id == p.Product).Select(q => q.ProductName).FirstOrDefault(),
                    SaleDate = p.SaleDate
                }).ToList();

        }
        public Partner GetPartnerInfo(ApplicationContext _context, int id)
        {
            return _context.Partners
                            .FirstOrDefault(p => p.Id == id);
            
        }
        public List<string> GetProductInfo(ApplicationContext _context)
        {
            return _context.Products.Select(p => p.ProductName).ToList();
        }

        public static int GetDiscount(int count)
        {
            if (count <= 10000) {
                return 0;
            }
            else if (count <= 30000 && count > 10000)
            {
                return 5;
            }
            else
            {
                return 15;
            }    
        }
        public void AddSale (ApplicationContext _context, PartnerProduct sale)
        {
            _context.PartnerProducts.Add(sale);
            _context.SaveChanges();
        }
        public void UpdateSale(ApplicationContext _context, PartnerProduct sale)
        {
            _context.PartnerProducts.Update(sale);
            _context.SaveChanges();
        }
        public void DeleteSale(ApplicationContext _context, PartnerProduct sale)
        {
            _context.PartnerProducts.Remove(sale);
            _context.SaveChanges();
        }
    }
}
