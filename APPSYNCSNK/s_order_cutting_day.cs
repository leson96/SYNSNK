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
    
    public partial class s_order_cutting_day
    {
        public int cddno { get; set; }
        public string cdd_no { get; set; }
        public string cd_no { get; set; }
        public string process_type { get; set; }
        public string prounit_cd { get; set; }
        public Nullable<int> cutting_target_qty { get; set; }
        public Nullable<int> sked_qty { get; set; }
        public Nullable<System.DateTime> work_ymd { get; set; }
        public Nullable<System.DateTime> work_dt { get; set; }
        public Nullable<decimal> work_time { get; set; }
        public Nullable<System.TimeSpan> start_time { get; set; }
        public Nullable<System.TimeSpan> end_time { get; set; }
        public Nullable<int> target_qty { get; set; }
        public Nullable<int> actual_qty { get; set; }
        public Nullable<int> defective_qty { get; set; }
        public string re_mark { get; set; }
        public string use_yn { get; set; }
        public string del_yn { get; set; }
        public string reg_id { get; set; }
        public Nullable<System.DateTime> reg_dt { get; set; }
        public string chg_id { get; set; }
        public Nullable<System.DateTime> chg_dt { get; set; }
    }
}