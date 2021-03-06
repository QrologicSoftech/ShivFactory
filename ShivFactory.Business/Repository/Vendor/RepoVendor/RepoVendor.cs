﻿using DataLibrary.DL;
using ShivFactory.Business.Model;
using ShivFactory.Business.Models;
using ShivFactory.Business.Models.Other;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShivFactory.Business.Repository
{
    public class RepoVendor
    {
        #region Parameters
        ShivFactoryEntities db = new ShivFactoryEntities();
        #endregion

        #region Get VendorId By UserId
        public int GetVendorIdByUserId(string userId)
        {
            var vendor = db.Vendors.Where(a => a.UserId == userId).AsNoTracking().FirstOrDefault();
            if (vendor != null)
            {
                return vendor.VendorId;
            }
            return 0;
        }

        #endregion

        #region Add Or Update VendorDetails
        public bool AddOrUpdateVendor(Vendor model)
        {
            var vendor = db.Vendors.Where(a => a.UserId == model.UserId).FirstOrDefault();
            if (vendor != null)
            {
                vendor.FirmName = model.FirmName;
                vendor.GSTIN = model.GSTIN;
                vendor.PanNo = model.PanNo;
                vendor.City = model.City;
                vendor.State = model.State;
                vendor.AddressProofImg = model.AddressProofImg;
                vendor.PIN = model.PIN;
                vendor.FullAddress = model.FullAddress;
                vendor.LastUpdate = DateTime.Now; 
             
            }
            else
            {
                db.Vendors.Add(model);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Add Or Update VendorBankDetails
        public bool AddVendorBankDetails(DataLibrary.DL.VendorBankDetail model)
        {
            var vendorbankdetails = db.VendorBankDetails.Where(a => a.UserID == model.UserID).FirstOrDefault();
            if (vendorbankdetails != null)
            {
                vendorbankdetails.AccountHolderName = model.AccountHolderName;
                vendorbankdetails.AccountNumber = model.AccountNumber;
                vendorbankdetails.IFSCCode = model.IFSCCode;
                vendorbankdetails.BankName = model.BankName;
                vendorbankdetails.LastUpdate = DateTime.Now;
               vendorbankdetails.BankProofImg = model.BankProofImg; 
            }
            else
            {
                db.VendorBankDetails.Add(model);
            }
            return db.SaveChanges() > 0;
        }
        #endregion

        #region Get VendorDetails By UserId
        public Vendor GetVendorDetailsByUserId(string userId)
        {
            var vendor = db.Vendors.Where(a => a.UserId == userId).FirstOrDefault();
            return vendor;


        }

        #endregion

        #region Get All Vendors
        public List<VendorResponse> GetAllVendors(PaginationRequest model, out int totalRecords)
        {
            var vendors = new List<VendorResponse>();
            totalRecords = 0;
            RepoCommon common = new RepoCommon();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Action", "GetAllVendors"));
            parameters.Add(new SqlParameter("@SearchText", model.searchText));
            parameters.Add(new SqlParameter("@Skip", model.Skip));
            parameters.Add(new SqlParameter("@Take", model.PageSize));
            parameters.Add(new SqlParameter("@OrderColumn", model.SortColumn));
            parameters.Add(new SqlParameter("@OrderDir", model.SortDirection));
            parameters.Add(new SqlParameter("@Role", UserRoles.Vendor));

            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "ManageUsers", parameters.ToArray());
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                totalRecords = ds.Tables[0].Rows[0]["TotalRow"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRow"].ToString()) : 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    vendors.Add(new VendorResponse()
                    {
                        SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                        Id = row["Id"] != DBNull.Value ? row["Id"].ToString() : "",
                        ImagePath = row["ImagePath"] != DBNull.Value ? common.checkfile(row["ImagePath"].ToString()) : common.checkfile(""),
                        FirstName = row["FirstName"] != DBNull.Value ? row["FirstName"].ToString() : "",
                        LastName = row["LastName"] != DBNull.Value ? row["LastName"].ToString() : "",
                        Gender = row["Gender"] != DBNull.Value ? row["Gender"].ToString() : "",
                        FirmName = row["FirmName"] != DBNull.Value ? row["FirmName"].ToString() : "",
                        UserName = row["UserName"] != DBNull.Value ? row["UserName"].ToString() : "",
                        Address = row["Address"] != DBNull.Value ? row["Address"].ToString() : "",
                        Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : "",
                        AddDate = row["AddDate"] != DBNull.Value ? Convert.ToDateTime(row["AddDate"]).ToString("dd/MM/yyyy") : ""
                    });
                }
            }

            return vendors;
        }

        #endregion

        #region  Add  Vendor Shipping Area  Helpers 
        public bool AddorUpdateShippingArea(VendorShippingArea area)
        {
            try
            {
                var shiparea = db.VendorShippingAreas.Where(a => a.vendorId == area.vendorId && a.pincode == area.pincode).AsNoTracking().FirstOrDefault();
                if (shiparea != null)
                {
                    shiparea.vendorId = area.vendorId;
                    shiparea.pincode = area.pincode;
                }
                else
                {
                    db.VendorShippingAreas.Add(area);
                }
            }catch(Exception e) { }
            return db.SaveChanges() > 0;
        }

       
        public List<PincodeResponce> GetAllPincodeByVendorId(int vendorId, PaginationRequest model, out int totalRecords)
        {
            var pincode = new List<PincodeResponce>();
            totalRecords = 0;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Action", "GetAllPincode"));
                parameters.Add(new SqlParameter("@SearchText", model.searchText));
                parameters.Add(new SqlParameter("@Skip", model.Skip));
                parameters.Add(new SqlParameter("@Take", model.PageSize));
                parameters.Add(new SqlParameter("@OrderColumn", model.SortColumn));
                parameters.Add(new SqlParameter("@OrderDir", model.SortDirection));
                parameters.Add(new SqlParameter("@VendorId", vendorId));

                DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, "ManagePincode", parameters.ToArray());
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    totalRecords = ds.Tables[0].Rows[0]["TotalRow"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRow"].ToString()) : 0;

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        pincode.Add(new PincodeResponce()
                        {
                            SrNo = row["SrNo"] != DBNull.Value ? Convert.ToInt32(row["SrNo"]) : 0,
                            Id = row["ID"] != DBNull.Value ? Convert.ToInt32(row["ID"]) : 0,
                            pincode = row["pincode"] != DBNull.Value ? row["pincode"].ToString() : "",
                        });
                    }
                }
            }
            catch (Exception e)
            {
            }
            return pincode;
        }


     
        public bool DeletePincodeByPincodeId(int pincodeId)
        {
            try
            {
                var pincode = db.VendorShippingAreas.Where(x => x.ID == pincodeId).FirstOrDefault();
            if (pincode != null)
            {
                db.VendorShippingAreas.Remove(pincode);
            }
            }
            catch (Exception e)
            {

            }
            return db.SaveChanges() > 0;
        }


        public VendorShippingArea  GetPincodeByID(int id)
        {
            
                var shiparea = db.VendorShippingAreas.Where(a => a.ID == id).AsNoTracking().FirstOrDefault();
                return shiparea;
        }
        #endregion

        #region check pincode Availibity for Item 
        public bool CheckPincodeAvailibityForProduct(string pincode, int vendorId)
        {
            var isAvailable = db.VendorShippingAreas.Where(a => a.pincode == pincode && a.vendorId==vendorId).AsNoTracking().FirstOrDefault();
            if (isAvailable != null)
            { return true; } else { return false;  }
        }
        #endregion
    }
}
