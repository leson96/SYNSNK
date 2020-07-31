using LinqToExcel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace APPSYNCSNK
{
    class syncDATA
    {

        private snkdbEntities db = new snkdbEntities();
        private Expert_SNKEntities db2 = new Expert_SNKEntities();
        private snk_interfaceEntities db3 = new snk_interfaceEntities();

        public bool insert_SNK_Material2()
        {



            try
            {
                // var count_item = db3.interface_info

                //.Count(x => x.inf_nm == "d_material_info");
                // if (count_item == 0)
                // {
                //     interface_info interface_info = new interface_info();
                //     interface_info.inf_nm = "d_material_info";
                //     interface_info.inf_last_time = (DateTime.Now).ToString("yyyyMMdd");
                //     interface_info.use_yn = "N";
                //     interface_info.re_mark = "Get data from ERP";
                //     //interface_info.reg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();
                //     //interface_info.chg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();

                //     interface_info.reg_dt = DateTime.Now;
                //     interface_info.chg_dt = DateTime.Now;
                //     db3.interface_info.Add(interface_info);
                //     db3.SaveChanges();
                // }
                // else
                // {
                //     interface_info interface_info = db3.interface_info.FirstOrDefault(x => x.inf_nm == "d_material_info");
                //     interface_info.inf_last_time = (DateTime.Now).ToString("yyyyMMdd");
                //     interface_info.re_mark = "Get data from ERP";
                //     interface_info.use_yn = "N";
                //     //interface_info.chg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();
                //     interface_info.chg_dt = DateTime.Now;
                //     db3.SaveChanges();
                // }


                db3.Database.ExecuteSqlCommand("truncate table d_material_info_interface");
                StringBuilder varname1 = new StringBuilder();
                varname1.Append("SELECT b.ICProductID AS mtid, \n");
                varname1.Append("       b.ICProductNo AS mt_no, \n");
                varname1.Append("       b.ICProductName AS mt_nm, \n");
                varname1.Append("       c.ICProductTypeName AS mt_type_nm, c.ICProductTypeNo AS mt_type , \n");
                varname1.Append("       a.MaterialQty AS total, \n");
                varname1.Append("       e.ICStockName AS lct_nm, \n");
                varname1.Append("       e.AAStatus AS lct_sts_cd, \n");
                varname1.Append("       ROUND((b.ICProductWidth/1000)*b.ICProductLength, 2) AS gr_qty, \n");
                varname1.Append("       b.AAStatus AS mt_sts_cd, \n");
                varname1.Append("       g.ICLotNo AS s_lot_no, \n");
                varname1.Append("       g.ICLotName AS s_lot_nm, \n");
                varname1.Append("       k.APSupplierName AS sp_nm, \n");
                varname1.Append("       k.APSupplierNo AS sp_cd, \n");
                varname1.Append("       m.ICQCItemSerialNo AS item_vcd, \n");
                varname1.Append("       n.ICQCName AS item_nm, \n");
                varname1.Append("       m.ICQCItemVolume AS qc_range_cd_nm, \n");
                varname1.Append("       b.AACreatedDate AS reg_dt, \n");
                varname1.Append("       b.AACreatedUser AS reg_id, \n");
                varname1.Append("       b.AAUpdatedDate AS chg_dt, \n");
                varname1.Append("       b.AAUpdatedUser AS chg_id, \n");
                varname1.Append("       b.ICProductHeight AS area_all, \n");
                varname1.Append("       b.ICProductSupplierPrice AS new_price, \n");
                varname1.Append("       zx.GECurrencyName as currency, \n");
                varname1.Append("       b.ICProductWidth AS new_with, \n");
                varname1.Append("       b.ICProductDesc AS re_mark, \n");
                varname1.Append("       q.ICStockUOMNo AS unit_cd, \n");
                varname1.Append("       q.ICStockUOMName AS unit_nm, \n");
                varname1.Append("       b.ICProductPicture AS photo_file, \n");
                varname1.Append("       b.ICProductLength AS new_spec,b.ICProductThickness mt_thick, \n");
                varname1.Append("       unit.ICUOMID as bundle_cd,unit.ICUOMNo as bundle_unit \n");
                varname1.Append("FROM ICProducts AS b \n");
                varname1.Append("LEFT JOIN ViewMaterialStatistics AS a ON a.FK_ICProductID=b.ICProductID \n");
                varname1.Append("LEFT JOIN ICProductTypes AS c ON c.ICProductTypeID =b.FK_ICProductTypeID \n");
                varname1.Append("LEFT JOIN ICStocks AS e ON e.ICStockID =a.FK_ICStockID \n");
                varname1.Append("LEFT JOIN ICProductLots AS f ON f.FK_ICProductID=b.ICProductID \n");
                varname1.Append("LEFT JOIN ICLots AS g ON b.FK_ICLotID=g.ICLotID \n");
                varname1.Append("LEFT JOIN APSuppliers AS k ON b.FK_APSupplierID =k.APSupplierID \n");
                varname1.Append("LEFT JOIN ICProductPrices AS l ON l.ICProductID=b.ICProductID \n");
                varname1.Append("LEFT JOIN ICQCItems AS m ON m.FK_ICProductID=b.ICProductID \n");
                varname1.Append("LEFT JOIN ICQCs AS n ON m.FK_ICQCID=n.ICQCID \n");
                varname1.Append("LEFT JOIN ICStockUOMs AS q ON q.ICStockUOMID=b.FK_ICStkUOMID");
                varname1.Append(" LEFT JOIN GECurrencys as zx on zx.GECurrencyID=b.FK_GECurrencyID");
                varname1.Append(" LEFT JOIN ICUOMS as unit on unit.ICUOMID=b.FK_ICStkUOMID");
                varname1.Append("  WHERE b.ICProductNo IS NOT NULL and b.ICProductNo!='' and b.AAStatus = 'Alive' AND LEN(b.ICProductNo)!=1 ");

                var data = new DataTable();
                using (var cmd = db2.Database.Connection.CreateCommand())
                {
                    db2.Database.Connection.Open();
                    cmd.CommandText = varname1.ToString();
                    using (var reader = cmd.ExecuteReader())
                    {
                        data.Load(reader);
                    }
                    db2.Database.Connection.Close();
                }
                var lstRows = new List<Dictionary<string, object>>();
                Dictionary<string, object> dictRow = null;
                d_material_info_interface d_material_info_interface = new d_material_info_interface();
                foreach (DataRow row in data.Rows)
                {
                    dictRow = new Dictionary<string, object>();
                    foreach (DataColumn column in data.Columns)
                    {
                        dictRow.Add(column.ColumnName, row[column]);
                    }
     

                    var reg_dt = TryParseDateTime(dictRow["reg_dt"].ToString());
                    var chg_dt = TryParseDateTime(dictRow["chg_dt"].ToString());

  

                    var mt_type = Convert.ToString(dictRow["mt_type"]);
                    var mt_no = Convert.ToString(dictRow["mt_no"]);
                    var mt_nm = @"" + Convert.ToString(dictRow["mt_nm"]).Replace("\"", "") + "";
                    var gr_qty = Convert.ToString(dictRow["gr_qty"]);
                    var sp_cd = Convert.ToString(dictRow["sp_cd"]);
                    var sp_nm = Convert.ToString(dictRow["sp_nm"]);
                    var s_lot_no = Convert.ToString(dictRow["s_lot_no"]);
                    var item_vcd = Convert.ToString(dictRow["item_vcd"]);
                    var width = Convert.ToString(dictRow["new_with"]);
                    var spec = Convert.ToString(dictRow["new_spec"]);
                    var area = Convert.ToString(dictRow["area_all"]);
                    var price = Convert.ToString(dictRow["new_price"]);
                    var price_unit = Convert.ToString(dictRow["currency"]);
                    //var spec = Convert.ToString(dictRow["new_spec"]);
                    var photo_file = Convert.ToString(dictRow["photo_file"]);
                    var re_mark = Convert.ToString(dictRow["re_mark"]);
                    var unit_cd = Convert.ToString(dictRow["bundle_unit"]);


                    var thick = Convert.ToString(dictRow["mt_thick"]);
                    //var reg_dt2 = reg_dt;
                    //var chg_dt2 = chg_dt;
                    var reg_id = Convert.ToString(dictRow["reg_id"]);
                    var chg_id = Convert.ToString(dictRow["chg_id"]);
                    //db3.d_material_info_interface.Add(d_material_info_interface);
                    //db3.SaveChanges();


                    var sql = "CALL sp_d_material_info_interface(   ";

                    sql += "'" + mt_type + "' ";
                    sql += ",'" + mt_no + "',";
                    sql += "\"" + mt_nm + "\"";
                    sql += ",'" + gr_qty + "' ";
                    sql += ",'" + sp_cd + "' ";
                    sql += ",'" + s_lot_no + "' ";
                    sql += ",'" + item_vcd + "' ";
                    sql += ",'" + width + "' ";
                    sql += ",'" + spec + "' ";
                    sql += ",'" + area + "' ";
                    sql += ",'" + price + "' ";

                    sql += ",'" + price_unit + "' ";
                    sql += ",'" + photo_file + "' ";


                    sql += ",'" + re_mark + "' ";
                    sql += ",'" + unit_cd + "' ";




                    sql += ",'" + thick + "' ";
                    sql += ",'" + reg_dt.ToString("yyyyMMddHHmmss") + "' ";

                    sql += ",'" + chg_dt.ToString("yyyyMMddHHmmss") + "' ";
                    sql += ",'" + reg_id + "' ";

                    sql += ",'" + chg_id + "')";
                    db3.Database.ExecuteSqlCommand(sql);
                }

                var count = db.d_material_info_interface_insert_view.Count();



                //var mt_nm = @"" + Convert.ToString(dictRow["mt_nm"]).Replace("\"", "") + "";
                //sql += "\"" + mt_nm + "\"";

                db.Database.ExecuteSqlCommand("call sp_d_material_info() ");
                //();

                //var kq = result_Material2();

                var count2 = db.d_material_info_interface_insert_view.Count();




                sync_log sync_log = new sync_log();
                sync_log.table_nm = "Material";


                sync_log.last_yn = (count > count2) ? "N" : "Y";

                sync_log.use_yn = "N";
                sync_log.re_mark = count.ToString();
                sync_log.table_last_time = count2.ToString();


                //sync_log.reg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();
                //sync_log.chg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();

                sync_log.reg_dt = DateTime.Now;
                sync_log.chg_dt = DateTime.Now;
                db.sync_log.Add(sync_log);
                db.SaveChanges();



                db.Database.Connection.Close();

                db2.Database.Connection.Close();
                db3.Database.Connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;


            }
        }

        public void get_from_ERP_BOM4()
        {


            try
            {
                //var db = new snkdbEntities();
                //var db2 = new Expert_SNKEntities();
                //var db3 = new snk_interfaceEntities();

                var data2 = string.Empty;
                var list_fail = new ArrayList();
                var list_done = new ArrayList();
                var test = Environment.CurrentDirectory;

                var fullPath = System.Reflection.Assembly.GetAssembly(typeof(syncDATA)).Location;





                //Server.MapPath("~/EXCEL/BOM");
                //get the folder that's in
                string theDirectory = Path.GetDirectoryName(fullPath);
                var basepath = theDirectory + "\\EXCEL\\BOM\\";


                var filelist_hmi = Directory.GetFiles(basepath, "*.xlsx");

                string date_today = (DateTime.Now).ToString("yyyy-MM-dd");
                //db3.Database.ExecuteSqlCommand(" truncate table d_bom_info_interface ");
                //db3.Database.ExecuteSqlCommand(" truncate table d_bom_info_interface ");

                foreach (var item in filelist_hmi)
                {
                    var fileName2 = Path.GetFileName(item).ToString();


                    var filename = item.Split(new char[] { '\\' });

                    var filename3 = filename.Last().ToString();
                    var str_filename = filename[filename.Length - 1].Split(new char[] { '_' });
                    db3.Database.ExecuteSqlCommand(" truncate table d_bom_info_interface ");

                    if (str_filename[0] == "MasterBOM")
                    {



                        var excelData = new ExcelQueryFactory(item);



                        //var data = from x in excelData.Worksheet<BOM_TABLE>()
                        //           select x;
                        var Data = excelData.Worksheet("Product").ToList();

                        foreach (var item3 in Data)
                        {



                            string BOM = @item3[0].ToString();

                            string ICProductCode = @item3[1].ToString();
                            string ICProductName = @item3[2].ToString();


                            if (BOM.Length != 7)
                            {
                                //list_fail.Add(d.bom_no);
                                string bom_fail = "";

                                //bom_fail = " " + BOM + " FILE: " + filename3 + " ";
                                bom_fail = "BOM: " + BOM + " Incorrect=>" + " FILE: " + filename3 + " ";
                                list_fail.Add(bom_fail).ToString().Distinct();
                                //list_fail.AddRange(filename);
                                continue;
                            }



                            //string ICProductName = string.Empty;
                            if ((String.IsNullOrWhiteSpace(ICProductName)))
                            {
                                ICProductName = "";

                            }
                            else
                            {

                                ICProductName = @"" + ICProductName.Replace("\"", "") + "";
                            }




                            var sql = "CALL sp_d_bom_info_interface('" + BOM + "', '" + ICProductCode + "',";
                            sql += " \"" + ICProductName + "\" " + " );";


                            //sql += " );";


                            //bom2 = bom;


                            //"\"" + d.BOM + "\", "\"" + d.ParentLevel + "\"", " + d.ChildLevel + ","+ d.ICProductName +", " + h2 + ", " + d.UOM + ", " + lot_pct + ", " + d.PartCode + ", " + string.Concat(d.PartCode, "-", d.ParentLevel) + ") ";
                            db3.Database.ExecuteSqlCommand(sql);




                            //d_bom_info_interface.bom_no = BOM;
                            //d_bom_info_interface.style_no = ICProductCode;
                            //d_bom_info_interface.style_nm = ICProductName;
                            //d_bom_info_interface.use_yn = "Y";
                            //d_bom_info_interface.del_yn = "N";
                            //d_bom_info_interface.reg_dt = reg_dt;
                            //d_bom_info_interface.chg_dt = chg_dt;

                            //db3.d_bom_info_interface.Add(d_bom_info_interface);
                            //db3.SaveChanges();
                            list_done.Add(BOM).ToString().Distinct();
                        }

                    }
                }
                db.Database.ExecuteSqlCommand(" CALL sp_d_bom_info() ");
                db.Database.Connection.Close();

                db2.Database.Connection.Close();
                db3.Database.Connection.Close();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool insert_SNK_BOM2()
        {
            //var db = new snkdbEntities();
            //var db2 = new Expert_SNKEntities();
            //var db3 = new snk_interfaceEntities();
            try
            {
                db.Database.ExecuteSqlCommand(" CALL sp_d_bom_info() ");
                db.Database.Connection.Close();

                db2.Database.Connection.Close();
                db3.Database.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void get_from_ERP_BOM_DT4()
        {

            //var db = new snkdbEntities();
            //var db2 = new Expert_SNKEntities();
            //var db3 = new snk_interfaceEntities();

            var bom2 = string.Empty;
            var list_fail = new ArrayList();
            var list_done = new ArrayList();
            var fullPath = System.Reflection.Assembly.GetAssembly(typeof(syncDATA)).Location;





            //Server.MapPath("~/EXCEL/BOM");
            //get the folder that's in
            string theDirectory = Path.GetDirectoryName(fullPath);
            var basepath = theDirectory + "\\EXCEL\\BOMDT\\";
            try
            {



                var filelist_hmi = Directory.GetFiles(basepath, "*.xlsx");
                string date_today = (DateTime.Now).ToString("yyyy-MM-dd");
                db3.Database.ExecuteSqlCommand(" truncate table d_bom_detail_interface_copy ");

                foreach (var item in filelist_hmi)
                {



                    var fileName2 = Path.GetFileName(item).ToString();





                    var filename = item.Split(new char[] { '\\' });
                    var filename3 = filename.Last().ToString();

                    var str_filename = filename[filename.Length - 1].Split(new char[] { '_' }); ;

                    if (str_filename[0] == "MasterBOM")
                    {


                        var excelData2 = new ExcelQueryFactory(item);



                        var Data = excelData2.Worksheet("Material").ToList();



                        foreach (var item3 in Data)
                        {



                            string BOM = @item3[0].ToString();

                            string ParentLevel = @item3[1].ToString();
                            string ChildLevel = @item3[2].ToString();
                            string ICProductName = @item3[3].ToString();
                            string UOM = @item3[4].ToString();

                            string BOMQty = @item3[5].ToString();
                            string Loss = @item3[6].ToString();
                            string PartCode = @item3[7].ToString();
                            string PartName = @item3[8].ToString();
                            string key_id = @item3[8].ToString();
                            if (BOM.Length != 7)
                            {
                                //list_fail.Add(d.bom_no);
                                string bom_fail = "";

                                bom_fail = "BOM: " + BOM + " Incorrect=>" + " FILE: " + fileName2 + " ";

                                list_fail.Add(bom_fail).ToString().Distinct();
                                //list_fail.AddRange(filename);
                                continue;
                            }






                            //decimal h2 = Decimal.Parse(BOMQty, System.Globalization.NumberStyles.Any);

                            decimal csp;
                            if (!(Decimal.TryParse(BOMQty, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out csp)))
                            {
                                csp = 0;
                                string bom_fail = "";

                                bom_fail = "BOM: " + BOM + "=>" + BOMQty + " BOMQty Not Number=>" + " FILE: " + fileName2 + "\n";

                                list_fail.Add(bom_fail);
                                //list_fail.AddRange(filename);
                                continue;


                            }
                            //listdata.csp = h2;



                            //listdata.feed_unit = d.UOM;

                            Decimal lot_pct;


                            if (!(Decimal.TryParse(Loss, out lot_pct)))
                            {

                                lot_pct = 0;

                                string bom_fail = "";

                                //bom_fail = "BOM: " + BOM + ": Loss Not Number " + Loss + " " + " FILE: " + fileName2 + "\n";


                                bom_fail = "BOM: " + BOM + "=>" + Loss + " %Loss Not Number=>" + " FILE: " + fileName2 + "\n";

                                list_fail.Add(bom_fail);
                                //list_fail.AddRange(filename);
                                continue;
                            }

                            string bom = @BOM;

                            //string ICProductName2 = string.Empty;
                            if ((String.IsNullOrWhiteSpace(ICProductName)))
                            {
                                ICProductName = "";

                            }
                            else
                            {

                                ICProductName = @"" + ICProductName.Replace("\"", "") + "";
                            }





                            var sql = "CALL sp_d_bom_detail_interface('" + bom + "', '" + ParentLevel + "', '" + ChildLevel + "',";
                            sql += "\"" + ICProductName + "\"";


                            sql += " ,'" + csp + "', '" + UOM + "' ,'" + lot_pct + "' ,'" + PartCode + "' , '" + string.Concat(PartCode, "-", ParentLevel) + "','" + PartName + "','" + key_id + "');";


                            //bom2 = bom;


                            //"\"" + d.BOM + "\", "\"" + d.ParentLevel + "\"", " + d.ChildLevel + ","+ d.ICProductName +", " + h2 + ", " + d.UOM + ", " + lot_pct + ", " + d.PartCode + ", " + string.Concat(d.PartCode, "-", d.ParentLevel) + ") ";
                            db3.Database.ExecuteSqlCommand(sql);
                            list_done.Add(BOM);
                        }




                        //list.Add(vaule);
                        //insert_SNK_BOM2();



                        //var basepath2 = Server.MapPath("~/BAT");

                        //var filelist_hmi = Directory.GetFiles(basepath + "\\" + ten_folder, "*.CSV");
                        //Thread.Sleep(10000);

                        //string path = @basepath + "\\" + fileName2;
                        ////string path2 = @"D:\KEY1\KEY11\BACKUP\" + filename[filename.Length - 1];

                        //string path2 = @basepath + "\\BACKUP\\" + fileName2;



                        ////var basepath = Server.MapPath("~/EXCEL/BOMDT");

                        //if (!System.IO.File.Exists(path))
                        //{
                        //    // This statement ensures that the file is created,  
                        //    // but the handle is not kept.  
                        //    using (FileStream fs = System.IO.File.Create(path)) { }
                        //}
                        //// Ensure that the target does not exist.  
                        //if (System.IO.File.Exists(path2))
                        //    System.IO.File.Delete(path2);
                        //// Move the file.  
                        //System.IO.File.Move(path, path2);

                    }

                }
                db.Database.ExecuteSqlCommand(" truncate table d_bom_detail ");
                //Thread.Sleep(1000);
                db.Database.ExecuteSqlCommand(" CALL sp_d_bom_detail() ");
                //Thread.Sleep(1000);
                db.Database.ExecuteSqlCommand(" truncate table d_bom_part_info ");
                //Thread.Sleep(1000);
                db3.Database.ExecuteSqlCommand(" CALL sp_d_bom_part_info_interface() ");
                //Thread.Sleep(1000);
                db.Database.ExecuteSqlCommand(" CALL sp_d_bom_part_info() ");

                db.Database.Connection.Close();

                db2.Database.Connection.Close();
                db3.Database.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;


            }
        }

        public void get_from_ERP_Product()
        {

            try
            {
                var count_item = db3.interface_info.Count(x => x.inf_nm == "d_style_info");
                //if (count_item == 0)
                //{
                //    interface_info interface_info = new interface_info();
                //    interface_info.inf_nm = "d_style_info";
                //    interface_info.inf_last_time = (DateTime.Now).ToString("yyyyMMdd");
                //    interface_info.use_yn = "N";
                //    interface_info.re_mark = "Get data from ERP";
                //    //interface_info.reg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();
                //    //interface_info.chg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();
                //    interface_info.reg_dt = DateTime.Now;
                //    interface_info.chg_dt = DateTime.Now;
                //    db3.interface_info.Add(interface_info);
                //    db3.SaveChanges();
                //}
                //else
                //{
                //    interface_info interface_info = db3.interface_info.FirstOrDefault(x => x.inf_nm == "d_style_info");
                //    interface_info.inf_last_time = (DateTime.Now).ToString("yyyyMMdd");
                //    interface_info.re_mark = "Get data from ERP";
                //    interface_info.use_yn = "N";
                //    //interface_info.chg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();
                //    interface_info.chg_dt = DateTime.Now;
                //    db3.SaveChanges();
                //}

                db3.Database.ExecuteSqlCommand("truncate table d_style_info_interface");
                StringBuilder varname1 = new StringBuilder();
                #region TEST
                //StringBuffer varname1 = new StringBuffer();
                varname1.Append("SELECT a.ICProductID AS sid, ");
                varname1.Append("       a.ICProductCodes AS style_no, ");
                varname1.Append("       a.ICProductCodes, ");
                varname1.Append("       a.ICProductName AS style_vn_nm, ");
                varname1.Append("       a.ICProductEngName AS style_nm, ");
                varname1.Append("       b.ICProductModelName AS md_nm, ");
                varname1.Append("       b.ICProductModelNo AS md_cd, ");
                varname1.Append("       a.ICProductBarCode, ");
                varname1.Append("       a.ICProductInfo, ");
                varname1.Append("       a.AACreatedDate AS reg_dt, ");
                varname1.Append("       a.AACreatedUser AS reg_id, ");
                varname1.Append("       a.AAUpdatedDate AS chg_dt, ");
                varname1.Append("       a.AAUpdatedUser AS chg_id, ");
                varname1.Append("       c.ICProductTypeName AS bom_type, ");
                varname1.Append("       a.ICProductDesc AS description, ");
                varname1.Append("       d.ICProductStandardName AS standard, ");
                varname1.Append("       a.ICProductLength, ");
                varname1.Append("       a.ICProductPackageTypeCombo AS pack_amt, ");
                varname1.Append("       a.AAStatus AS use_yn, ");
                varname1.Append("       a.AAStatus AS del_yn, ");
                varname1.Append("       a.AASelected, ");
                varname1.Append("       f.ICProductCustomerID, ");
                varname1.Append("       e.ARCustomerName AS cust_rev, ");
                varname1.Append("       f.ICProductCustomerProductNumber AS order_num, ");
                varname1.Append("       n.ICQCName AS item_nm, ");
                varname1.Append("       m.ICQCItemVolume AS qc_range_cd_nm, ");
                varname1.Append("       m.ICQCItemVolume AS qc_range_cd, ");
                varname1.Append("       stk.ICStockNo AS wh_nm, ");
                varname1.Append("       a.ICProductCodes AS style_cust_no, ");
                varname1.Append("       gr.ICProductGroupNo AS ivtr_type, ");
                varname1.Append("       unit.ICUOMNo AS prd_unit , ");
                varname1.Append("       a.ICProductLength AS s_length, ");
                varname1.Append("       a.ICProductHeight AS s_height , ");
                varname1.Append("       a.ICProductWidth AS s_width, ");
                varname1.Append("       a.ICProductPkgFact AS case_pcs_qty, ");
                varname1.Append("       a.ICProductPkgLength AS c_length , ");
                varname1.Append("       a.ICProductPkgHeight AS c_height , ");
                varname1.Append("       a.ICProductPkgWidth AS c_width, ");
                varname1.Append("       a.ICProductPkgCBM AS c_cbm , ");
                varname1.Append("       c.ICProductTypeNo AS prd_group, ");
                varname1.Append("       a.ICProductIOF03Combo AS prd_brand, ");
                varname1.Append("       a.ICProductWeight AS s_weight, ");
                varname1.Append("       a.FK_ICWeightUOMID AS s_weight_unit, ");
                varname1.Append("       a.ICProductVolume AS s_volume, ");
                varname1.Append("       a.FK_ICVolumeUOMID AS s_volume_unit, ");
                varname1.Append("       a.ICProductPurchaseVariant AS s_tolerance ");
                varname1.Append("FROM ICProducts AS a ");
                varname1.Append("LEFT JOIN ICProductModels AS b ON a.FK_ICProductModelID = b.ICProductModelID ");
                varname1.Append("LEFT JOIN ICProductTypes AS c ON a.FK_ICProductTypeID = c.ICProductTypeID ");
                varname1.Append("LEFT JOIN ICProductStandards AS d ON a.FK_ICProductStandardID = d.ICProductStandardID ");
                varname1.Append("LEFT JOIN ICProductCustomers AS f ON f.FK_ICProductID = a.ICProductID ");
                varname1.Append("LEFT JOIN ARCustomers AS e ON e.ARCustomerID = f.FK_ARCustomerID ");
                varname1.Append("LEFT JOIN ICQCItems AS m ON m.FK_ICProductID = a.ICProductID ");
                varname1.Append("LEFT JOIN ICQCs AS n ON m.FK_ICQCID = n.ICQCID ");
                varname1.Append("LEFT JOIN ICStocks AS stk ON stk.ICStockID=a.FK_ICStockID ");
                varname1.Append("LEFT JOIN ICProductGroups AS gr ON gr.ICProductGroupID=a.FK_ICProductGroupID ");
                varname1.Append("LEFT JOIN ICUOMS AS unit ON unit.ICUOMID=a.FK_ICStkUOMID ");
                varname1.Append("where a.ICProductNo IS NOT NULL and a.ICProductNo!=''AND a.AAStatus= 'Alive' ");
                varname1.Append(" AND a.ICProductCodes IS NOT NULL and a.ICProductCodes!='' ");
                varname1.Append("UNION ");
                varname1.Append("  SELECT a.ICProductID AS sid, ");
                varname1.Append("       a.ICProductNo AS style_no, ");
                varname1.Append("       a.ICProductCodes, ");
                varname1.Append("       a.ICProductName AS style_vn_nm, ");
                varname1.Append("       a.ICProductEngName AS style_nm, ");
                varname1.Append("       b.ICProductModelName AS md_nm, ");
                varname1.Append("       b.ICProductModelNo AS md_cd, ");
                varname1.Append("       a.ICProductBarCode, ");
                varname1.Append("       a.ICProductInfo, ");
                varname1.Append("       a.AACreatedDate AS reg_dt, ");
                varname1.Append("       a.AACreatedUser AS reg_id, ");
                varname1.Append("       a.AAUpdatedDate AS chg_dt, ");
                varname1.Append("       a.AAUpdatedUser AS chg_id, ");
                varname1.Append("       c.ICProductTypeName AS bom_type, ");
                varname1.Append("       a.ICProductDesc AS description, ");
                varname1.Append("       d.ICProductStandardName AS standard, ");
                varname1.Append("       a.ICProductLength, ");
                varname1.Append("       a.ICProductPackageTypeCombo AS pack_amt, ");
                varname1.Append("       a.AAStatus AS use_yn, ");
                varname1.Append("       a.AAStatus AS del_yn, ");
                varname1.Append("       a.AASelected, ");
                varname1.Append("       f.ICProductCustomerID, ");
                varname1.Append("       e.ARCustomerName AS cust_rev, ");
                varname1.Append("       f.ICProductCustomerProductNumber AS order_num, ");
                varname1.Append("       n.ICQCName AS item_nm, ");
                varname1.Append("       m.ICQCItemVolume AS qc_range_cd_nm, ");
                varname1.Append("       m.ICQCItemVolume AS qc_range_cd, ");
                varname1.Append("       stk.ICStockNo AS wh_nm, ");
                varname1.Append("       a.ICProductCodes AS style_cust_no, ");
                varname1.Append("       gr.ICProductGroupNo AS ivtr_type, ");
                varname1.Append("       unit.ICUOMNo AS prd_unit , ");
                varname1.Append("       a.ICProductLength AS s_length, ");
                varname1.Append("       a.ICProductHeight AS s_height , ");
                varname1.Append("       a.ICProductWidth AS s_width, ");
                varname1.Append("       a.ICProductPkgFact AS case_pcs_qty, ");
                varname1.Append("       a.ICProductPkgLength AS c_length , ");
                varname1.Append("       a.ICProductPkgHeight AS c_height , ");
                varname1.Append("       a.ICProductPkgWidth AS c_width, ");
                varname1.Append("       a.ICProductPkgCBM AS c_cbm , ");
                varname1.Append("       c.ICProductTypeNo AS prd_group, ");
                varname1.Append("       a.ICProductIOF03Combo AS prd_brand, ");
                varname1.Append("       a.ICProductWeight AS s_weight, ");
                varname1.Append("       a.FK_ICWeightUOMID AS s_weight_unit, ");
                varname1.Append("       a.ICProductVolume AS s_volume, ");
                varname1.Append("       a.FK_ICVolumeUOMID AS s_volume_unit, ");
                varname1.Append("       a.ICProductPurchaseVariant AS s_tolerance ");
                varname1.Append("FROM ICProducts AS a ");
                varname1.Append("LEFT JOIN ICProductModels AS b ON a.FK_ICProductModelID = b.ICProductModelID ");
                varname1.Append("LEFT JOIN ICProductTypes AS c ON a.FK_ICProductTypeID = c.ICProductTypeID ");
                varname1.Append("LEFT JOIN ICProductStandards AS d ON a.FK_ICProductStandardID = d.ICProductStandardID ");
                varname1.Append("LEFT JOIN ICProductCustomers AS f ON f.FK_ICProductID = a.ICProductID ");
                varname1.Append("LEFT JOIN ARCustomers AS e ON e.ARCustomerID = f.FK_ARCustomerID ");
                varname1.Append("LEFT JOIN ICQCItems AS m ON m.FK_ICProductID = a.ICProductID ");
                varname1.Append("LEFT JOIN ICQCs AS n ON m.FK_ICQCID = n.ICQCID ");
                varname1.Append("LEFT JOIN ICStocks AS stk ON stk.ICStockID=a.FK_ICStockID ");
                varname1.Append("LEFT JOIN ICProductGroups AS gr ON gr.ICProductGroupID=a.FK_ICProductGroupID ");
                varname1.Append("LEFT JOIN ICUOMS AS unit ON unit.ICUOMID=a.FK_ICStkUOMID ");
                varname1.Append("where a.ICProductNo IS NOT NULL and a.ICProductNo!=''AND a.AAStatus= 'Alive' ");
                #endregion
                #region OLD

                //varname1.Append("SELECT a.ICProductID AS sid, \n");
                ////varname1.Append("       a.ICProductCodes AS style_no, \n");
                //varname1.Append("       a.ICProductNo AS style_no, \n");
                //varname1.Append("       a.ICProductCodes, \n");
                //varname1.Append("       a.ICProductName AS style_vn_nm, \n");
                //varname1.Append("       a.ICProductEngName AS style_nm, \n");
                //varname1.Append("       b.ICProductModelName AS md_nm,  \n");
                //varname1.Append("       b.ICProductModelNo AS md_cd, \n");
                //varname1.Append("       a.ICProductBarCode, \n");
                //varname1.Append("       a.ICProductInfo, \n");
                //varname1.Append("       a.AACreatedDate AS reg_dt, \n");
                //varname1.Append("       a.AACreatedUser AS reg_id, \n");
                //varname1.Append("       a.AAUpdatedDate AS chg_dt, \n");
                //varname1.Append("       a.AAUpdatedUser AS chg_id, \n");
                //varname1.Append("       c.ICProductTypeName AS bom_type, \n");
                //varname1.Append("       a.ICProductDesc AS description, \n");
                //varname1.Append("       d.ICProductStandardName AS standard, \n");
                //varname1.Append("       a.ICProductLength, \n");
                //varname1.Append("       a.ICProductPackageTypeCombo AS pack_amt, \n");
                //varname1.Append("       a.AAStatus AS use_yn, \n");
                //varname1.Append("       a.AAStatus AS del_yn, \n");
                //varname1.Append("       a.AASelected, \n");
                //varname1.Append("       f.ICProductCustomerID, \n");
                //varname1.Append("       e.ARCustomerName AS cust_rev, \n");
                //varname1.Append("       f.ICProductCustomerProductNumber AS order_num, \n");
                //varname1.Append("       n.ICQCName AS item_nm, \n");
                //varname1.Append("       m.ICQCItemVolume AS qc_range_cd_nm, \n");
                //varname1.Append("       m.ICQCItemVolume AS qc_range_cd, \n");
                //varname1.Append("       stk.ICStockNo AS wh_nm, \n");
                //varname1.Append("       a.ICProductCodes AS style_cust_no, \n");
                //varname1.Append("       gr.ICProductGroupNo AS ivtr_type, \n");
                //varname1.Append("       unit.ICUOMNo AS prd_unit , \n");
                //varname1.Append("       a.ICProductLength AS s_length, \n");
                //varname1.Append("       a.ICProductHeight AS s_height , \n");
                //varname1.Append("       a.ICProductWidth AS s_width, \n");
                //varname1.Append("       a.ICProductPkgFact AS case_pcs_qty, \n");
                //varname1.Append("       a.ICProductPkgLength AS c_length , \n");
                //varname1.Append("       a.ICProductPkgHeight AS c_height , \n");
                //varname1.Append("       a.ICProductPkgWidth AS c_width, \n");
                //varname1.Append("       a.ICProductPkgCBM AS c_cbm , \n");
                //varname1.Append("       c.ICProductTypeNo AS prd_group, \n");
                //varname1.Append("       a.ICProductIOF03Combo AS prd_brand, \n");
                //varname1.Append("       a.ICProductWeight AS s_weight, \n");
                //varname1.Append("       a.FK_ICWeightUOMID AS s_weight_unit, \n");
                //varname1.Append("       a.ICProductVolume AS s_volume, \n");
                //varname1.Append("       a.FK_ICVolumeUOMID AS s_volume_unit, \n");
                //varname1.Append("       a.ICProductPurchaseVariant AS s_tolerance \n");
                //varname1.Append("FROM ICProducts AS a \n");
                //varname1.Append("LEFT JOIN ICProductModels AS b ON a.FK_ICProductModelID = b.ICProductModelID \n");
                //varname1.Append("LEFT JOIN ICProductTypes AS c ON a.FK_ICProductTypeID = c.ICProductTypeID \n");
                //varname1.Append("LEFT JOIN ICProductStandards AS d ON a.FK_ICProductStandardID = d.ICProductStandardID \n");
                //varname1.Append("LEFT JOIN ICProductCustomers AS f ON f.FK_ICProductID = a.ICProductID \n");
                //varname1.Append("LEFT JOIN ARCustomers AS e ON e.ARCustomerID = f.FK_ARCustomerID \n");
                //varname1.Append("LEFT JOIN ICQCItems AS m ON m.FK_ICProductID = a.ICProductID \n");
                //varname1.Append("LEFT JOIN ICQCs AS n ON m.FK_ICQCID = n.ICQCID \n");
                //varname1.Append("LEFT JOIN ICStocks AS stk ON stk.ICStockID=a.FK_ICStockID \n");
                //varname1.Append("LEFT JOIN ICProductGroups AS gr ON gr.ICProductGroupID=a.FK_ICProductGroupID \n");
                //varname1.Append("LEFT JOIN ICUOMS AS unit ON unit.ICUOMID=a.FK_ICStkUOMID   where a.ICProductNo IS NOT NULL and a.ICProductNo!=''  AND a.AAStatus= 'Alive' /*AND a.ICProductCodes IN ('D61861','D61910','D61333','D61304','D21205','D61868','D61870') AND a.ICProductCodes IS NOT NULL and a.ICProductCodes!='' */ ");
                ////varname1.Append("LEFT JOIN ICUOMS AS unit ON unit.ICUOMID=a.FK_ICStkUOMID   where a.ICProductNo IS NOT NULL and a.ICProductNo!=''  /*AND a.ICProductCodes IN ('D61861','D61910','D61333','D61304','D21205','D61868','D61870')*/ AND a.ICProductCodes IS NOT NULL and a.ICProductCodes!=''  AND a.AAStatus= 'Alive' ");
                ////return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
                #endregion


                var data = new DataTable();
                using (var cmd = db2.Database.Connection.CreateCommand())
                {
                    db2.Database.Connection.Open();
                    cmd.CommandText = varname1.ToString();
                    using (var reader = cmd.ExecuteReader())
                    {
                        data.Load(reader);
                    }
                    db2.Database.Connection.Close();
                }
                var lstRows = new List<Dictionary<string, object>>();
                Dictionary<string, object> dictRow = null;
                d_style_info_interface d_style_info_interface = new d_style_info_interface();
                foreach (DataRow row in data.Rows)
                {
                    dictRow = new Dictionary<string, object>();
                    foreach (DataColumn column in data.Columns)
                    {
                        dictRow.Add(column.ColumnName, row[column]);
                    }

                    var reg_dt = TryParseDateTime(dictRow["reg_dt"].ToString());

                    var chg_dt = TryParseDateTime(dictRow["chg_dt"].ToString());


                    //String date_rull_condition = "19000101";
                    //String reg_dt_condition = Convert.ToDateTime(dictRow["reg_dt"]).ToString("yyyyMMdd");
                    //String chg_dt_condition = Convert.ToDateTime(dictRow["chg_dt"]).ToString("yyyyMMdd");
                    //DateTime reg_dt = DateTime.Now;
                    //DateTime chg_dt = DateTime.Now;


                    //if (Convert.ToDecimal(reg_dt_condition) > Convert.ToDecimal(date_rull_condition))
                    //{
                    //    reg_dt = Convert.ToDateTime(dictRow["reg_dt"]);
                    //}

                    //if (Convert.ToDecimal(chg_dt_condition) > Convert.ToDecimal(date_rull_condition))
                    //{
                    //    chg_dt = Convert.ToDateTime(dictRow["chg_dt"]);
                    //}

                    var style_no = Convert.ToString(dictRow["style_no"]);
                    var style_nm = @"" + Convert.ToString(dictRow["style_nm"]).Replace("\"", "") + "";
                    var style_vn_nm = @"" + Convert.ToString(dictRow["style_vn_nm"]).Replace("\"", "") + "";
                    var wh_nm = @"" + Convert.ToString(dictRow["wh_nm"]).Replace("\"", "") + "";
                    var style_cust_no = @"" + Convert.ToString(dictRow["style_cust_no"]).Replace("\"", "") + "";
                    var ivtr_type = @"" + Convert.ToString(dictRow["ivtr_type"]).Replace("\"", "") + "";
                    var prd_unit = Convert.ToString(dictRow["prd_unit"]);
                    var s_length = (dictRow["s_length"] == "") ? 0 : Convert.ToDecimal(dictRow["s_length"]);
                    var s_width = (dictRow["s_width"] == "") ? 0 : Convert.ToDecimal(dictRow["s_width"]);
                    var s_height = (dictRow["s_height"] == "") ? 0 : Convert.ToDecimal(dictRow["s_height"]);
                    var case_pcs_qty = (dictRow["case_pcs_qty"] == "") ? 0 : Convert.ToInt32(dictRow["case_pcs_qty"]);
                    var c_length = (dictRow["c_length"] == "") ? 0 : Convert.ToDecimal(dictRow["c_length"]);
                    var c_width = (dictRow["c_width"] == "") ? 0 : Convert.ToDecimal(dictRow["c_width"]);
                    var c_height = Convert.ToInt32(dictRow["c_height"]);
                    var c_cbm = (dictRow["c_cbm"] == "") ? 0 : Convert.ToDecimal(dictRow["c_cbm"]);
                    var prd_group = Convert.ToString(dictRow["prd_group"]);
                    var prd_brand = Convert.ToString(dictRow["prd_brand"]);
                    var standard = Convert.ToString(dictRow["standard"]);
                    var cust_rev = Convert.ToString(dictRow["cust_rev"]);
                    var order_num = Convert.ToString(dictRow["order_num"]);
                    var pack_amt = (dictRow["pack_amt"] == "") ? 0 : Convert.ToInt32(dictRow["pack_amt"]);
                    var bom_type = Convert.ToString(dictRow["bom_type"]);
                    var qc_range_cd = Convert.ToString(dictRow["qc_range_cd"]);
                    var use_yn = Convert.ToString(dictRow["use_yn"]);
                    var del_yn = Convert.ToString(dictRow["del_yn"]);
                    //var reg_dt2 = reg_dt;
                    //var chg_dt2 = chg_dt;
                    var reg_id = Convert.ToString(dictRow["reg_id"]);
                    var chg_id = Convert.ToString(dictRow["chg_id"]);


                    var sql = "CALL sp_d_style_info_interface(   ";

                    //sql += "'" +  + "',";
                    sql += "\"" + style_no + "\",";
                    sql += "\"" + style_nm + "\",";
                    sql += "\"" + style_vn_nm + "\",";
                    sql += "\"" + wh_nm + "\",";
                    //sql += ",'" +  + "',";

                    sql += "\"" + style_cust_no + "\",";
                    sql += "\"" + ivtr_type + "\"";


                    sql += ", \"" + prd_unit + "\"";
                    //sql += ",'" +  + "' ";
                    sql += ",'" + s_length + "' ";
                    sql += ",'" + s_width + "' ";
                    sql += ",'" + s_height + "' ";
                    sql += ",'" + case_pcs_qty + "' ";
                    sql += ",'" + c_length + "' ";

                    sql += ",'" + c_width + "' ";
                    sql += ",'" + c_height + "' ";


                    sql += ",'" + c_cbm + "',";
                    sql += "\"" + prd_group + "\",";

                    sql += "\"" + prd_brand + "\",";
                    sql += "\"" + standard + "\",";
                    sql += "\"" + cust_rev + "\",";
                    sql += "\"" + order_num + "\"";
                    sql += ",'" + pack_amt + "' ";
                    sql += ",'" + bom_type + "' ";

                    sql += ",'" + qc_range_cd + "' ";
                    sql += ",'" + use_yn + "' ";
                    sql += ",'" + del_yn + "' ";
                    sql += ",'" + reg_dt.ToString("yyyyMMddHHmmss") + "' ";

                    sql += ",'" + chg_dt.ToString("yyyyMMddHHmmss") + "' ";
                    sql += ",'" + reg_id + "' ";

                    sql += ",'" + chg_id + "')";
                    db3.Database.ExecuteSqlCommand(sql);

                    //var mt_nm = @"" + Convert.ToString(dictRow["mt_nm"]).Replace("\"", "") + "";
                    //sql += "\"" + mt_nm + "\"";
                }

                var count = db.d_style_info_insert_view.Count();


                db.Database.ExecuteSqlCommand("call sp_d_style_info_insertandupdate() ");


                var count2 = db.d_style_info_insert_view.Count();

                sync_log sync_log = new sync_log();
                sync_log.table_nm = "Product";


                sync_log.last_yn = (count > count2) ? "N" : "Y";

                sync_log.use_yn = "N";
                sync_log.re_mark = count.ToString();
                sync_log.table_last_time = count2.ToString();


                //sync_log.reg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();
                //sync_log.chg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();

                sync_log.reg_dt = DateTime.Now;
                sync_log.chg_dt = DateTime.Now;
                db.sync_log.Add(sync_log);
                db.SaveChanges();



                db.Database.Connection.Close();

                db2.Database.Connection.Close();
                db3.Database.Connection.Close();
                //return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;

                //return Json(new { result = false, Message = "Product: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public void get_from_ERP_Supplier_Info()
        {

            try
            {


                db3.Database.ExecuteSqlCommand("truncate table supplier_info_interface");
                StringBuilder varname1 = new StringBuilder();
                varname1.Append("SELECT a.APSupplierNo AS sp_cd, \n");
                varname1.Append("       a.APSupplierName AS sp_nm, \n");
                varname1.Append("       a.APSupplierAddL1 AS address, \n");
                varname1.Append("       b.GECurrencyName AS currency, \n");
                varname1.Append("       c.APPmtTermDesc AS pay_term, \n");
                varname1.Append("       d.APSupplierConName AS contant_person, \n");
                varname1.Append("       d.APSupplierConEmail1 AS e_mail, \n");
                varname1.Append("       a.AACreatedDate AS reg_dt, \n");
                varname1.Append("       a.AACreatedUser AS reg_id, \n");
                varname1.Append("       a.AAUpdatedDate AS chg_dt, \n");
                varname1.Append("       a.AAUpdatedUser AS chg_id, e.ARTradeTermDay as transit_time \n");
                varname1.Append("FROM APSuppliers AS a \n");
                varname1.Append("LEFT JOIN GECurrencys AS b ON a.FK_GECurrencyID=b.GECurrencyID \n");
                varname1.Append("LEFT JOIN APPmtTerms AS c ON a.APSupplierPmtTerm=c.APPmtTermID \n");
                varname1.Append("LEFT JOIN APSupplierCons AS d ON d.FK_APSupplierID=a.APSupplierID ");
                varname1.Append("LEFT JOIN  ARTradeTerms as e on a.FK_ARTradeTermID=e.ARTradeTermID ");
                varname1.Append("where a.APSupplierNo IS NOT NULL and a.APSupplierNo!='' AND a.AAStatus= 'Alive' ");
                var data = new DataTable();
                using (var cmd = db2.Database.Connection.CreateCommand())
                {
                    db2.Database.Connection.Open();
                    cmd.CommandText = varname1.ToString();
                    using (var reader = cmd.ExecuteReader())
                    {
                        data.Load(reader);
                    }
                    db2.Database.Connection.Close();
                }

                var lstRows = new List<Dictionary<string, object>>();
                Dictionary<string, object> dictRow = null;
                supplier_info_interface supplier_info_interface = new supplier_info_interface();
                foreach (DataRow row in data.Rows)
                {
                    dictRow = new Dictionary<string, object>();
                    foreach (DataColumn column in data.Columns)
                    {
                        dictRow.Add(column.ColumnName, row[column]);
                    }
                    //String date_rull_condition = "19000101";
                    //String reg_dt_condition = Convert.ToDateTime(dictRow["reg_dt"]).ToString("yyyyMMdd");
                    //String chg_dt_condition = Convert.ToDateTime(dictRow["chg_dt"]).ToString("yyyyMMdd");
                    //DateTime reg_dt = DateTime.Now;
                    //DateTime chg_dt = DateTime.Now;
                    //if (Convert.ToDecimal(reg_dt_condition) > Convert.ToDecimal(date_rull_condition))
                    //{
                    //    reg_dt = Convert.ToDateTime(dictRow["reg_dt"]);
                    //}

                    //if (Convert.ToDecimal(chg_dt_condition) > Convert.ToDecimal(date_rull_condition))
                    //{
                    //    chg_dt = Convert.ToDateTime(dictRow["chg_dt"]);
                    //}


                    var reg_dt = TryParseDateTime(dictRow["reg_dt"].ToString());
                    var chg_dt = TryParseDateTime(dictRow["chg_dt"].ToString());



                    var sp_cd = Convert.ToString(dictRow["sp_cd"]);
                    var sp_nm = @"" + Convert.ToString(dictRow["sp_nm"]).Replace("\"", "") + "";
                    var address = Convert.ToString(dictRow["address"]);
                    var currency = Convert.ToString(dictRow["currency"]);
                    var contant_person = Convert.ToString(dictRow["contant_person"]);
                    var e_mail = Convert.ToString(dictRow["e_mail"]);



                    var pay_term = Convert.ToString(dictRow["pay_term"]);
                    var transit_time = Convert.ToString(dictRow["transit_time"]);

                    //var use_yn = "Y";
                    //var del_yn = "N";
                    //var reg_dt = reg_dt;
                    //var chg_dt = chg_dt;
                    var reg_id = Convert.ToString(dictRow["reg_id"]);
                    var chg_id = Convert.ToString(dictRow["chg_id"]);
                    //db3.var Add(supplier_info_interface);
                    //db3.SaveChanges();









                    var sql = "CALL sp_supplier_info_interface(   ";

                    sql += "'" + sp_cd + "',";
                    sql += "\"" + sp_nm + "\",";
                    sql += "\"" + address + "\",";
                    sql += "\"" + currency + "\",";

                    sql += "\"" + contant_person + "\",";
                    sql += "\"" + e_mail + "\"";


                    sql += ", \"" + pay_term + "\"";

                    sql += ",'" + transit_time + "' ";

                    sql += ",'" + reg_dt.ToString("yyyyMMddHHmmss") + "' ";

                    sql += ",'" + chg_dt.ToString("yyyyMMddHHmmss") + "' ";
                    sql += ",'" + reg_id + "' ";

                    sql += ",'" + chg_id + "')";



                    db3.Database.ExecuteSqlCommand(sql);

                }



                var count = db.supplier_info_insert_view.Count();
                db.Database.ExecuteSqlCommand("call sp_supplier_info() ");
                //insert_SNK_Product2();



                var count2 = db.supplier_info_insert_view.Count();

                sync_log sync_log = new sync_log();
                sync_log.table_nm = "Supplier";

                sync_log.use_yn = "N";
                sync_log.last_yn = (count > count2) ? "N" : "Y";
                sync_log.re_mark = count.ToString();
                sync_log.table_last_time = count2.ToString();


                //sync_log.reg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();
                //sync_log.chg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();

                sync_log.reg_dt = DateTime.Now;
                sync_log.chg_dt = DateTime.Now;
                db.sync_log.Add(sync_log);
                db.SaveChanges();

                db.Database.Connection.Close();
                db2.Database.Connection.Close();
                db3.Database.Connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
                //return Json(new { result = false, Message = "Supplier: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public void get_from_ERP_Buyer_Info()
        {

            try
            {

                db3.Database.ExecuteSqlCommand("truncate table buyer_info_interface");

                StringBuilder varname1 = new StringBuilder();
                varname1.Append("SELECT a.APSupplierNo AS buyer_cd, \n");
                varname1.Append("       a.APSupplierName AS buyer_nm, \n");
                varname1.Append("       a.AACreatedDate AS reg_dt, \n");
                varname1.Append("       a.AACreatedUser AS reg_id, \n");
                varname1.Append("       a.AAUpdatedDate AS chg_dt, \n");
                varname1.Append("       a.AAUpdatedUser AS chg_id \n");
                varname1.Append("FROM APSuppliers AS a ");
                varname1.Append(" where a.APSupplierNo IS NOT NULL and a.APSupplierNo!='' AND a.AAStatus= 'Alive' ");

                var data = new DataTable();
                using (var cmd = db2.Database.Connection.CreateCommand())
                {
                    db2.Database.Connection.Open();
                    cmd.CommandText = varname1.ToString();
                    using (var reader = cmd.ExecuteReader())
                    {
                        data.Load(reader);
                    }
                    db2.Database.Connection.Close();
                }
                var lstRows = new List<Dictionary<string,
                object>>();
                Dictionary<string,
                object> dictRow = null;
                buyer_info_interface buyer_info_interface = new buyer_info_interface();
                foreach (DataRow row in data.Rows)
                {
                    dictRow = new Dictionary<string,
                    object>();
                    foreach (DataColumn column in data.Columns)
                    {
                        dictRow.Add(column.ColumnName, row[column]);
                    }
                    var reg_dt = TryParseDateTime(dictRow["reg_dt"].ToString());
                    var chg_dt = TryParseDateTime(dictRow["chg_dt"].ToString());



                    var buyer_cd = Convert.ToString(dictRow["buyer_cd"]);

                    var buyer_nm = @"" + Convert.ToString(dictRow["buyer_nm"]).Replace("\"", "") + "";
                    //var reg_dt = reg_dt;
                    //  buyer_info_interface.chg_dt = chg_dt;
                    var reg_id = Convert.ToString(dictRow["reg_id"]);
                    var chg_id = Convert.ToString(dictRow["chg_id"]);

                    var sql = "INSERT INTO buyer_info_interface (buyer_cd,buyer_nm,reg_dt,chg_dt,reg_id,chg_id)   ";
                    sql += " VALUES(   ";
                    sql += "'" + buyer_cd + "',";
                    sql += "\"" + buyer_nm + "\",";

                    sql += "'" + reg_dt.ToString("yyyyMMddHHmmss") + "' ";

                    sql += ",'" + chg_dt.ToString("yyyyMMddHHmmss") + "' ";

                    //sql += ",'" + chg_dt + "' ";
                    sql += ",'" + reg_id + "' ";

                    sql += ",'" + chg_id + "')";
                    db3.Database.ExecuteSqlCommand(sql);
                }

                var count = db.buyer_info_insert_view.Count();
                db.Database.ExecuteSqlCommand("call sp_buyer_info() ");

                var count2 = db.buyer_info_insert_view.Count();

                sync_log sync_log = new sync_log();
                sync_log.table_nm = "Buyer";
                sync_log.last_yn = (count > count2) ? "N" : "Y";
                sync_log.use_yn = "N";
                sync_log.re_mark = count.ToString();
                sync_log.table_last_time = count2.ToString();

                sync_log.reg_dt = DateTime.Now;
                sync_log.chg_dt = DateTime.Now;
                db.sync_log.Add(sync_log);
                db.SaveChanges();
                db.Database.Connection.Close();
                db2.Database.Connection.Close();
                db3.Database.Connection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //public DateTime? TryParseNullable(this DateTime dateTime, string val)
        //{
        //    DateTime outValue;
        //    return DateTime.TryParse(val, out outValue) ? (DateTime?)outValue : null;
        //}

        public DateTime TryParseDateTime(string stringDate)
        {
            DateTime date;
            return DateTime.TryParse(stringDate, out date) ? date : DateTime.Now;
        }

        public void get_from_ERP_PO_INFO()
        {
            try
            {
                db3.Database.ExecuteSqlCommand(" truncate table s_order_master_interface ");
                StringBuilder varname1 = new StringBuilder();
                varname1.Append("SELECT PPWOs.PPWONo pm_no,");
                varname1.Append("       PPWODesc re_mark, ");
                varname1.Append("       PPWOs.AACreatedUser AS reg_id, ");
                varname1.Append("       PPWOs.AAUpdatedUser AS chg_id, ");
                varname1.Append("       PPWOs.PPWODate AS order_dt, ");
                varname1.Append("       PPWOs.PPWONo ");
                varname1.Append("FROM PPWOs ");
                varname1.Append("WHERE PPWOs.AAStatus = 'Alive' ");
                varname1.Append("ORDER BY PPWOs.PPWONo");

                var data = new DataTable();
                using (var cmd = db2.Database.Connection.CreateCommand())
                {
                    db2.Database.Connection.Open();
                    cmd.CommandText = varname1.ToString();
                    using (var reader = cmd.ExecuteReader())
                    {
                        data.Load(reader);
                    }
                    db2.Database.Connection.Close();
                }
                var lstRows = new List<Dictionary<string,
                object>>();
                Dictionary<string,
                object> dictRow = null;
                s_order_master_interface s_order_master_interface = new s_order_master_interface();
                foreach (DataRow row in data.Rows)
                {
                    dictRow = new Dictionary<string,
                    object>();
                    foreach (DataColumn column in data.Columns)
                    {
                        dictRow.Add(column.ColumnName, row[column]);
                    }

                    //String reg_dt_condition = Convert.ToDateTime(dictRow["reg_dt"]).ToString("yyyyMMdd");
                    //String chg_dt_condition = Convert.ToDateTime(dictRow["chg_dt"]).ToString("yyyyMMdd");
                    //DateTime reg_dt = DateTime.Now;
                    //DateTime chg_dt = DateTime.Now;

                    var pm_no = Convert.ToString(dictRow["pm_no"]);
                    var re_mark = @"" + Convert.ToString(dictRow["re_mark"]).Replace("\"", "") + "";
                    //s_order_master_interface.sor_sts_nm = Convert.ToString(dictRow["sor_sts_nm"]);
                    //s_order_master_interface.delivery_qty = Convert.ToInt32(dictRow["delivery_qty"]);

                    //s_order_master_interface.use_yn = "Y";
                    //s_order_master_interface.del_yn = "N";

                    //s_order_master_interface.contract_no = Convert.ToString(dictRow["contract_no"]);
                    //s_order_master_interface.contract_dt = Convert.ToString(dictRow["contract_dt"]);
                    //s_order_master_interface.sp_nm = Convert.ToString(dictRow["sp_nm"]);
                    //var order_dt = Convert.ToString(dictRow["order_dt"]);
                    var order_dt = TryParseDateTime(dictRow["order_dt"].ToString());
                    //sql += ",'" + reg_dt.ToString("yyyyMMddHHmmss") + "' ";
                    //s_order_master_interface.employee_nm = Convert.ToString(dictRow["employee_nm"]);

                    //s_order_master_interface.reg_dt = reg_dt;
                    //s_order_master_interface.chg_dt = chg_dt;

                    var reg_id = Convert.ToString(dictRow["reg_id"]);
                    var chg_id = Convert.ToString(dictRow["chg_id"]);

                    var sql = "INSERT INTO s_order_master_interface (pm_no,re_mark,order_dt,reg_dt,chg_dt,reg_id,chg_id)   ";
                    sql += " VALUES(   ";
                    sql += "'" + pm_no + "',";
                    sql += "\"" + re_mark + "\",";
                    sql += "'" + order_dt.ToString("yyyy-MM-dd") + "' ";

                    sql += ", NOW() ";

                    sql += ", NOW() ";
         
                    sql += ",'" + reg_id + "' ";

                    sql += ",'" + chg_id + "')";
                    db3.Database.ExecuteSqlCommand(sql);

                }

                var count = db.s_order_master_insert_view.Count();

                db.Database.ExecuteSqlCommand("call sp_s_order_master() ");

                //insert_SNK_Product2();

                var count2 = db.s_order_master_insert_view.Count();

                sync_log sync_log = new sync_log();
                sync_log.table_nm = "PO INFO";

                sync_log.use_yn = "N";
                sync_log.re_mark = count.ToString();
                sync_log.table_last_time = count2.ToString();
                sync_log.last_yn = (count > count2) ? "N" : "Y";

                //sync_log.reg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();
                //sync_log.chg_id = (Session["userid"] == null) ? "" : Session["userid"].ToString();

                sync_log.reg_dt = DateTime.Now;
                sync_log.chg_dt = DateTime.Now;
                db.sync_log.Add(sync_log);
                db.SaveChanges();

                db.Database.Connection.Close();
                db2.Database.Connection.Close();
                db3.Database.Connection.Close();
                Thread.Sleep(1000);
                get_from_ERP_PO_DETAIL();
                ////var kq = insert_SNK_PO_INFO();
                //return true;
                //{

                //}
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void get_from_ERP_PO_DETAIL()
        {
            try
            {
                db3.Database.ExecuteSqlCommand(" truncate table s_order_info_interface ");
                StringBuilder varname1 = new StringBuilder();

                varname1.Append("SELECT pm_no2 = ARSOs.ArSOno, ");
                varname1.Append("       PPWOs.PPWONo pm_no, ");
                varname1.Append("       ARSOs.ARSOName po_no2, ");
                varname1.Append("       style_no_fgc = ICProducts.ICProductCodes, ");
                varname1.Append("       style_nm_fgc = ICProducts.ICProductName, ");
                varname1.Append("       PPWOs.PPWODate order_dt, ");
                varname1.Append("       ARSOs.ARSOStatusCombo po_sts, ");
                varname1.Append("       unit.ICUOMNo AS prd_unit, ");
                varname1.Append("       ARSOs.AACreatedUser AS reg_id, ");
                varname1.Append("       ARSOs.AAUpdatedUser AS chg_id, ");
                varname1.Append("       BRBranchs.BRBranchName branch_nm, ");
                varname1.Append("       PPWOPItems.PPWOPItemQty delivery_qty, PPWOPItems.PPWOPItemShpDate delivery_dt ");
                varname1.Append("FROM PPWOs PPWOs ");
                varname1.Append("INNER JOIN PPWOPItems ON PPWOs.PPWOID= PPWOPItems.FK_PPWOID ");
                varname1.Append("INNER JOIN ARSOs ON PPWOPItems.FK_ARSOID=ARSOs.ARSOID ");
                varname1.Append("INNER JOIN ICProducts ON ICProducts.ICProductID = PPWOPItems.FK_ICProductID ");
                varname1.Append("INNER JOIN ICUOMS AS unit ON unit.ICUOMID=ICProducts.FK_ICStkUOMID ");
                varname1.Append("INNER JOIN BRBranchs AS BRBranchs ON BRBranchs.BRBranchID=PPWOs.FK_BRBranchID ");
                varname1.Append("WHERE PPWOs.AAStatus='Alive'");


                var data = new DataTable();
                using (var cmd = db2.Database.Connection.CreateCommand())
                {
                    db2.Database.Connection.Open();
                    cmd.CommandText = varname1.ToString();
                    using (var reader = cmd.ExecuteReader())
                    {
                        data.Load(reader);
                    }
                    db2.Database.Connection.Close();
                }
                var lstRows = new List<Dictionary<string, object>>();
                Dictionary<string, object> dictRow = null;
                s_order_info_interface s_order_info_interface = new s_order_info_interface();
                foreach (DataRow row in data.Rows)
                {
                    dictRow = new Dictionary<string, object>();
                    foreach (DataColumn column in data.Columns)
                    {
                        dictRow.Add(column.ColumnName, row[column]);
                    }



                    var delivery_qty = (dictRow["delivery_qty"] == "") ? 0 : Convert.ToInt32(dictRow["delivery_qty"]);
//                    var  = Convert.ToString(dictRow["delivery_dt"]).Substring(0, 10);


//delivery_dt
                    var delivery_dt = TryParseDateTime(dictRow["delivery_dt"].ToString());
                    var order_dt = TryParseDateTime(dictRow["order_dt"].ToString());

                    //var order_dt = Convert.ToString(dictRow["order_dt"]).Substring(0, 10);

                    var pm_no = Convert.ToString(dictRow["pm_no"]);
                    //var style_no_fgc = Convert.ToString(dictRow["style_no_fgc"]);

                    var style_no_fgc = @"" + Convert.ToString(dictRow["style_no_fgc"]).Replace("\"", "") + "";


                    var style_nm_fgc = @"" + Convert.ToString(dictRow["style_nm_fgc"]).Replace("\"", "") + "";



                    //var re_mark = Convert.ToString(dictRow["re_mark"]);

                    var prd_unit = Convert.ToString(dictRow["prd_unit"]);


                    var so_no = Convert.ToString(dictRow["pm_no2"]);
                    var branch_nm = @"" + Convert.ToString(dictRow["branch_nm"]).Replace("\"", "") + "";

                    var po_no2 = Convert.ToString(dictRow["po_no2"]);


                    //var po_dt = Convert.ToString(dictRow["po_dt"]).Substring(0, 10);

                    var po_sts = Convert.ToString(dictRow["po_sts"]);



                    var po_no = string.Empty;
                    int countlist = db3.s_order_info_interface.ToList().Count();
                    var po_no_tmp = string.Empty;
                    var subponoconvert = 0;

                    var reg_id = Convert.ToString(dictRow["reg_id"]);
                    var chg_id = Convert.ToString(dictRow["chg_id"]);
                    DateTime reg_dt = DateTime.Now;
                    DateTime chg_dt = DateTime.Now;

                    if (countlist == 0)
                    {
                        po_no = "P0000000001";
                    }
                    else
                    {
                        var listlast = db3.s_order_info_interface.OrderBy(item => item.po_no).ToList().LastOrDefault();
                        var bien = listlast.po_no;
                        var subbbno = bien.Substring(bien.Length - 10, 10);
                        int.TryParse(subbbno, out subponoconvert);
                        po_no_tmp = "P" + string.Format("{0}{1}", po_no_tmp, new InitMethods().autoPono((subponoconvert + 1)));
                        po_no = po_no_tmp;



                        //var mdpo_no_out = 
                    }



                    s_order_info_interface.prd_unit = prd_unit;
                    s_order_info_interface.delivery_qty = delivery_qty;
                    s_order_info_interface.delivery_dt = delivery_dt.ToString("yyyy-MM-dd");
                    s_order_info_interface.pm_no = pm_no;


                    s_order_info_interface.so_no = so_no;


                    s_order_info_interface.branch_nm = branch_nm;

                    s_order_info_interface.order_dt = order_dt.ToString("yyyy-MM-dd");

                    s_order_info_interface.po_sts = po_sts;
                    s_order_info_interface.po_no2 = po_no2;
                    s_order_info_interface.style_no_fgc = style_no_fgc;
                    s_order_info_interface.style_nm_fgc = style_nm_fgc;
                    s_order_info_interface.po_no = po_no;



                    //s_order_info_interface.reg_dt = reg_dt;
                    //s_order_info_interface.chg_dt = chg_dt;
                    //s_order_info_interface.use_yn = "Y";
                    //s_order_info_interface.del_yn = "N";






                    var sql = "INSERT INTO s_order_info_interface (prd_unit,delivery_qty,delivery_dt,pm_no,so_no,branch_nm,order_dt,po_sts,po_no2,style_no_fgc,style_nm_fgc,po_no,reg_dt,chg_dt,reg_id,chg_id)   ";
                    sql += " VALUES(   ";
                    sql += "'" + prd_unit + "',";
                    sql += "'" + delivery_qty + "',";
                    sql += "'" + delivery_dt.ToString("yyyy-MM-dd") + "' ,";
                    sql += "'" + pm_no + "', ";
                    sql += "'" + so_no + "', ";
                    sql += "\"" + branch_nm + "\",";
                    sql += "'" + order_dt.ToString("yyyy-MM-dd") + "', ";
                    sql += "'" + po_sts + "' ,";
                    sql += "'" + po_no2 + "' ,";

                    sql += "\"" + style_no_fgc + "\",";
                    sql += "\"" + style_nm_fgc + "\",";
                    sql += "'" + po_no + "' ";




                    sql += ", Date_format('" + reg_dt + "', '%Y-%m-%d %H:%i:%s') ";

                    sql += ",Date_format('" + chg_dt + "', '%Y-%m-%d %H:%i:%s') ";
                    sql += ",'" + reg_id + "' ";

                    sql += ",'" + chg_id + "')";
                    db3.Database.ExecuteSqlCommand(sql);






                }

                var count = db.s_order_info_insert_view.Count();
                db.Database.ExecuteSqlCommand(" call sp_s_order_info() ");



                var count2 = db.s_order_info_insert_view.Count();

                sync_log sync_log = new sync_log();
                sync_log.table_nm = "PO DETAIL";
                sync_log.last_yn = (count > count2) ? "N" : "Y";
                sync_log.use_yn = "N";
                sync_log.re_mark = count.ToString();
                sync_log.table_last_time = count2.ToString();



                sync_log.reg_dt = DateTime.Now;
                sync_log.chg_dt = DateTime.Now;
                db.sync_log.Add(sync_log);
                db.SaveChanges();



                db.Database.Connection.Close();
                db2.Database.Connection.Close();
                db3.Database.Connection.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }




        public string autoPono(int id)
        {
            if (id.ToString().Length < 2)
            {
                return string.Format("000000000{0}", id);
            }

            if ((id.ToString().Length < 3) || (id.ToString().Length == 2))
            {
                return string.Format("00000000{0}", id);
            }

            if ((id.ToString().Length < 4) || (id.ToString().Length == 3))
            {
                return string.Format("0000000{0}", id);
            }

            if ((id.ToString().Length < 5) || (id.ToString().Length == 4))
            {
                return string.Format("000000{0}", id);
            }
            if ((id.ToString().Length < 6) || (id.ToString().Length == 5))
            {
                return string.Format("00000{0}", id);
            }
            if ((id.ToString().Length < 7) || (id.ToString().Length == 6))
            {
                return string.Format("0000{0}", id);
            }
            if ((id.ToString().Length < 8) || (id.ToString().Length == 7))
            {
                return string.Format("000{0}", id);
            }
            if ((id.ToString().Length < 9) || (id.ToString().Length == 8))
            {
                return string.Format("00{0}", id);
            }
            if ((id.ToString().Length < 10) || (id.ToString().Length == 9))
            {
                return string.Format("0{0}", id);
            }

            if ((id.ToString().Length < 11) || (id.ToString().Length == 10))
            {
                return string.Format("{0}", id);
            }
            return string.Empty;
        }




        public void get_from_ERP_PMS_INFO()
        {

            var data2 = string.Empty;
            var list_fail = new ArrayList();
            var list_done = new ArrayList();


            var fullPath = System.Reflection.Assembly.GetAssembly(typeof(syncDATA)).Location;

            string theDirectory = Path.GetDirectoryName(fullPath);
            var basepath = theDirectory + "\\EXCEL\\PMSDT\\";

            try
            {
                db3.Database.ExecuteSqlCommand(" truncate table p_mpo_info_interface ");

                var filelist_hmi = Directory.GetFiles(basepath, "*.xlsx");

                foreach (var item in filelist_hmi)
                {


                    var excelData = new ExcelQueryFactory(item);


                    var data = excelData.Worksheet("POs").ToList();
                    //select x;

                    foreach (var d in data)
                    {

                        var mpo_no = @d[0].ToString();


                        //if (mpo_no == "DHM/2006/0043")
                        //{
                        var order_dt = @d[1].ToString().Substring(0, 10);
                        var re_mark = @d[2].ToString();
                        var sp_cd = @d[3].ToString();

                        var sp_nm = @d[4].ToString();

                        data2 = @d[0].ToString();
                        var mpo_qty = @d[5].ToString();




                        var emp_no = @d[6].ToString();
                        var emp_nm = @d[7].ToString();
                        var sts_mt = @d[8].ToString();
                        //var mt_no = Convert.ToString(dictRow["mt_no"]);
                        var brand_no = @d[9].ToString();
                        var brand_nm = @d[10].ToString();
                        var po_sts = @d[11].ToString();



                        //var mpono = Convert.ToString(dictRow["mpo_no"]);




                        re_mark = @"" + re_mark.Replace("\"", "") + "";
                        sp_nm = @"" + sp_nm.Replace("\"", "") + "";


                        var sql = "CALL sp_p_mpo_info_interface('" + mpo_no + "', '" + order_dt + "',";
                        sql += "\"" + re_mark + "\"";
                        sql += ",'" + sp_cd + "' ";
                        sql += ",'" + sp_nm + "' ";
                        sql += ",'" + mpo_qty + "' ";
                        sql += ",'" + emp_no + "' ";
                        sql += ",'" + emp_nm + "' ";
                        sql += ",'" + sts_mt + "' ";
                        sql += ",'" + brand_no + "' ";
                        sql += ",'" + brand_nm + "' ";
                        sql += ",'" + po_sts + "')";

                        db3.Database.ExecuteSqlCommand(sql);
                        //}
                    }
                }
                db.Database.ExecuteSqlCommand("call sp_p_mpo_info() ");



                db.Database.Connection.Close();

                db2.Database.Connection.Close();
                db3.Database.Connection.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void get_from_ERP_PMSDT2()
        {

            var data2 = string.Empty;
            var list_fail = new ArrayList();
            var list_done = new ArrayList();
            var fullPath = System.Reflection.Assembly.GetAssembly(typeof(syncDATA)).Location;

            string theDirectory = Path.GetDirectoryName(fullPath);
            var basepath = theDirectory + "\\EXCEL\\PMSDT\\";

            try
            {

                var filelist_hmi = Directory.GetFiles(basepath, "*.xlsx");

                db3.Database.ExecuteSqlCommand(" truncate table p_mpo_detail_interface ");
                foreach (var item in filelist_hmi)
                {

                    var excelData = new ExcelQueryFactory(item);


                    var data = excelData.Worksheet("Detail").ToList();
                    //select x;
                    p_mpo_detail_interface p_mpo_detail_interface = new p_mpo_detail_interface();
                    foreach (var d in data)
                    {

                        var mpo_no = @d[0].ToString();
                        var sp_cd = @d[3].ToString();
                        var sp_nm = @"" + @d[4].ToString().Replace("\"", "") + "";




                        var brand_no = @d[9].ToString();
                        var brand_nm = @"" + @d[10].ToString().Replace("\"", "") + "";


                        var mt_no = @d[11].ToString();
                        var mt_nm = @"" + @d[12].ToString().Replace("\"", "") + "";


                        var mt_spec = @d[13].ToString();
                        var mt_width = @d[14].ToString();
                        var mt_height = @d[15].ToString();
                        var mt_thick = @d[16].ToString();
                        var mt_re_mark = @"" + @d[17].ToString().Replace("\"", "") + "";


                        var pur_unit = @d[18].ToString();


                        var act_pur_qty = @d[19].ToString();
                        var qty_stk = @d[20].ToString();
                        var pur_unit_stk = @d[21].ToString();
                        var unit_price = @d[22].ToString();
                        var unit_price_stk = @d[23].ToString();

                        var tot_pur_amt = @d[24].ToString();
                        var pay_amt = @d[25].ToString();
                        var rec_input_dt = @d[26].ToString();

                        var po_sts = @d[27].ToString();
                        var mpo_desc = @d[28].ToString();

                        var sql = "CALL sp_p_mpo_detail_interface('" + mpo_no + "', '" + sp_cd + "',";
                        sql += "\"" + sp_nm + "\"";
                        sql += ",'" + brand_no + "' ";
                        sql += ",\"" + brand_nm + "\"";


                        sql += ",'" + mt_no + "' ";

                        sql += ",\"" + mt_nm + "\"";
                        sql += ",'" + mt_spec + "' ";
                        sql += ",'" + mt_width + "' ";
                        sql += ",'" + mt_height + "' ";
                        sql += ",'" + mt_thick + "' ";
                        sql += ",\"" + mt_re_mark + "\"";
                        sql += ",'" + pur_unit + "' ";



                        sql += ",'" + act_pur_qty + "' ";



                        sql += ",'" + qty_stk + "' ";
                        sql += ",'" + pur_unit_stk + "' ";






                        sql += ",'" + unit_price + "' ";
                        sql += ",'" + unit_price_stk + "' ";
                        sql += ",'" + tot_pur_amt + "' ";



                        sql += ",'" + pay_amt + "' ";
                        sql += ",'" + rec_input_dt + "' ";



                        sql += ",'" + po_sts + "' ";
                        sql += ",'" + mpo_desc + "')";

                        db3.Database.ExecuteSqlCommand(sql);




                    }
                    db.Database.Connection.Close();

                    db2.Database.Connection.Close();
                    db3.Database.Connection.Close();


                    insert_from_ERP_PMS_DT2();

                }



            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



        public void insert_from_ERP_PMS_DT2()
        {
            try
            {
                db.Database.ExecuteSqlCommand(" truncate table p_mpo_detail ");

                //StringBuilder sql2 = new StringBuilder();


                StringBuilder varname1 = new StringBuilder();

                varname1.Append(" SELECT * FROM p_mpo_detail_interface ");

                var data = new DataTable();
                using (var cmd = db3.Database.Connection.CreateCommand())
                {
                    db3.Database.Connection.Open();
                    cmd.CommandText = varname1.ToString();
                    using (var reader = cmd.ExecuteReader())
                    {
                        data.Load(reader);
                    }
                    db3.Database.Connection.Close();

                }
                var lstRows = new List<Dictionary<string, object>>();
                Dictionary<string, object> dictRow = null;
                //p_mpo_detail p_mpo_detail_interface = new p_mpo_detail();
                foreach (DataRow row in data.Rows)
                {
                    dictRow = new Dictionary<string, object>();
                    foreach (DataColumn column in data.Columns)
                    {
                        dictRow.Add(column.ColumnName, row[column]);
                    }




                    var po_no = string.Empty;
                    int countlist = db.p_mpo_detail.ToList().Count();
                    var po_no_tmp = string.Empty;
                    var subponoconvert = 0;



                    if (countlist == 0)
                    {
                        po_no = "D0000000001";
                    }
                    else
                    {
                        var listlast = db.p_mpo_detail.OrderBy(item => item.mdpo_no).ToList().LastOrDefault();
                        var bien = listlast.mdpo_no;
                        var subbbno = bien.Substring(bien.Length - 10, 10);
                        int.TryParse(subbbno, out subponoconvert);
                        po_no_tmp = "D" + string.Format("{0}{1}", po_no_tmp, new InitMethods().CreateMDPO((subponoconvert + 1)));
                        po_no = po_no_tmp;
                    }
                    db.Database.Connection.Close();

                    //db2.Database.Connection.Close();
                    //db3.Database.Connection.Close();

                    //var sp_nm = @"" + @d[4].ToString().Replace("\"", "") + "";

                    var mpo_no = Convert.ToString(dictRow["mpo_no"]);
                    var sp_cd =
                    @"" + Convert.ToString(dictRow["sp_cd"]).Replace("\"", "") + "";
                    var sp_nm = @"" + Convert.ToString(dictRow["sp_nm"]).Replace("\"", "") + "";
                    var brand_no = Convert.ToString(dictRow["brand_no"]);
                    var brand_nm = @"" + Convert.ToString(dictRow["brand_nm"]).Replace("\"", "") + "";




                    var mt_no = Convert.ToString(dictRow["mt_no"]);
                    var mt_nm = @"" + Convert.ToString(dictRow["mt_thick"]).ToString().Replace("\"", "") + "";



                    var mt_spec = Convert.ToString(dictRow["mt_spec"]);
                    var mt_width = Convert.ToString(dictRow["mt_width"]);
                    var mt_height = Convert.ToString(dictRow["mt_height"]);
                    var mt_thick = Convert.ToString(dictRow["mt_thick"]);
                    var mt_re_mark = @"" + Convert.ToString(dictRow["mt_re_mark"]).Replace("\"", "") + ""; ;
                    var pur_unit = Convert.ToString(dictRow["pur_unit"]);




                    var pur_unit_nm = Convert.ToString(dictRow["pur_unit_nm"]);

                    var act_pur_qty = (dictRow["act_pur_qty"] == "") ? "0" : Convert.ToString(dictRow["act_pur_qty"]);


                    var qty_stk = Convert.ToString(dictRow["qty_stk"]);
                    var pur_unit_stk = Convert.ToString(dictRow["pur_unit_stk"]);
                    var unit_price = Convert.ToString(dictRow["unit_price"]);
                    var unit_price_stk = Convert.ToString(dictRow["unit_price_stk"]);

                    var tot_pur_amt = Convert.ToString(dictRow["tot_pur_amt"]);
                    var pay_amt = Convert.ToString(dictRow["pay_amt"]);

                    var rec_input_dt = Convert.ToString(dictRow["rec_input_dt"]);


                    var yyyy = String.Format("{0}-{1}-{2}", rec_input_dt.Substring(6, 4), rec_input_dt.Substring(0, 2), rec_input_dt.Substring(3, 2));







                    var po_sts = Convert.ToString(dictRow["po_sts"]);
                    var mpo_desc = Convert.ToString(dictRow["mpo_desc"]);







                    //var po_no = string.Empty;
                    //int countlist = db.p_mpo_detail.ToList().Count();
                    //var po_no_tmp = string.Empty;
                    //var subponoconvert = 0;



                    //if (countlist == 0)
                    //{
                    //    po_no = "D0000000001";
                    //}
                    //else
                    //{
                    //    var listlast = db.p_mpo_detail.OrderBy(item => item.mdpo_no).ToList().LastOrDefault();
                    //    var bien = listlast.mdpo_no;
                    //    var subbbno = bien.Substring(bien.Length - 10, 10);
                    //    int.TryParse(subbbno, out subponoconvert);
                    //    po_no_tmp = "D" + string.Format("{0}{1}", po_no_tmp, CreateMDPO((subponoconvert + 1)));
                    //    po_no = po_no_tmp;
                    //}

                    //CALL sp_TEST(@mdpo_no_out);

                    #region MyRegion
                    //StringBuilder sql_TEST = new StringBuilder(" CALL sp_TEST();");
                    ////List<mdpo_init> mdpo_no_out = db.Database.SqlQuery<mdpo_init>(sql_TEST).ToList();
                    //List<mdpo_init> mdpo_no_out = new InitMethods().ConvertDataTableToList<mdpo_init>(sql_TEST);



                    //var po_no = mdpo_no_out.Select(x => x.mdpo_no_t).FirstOrDefault();
                    #endregion






                    var sql = " CALL sp_p_mpo_detail ('" + mpo_no + "',";

                    sql += "\"" + sp_cd + "\",";

                    sql += "\"" + sp_nm + "\"";
                    sql += ",'" + brand_no + "' ";
                    sql += ",\"" + brand_nm + "\"";


                    sql += ",'" + mt_no + "' ";

                    sql += ",\"" + mt_nm + "\"";
                    sql += ",'" + mt_spec + "' ";
                    sql += ",'" + mt_width + "' ";
                    sql += ",'" + mt_height + "' ";
                    sql += ",'" + mt_thick + "' ";
                    sql += ",\"" + mt_re_mark + "\"";
                    sql += ",'" + pur_unit + "' ";



                    sql += ",'" + act_pur_qty + "' ";



                    sql += ",'" + qty_stk + "' ";
                    sql += ",'" + pur_unit_stk + "' ";

                    sql += ",'" + unit_price + "' ";
                    sql += ",'" + unit_price_stk + "' ";
                    sql += ",'" + tot_pur_amt + "' ";



                    sql += ",'" + pay_amt + "' ";
                    sql += ",'" + yyyy + "' ";



                    sql += ",'" + po_sts + "' ";
                    sql += ",'" + po_no + "' ";
                    sql += ",'" + mpo_desc + "')";

                    db.Database.ExecuteSqlCommand(sql);

                    db.Database.Connection.Close();

                    db2.Database.Connection.Close();
                    db3.Database.Connection.Close();
                }


                //db.Database.ExecuteSqlCommand(" truncate table p_mpo_detail; ");
                //db.Database.ExecuteSqlCommand(" INSERT INTO p_mpo_detail SELECT * FROM snk_interface.p_mpo_detail_interface ");



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public class mdpo_init
        {
            public string mdpo_no_t { get; set; }
            //public string time_up { get; set; }

        }

    }
}
