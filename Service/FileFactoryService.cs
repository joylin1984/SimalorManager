﻿using OPT.Product.SimalorManager.Eclipse.FileInfos;
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace OPT.Product.SimalorManager
{
    /// <summary> 文件类型构造工厂服务 </summary>
    public class FileFactoryService : ServiceFactory<FileFactoryService>
    {
        /// <summary> 利用数模文件异步创建指定大小栈的内存模型 </summary>
        public EclipseData ThreadLoadResize(string fileFullPath, int stactSize = 4194304)
        {
            EclipseData eclData = null;

            Thread thread = new Thread(() => eclData = new EclipseData(fileFullPath), stactSize);// 4mb栈

            thread.Start();

            while (true)
            {
                if (thread.ThreadState == ThreadState.Stopped)
                {
                    break;
                }
            }

            return eclData;
        }

        /// <summary> 利用数模文件异步创建指定大小栈的内存模型 </summary>
        public  INCLUDE ThreadLoadFromFile(string pfilePath, int stactSize = 4194304)
        {
            INCLUDE include = new INCLUDE("INCLUDE");

            return ThreadLoadFromFile(include, pfilePath, stactSize);
        }

        /// <summary> 利用数模文件异步创建指定大小栈的内存模型 </summary>
        public INCLUDE ThreadLoadFromFile(INCLUDE include, string pfilePath, int stactSize = 4194304)
        {

            include.FileName = Path.GetFileName(pfilePath);

            include.FilePath = pfilePath;

            Thread thread = new Thread(() => include.ReadFromStream(), stactSize);// 4mb栈

            thread.Start();

            while (true)
            {
                if (thread.ThreadState == ThreadState.Stopped)
                {
                    break;
                }
            }
            return include;
        }

    }
}
