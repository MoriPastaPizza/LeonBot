using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MoriPastaPizza.LeonBot.Attributes;

namespace MoriPastaPizza.LeonBot.Controller
{
    public class MediaGroupController
    {
        private readonly ILogger<MediaGroupController> _logger;

        private Dictionary<string, List<MethodInfo>> _groups;

        public MediaGroupController(ILogger<MediaGroupController> logger)
        {
            _logger = logger;
            _groups = new Dictionary<string, List<MethodInfo>>();
        }


        public void StartMediaGroupController()
        {
            _logger.LogDebug("Starting: " + nameof(MediaGroupController));

            var assembly = Assembly.GetExecutingAssembly();
            var allMethods = assembly
                .GetTypes()
                .SelectMany(m => m.GetMethods())
                .Where(m => m.GetCustomAttributes().OfType<MediaGroupAttribute>().Any())
                .ToList();

            foreach (var method in allMethods)
            {
                foreach (var mediaGroupAttribute in method.GetCustomAttributes().OfType<MediaGroupAttribute>())
                {
                    if (_groups.TryGetValue(mediaGroupAttribute.Group, out var groupMethods))
                    {
                        groupMethods.Add(method);
                    }
                    else
                    {
                        _groups.Add(mediaGroupAttribute.Group, new List<MethodInfo>{method});
                    }
                }
            }

            foreach (var group in _groups)
            {
                _logger.LogInformation($"Found {group.Value.Count} elements in group: {group.Key}");
            }
        }

        public IEnumerable<MethodInfo> GetMethodsForGroup(string group)
        {
            return _groups.GetValueOrDefault(group) ?? new List<MethodInfo>();
        }
    }
}
