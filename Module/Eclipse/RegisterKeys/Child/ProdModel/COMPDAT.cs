﻿#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01

 * 说明：


/
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 完井数据 </summary>
    public class COMPDAT : ItemsKey<COMPDAT.Item>, ICompdatInterface, IProductEvent
    {
        public COMPDAT(string _name)
            : base(_name)
        {
            this.EachLineCmdHandler = l =>
            {
                //  截取前后空格判断是否为关键字
                return l.Trim();
            };
        }

        public Item GetSingleItem()
        {
            if (this.Items.Count == 0)
            {
                Item item = new Item();
                this.Items.Add(item);
                return item;

            }
            else
            {
                return this.Items[0];
            }
        }


        /// <summary> 排序</summary>
        public void OrderBy()
        {
            //this.Items.OrderBy(l => l.i1 + l.j2 + l.swg3);

            this.Items = this.Items.OrderBy(l => l.i1.ToInt()).ThenBy(l => l.j2.ToInt()).ThenBy(l => l.swg3.ToInt()).ToList();
        }


        /// <summary> 去重复 </summary>
        public void Distinct()
        {
            var its = this.Items.Distinct(new COMPDAT.ItemCompare()).ToList();

            this.Items.Clear();

            this.Items.AddRange(its);
        }



        public class Item : OPT.Product.SimalorManager.Item, IProductItem
        {
            /// <summary> 井名 </summary>
            public string jm0 = "新增";

            /// <summary> 网格i </summary>
            public string i1;

            /// <summary> 网格j </summary>
            public string j2;

            /// <summary> 上网格k </summary>
            public string swg3;

            /// <summary> 下网格k </summary>
            public string xwg4;

            /// <summary> 开关标志 </summary>
            public string kgbz5 = "OPEN";

            /// <summary> 相渗分区号 </summary>
            public string xsfqh6;

            /// <summary> 连接因子 </summary>
            public string ljyz7;

            /// <summary> 井筒内径 </summary>
            public string jtnj8;

            /// <summary> 有效地层系数 </summary>
            public string yxdcxs9;

            /// <summary> 表皮系数 </summary>
            public string bpxs10;

            /// <summary> D因子 </summary>
            public string dyz11;

            /// <summary> 射孔方向 默认Z方向 0 X 1 Y 2 Z </summary>
            public string skfx12 = "Z";

            /// <summary> 等效压力半径 </summary>
            public string dxylbj13;

            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jm0.ToEclStr(), i1.ToDD(), j2.ToDD(),
                    swg3.ToDD(), xwg4.ToDD(), kgbz5.ToEclStr(), xsfqh6.ToDD(),
                    ljyz7.ToDD(), jtnj8.ToDD(), yxdcxs9.ToDD(), bpxs10.ToDD(),
                    dyz11.ToDD(), skfx12.ToDD(), dxylbj13.ToDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.jm0 = newStr[0];
                            break;
                        case 1:
                            this.i1 = newStr[1];
                            break;
                        case 2:
                            this.j2 = newStr[2];
                            break;
                        case 3:
                            this.swg3 = newStr[3];
                            break;
                        case 4:
                            this.xwg4 = newStr[4];
                            break;
                        case 5:
                            this.kgbz5 = newStr[5];
                            break;
                        case 6:
                            this.xsfqh6 = newStr[6];
                            break;
                        case 7:
                            this.ljyz7 = newStr[7];
                            break;
                        case 8:
                            this.jtnj8 = newStr[8];
                            break;
                        case 9:
                            this.yxdcxs9 = newStr[9];
                            break;
                        case 10:
                            this.bpxs10 = newStr[10];
                            break;
                        case 11:
                            this.dyz11 = newStr[11];
                            break;
                        case 12:
                            this.skfx12 = newStr[12];
                            break;
                        case 13:
                            this.dxylbj13 = newStr[13];
                            break;
                        default:
                            break;
                    }
                }
            }


            public string Name
            {
                get
                {
                    return this.jm0;
                }
                set
                {
                    this.jm0 = value;
                }
            }

            public bool Equals(object x, object y)
            {
                throw new NotImplementedException();
            }

            public int GetHashCode(object obj)
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<COMPDAT.Item> GetAllItems()
        {
            return this.Items;
        }

        public void SetWellName(string wellName)
        {
            this.Items.ForEach(l => l.Name = wellName);
        }

        /// <summary> 自定义Item比较方法 </summary>
        public class ItemCompare : IEqualityComparer<Item>
        {

            public bool Equals(Item x, Item y)
            {
                //Check whether the compared objects reference the same data. 
                if (Object.ReferenceEquals(x, y)) return true;

                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;

                return x.i1 == y.i1 && x.j2 == y.j2 && x.swg3 == y.swg3 && x.xwg4 == y.xwg4;

            }

            public int GetHashCode(Item obj)
            {
                //Check whether the object is null  
                if (Object.ReferenceEquals(obj, null)) return 0;

                return (obj.i1 + obj.j2 + obj.swg3 + obj.xwg4).GetHashCode();
            }
        }
    }

    /// <summary> 局部加密完井数据 </summary>
    public class COMPDATL : ItemsKey<COMPDATL.Item>, ICompdatInterface, IProductEvent
    {
        public COMPDATL(string _name)
            : base(_name)
        {

        }

        public Item GetSingleItem
        {
            get
            {
                if (this.Items.Count == 0)
                {
                    Item item = new Item();
                    this.Items.Add(item);
                    return item;

                }
                else
                {
                    return this.Items[0];
                }
            }
        }

        public class Item : COMPDAT.Item
        {
            /// <summary> 局部加密网格名 </summary>
            public string jbwgjmm;

            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jm0.ToEclStr(), jbwgjmm.ToEclStr(), i1.ToDD(), j2.ToDD(),
                    swg3.ToDD(), xwg4.ToDD(), kgbz5.ToEclStr(), xsfqh6.ToDD(),
                    ljyz7.ToDD(), jtnj8.ToDD(), yxdcxs9.ToDD(), bpxs10.ToDD(),
                    dyz11.ToDD(), skfx12.ToDD(), dxylbj13.ToDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.jm0 = newStr[0];
                            break;
                        case 1:
                            this.jbwgjmm = newStr[1];
                            break;
                        case 2:
                            this.i1 = newStr[2];
                            break;
                        case 3:
                            this.j2 = newStr[3];
                            break;
                        case 4:
                            this.swg3 = newStr[4];
                            break;
                        case 5:
                            this.xwg4 = newStr[5];
                            break;
                        case 6:
                            this.kgbz5 = newStr[6];
                            break;
                        case 7:
                            this.xsfqh6 = newStr[7];
                            break;
                        case 8:
                            this.ljyz7 = newStr[8];
                            break;
                        case 9:
                            this.jtnj8 = newStr[9];
                            break;
                        case 10:
                            this.yxdcxs9 = newStr[10];
                            break;
                        case 11:
                            this.bpxs10 = newStr[11];
                            break;
                        case 12:
                            this.dyz11 = newStr[12];
                            break;
                        case 13:
                            this.skfx12 = newStr[13];
                            break;
                        case 14:
                            this.dxylbj13 = newStr[14];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public IEnumerable<COMPDAT.Item> GetAllItems()
        {
            return this.Items;
        }


        public void SetWellName(string wellName)
        {
            this.Items.ForEach(l => l.Name = wellName);
        }
    }


    /// <summary> 完井数据接口 </summary>
    public interface ICompdatInterface
    {
        IEnumerable<COMPDAT.Item> GetAllItems();
    }
}
