﻿using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.BussData.Controllers
{
    public class WaitAreaController : frmMainController
    {
        private string sWhere
        {
            set { TempData["Where_" + RouteData.Values["controller"].ToString()] = value; }
            get
            {
                var temp = TempData.Peek("Where_" + RouteData.Values["controller"].ToString());
                if (temp == null)
                {
                    return "";
                }
                return temp.ToString();
            }
        }

        //
        // GET: /System/Role/
        public override ActionResult Index()
        {
            return RedirectToAction("List");
        }


        //[(Message = "信息列表(List)")]
        public override ActionResult List()
        {
            try
            {
                PageIndex = int.Parse(Request.Form["pageIndex"] == null ? "1" : Request.Form["pageIndex"].ToString());
                Condition = " BranchNo='" + PublicHelper.Get_BranchNo() + "'";
                if (!string.IsNullOrEmpty(sWhere))
                {
                    Condition += " And " + sWhere;
                }

                WaitAreaBLL infoBLL = new WaitAreaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                WaitAreaCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
                int totalCount = infoBLL.GetCountByCondition(Condition);

                PagerHelper pager = new PagerHelper(PageIndex, PageSize, totalCount);

                Dictionary<string, object> stackHolder = new Dictionary<string, object>();
                stackHolder.Add("infoList", infoColl);
                stackHolder.Add("pager", pager);
                ViewBag.StackHolder = stackHolder;
            }
            catch (Exception ex)
            { }
            return View();
        }

        //
        // GET: /System/Role/Search
        //[(Message = "信息查询(Search)")]
        public override ActionResult Search()
        {
            sWhere = "1=1 ";

            //if (!string.IsNullOrEmpty(sTrueName))
            //{
            //    sWhere += " And (TrueName like '%" + sTrueName + "%'  OR LoginId like '%" + sTrueName + "%' )";
            //}

            return RedirectToAction("List");
        }

        //
        // GET: /System/Role/Add
        //[(Message = "信息添加(Add)")]
        public override ActionResult Add()
        {
            try
            {
                WaitArea info = new WaitArea();
                info.sWAreaNo = CommonHelper.Get_New12ByteGuid();
                info.iAreaIndex = 1;
                info.iAreaScale = 100;

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }

        ////
        //// GET: /System/Role/Edit/5
        //[(Message = "信息编辑(Edit)")]
        public override ActionResult Edit(string id)
        {
            try
            {
                WaitAreaBLL infoBLL = new WaitAreaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                WaitArea info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost]
        //[(Message = "信息保存(Save)")]
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string sNo = Request.Form["WAreaNo"].ToString();
                string sWAreaName = Request.Form["WAreaName"].ToString();
                string iAreaIndex = Request.Form["AreaIndex"].ToString();
                string iAreaScale = Request.Form["AreaScale"].ToString();
                string sComments = Request.Form["Comments"].ToString();

                WaitAreaBLL infoBLL = new WaitAreaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                WaitArea info = infoBLL.GetRecordByNo(sNo);

                //新增操作
                if (info == null)
                {
                    info = new WaitArea();
                    info.sWAreaNo = sNo;
                    info.sWAreaName = sWAreaName;
                    info.iAreaIndex = int.Parse(iAreaIndex);
                    info.iAreaScale = int.Parse(iAreaScale);
                    info.sBranchNo = PublicHelper.Get_BranchNo();
                    info.sComments = sComments; 

                    info.sAddOptor = sSuNo;
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";

                    if (infoBLL.AddNewRecord(info))
                    {
                        json.Message = "保存成功";
                        json.Status = "Success";
                    }
                }
                //更新操作
                else
                {
                    info.sWAreaName = sWAreaName;
                    info.iAreaIndex = int.Parse(iAreaIndex);
                    info.iAreaScale = int.Parse(iAreaScale); 
                    info.sComments = sComments;

                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;

                    if (infoBLL.UpdateRecord(info))
                    {
                        json.Message = "保存成功";
                        json.Status = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                json.Message = "信息发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }


        // GET: /System/Role/Delete/5
        //[(Message = "信息删除(Delete)")]
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                WaitAreaBLL infoBLL = new WaitAreaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                infoBLL.SoftDeleteRecord(sNos);
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }

        // GET: /PubsData/Content/Detail/5
        //[(Message = "信息详细(Detail)")]
        public override ActionResult Detail(string id)
        {
            try
            {
                WaitAreaBLL infoBLL = new WaitAreaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                WaitArea info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}