﻿#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17
 * 文件名：GCONPROD
 * 说明：
 * 
WEFAC
‘P25’  0.89  NO/
‘P12’  0.7  /
/


 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 支撑剂名称表 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include)]
    public class PROPANTNAMES : DynamicKey
    {
        public PROPANTNAMES(string _name)
            : base(_name)
        {

        }


        PROPANTTABLE _propantTable;
        /// <summary> 支撑剂对应的数据表 </summary>
        public PROPANTTABLE PropantTable
        {
            get { return _propantTable; }
            set { _propantTable = value; }
        }

        /// <summary> 绑定数据表 </summary>
        public void BindTable(PROPANTTABLE table)
        {
            if (this.Items.Count != table.GridTable.Columns.Count - 1)
                return;

            _propantTable = table;

            table.GridTable.Columns[0].Caption = "压力";

            table.GridTable.Columns[0].ColumnName = "压力";


            for (int i = 0; i < this.Items.Count; i++)
            {

                table.GridTable.Columns[i + 1].Caption = this.Items[i].Value;

                table.GridTable.Columns[i + 1].ColumnName = this.Items[i].Value;

            }
        }

    }
}