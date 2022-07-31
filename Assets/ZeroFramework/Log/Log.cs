using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

using UnityEngine;

namespace ZeroFramework.Log
{
    public static class ZLog
    {
        /// <summary>
        /// 获取日志文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetLogPath()
        {
            return _ZLog.Instance.LogFileName;
        }

        //--------------------------------//
        public static void LogDebug(string content)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Log(content);
#endif
            _ZLog.Instance.LogDebug(content);
        }

        public static void LogWarning(string content)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.LogWarning(content);
#endif
            _ZLog.Instance.LogWarning(content);
        }

        public static void LogInfo(string content)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Log(content);
#endif
            _ZLog.Instance.LogInfo(content);
        }

        public static void LogError(string content)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.LogError(content);
#endif
            _ZLog.Instance.LogError(content);
        }

        public static void LogFatal(string content)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.LogError(content);
#endif
            _ZLog.Instance.LogFatal(content);
        }
    }

    public class LogItem
    {
        public string level;
        public string time;
        public string module;
        public string content;
    }

    public class _ZLog : SingletonBase<_ZLog>
    {
        // 日志路径和文件名: @"D:\Log.txt", @"../Log.txt", @"Log.txt"
        private string logFileName = $"{Application.persistentDataPath}/log/ZLog-{System.DateTime.Now.ToString("yyyyMMdd")}.log";

        public string LogFileName
        {
            get { return logFileName; }
            private set { logFileName = value; }
        }

        private FileStream logFileStream;
        private StreamWriter logStreamWriter;

        // 日志缓存队列
        private Queue<LogItem> queue = new Queue<LogItem>();

        private static readonly int BUFFER_SIZE = 10;

        // Semaphore and Mutex
        private Semaphore fillCount = new Semaphore(0, BUFFER_SIZE);

        private Semaphore emptyCount = new Semaphore(BUFFER_SIZE, BUFFER_SIZE);
        private Mutex bufferMutex = new Mutex();

        // Consumer thread: write log
        private Thread consumerThread;

        public _ZLog()
        {
            if (!Directory.Exists($"{Application.persistentDataPath}/log"))
            {
                Directory.CreateDirectory($"{Application.persistentDataPath}/log");
            }
            // Open file stream
            OpenFileStream();
            // Log consumer
            consumerThread = new Thread(Consumer);
            consumerThread.Start();
        }

        ~_ZLog()
        {
            CloseFileStream();
        }

        protected override void OnInit()
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Log("log文件路径: " + logFileName);
#endif
        }

        /// <summary>
        ///  写日志
        /// Other modules write logs by calling this method.
        /// </summary>
        /// <param name="content">Log content</param>
        private void Write(string loglevel, string content)
        {
            // 日志打印时间
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //日志等级
            // 获取哪个模块写入的日志 ( namespace:class.method )
            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1);
            MethodBase method = frame.GetMethod();
            string namespaceName = method.ReflectedType.Namespace;
            string className = method.ReflectedType.Name;
            string methodName = method.Name;
            //string module = namespaceName + ":" + className + "." + methodName;
            string module = methodName;

            // 组装日志信息
            var item = ProduceItem(loglevel, time, module, content);
            // Waiting for production Permission
            emptyCount.WaitOne();
            bufferMutex.WaitOne();
            // 把日志信息放入缓冲区
            putItemIntoBuffer(item);
            bufferMutex.ReleaseMutex();

            // Release one to take permission
            fillCount.Release();
        }

        public void LogDebug(string content)
        {
            Write("Debug", content);
        }

        public void LogWarning(string content)
        {
            Write("Warning", content);
        }

        public void LogInfo(string content)
        {
            Write("Info", content);
        }

        public void LogError(string content)
        {
            Write("Error", content);
        }

        public void LogFatal(string content)
        {
            Write("Fatal", content);
        }

        // Open file stream
        private void OpenFileStream()
        {
            if (!string.IsNullOrEmpty(logFileName))
            {
                if (logFileStream == null)
                    logFileStream = new FileStream(logFileName, FileMode.Append);
                if (logStreamWriter == null)
                    logStreamWriter = new StreamWriter(logFileStream);
            }
        }

        // Close file stream
        private void CloseFileStream()
        {
            if (logStreamWriter != null)
            {
                logStreamWriter.Flush();
                logStreamWriter.Close();
                logStreamWriter = null;
            }
            if (logFileStream != null)
            {
                logFileStream.Close();
                logFileStream = null;
            }
        }

        // Consumer
        private void Consumer()
        {
            while (true)
            {
                // 等待锁释放
                fillCount.WaitOne();
                bufferMutex.WaitOne();

                // 移除一个项目
                var item = removeItemFromBuffer();
                bufferMutex.ReleaseMutex();

                // Release a production permission
                emptyCount.Release();

                // 写入一条日志
                WriteLog(item);
            }
        }

        // Put the product in the cache
        private void putItemIntoBuffer(LogItem item)
        {
            queue.Enqueue(item);
        }

        // Get products from the cache
        private LogItem removeItemFromBuffer()
        {
            var item = queue.Peek();
            queue.Dequeue();
            return item;
        }

        // Produce Item
        private LogItem ProduceItem(string logLevel, string logTime, string logModule, string logContent)
        {
            LogItem item = new LogItem() { level = logLevel, time = logTime, module = logModule, content = logContent };
            return item;
        }

        // 写日志到文件
        private void WriteLog(LogItem logItem)
        {
            if (logStreamWriter == null)
                OpenFileStream();
            logStreamWriter.WriteLine(logItem.time + "  " + logItem.module + "  " + logItem.content);
            logStreamWriter.Flush();
        }
    }
}