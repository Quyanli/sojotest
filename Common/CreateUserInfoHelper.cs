﻿using MVVMLightDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;

namespace MVVMLightDemo.Common
{
    public class CreateUserInfoHelper
    {
        //执行进度事件(响应注册的事件)
        public event EventHandler<CreateArgs> CreateProcess;

        //待创建信息
        public UserParam up { get; set; }

        public CreateUserInfoHelper(UserParam _up)
        {
            up = _up;
        }

        public void Create()
        {
            Thread t = new Thread(Start);
            t.Start();
        }

        private void Start()
        {
            try
            {
                //ToDo：编写创建用户的DataAccess代码
                for (Int32 idx = 1; idx <= 10; idx++)
                {
                    CreateProcess(this, new CreateArgs()
                    {
                        isFinish = ((idx == 10) ? true : false),
                        process = idx * 10,
                        userInfo = null
                    });
                Thread.Sleep(1000);
                }

                CreateProcess(this, new CreateArgs()
                {
                    isFinish = true,
                    process = 100,
                    userInfo = up
                });
            }
            catch 
            {
                CreateProcess(this, new CreateArgs()
                {
                    isFinish = true,
                    process = 100,
                    userInfo = null
                });
            }
        }

        /// <summary>
        /// 创建步骤反馈参数
        /// </summary>
        public class CreateArgs : EventArgs
        {
            /// <summary>
            /// 是否创建结束
            /// </summary>
            public Boolean isFinish { get; set; }
            /// <summary>
            /// 进度
            /// </summary>
            public Int32 process { get; set; }
            /// <summary>
            /// 处理后的用户信息
            /// </summary>
            public UserParam userInfo { get; set; }
        }
    }
}
