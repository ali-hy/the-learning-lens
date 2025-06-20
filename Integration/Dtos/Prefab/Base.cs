using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Dtos.Prefab
{
    public class Base
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
