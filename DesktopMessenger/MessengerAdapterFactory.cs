﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DesktopMessenger.Common;

namespace DesktopMessenger
{
    public static class MessengerAdapterFactory
    {
        private static readonly IDictionary<string, Type> _adapters = new Dictionary<string, Type>();

        public static string[] Adapters { get { return _adapters.Keys.ToArray(); } }

        static MessengerAdapterFactory()
        {
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            foreach (var file in files)
            {
                try
                {
                    var asm = Assembly.LoadFile(file);
                    foreach (var type in asm.GetExportedTypes())
                    {
                        if (type.GetInterface("IMessengerAdapter") == null)
                            continue;

                        var name =
                            type.GetProperty("ServiceName",
                                             BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty)
                                .GetValue(null, null)
                                .ToString();
                        if (!_adapters.ContainsKey(name))
                            _adapters.Add(name, type);
                    }
                }
                catch (NullReferenceException)
                {
                    throw new Exception("ServiceName was not found.");
                }
                catch
                {
                    // ignore everything else
                }
            }
        }

        public static IMessengerAdapter CreateInstance(string name)
        {
            return Activator.CreateInstance(_adapters[name]) as IMessengerAdapter;
        }

        /*public static T CreateInstance<T>() where T : IMessengerAdapter, new()
        {
            return new T();
        }

        public static void RegisterType<T>(string name) where T : IMessengerAdapter, new()
        {
            if (!_adapters.ContainsKey(name))
                _adapters.Add(name, typeof(T));
        }*/
    }
}
