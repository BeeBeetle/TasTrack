using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasTrack
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class JSONClass
    {
        public Userinfo UserInfo { get; set; }
        public List<Task> TaskList { get; set; }
    }
    public class Task
    {
        public string TaskName { get; set; }
        public DateTime TaskDue { get; set; }
        public DateTime TaskCreate { get; set; }
    }
    public class Userinfo
    {
        public string Username { get; set; }
        public string HashPass { get; set; }
    }
}
