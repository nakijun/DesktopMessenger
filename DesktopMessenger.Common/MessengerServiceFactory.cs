using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DesktopMessenger.Common
{
    public static class MessengerServiceFactory
    {
        private static readonly IDictionary<string, Type> _services = new Dictionary<string, Type>();

        public static string[] Services { get { return _services.Keys.ToArray(); } }

        static MessengerServiceFactory()
        {
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            foreach (var file in files)
            {
                var asm = Assembly.LoadFile(file);
                foreach (var type in asm.GetExportedTypes())
                {
                    if (type.GetInterface("IMessengerService") == null)
                        continue;

                    string name;
                    using (var service = Activator.CreateInstance(type) as IMessengerService)
                    {
                        name = service.ServiceName;
                    }
                    if (!_services.ContainsKey(name))
                        _services.Add(name, type);
                }
            }
        }

        public static IMessengerService CreateInstance(string name)
        {
            if (_services.ContainsKey(name))
                return Activator.CreateInstance(_services[name]) as IMessengerService;
            throw new ServiceNotFoundException(String.Format("Unknown service: {0}", name));
        }
    }
}
