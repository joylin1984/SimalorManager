﻿#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01

 * 说明：

 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 平衡分区</summary>
    [KeyAttribute(EclKeyType = EclKeyType.Regions, SimKeyType = SimKeyType.Eclipse )]
    public class EQLNUM : TableKey
    {
        public EQLNUM(string _name)
            : base(_name)
        {
            this.DefaultValue = 1;
        }

    }
}
