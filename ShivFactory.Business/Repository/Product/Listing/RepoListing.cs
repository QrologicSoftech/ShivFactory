using Dapper;
using ShivFactory.Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShivFactory.Business.Repository
{
    public class RepoListing
    {
        #region GetAllProductList
        public List<ClsProduct> GetallProductlist(ProductListingPagination model, out int totalRecords)
        {
            totalRecords = 0;
            List<ClsProduct> product = new List<ClsProduct>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            string varinetName = "";
            string varinetValue = "";
            if (!string.IsNullOrEmpty(model.VarientName1))
            {
                varinetName = varinetName + "," + model.VarientName1;
                varinetValue = varinetValue + "," + model.VarientValue1;
            }
            if (!string.IsNullOrEmpty(model.VarientName2))
            {
                varinetName = varinetName + "," + model.VarientName2;
                varinetValue = varinetValue + "," + model.VarientValue2;
            }
            if (!string.IsNullOrEmpty(model.VarientName3))
            {
                varinetName = varinetName + "," + model.VarientName3;
                varinetValue = varinetValue + "," + model.VarientValue3;
            }
            if (!string.IsNullOrEmpty(model.VarientName4))
            {
                varinetName = varinetName + "," + model.VarientName4;
                varinetValue = varinetValue + "," + model.VarientValue4;
            }
            if (!string.IsNullOrEmpty(model.VarientName5))
            {
                varinetName = varinetName + "," + model.VarientName5;
                varinetValue = varinetValue + "," + model.VarientValue5;
            }
            if (!string.IsNullOrEmpty(model.VarientName6))
            {
                varinetName = varinetName + "," + model.VarientName6;
                varinetValue = varinetValue + "," + model.VarientValue6;
            }
            if (!string.IsNullOrEmpty(model.VarientName7))
            {
                varinetName = varinetName + "," + model.VarientName7;
                varinetValue = varinetValue + "," + model.VarientValue7;
            }
            if (!string.IsNullOrEmpty(model.VarientName8))
            {
                varinetName = varinetName + "," + model.VarientName8;
                varinetValue = varinetValue + "," + model.VarientValue8;
            }
            if (!string.IsNullOrEmpty(model.VarientName9))
            {
                varinetName = varinetName + "," + model.VarientName9;
                varinetValue = varinetValue + "," + model.VarientValue9;
            }
            if (!string.IsNullOrEmpty(model.VarientName10))
            {
                varinetName = varinetName + "," + model.VarientName10;
                varinetValue = varinetValue + "," + model.VarientValue10;
            }

            parameters.Add(new SqlParameter("@ACTION", "VarientFilter"));
            parameters.Add(new SqlParameter("@PageIndex", model.PageIndex));
            parameters.Add(new SqlParameter("@PageSize", model.PageSize));
            parameters.Add(new SqlParameter("@CategoryId", model.CategoryId));
            parameters.Add(new SqlParameter("@SubCategoryId", model.SubCategoryId));
            parameters.Add(new SqlParameter("@MiniCategoryId", model.MiniCategoryId));
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            parameters.Add(new SqlParameter("@VarientName", varinetName));
            parameters.Add(new SqlParameter("@VarientValue", varinetValue));

            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, CommandType.StoredProcedure, "GetProductsPageWise", parameters.ToArray());
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                RepoCommon repoCommon = new RepoCommon();
                totalRecords = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRow"].ToString());
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    product.Add(new ClsProduct()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        ProductId = row["ProductId"] != DBNull.Value ? Convert.ToInt32(row["ProductId"]) : 0,
                        ProductVarientId = row["VarientId"] != DBNull.Value ? Convert.ToInt32(row["VarientId"]) : 0,
                        ProductName = row["ProductName"] != DBNull.Value ? row["ProductName"].ToString() : "",

                        SalePrice = row["SalePrice"] != DBNull.Value ? row["SalePrice"].ToString() : "0.00",
                        ListPrice = row["ListPrice"] != DBNull.Value ? row["ListPrice"].ToString() : "0.00",
                        MainImage = repoCommon.checkfile(row["MainImage"].ToString()),
                        SubCategoryName = row["SubCategoryName"] != DBNull.Value ? row["SubCategoryName"].ToString() : "",
                        CategoryName = row["CategoryName"] != DBNull.Value ? row["CategoryName"].ToString() : "",
                        CategoryId = row["CategoryId"] != DBNull.Value ? row["CategoryId"].ToString() : "0",
                        SubCategoryId = row["SubCategoryId"] != DBNull.Value ? row["SubCategoryId"].ToString() : "0",
                        VendorId = row["vendorId"] != DBNull.Value ? Convert.ToInt32(row["vendorId"]) : 0,

                    });
                }

            }
            return product;
        }
        #endregion


        #region GetProductDetail
        public ProductDetail GetProductDetail(int productId, string VarientName, string VarientValue)
        {
            //foreach (DataRow row in ds.Tables[0].Rows)
            //{
            RepoCommon repoCommon = new RepoCommon();
            var productDetail = new ProductDetail();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ProductId", productId));
            param.Add(new SqlParameter("@VarientName", VarientName));
            param.Add(new SqlParameter("@VarientValue", VarientValue));
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, CommandType.StoredProcedure, "GetProductDetail", param.ToArray());
            if (ds.Tables[0].Rows.Count > 0)
            {
                productDetail = new ProductDetail()
                {
                    ProductId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductId"]),
                    ProductVarientId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductVarientId"]),
                    ProductName = Convert.ToString(ds.Tables[0].Rows[0]["ProductName"]),
                    Description = ds.Tables[0].Rows[0]["Description"] != DBNull.Value ? ds.Tables[0].Rows[0]["Description"].ToString() : "",
                    SalePrice = ds.Tables[0].Rows[0]["SalePrice"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["SalePrice"]) : 0,
                    ListPrice = ds.Tables[0].Rows[0]["ListPrice"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["ListPrice"]) : 0,
                    productWarning = ds.Tables[0].Rows[0]["productWarning"] != DBNull.Value ? ds.Tables[0].Rows[0]["productWarning"].ToString() : "",
                    ReturnDays = ds.Tables[0].Rows[0]["ReturnDays"] != DBNull.Value ? Convert.ToInt16(ds.Tables[0].Rows[0]["ReturnDays"]) : 0,
                    MainImage = ds.Tables[0].Rows[0]["MainImage"].ToString().ImagePath(),
                    Image1 = ds.Tables[0].Rows[0]["Image1"].ToString().ImagePath(),
                    Image2 = ds.Tables[0].Rows[0]["Image2"].ToString().ImagePath(),
                    Image3 = ds.Tables[0].Rows[0]["Image3"].ToString().ImagePath(),
                    Image4 = ds.Tables[0].Rows[0]["Image4"].ToString().ImagePath(),
                    Image5 = ds.Tables[0].Rows[0]["Image5"].ToString().ImagePath(),
                    CategoryName = ds.Tables[0].Rows[0]["CategoryName"] != DBNull.Value ? ds.Tables[0].Rows[0]["CategoryName"].ToString() : "",
                    SubCategoryName = ds.Tables[0].Rows[0]["SubCategoryName"] != DBNull.Value ? ds.Tables[0].Rows[0]["SubCategoryName"].ToString() : "",
                    MiniCategoryName = ds.Tables[0].Rows[0]["MiniCategoryName"] != DBNull.Value ? ds.Tables[0].Rows[0]["MiniCategoryName"].ToString() : "",
                    FirmName = ds.Tables[0].Rows[0]["FirmName"] != DBNull.Value ? ds.Tables[0].Rows[0]["FirmName"].ToString() : "",
                    IsReturnable = ds.Tables[0].Rows[0]["IsReturnable"] != DBNull.Value ? Convert.ToBoolean(ds.Tables[0].Rows[0]["IsReturnable"]) : false,
                    paymentModeCash = ds.Tables[0].Rows[0]["paymentModeCash"] != DBNull.Value ? Convert.ToBoolean(ds.Tables[0].Rows[0]["paymentModeCash"]) : false,
                    CategoryId = Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryId"]),
                    SubCategoryId = Convert.ToInt32(ds.Tables[0].Rows[0]["SubCategoryId"]),
                    MiniCategoryId = Convert.ToInt32(ds.Tables[0].Rows[0]["MiniCategoryId"]),
                    vendorId = Convert.ToInt32(ds.Tables[0].Rows[0]["VendorId"]),
                    EstimateDeliveryTime = ds.Tables[0].Rows[0]["EstimateDeliveryTime"] != DBNull.Value ? ds.Tables[0].Rows[0]["EstimateDeliveryTime"].ToString() : "Estimate delivery in 3-4 days",
                };
                if (ds.Tables[1].Rows.Count > 0)
                {
                    productDetail.varientList = ds.Tables[1].AsEnumerable().GroupBy(item => item.Field<string>("columnName"))
          .Select(group => new ProductVariations()
          {
              VarientName = group.Key,
              VarientValue = group.Select(item => item.Field<string>("columnValue")).ToList(),
              ImagePath = group.Select(item => item.Field<string>("MainImage").ImagePath()).FirstOrDefault()
          }).ToList();
                }
            }

            return productDetail;
        }
        #endregion
    }
}


