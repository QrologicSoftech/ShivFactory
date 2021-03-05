using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository.Admin
{
    #region UnApprovedProductResponse
    public class UnApprovedProductResponse
    {
        public int SrNo { get; set; }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandName { get; set; }
        public string AddDate { get; set; }
        public string InactiveReason { get; set; }
    }
    #endregion

    #region ProductBasicInfoResponse
    public class ProductBasicInfoResponse
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandName { get; set; }
        public string AddDate { get; set; }
        public string InactiveReason { get; set; }
    }
    #endregion
}
