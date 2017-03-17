﻿#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01
 * 文件名：START
 * 说明：
 * ROCK
             0            11
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 射孔数据 </summary>
    public class TIME : BaseKey, IComparable, IRootNode
    {
        public TIME(string _name, DateTime date = default(DateTime))
            : base(_name)
        {

            this.BuilderHandler += (l, k) =>
                {
                    this.Build();

                    return this;
                };


            this.EachLineCmdHandler += l =>
            {
                // Todo ：读取时兼容老版本DATE，替换成DAYS  截取前后空格判断是否为关键字
                return l.Contains("DATE") ? l.Replace("DATE", "DAYS").Trim() : l.Trim();
            };

            _date = date;
        }

        public TIME(string _name)
            : base(_name)
        {

            this.BuilderHandler += (l, k) =>
            {
                this.Build();

                return this;
            };

            this.EachLineCmdHandler += l =>
            {
                // Todo ：读取时兼容老版本DATE，替换成DAYS  截取前后空格判断是否为关键字
                return l.Contains("DATE") ? l.Replace("DATE", "DAYS").Trim() : l.Trim();
            };

        }



        private DateTime _date;
        /// <summary> 日期 </summary>
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        /// <summary> 构建日期 </summary>
        void Build()
        {
            //  构建WELL
            this.Lines.RemoveAll(l => !l.IsWorkLine());

            if (this.Lines.Count > 0)
            {
                _date = DatesKeyService.Instance.GetSimONDateTime(this.Lines[0].Substring(0, 8));
            }
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }


        /// <summary> 复制时间信息 (不包括子节点事件) </summary>
        public TIME Clone()
        {
            return new TIME("TIME") { Date = this.Date };
        }

        /// <summary> yyyy-MM-dd </summary>
        public override string ToString()
        {
            return this.Date.ToString("yyyy-MM-dd");
        }

        bool isContain;

        /// <summary> 是否包含Restart关键字 </summary>
        public bool IsContainEnd
        {
            get
            {
                //return isContain = this.Find<END>() != null;
                isContain = this.Find<RESTART>() != null;
                return isContain;
            }
            set
            {

                if (value)
                {
                    //  是则没有增加
                    if (this.Find<RESTART>() == null)
                    {
                        RESTART end = new RESTART("RESTART");

                        this.Add(end);
                    }
                }
                else
                {
                    //  否则删除节点下所有End

                    RESTART end = this.Find<RESTART>();

                    if (end != null)
                    {
                        this.Keys.Remove(end);
                    }
                }
                //isContain = value;

            }
        }

        private string _totalDays;
        /// <summary> 距离开始时间的总天数 </summary>
        public string TotalDays
        {
            get { return _totalDays; }
            set { _totalDays = value; }
        }

        /// <summary> 转换成数模文件格式 </summary>
        string ToFileStr()
        {
            string formatStr = this.Date.ToString("yyyyMMdd");

            return formatStr;
        }

        public override void WriteKey(StreamWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine("TIME".ToDWithOutSpace() + (this.ToFileStr() + "D").ToD());
            this.Lines.Clear();
            this.Distinct();
            //this.Sort();
            base.WriteKey(writer);
        }

        /// <summary> 排序 </summary>
        void Sort()
        {
            //  将RESTART放到最后
            RESTART wpimult = this.Find<RESTART>();
            if (wpimult != null)
            {
                this.Keys.RemoveAll(l => l is RESTART);
                this.Add(wpimult);
            }
        }


        /// <summary> 去重复保存最后一项 </summary>
        void Distinct()
        {
            var wells = this.FindAll<WELL>();

            var boxs = this.FindAll<BOX>();

            var restart = this.Find<RESTART>();

            this.Clear();

            // Todo ：对WELL分组去重复，保留最后一条 
            var ss = wells.GroupBy(l => l.WellName0);
            foreach (var item in ss)
            {
                this.Add(item.Last());
            }

            if (boxs != null && boxs.Count > 0)
            {
                this.AddRange(boxs);
            }

            if (restart != null)
            {
                this.Add(restart);
            }

        }

    }
}