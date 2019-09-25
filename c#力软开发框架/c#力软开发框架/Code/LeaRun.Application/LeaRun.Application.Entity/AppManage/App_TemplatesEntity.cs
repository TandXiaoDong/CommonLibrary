﻿namespace LeaRun.Application.Entity.AppManage
{
    using LeaRun.Application.Code;
    using LeaRun.Application.Entity;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class App_TemplatesEntity : BaseEntity
    {
        public override void Create()
        {
            //this.F_Id = Guid.NewGuid().ToString();
            this.F_CreateDate = new DateTime?(DateTime.Now);
            this.F_CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.F_CreateUserName = OperatorProvider.Provider.Current().UserName;
        }

        public override void Modify(string keyValue)
        {
            this.F_Id = keyValue;
        }
        [Column("F_ID")]
        public string F_Id { get; set; }
        [Column("F_PROJECTID")]
        public string F_ProjectId { get; set; }
        [Column("F_NAME")]
        public string F_Name { get; set; }
        [Column("F_VALUE")]
        public string F_Value { get; set; }
        [Column("F_TYPE")]
        public string F_Type { get; set; }
        [Column("F_PARENT")]
        public string F_Parent { get; set; }
        [Column("F_LEVEL")]
        public int? F_level { get; set; }
        [Column("F_IMG")]
        public string F_img { get; set; }
        [Column("F_CONTENT")]
        public string F_Content { get; set; }

        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }

        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }

        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }

      
      
    }
}

