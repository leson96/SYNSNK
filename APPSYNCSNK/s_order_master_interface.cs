//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APPSYNCSNK
{
    using System;
    using System.Collections.Generic;
    
    public partial class s_order_master_interface
    {
        public int smid { get; set; }
        public string pm_no { get; set; }
        public string order_dt { get; set; }
        public Nullable<int> prod_cnt { get; set; }
        public string sp_nm { get; set; }
        public string employee_nm { get; set; }
        public string sor_sts_nm { get; set; }
        public string contract_no { get; set; }
        public string contract_dt { get; set; }
        public Nullable<int> delivery_qty { get; set; }
        public Nullable<int> fo_qty { get; set; }
        public Nullable<int> so_qty { get; set; }
        public Nullable<int> fo_rm_qty { get; set; }
        public Nullable<int> so_rm_qty { get; set; }
        public string re_mark { get; set; }
        public string use_yn { get; set; }
        public string del_yn { get; set; }
        public string reg_id { get; set; }
        public Nullable<System.DateTime> reg_dt { get; set; }
        public string chg_id { get; set; }
        public Nullable<System.DateTime> chg_dt { get; set; }
    }
}
