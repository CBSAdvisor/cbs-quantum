using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using log4net;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Cbs.LogLib
{
    public class Log4
    {
        // ���������������� ������� �����������
        public static void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        // ������ ��� ������������� - � �����
        public static log4net.ILog DeveloperLog = log4net.LogManager.GetLogger("DeveloperLog");

        // ������ ��� ����������� - � �����
        public static log4net.ILog TraceLog = log4net.LogManager.GetLogger("TraceLog");

        // ���������������� ������ - � ��������
        public static log4net.ILog UserLog = log4net.LogManager.GetLogger("UserLog");
    }
}
