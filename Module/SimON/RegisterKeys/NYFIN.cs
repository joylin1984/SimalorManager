﻿//#region <版 本 注 释>
///*
// * ========================================================================
// * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
// * ========================================================================
// *    
// * 作者：[李海军]   时间：2015/12/2 10:38:01

// * 说明：
// * 
// * 
// * 修改者：           时间：               
// * 修改说明：
// * ========================================================================
//*/
//#endregion
//using OPT.Product.SimalorManager.Base.AttributeEx;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OPT.Product.SimalorManager.RegisterKeys.SimON
//{
//    /// <summary> NXFIN中参数个数=X2-X1+1</summary>
//    [KeyAttribute(EclKeyType = EclKeyType.Grid, SimKeyType = SimKeyType.Eclipse)]
//    public class NYFIN : FinConfigerKey
//    {
//        public NYFIN(string _name)
//            : base(_name)
//        {
//            this.EachLineCmdHandler = l =>
//            {

//                // HTodo  ：截取前面空格来判断是否为关键字 
//                return l.TrimStart();
//            };
//        }

//    }
//}
